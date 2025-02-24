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

        TB_ExtraTimeReturnHome.Text = $"{c.ExtraTimeReturnHome}";
        TB_ExtraTimeCloseGame.Text = $"{c.ExtraTimeCloseGame}";

        TB_ExtraTimeLoadProfile.Text = $"{c.ExtraTimeLoadProfile}";
        TB_ExtraTimeCheckDLC.Text = $"{c.ExtraTimeCheckDLC}";
        TB_ExtraTimeLoadGame.Text = $"{c.ExtraTimeLoadGame}";
    }

    private void SeedResetSettings_FormClosing(object sender, FormClosingEventArgs e)
    {
        string output = JsonSerializer.Serialize(c);
        using StreamWriter sw = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"));
        sw.Write(output);

        p.Config = c;
        p.SeedSearchFormOpen = false;
    }

    private void TB_ExtraTimeReturnHome_TextChanged(object sender, EventArgs e)
    {
        c.ExtraTimeReturnHome = int.Parse(TB_ExtraTimeReturnHome.Text);
    }

    private void CB_AvoidSystemUpdate_CheckedChanged(object sender, EventArgs e)
    {
        c.AvoidSystemUpdate = CB_AvoidSystemUpdate.Checked;
    }

    private void TB_ExtraTimeCloseGame_TextChanged(object sender, EventArgs e)
    {
        c.ExtraTimeCloseGame = int.Parse(TB_ExtraTimeCloseGame.Text);
    }

    private void TB_ExtraTimeLoadProfile_TextChanged(object sender, EventArgs e)
    {
        c.ExtraTimeLoadProfile = int.Parse(TB_ExtraTimeLoadProfile.Text);
    }

    private void TB_ExtraTimeCheckDLC_TextChanged(object sender, EventArgs e)
    {
        c.ExtraTimeCheckDLC = int.Parse(TB_ExtraTimeCheckDLC.Text);
    }

    private void TB_ExtraTimeLoadGame_TextChanged(object sender, EventArgs e)
    {
        c.ExtraTimeLoadGame = int.Parse(TB_ExtraTimeLoadGame.Text);
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
}
