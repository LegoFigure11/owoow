using FlashCap;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace owoow.WinForms.Subforms;

public partial class VideoFeed : Form
{
    readonly MainWindow MainWindow;

    private CancellationTokenSource? _cts;
    private bool _isFeedRunning = false;

    private readonly Lock _frameLock = new();
    private Mat? _latestFrame;

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

        if (!capture.IsOpened())
        {
            throw new Exception("Could not open the selected camera device.");
        }

        using var frame = new Mat();
        string windowName = "Video Source Feed";

        Cv2.NamedWindow(windowName, WindowFlags.KeepRatio);
        Cv2.ResizeWindow(windowName, 480, 270);

        while (!token.IsCancellationRequested)
        {
            capture.Read(frame);
            if (frame.Empty()) continue;

            // Create copy in order to allow screenshot
            lock (_frameLock)
            {
                _latestFrame?.Dispose();
                _latestFrame = frame.Clone();
            }

            Cv2.ImShow(windowName, frame);

            // 30ms = ~33 fps, should be plenty to do the analysis that we need
            int key = Cv2.WaitKey(30);

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

    private void SaveScreenshot(string filename)
    {
        if (!_isFeedRunning || _latestFrame == null)
        {
            MessageBox.Show("Please start the camera before taking a screenshot.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var frameToSave = new Mat();
        lock (_frameLock)
        {
            _latestFrame.CopyTo(frameToSave);
        }

        using var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg|Bitmap Image|*.bmp";
        saveFileDialog.Title = "Save Screenshot";
        saveFileDialog.FileName = filename;

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            try { 
                if (!frameToSave.Empty())
                {
                    Cv2.ImWrite(saveFileDialog.FileName, frameToSave);
                    MessageBox.Show("Screenshot saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save screenshot: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
