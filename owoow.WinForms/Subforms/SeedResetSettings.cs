using System.Text.Json;

namespace owoow.WinForms.Subforms;

public partial class SeedResetSettings : Form
{
    private readonly ClientConfig c;
    private readonly MainWindow p;

    public SeedResetSettings(ref ClientConfig config, MainWindow parent)
    {
        c = config;
        p = parent;

        InitializeComponent();
    }

    private void SeedResetSettings_Load(object sender, EventArgs e)
    {
        CB_AvoidSystemUpdate.Checked = c.AvoidSystemUpdate;
        CB_ScreenState.Checked = c.ScreenOff;

        TB_ExtraTimeReturnHome.Text = $"{c.ExtraTimeReturnHome}";
        TB_ExtraTimeCloseGame.Text = $"{c.ExtraTimeCloseGame}";

        TB_ExtraTimeLoadProfile.Text = $"{c.ExtraTimeLoadProfile}";
        TB_ExtraTimeLoadGame.Text = $"{c.ExtraTimeLoadGame}";

        CB_EnableWebhooks.Checked = c.WebhookEnabled;

        TB_WebhookMessage.Text = $"{c.ResultWebhookMessageContent}";
        TB_WebhookErrorMessage.Text = $"{c.ErrorWebhookMessageContent}";
        TB_ResultURLs.Text = $"{c.ResultNotificationURL}";
        TB_ErrorURLs.Text = $"{c.ErrorNotificationURL}";

        CB_Log.Checked = c.LogResultsWhileInProgress;

        p.SetControlEnabledState(c.WebhookEnabled, TB_WebhookMessage, TB_WebhookErrorMessage, TB_ResultURLs, TB_ErrorURLs, B_TestWebhooks);
    }

    private void SeedResetSettings_FormClosing(object sender, FormClosingEventArgs e)
    {
        string output = JsonSerializer.Serialize(c);
        using StreamWriter sw = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"));
        sw.Write(output);

        p.Config = c;
        p.Webhook = new(c);
        p.SeedSearchFormOpen = false;
    }

    private void TB_ExtraTimeReturnHome_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(TB_ExtraTimeReturnHome.Text)) c.ExtraTimeReturnHome = int.Parse(TB_ExtraTimeReturnHome.Text);
    }

    private void CB_AvoidSystemUpdate_CheckedChanged(object sender, EventArgs e)
    {
        c.AvoidSystemUpdate = CB_AvoidSystemUpdate.Checked;
    }

    private void TB_ExtraTimeCloseGame_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(TB_ExtraTimeCloseGame.Text)) c.ExtraTimeCloseGame = int.Parse(TB_ExtraTimeCloseGame.Text);
    }

    private void TB_ExtraTimeLoadProfile_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(TB_ExtraTimeLoadProfile.Text)) c.ExtraTimeLoadProfile = int.Parse(TB_ExtraTimeLoadProfile.Text);
    }

    private void TB_ExtraTimeLoadGame_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(TB_ExtraTimeLoadGame.Text)) c.ExtraTimeLoadGame = int.Parse(TB_ExtraTimeLoadGame.Text);
    }

    private void CB_EnableWebhooks_CheckedChanged(object sender, EventArgs e)
    {
        c.WebhookEnabled = CB_EnableWebhooks.Checked;
        p.SetControlEnabledState(c.WebhookEnabled, TB_WebhookMessage, TB_WebhookErrorMessage, TB_ResultURLs, TB_ErrorURLs, B_TestWebhooks);
    }

    private void TB_WebhookMessage_TextChanged(object sender, EventArgs e)
    {
        c.ResultWebhookMessageContent = TB_WebhookMessage.Text;
    }

    private void TB_ResultURLs_TextChanged(object sender, EventArgs e)
    {
        c.ResultNotificationURL = TB_ResultURLs.Text;
    }

    private void TB_ErrorURLs_TextChanged(object sender, EventArgs e)
    {
        c.ErrorNotificationURL = TB_ErrorURLs.Text;
    }

    private void TB_WebhookErrorMessage_TextChanged(object sender, EventArgs e)
    {
        c.ErrorWebhookMessageContent = TB_WebhookErrorMessage.Text;
    }

    private void B_TestWebhooks_Click(object sender, EventArgs e)
    {
        p.Webhook = new(c);
        _ = p.Webhook.SendTestWebhook(CancellationToken.None);
    }

    private void KeyPress_AllowOnlyNumerical(object sender, KeyPressEventArgs e)
    {
        var c = e.KeyChar;
        if (c != (char)Keys.Back && !char.IsControl(c))
        {
            if (!char.IsBetween(c, '0', '9'))
            {
                System.Media.SystemSounds.Asterisk.Play();
                e.Handled = true;
            }
        }
    }

    private void TB_Leave(object sender, EventArgs e)
    {
        var tb = (TextBox)sender;
        if (string.IsNullOrEmpty(tb.Text)) tb.Text = "0";
    }

    private void CB_Log_CheckedChanged(object sender, EventArgs e)
    {
        c.LogResultsWhileInProgress = CB_Log.Checked;
    }

    private void CB_ScreenState_CheckedChanged(object sender, EventArgs e)
    {
        c.ScreenOff = CB_ScreenState.Checked;
    }
}
