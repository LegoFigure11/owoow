namespace owoow.WinForms.Subforms;

public partial class UpdateNotifPopup : Form
{
    private readonly Version cv;
    private readonly Version nv;
    public UpdateNotifPopup(Version currentVersion, Version newVersion)
    {
        cv = currentVersion;
        nv = newVersion;
        InitializeComponent();
        ChineseLocalizer.Apply(this);
    }

    private void UpdateNotifPopup_Load(object sender, EventArgs e)
    {
        L_Version.Text = $"当前：v{cv.Major}.{cv.Minor}.{cv.Build}｜最新：v{nv.Major}.{nv.Minor}.{nv.Build}";
        B_Download.Focus();
        CenterToScreen();
    }
}
