namespace owoow.WinForms.Subforms;

public partial class VideoFeedLog : Form
{
    private readonly VideoFeed feed;
    private bool _accept = true;
    private bool _reject;
    public VideoFeedLog(VideoFeed parent)
    {
        feed = parent;
        InitializeComponent();
    }

    public void AddLine(string line, bool isAccept, bool isInfo = false)
    {
        if (!isInfo && (isAccept && !_accept || !isAccept && !_reject)) return;

        var tb = TB_Logs;
        if (InvokeRequired)
        {
            Invoke(() =>
            {
                tb.AppendText(line + Environment.NewLine);
            });
        }
        else
        {
            tb.AppendText(line + Environment.NewLine);
        }
    }

    private void VideoFeedDebugLog_FormClosing(object sender, FormClosingEventArgs e)
    {
        feed.ToggleLogs(false);
    }

    private void CB_Accept_CheckedChanged(object sender, EventArgs e)
    {
        _accept = CB_Accept.GetIsChecked();
    }

    private void CB_Reject_CheckedChanged(object sender, EventArgs e)
    {
        _reject = CB_Reject.GetIsChecked();
    }
}
