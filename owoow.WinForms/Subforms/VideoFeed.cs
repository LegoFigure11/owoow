using FlashCap;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Diagnostics;

namespace owoow.WinForms.Subforms;

public partial class VideoFeed : Form
{
    readonly MainWindow MainWindow;

    private readonly static string baseDir = AppContext.BaseDirectory;

    private CancellationTokenSource? _cts;
    private bool _isFeedRunning = false;
    private bool _isComparing = false;

    private readonly Lock _frameLock = new();
    private readonly Lock _compareLock = new();
    private Mat? _latestFrame;
    private Mat? _comparisonReferenceFrame;

    private readonly Lock _templateLock = new();
    private Mat? _physMat;
    private Mat? _specMat;
    private Mat? _idleMat;

    private Image? _phys;
    private Image? _spec;
    private Image? _idle;

    private Winner winner = Winner.Idle;
    private Winner lastwinner = Winner.Idle;

    private enum Winner
    {
        Idle,
        Physical,
        Special
    }

    public VideoFeed(MainWindow f)
    {
        InitializeComponent();
        MainWindow = f;

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
        MainWindow.SpreadFinderFormOpen = false;
        StopCamera();
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

        RefreshVideoSources();
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

        try
        {
            Task.Run(() => RunCameraLoop(cameraIndex, _cts.Token), _cts.Token);
        }
        catch (OperationCanceledException)
        {
            // ignore, clean exit handled safely
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Feed Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            StopCamera();
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

        long lastLogTimestamp = 0;
        const long logCooldownMs = 1000; // 1 second cooldown

        string windowName = "Video Source Feed";

        Cv2.NamedWindow(windowName, WindowFlags.KeepRatio);
        Cv2.ResizeWindow(windowName, 480, 270);

        while (!token.IsCancellationRequested)
        {
            capture.Read(frame);
            if (frame.Empty()) continue;

            lock (_frameLock)
            {
                _latestFrame?.Dispose();
                _latestFrame = frame.Clone();
            }

            bool templatesAreReady = false;
            lock (_templateLock)
            {
                if (_physMat != null && !_physMat.Empty() && _specMat != null && !_specMat.Empty() && _idleMat != null && !_idleMat.Empty())
                {
                    if (_physMat.Size() == frame.Size() && _specMat.Size() == frame.Size() && _idleMat.Size() == frame.Size())
                    {
                        _physMat.CopyTo(localPhys);
                        _specMat.CopyTo(localSpec);
                        _idleMat.CopyTo(localIdle);
                        templatesAreReady = true;
                    }
                }
            }

            string resultText = "No reference matches found";

            if (templatesAreReady)
            {
                Cv2.Absdiff(frame, localPhys, diffPhys);
                Cv2.CvtColor(diffPhys, grayPhys, ColorConversionCodes.BGR2GRAY);
                Cv2.Threshold(grayPhys, grayPhys, 30, 255, ThresholdTypes.Binary);
                int diffCountPhys = Cv2.CountNonZero(grayPhys);

                Cv2.Absdiff(frame, localSpec, diffSpec);
                Cv2.CvtColor(diffSpec, graySpec, ColorConversionCodes.BGR2GRAY);
                Cv2.Threshold(graySpec, graySpec, 30, 255, ThresholdTypes.Binary);
                int diffCountSpec = Cv2.CountNonZero(graySpec);
                int pixelDifferenceThreshold = (frame.Size().Height * frame.Size().Width) / 100;

                Cv2.Absdiff(frame, localIdle, diffIdle);
                Cv2.CvtColor(diffIdle, grayIdle, ColorConversionCodes.BGR2GRAY);
                Cv2.Threshold(grayIdle, grayIdle, 30, 255, ThresholdTypes.Binary);
                int diffCountIdle = Cv2.CountNonZero(grayIdle);

                int minDiff = Math.Min(diffCountPhys, Math.Min(diffCountSpec, diffCountIdle));
                long currentTimestamp = Environment.TickCount64;
                bool allowLog = (currentTimestamp - lastLogTimestamp >= logCooldownMs);

                if (minDiff == diffCountPhys && lastwinner == Winner.Idle)
                {
                    winner = Winner.Physical;
                    resultText = $"Match: Physical (Diff: {diffCountPhys})";
                    if (diffCountPhys < pixelDifferenceThreshold && allowLog)
                    {
                        Debug.WriteLine($"[{DateTime.Now:HH:mm:ss}] [MATCH ACCEPTED] Physical Matrix. Score: {diffCountPhys}");
                        lastLogTimestamp = currentTimestamp;
                        MainWindow.SetControlText(TB_Obs.GetText() + "0", TB_Obs);
                    }
                }
                else if (minDiff == diffCountSpec && lastwinner == Winner.Idle)
                {
                    winner = Winner.Special;
                    resultText = $"Match: Special (Diff: {diffCountSpec})";
                    if (diffCountSpec < pixelDifferenceThreshold && allowLog)
                    {
                        Debug.WriteLine($"[{DateTime.Now:HH:mm:ss}] [MATCH ACCEPTED] Special Matrix. Score: {diffCountSpec}");
                        lastLogTimestamp = currentTimestamp;
                        MainWindow.SetControlText(TB_Obs.GetText() + "1", TB_Obs);
                    }
                }
                else
                {
                    winner = Winner.Idle;
                    resultText = $"Match: Idle (Diff: {diffCountIdle})";
                }
                lastwinner = winner;
            }

            Cv2.PutText(frame, resultText, new OpenCvSharp.Point(20, 40),
                        HersheyFonts.HersheySimplex, 0.8, Scalar.Green, 2);


            Cv2.ImShow(windowName, frame);

            int key = Cv2.WaitKey(1);

            if (key == (int)Keys.Escape)
            {
                Invoke(new Action(StopCamera));
                break;
            }
        }

        Cv2.DestroyWindow(windowName);
    }

    private void StopCamera()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
        _isFeedRunning = false;

        lock (_frameLock)
        {
            _latestFrame?.Dispose();
            _latestFrame = null;
        }

        lock (_compareLock)
        {
            _comparisonReferenceFrame?.Dispose();
            _comparisonReferenceFrame = null;
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


    private void B_Stop_Click(object sender, EventArgs e)
    {
        StopCamera();
        MainWindow.SetControlEnabledState(false, B_Stop);
        MainWindow.SetControlEnabledState(true, B_Start, B_RefreshSources);
    }

    private void B_ScreenshotPhysical_Click(object sender, EventArgs e)
    {
        SaveScreenshot("physical");
    }
    private void B_ScreenshotSpecial_Click(object sender, EventArgs e)
    {
        SaveScreenshot("special");
    }

    private void SaveScreenshot(string filename)
    {
        if (!_isFeedRunning || _latestFrame == null)
        {
            MessageBox.Show("Please start the feed before taking a screenshot.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var frameToSave = new Mat();
        lock (_frameLock)
        {
            _latestFrame.CopyTo(frameToSave);
        }

        using var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "PNG Image|*.png";
        saveFileDialog.Title = "Save Screenshot";
        saveFileDialog.FileName = filename;
        saveFileDialog.InitialDirectory = baseDir;

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                if (!frameToSave.Empty())
                {
                    Cv2.ImWrite(saveFileDialog.FileName, frameToSave);
                    if (filename == "physical")
                    {
                        byte[] bytes = File.ReadAllBytes(saveFileDialog.FileName);
                        using var ms = new MemoryStream(bytes);
                        _phys?.Dispose();
                        _phys = Image.FromStream(ms);
                        PB_Physical.Image?.Dispose();
                        PB_Physical.Image = _phys;
                        lock (_templateLock)
                        {
                            _physMat?.Dispose();
                            _physMat = Cv2.ImRead(saveFileDialog.FileName);
                        }
                    }
                    else if (filename == "special")
                    {
                        byte[] bytes = File.ReadAllBytes(saveFileDialog.FileName);
                        using var ms = new MemoryStream(bytes);
                        _spec?.Dispose();
                        _spec = Image.FromStream(ms);
                        PB_Special.Image?.Dispose();
                        PB_Special.Image = _spec;
                        lock (_templateLock)
                        {
                            _specMat?.Dispose();
                            _specMat = Cv2.ImRead(saveFileDialog.FileName);
                        }
                    }
                    else
                    {
                        byte[] bytes = File.ReadAllBytes(saveFileDialog.FileName);
                        using var ms = new MemoryStream(bytes);
                        _idle?.Dispose();
                        _idle = Image.FromStream(ms);
                        PB_Idle.Image?.Dispose();
                        PB_Idle.Image = _idle;
                        lock (_templateLock)
                        {
                            _idleMat?.Dispose();
                            _idleMat = Cv2.ImRead(saveFileDialog.FileName);
                        }
                    }
                    MessageBox.Show("Screenshot saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save screenshot: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void B_Compare_Click(object sender, EventArgs e)
    {
        _isComparing = !_isComparing;
    }

    private void B_ScreenshotIdle_Click(object sender, EventArgs e)
    {
        SaveScreenshot("idle");
    }

    private void TB_Obs_TextChanged(object sender, EventArgs e)
    {
        MainWindow.SetControlText($"Observations: {TB_Obs.GetText().Length}", L_Obs);
    }
}
