using FlashCap;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Runtime.InteropServices;

namespace owoow.WinForms.Subforms;

public partial class VideoFeed : Form
{
    #region Dll Imports and System Calls
#pragma warning disable SYSLIB1054
    [DllImport("user32.dll", EntryPoint = "FindWindowW", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr FindWindow(string? lpClassName, string lpWindowName);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

#pragma warning restore SYSLIB1054
    private const uint WM_SETICON = 0x0080;
    private static readonly IntPtr ICON_SMALL = new IntPtr(0);
    private static readonly IntPtr ICON_BIG = new IntPtr(1);
    #endregion

    readonly MainWindow MainWindow;
    readonly ClientConfig _cfg;

    private readonly static string baseDir = AppContext.BaseDirectory;

    private readonly Lock _logLock = new();
    private VideoFeedLog? _log;

    private CancellationTokenSource? _cts;
    private bool _isFeedRunning = false;
    private bool _isComparing = false;

    private double topMost = 0;

    private readonly Lock _frameLock = new();
    private readonly Lock _compareLock = new();
    private Mat? _latestFrame;
    private Mat? _referenceFrame;

    private readonly Lock _templateLock = new();
    private Mat? _physMat;
    private Mat? _specMat;
    private Mat? _idleMat;

    private Image? _phys;
    private Image? _spec;
    private Image? _idle;

    private readonly Lock _thresholdLock = new();
    private uint _threshold = (1920 * 1080) / 100;

    private readonly Lock _cooldownLock = new();
    private long _cooldown = 1000;

    private readonly Lock _cvLock = new();
    private bool _showCv = false;

    private Winner winner = Winner.Idle;
    private Winner lastwinner = Winner.Idle;

    private const int max = 128;

    private enum Winner
    {
        Idle,
        Physical,
        Special
    }

    public VideoFeed(MainWindow f, ref ClientConfig cfg)
    {
        InitializeComponent();
        MainWindow = f;
        _cfg = cfg;
    }

    public static List<string> GetCameraNames()
    {
        var names = new List<string>();
        var devices = new CaptureDevices();

        foreach (var descriptor in devices.EnumerateDescriptors())
        {
            names.Add(descriptor.Name);
        }
        return names;
    }

    private void VideoFeed_FormClosing(object sender, FormClosingEventArgs e)
    {
        MainWindow.VideoFeedFormOpen = false;
        StopCamera();
        _log?.Dispose();
    }

    private void VideoFeed_Load(object sender, EventArgs e)
    {
        var phys = baseDir + @"\physical.png";
        var spec = baseDir + @"\special.png";
        var idle = baseDir + @"\idle.png";

        if (File.Exists(phys))
        {
            byte[] bytes = File.ReadAllBytes(phys);
            using var ms = new MemoryStream(bytes);
            _phys = Image.FromStream(ms);
            PB_Physical.Image?.Dispose();
            PB_Physical.Image = _phys;
            lock (_templateLock)
            {
                _physMat = Cv2.ImRead(phys);
            }
        }

        if (File.Exists(spec))
        {
            byte[] bytes = File.ReadAllBytes(spec);
            using var ms = new MemoryStream(bytes);
            _spec = Image.FromStream(ms);
            PB_Special.Image?.Dispose();
            PB_Special.Image = _spec;
            lock (_templateLock)
            {
                _specMat = Cv2.ImRead(spec);
            }
        }

        if (File.Exists(idle))
        {
            byte[] bytes = File.ReadAllBytes(idle);
            using var ms = new MemoryStream(bytes);
            _idle = Image.FromStream(ms);
            PB_Idle.Image?.Dispose();
            PB_Idle.Image = _idle;
            lock (_templateLock)
            {
                _idleMat = Cv2.ImRead(idle);
            }
        }

        CheckShouldEnableMonitorButtons();

        RefreshVideoSources();

        _cooldown = _cfg.VideoFeedCooldown;
        _threshold = _cfg.VideoFeedThreshold;
        MainWindow.SetNUDValue(_cooldown, NUD_Time);
        MainWindow.SetNUDValue(_threshold, NUD_Thresh);

        MainWindow.SetControlEnabledState(false, B_Thresh, B_Time);
    }

    private void B_RefreshSources_Click(object sender, EventArgs e)
    {
        RefreshVideoSources();
    }

    private void RefreshVideoSources()
    {
        CB_SourceSelect.Items.Clear();
        foreach (var device in GetCameraNames())
        {
            CB_SourceSelect.Items.Add(device);
        }
        CB_SourceSelect.SelectedIndex = 0;
    }

    private void B_Start_Click(object sender, EventArgs e)
    {
        _cts = new CancellationTokenSource();
        _isFeedRunning = true;
        var cameraIndex = CB_SourceSelect.SelectedIndex;

        MainWindow.SetControlEnabledState(true, B_Stop);
        MainWindow.SetControlEnabledState(false, B_Start, B_RefreshSources);

        CheckShouldEnableMonitorButtons();

        try
        {
            Task.Run(() => RunCameraLoop(cameraIndex, _cts.Token), _cts.Token);
        }
        catch (Exception ex)
        {
            if (ex is not OperationCanceledException)
            {
                StopCamera();
                this.DisplayMessageBox(ex.Message, "Feed Error");
            }
        }
    }

    private void RunCameraLoop(int index, CancellationToken token)
    {
        using var capture = new VideoCapture(index);
        capture.Set(VideoCaptureProperties.Fps, 60);

        if (!capture.IsOpened())
        {
            throw new Exception("Could not open the selected video device.");
        }

        using var frame = new Mat();
        using var localPhys = new Mat();
        using var localSpec = new Mat();
        using var localIdle = new Mat();

        using var diffPhys = new Mat();
        using var diffSpec = new Mat();
        using var diffIdle = new Mat();
        using var grayPhys = new Mat();
        using var graySpec = new Mat();
        using var grayIdle = new Mat();

        long lastLog = 0;

        string windowName = "Video Source Feed";

        Cv2.NamedWindow(windowName, WindowFlags.KeepRatio);
        Cv2.ResizeWindow(windowName, 480, 270);

        Cv2.SetWindowProperty(windowName, WindowPropertyFlags.Topmost, topMost);

        IntPtr windowHandle = IntPtr.Zero;
        for (int i = 0; i < 20; i++)
        {
            windowHandle = FindWindow(null, windowName);
            if (windowHandle != IntPtr.Zero) break;
            Thread.Sleep(50);
        }

        if (windowHandle != IntPtr.Zero)
        {
            IntPtr handle = Icon!.Handle;
            if (handle != IntPtr.Zero)
            {
                SendMessage(windowHandle, WM_SETICON, ICON_SMALL, handle);
                SendMessage(windowHandle, WM_SETICON, ICON_BIG, handle);
            }
        }

        while (!token.IsCancellationRequested)
        {
            capture.Read(frame);
            if (frame.Empty()) continue;

            if (frame.Width == 0 || frame.Height == 0) continue;

            lock (_frameLock)
            {
                _latestFrame?.Dispose();
                _latestFrame = frame.Clone();
            }

            if (_isComparing)
            {

                bool templatesReady = false;
                lock (_templateLock)
                {
                    if (_physMat != null && !_physMat.Empty() && _specMat != null && !_specMat.Empty() && _idleMat != null && !_idleMat.Empty())
                    {
                        if (_physMat.Size() == frame.Size() && _specMat.Size() == frame.Size() && _idleMat.Size() == frame.Size())
                        {
                            _physMat.CopyTo(localPhys);
                            _specMat.CopyTo(localSpec);
                            _idleMat.CopyTo(localIdle);
                            templatesReady = true;
                        }
                    }
                    else
                    {
                        if (_physMat == null && _phys != null)
                        {
                            Mat newMat = new();
                            var tmp = new Bitmap(_phys);
                            using var bgraMat = tmp.ToMat();
                            Cv2.CvtColor(bgraMat, newMat, ColorConversionCodes.BGRA2BGR);
                            _physMat = newMat;
                        }
                        if (_specMat == null && _spec != null)
                        {
                            Mat newMat = new();
                            var tmp = new Bitmap(_spec);
                            using var bgraMat = tmp.ToMat();
                            Cv2.CvtColor(bgraMat, newMat, ColorConversionCodes.BGRA2BGR);
                            _specMat = newMat;
                        }
                        if (_idleMat == null && _idle != null)
                        {
                            Mat newMat = new();
                            var tmp = new Bitmap(_idle);
                            using var bgraMat = tmp.ToMat();
                            Cv2.CvtColor(bgraMat, newMat, ColorConversionCodes.BGRA2BGR);
                            _idleMat = newMat;
                        }
                    }
                }

                string resultText = $"No reference matches found.\nPhysical Mat Exists: {_physMat != null}\nSpecial Mat Exists: {_specMat != null}\nIdle Mat Exists: {_idleMat != null}";

                if (templatesReady)
                {
                    try
                    {
                        Cv2.Absdiff(frame, localPhys, diffPhys);
                        Cv2.CvtColor(diffPhys, grayPhys, ColorConversionCodes.BGR2GRAY);
                        Cv2.Threshold(grayPhys, grayPhys, 30, 255, ThresholdTypes.Binary);
                        int diffCountPhys = Cv2.CountNonZero(grayPhys);

                        Cv2.Absdiff(frame, localSpec, diffSpec);
                        Cv2.CvtColor(diffSpec, graySpec, ColorConversionCodes.BGR2GRAY);
                        Cv2.Threshold(graySpec, graySpec, 30, 255, ThresholdTypes.Binary);
                        int diffCountSpec = Cv2.CountNonZero(graySpec);

                        Cv2.Absdiff(frame, localIdle, diffIdle);
                        Cv2.CvtColor(diffIdle, grayIdle, ColorConversionCodes.BGR2GRAY);
                        Cv2.Threshold(grayIdle, grayIdle, 30, 255, ThresholdTypes.Binary);
                        int diffCountIdle = Cv2.CountNonZero(grayIdle);

                        var minDiff = Math.Min(diffCountPhys, Math.Min(diffCountSpec, diffCountIdle));
                        var currentTimestamp = Environment.TickCount64;
                        var logTime = currentTimestamp - lastLog;
                        var allowLog = (logTime >= _cooldown);

                        if (minDiff == diffCountPhys && lastwinner == Winner.Idle)
                        {
                            winner = Winner.Physical;
                            resultText = $"Match: Physical\nPhysical: {diffCountPhys}\nSpecial: {diffCountSpec}\nIdle: {diffCountIdle}";
                            if (diffCountPhys < _threshold && allowLog)
                            {
                                _log?.AddLine($"[{DateTime.Now:HH:mm:ss}] [MATCH ACCEPTED] (Log: 0) Physical | Score: {diffCountPhys,7} | Time since match: {logTime}", true);
                                lastLog = currentTimestamp;
                                AppendTextBoxText(true, "0", TB_Obs);
                            }
                            else if (!allowLog && diffCountPhys < _threshold)
                            {
                                _log?.AddLine($"[{DateTime.Now:HH:mm:ss}] [REJECTED]       [TIME]   Physical | Score: {diffCountPhys,7} | Time since match: {logTime}", false);
                            }
                            else if (allowLog && !(diffCountPhys < _threshold))
                            {
                                _log?.AddLine($"[{DateTime.Now:HH:mm:ss}] [REJECTED]       [THRESH] Physical | Score: {diffCountPhys,7} | Time since match: {logTime}", false);
                            }
                            else
                            {
                                _log?.AddLine($"[{DateTime.Now:HH:mm:ss}] [REJECTED]       [BOTH]   Physical | Score: {diffCountPhys,7} | Time since match: {logTime}", false);
                            }
                        }
                        else if (minDiff == diffCountSpec && lastwinner == Winner.Idle)
                        {
                            winner = Winner.Special;
                            resultText = $"Match: Special\nPhysical: {diffCountPhys}\nSpecial: {diffCountSpec}\nIdle: {diffCountIdle}";
                            if (diffCountSpec < _threshold && allowLog)
                            {
                                _log?.AddLine($"[{DateTime.Now:HH:mm:ss}] [MATCH ACCEPTED] (Log: 1) Special  | Score: {diffCountSpec,7} | Time since match: {logTime}", true);
                                lastLog = currentTimestamp;
                                AppendTextBoxText(true, "1", TB_Obs);
                            }
                            else if (!allowLog && diffCountSpec < _threshold)
                            {
                                _log?.AddLine($"[{DateTime.Now:HH:mm:ss}] [REJECTED]       [TIME]   Special  | Score: {diffCountSpec,7} | Time since match: {logTime}", false);
                            }
                            else if (allowLog && !(diffCountSpec < _threshold))
                            {
                                _log?.AddLine($"[{DateTime.Now:HH:mm:ss}] [REJECTED]       [THRESH] Special  | Score: {diffCountSpec,7} | Time since match: {logTime}", false);
                            }
                            else
                            {
                                _log?.AddLine($"[{DateTime.Now:HH:mm:ss}] [REJECTED]       [BOTH]   Special  | Score: {diffCountSpec,7} | Time since match: {logTime}", false);
                            }
                        }
                        else
                        {
                            winner = Winner.Idle;
                            resultText = $"Match: Idle\nPhysical: {diffCountPhys}\nSpecial: {diffCountSpec}\nIdle: {diffCountIdle}";
                        }
                        lastwinner = winner;
                    }
                    catch (Exception ex)
                    {
                        this.DisplayMessageBox(ex.Message, nameof(ex.GetType));
                    }
                }

                if (_showCv) DrawMultiLineText(frame, resultText, new OpenCvSharp.Point(35, 720));
            }


            Cv2.ImShow(windowName, frame);

            var key = Cv2.WaitKey(1);

            if ((windowHandle != IntPtr.Zero && !IsWindow(windowHandle)) || key == (int)Keys.Escape)
            {
                Invoke(new Action(StopCamera));
                break;
            }
        }

        Cv2.DestroyWindow(windowName);
    }

    public void AppendTextBoxText(bool limitTo128, string text, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not TextBox tb)
                continue;

            if (InvokeRequired)
            {
                Invoke(() =>
                {
                    if (limitTo128 && tb.GetText().Length == max)
                    {
                        MainWindow.SetControlText(tb.GetText()[1..], tb);
                        _log?.AddLine($"[{DateTime.Now:HH:mm:ss}] [INFO] Max observations reached, discarding first observation", true, true);

                    }
                    tb.AppendText(text);
                });
            }
            else
            {
                if (limitTo128 && tb.GetText().Length == max)
                {
                    MainWindow.SetControlText(tb.GetText()[1..], tb);
                }
                tb.AppendText(text);
            }
        }
    }

    private void StopCamera()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
        _isFeedRunning = false;

        CheckShouldEnableMonitorButtons();

        lock (_frameLock)
        {
            _latestFrame?.Dispose();
            _latestFrame = null;
        }

        lock (_compareLock)
        {
            _referenceFrame?.Dispose();
            _referenceFrame = null;
            if (_isComparing)
                _log?.AddLine($"[{DateTime.Now:HH:mm:ss}] [INFO] Stopping monitoring...", true, true);
            _isComparing = false;
        }

        lock (_templateLock)
        {
            _physMat?.Dispose();
            _physMat = null;
            _specMat?.Dispose();
            _specMat = null;
            _idleMat?.Dispose();
            _idleMat = null;
        }
    }

    private static void DrawMultiLineText(Mat img, string text, OpenCvSharp.Point point, int lineSpacing = 95)
    {
        string[] lines = text.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            var linePoint = new OpenCvSharp.Point(point.X, point.Y + (i * lineSpacing));

            Cv2.PutText(img, lines[i], linePoint, HersheyFonts.HersheySimplex, 3, Scalar.Green, 3);
        }
    }

    private void B_Stop_Click(object sender, EventArgs e)
    {
        StopCamera();
        MainWindow.SetControlEnabledState(false, B_Stop);
        MainWindow.SetControlEnabledState(true, B_Start, B_RefreshSources);
    }

    private void OpenScreenshot(string filename)
    {
        using var ofd = new OpenFileDialog();
        ofd.Filter = "PNG Image|*.png";
        ofd.Title = "Open Screenshot";
        ofd.FileName = filename;
        ofd.InitialDirectory = baseDir;

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            byte[] bytes = File.ReadAllBytes(ofd.FileName);
            using var ms = new MemoryStream(bytes);

            if (filename == "physical")
            {
                _phys?.Dispose();
                _phys = Image.FromStream(ms);
                PB_Physical.Image?.Dispose();
                PB_Physical.Image = _phys;
                lock (_templateLock)
                {
                    _physMat?.Dispose();
                    _physMat = Cv2.ImRead(ofd.FileName);
                }
            }
            else if (filename == "special")
            {
                _spec?.Dispose();
                _spec = Image.FromStream(ms);
                PB_Special.Image?.Dispose();
                PB_Special.Image = _spec;
                lock (_templateLock)
                {
                    _specMat?.Dispose();
                    _specMat = Cv2.ImRead(ofd.FileName);
                }
            }
            else
            {
                _idle?.Dispose();
                _idle = Image.FromStream(ms);
                PB_Idle.Image?.Dispose();
                PB_Idle.Image = _idle;
                lock (_templateLock)
                {
                    _idleMat?.Dispose();
                    _idleMat = Cv2.ImRead(ofd.FileName);
                }
            }
            CheckShouldEnableMonitorButtons();
        }
    }

    private void SaveScreenshot(string filename)
    {
        if (!_isFeedRunning || _latestFrame == null)
        {
            this.DisplayMessageBox("Please start the feed before taking a screenshot.", "Notice");
            return;
        }

        using var frameToSave = new Mat();
        lock (_frameLock)
        {
            _latestFrame.CopyTo(frameToSave);
        }

        using var sfd = new SaveFileDialog();
        sfd.Filter = "PNG Image|*.png";
        sfd.Title = "Save Screenshot";
        sfd.FileName = filename;
        sfd.InitialDirectory = baseDir;

        if (sfd.ShowDialog() == DialogResult.OK)
        {
            try
            {
                if (!frameToSave.Empty())
                {
                    Cv2.ImWrite(sfd.FileName, frameToSave);

                    byte[] bytes = File.ReadAllBytes(sfd.FileName);
                    using var ms = new MemoryStream(bytes);

                    if (filename == "physical")
                    {
                        _phys?.Dispose();
                        _phys = Image.FromStream(ms);
                        PB_Physical.Image?.Dispose();
                        PB_Physical.Image = _phys;
                        lock (_templateLock)
                        {
                            _physMat?.Dispose();
                            _physMat = Cv2.ImRead(sfd.FileName);
                        }
                    }
                    else if (filename == "special")
                    {
                        _spec?.Dispose();
                        _spec = Image.FromStream(ms);
                        PB_Special.Image?.Dispose();
                        PB_Special.Image = _spec;
                        lock (_templateLock)
                        {
                            _specMat?.Dispose();
                            _specMat = Cv2.ImRead(sfd.FileName);
                        }
                    }
                    else
                    {
                        _idle?.Dispose();
                        _idle = Image.FromStream(ms);
                        PB_Idle.Image?.Dispose();
                        PB_Idle.Image = _idle;
                        lock (_templateLock)
                        {
                            _idleMat?.Dispose();
                            _idleMat = Cv2.ImRead(sfd.FileName);
                        }
                    }
                    CheckShouldEnableMonitorButtons();
                    System.Media.SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessageBox($"Failed to save screenshot: {ex.Message}");
            }
        }
    }

    private void B_Compare_Click(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(true, B_ObserveStop);
        MainWindow.SetControlEnabledState(false, B_ObserveStart);
        _isComparing = true;
        _log?.AddLine($"[{DateTime.Now:HH:mm:ss}] [INFO] Starting monitoring...", true, true);

    }

    private void TB_Obs_TextChanged(object sender, EventArgs e)
    {
        var len = TB_Obs.GetText().Length;

        if (len == max)
        {
            TB_Obs.BackColor = Color.YellowGreen;
            System.Media.SystemSounds.Asterisk.Play();
        }
        else
        {
            TB_Obs.BackColor = DefaultBackColor;
        }
        MainWindow.SetControlText($"Observations: {len}", L_Obs);
    }

    private void B_ObserveStop_Click(object sender, EventArgs e)
    {
        if (_isComparing)
            _log?.AddLine($"[{DateTime.Now:HH:mm:ss}] [INFO] Stopping monitoring...", true, true);
        _isComparing = false;
        MainWindow.SetControlEnabledState(true, B_ObserveStart);
        MainWindow.SetControlEnabledState(false, B_ObserveStop);
    }

    private void CheckShouldEnableMonitorButtons()
    {
        if (_isComparing)
            _log?.AddLine($"[{DateTime.Now:HH:mm:ss}] [INFO] Stopping monitoring...", true, true);
        _isComparing = false;
        if (_isFeedRunning && _phys is not null && _spec is not null && _idle is not null)
        {
            MainWindow.SetControlEnabledState(true, B_ObserveStart);
        }
        else
        {
            MainWindow.SetControlEnabledState(false, B_ObserveStart);
        }
        MainWindow.SetControlEnabledState(false, B_ObserveStop);

        if (!_isFeedRunning)
        {
            MainWindow.SetControlEnabledState(false, B_Stop);
            MainWindow.SetControlEnabledState(true, B_Start, B_RefreshSources);
        }
    }

    private void CB_TopMost_CheckedChanged(object sender, EventArgs e)
    {
        topMost = CB_TopMost.GetIsChecked() ? 1 : 0;
        try
        {
            Cv2.SetWindowProperty("Video Source Feed", WindowPropertyFlags.Topmost, topMost);
        }
        catch
        {
            // Ignore
        }
    }

    private void B_LoadPhys_Click(object sender, EventArgs e)
    {
        OpenScreenshot("physical");
    }

    private void B_LoadIdle_Click(object sender, EventArgs e)
    {
        OpenScreenshot("idle");
    }

    private void B_LoadSpec_Click(object sender, EventArgs e)
    {
        OpenScreenshot("special");
    }
    private void B_ScreenshotPhysical_Click(object sender, EventArgs e)
    {
        SaveScreenshot("physical");
    }

    private void B_ScreenshotIdle_Click(object sender, EventArgs e)
    {
        SaveScreenshot("idle");
    }
    private void B_ScreenshotSpecial_Click(object sender, EventArgs e)
    {
        SaveScreenshot("special");
    }

    private void B_Thresh_Click(object sender, EventArgs e)
    {
        lock (_thresholdLock)
        {
            _threshold = NUD_Thresh.GetValue();
            _cfg.VideoFeedThreshold = _threshold;
        }
        MainWindow.SetControlEnabledState(false, B_Thresh);
    }

    private void B_Time_Click(object sender, EventArgs e)
    {
        lock (_cooldownLock)
        {
            _cooldown = NUD_Time.GetValue();
            _cfg.VideoFeedCooldown = _cooldown;
        }
        MainWindow.SetControlEnabledState(false, B_Time);
    }

    private void NUD_Thresh_ValueChanged(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(true, B_Thresh);
    }

    private void NUD_Time_ValueChanged(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(true, B_Time);
    }

    private void CB_ShowLog_CheckedChanged(object sender, EventArgs e)
    {
        lock (_cvLock)
        {
            _showCv = CB_ShowLog.GetIsChecked();
        }
    }

    private void B_Clear_Click(object sender, EventArgs e)
    {
        MainWindow.SetControlText(string.Empty, TB_Obs);
    }

    private void B_Copy_Click(object sender, EventArgs e)
    {
        try
        {
            Clipboard.Clear();
            Clipboard.SetText(TB_Obs.GetText());
            System.Media.SystemSounds.Asterisk.Play();
        }
        catch (Exception ex)
        {
            this.DisplayMessageBox(ex.Message);
        }
    }

    internal void ToggleLogs(bool state)
    {
        MainWindow.SetCheckBoxCheckedState(state, CB_ShowLogs);
    }

    private void CB_ShowLogs_CheckedChanged(object sender, EventArgs e)
    {
        lock (_logLock)
        {
            if (CB_ShowLogs.GetIsChecked())
            {
                _log = new(this);
                _log.Show();
            }
            else
            {
                _log?.Dispose();
                _log = null;
            }
        }
    }
}
