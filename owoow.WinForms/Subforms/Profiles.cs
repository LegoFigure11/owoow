using System.Text.Json;
using owoow.Core.Interfaces;

namespace owoow.WinForms.Subforms;

public partial class Profiles : Form
{
    private readonly ClientConfig config;
    private readonly BindingSource bs = [];
    private readonly MainWindow parent;

    public int Index
    {
        get
        {
            return LB_ProfileList.SelectedIndex;
        }
    }

    public Profiles(MainWindow parent, ref ClientConfig config)
    {
        InitializeComponent();

        this.parent = parent;
        this.config = config;

        ReloadList();

        var idx = LB_ProfileList.SelectedIndex;
        B_Remove.Enabled = idx >= 0;
        B_Select.Enabled = idx >= 0;
    }

    private void ReloadList()
    {
        if (bs.DataSource is null)
        {
            bs.DataSource = config.Profiles;
            LB_ProfileList.DataSource = bs;
            LB_ProfileList.DisplayMember = "Name";
        }
        else
        {
            bs.ResetBindings(false);
        }
    }

    private void B_Add_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TB_Name.Text))
        {
            MessageBox.Show("Name is a required field!");
            return;
        }

        if (string.IsNullOrEmpty(TB_TID.Text) || !int.TryParse(TB_TID.Text, out _))
        {
            TB_TID.Text = "0";
        }

        if (string.IsNullOrEmpty(TB_SID.Text) || !int.TryParse(TB_SID.Text, out _))
        {
            TB_SID.Text = "0";
        }

        if (CB_Game.SelectedIndex < 0) CB_Game.SelectedIndex = 0;

        TB_TID.Text = TB_TID.Text.PadLeft(5, '0');
        TB_SID.Text = TB_SID.Text.PadLeft(5, '0');

        Profile profile = new()
        {
            Name = TB_Name.Text,
            GameVersion = CB_Game.SelectedIndex,
            HasMarkCharm = CB_MarkCharm.Checked,
            HasShinyCharm = CB_ShinyCharm.Checked,
            TID = TB_TID.Text,
            SID = TB_SID.Text,
        };

        // Refresh details of current profile
        for (int i = 0; i < LB_ProfileList.Items.Count; i++)
        {
            var p = config.Profiles[i];
            if (p.Name != profile.Name) continue;
            config.Profiles.RemoveAt(i);
            break;
        }

        config.Profiles.Add(profile);

        ReloadList();
        LB_ProfileList.SelectedIndex = LB_ProfileList.Items.Count - 1;
        var idx = LB_ProfileList.SelectedIndex;
        B_Remove.Enabled = idx >= 0;
        B_Select.Enabled = idx >= 0;
        UpdateAddLabel();
    }

    private void B_Remove_Click(object sender, EventArgs e)
    {
        if (LB_ProfileList.Items.Count is 0 && LB_ProfileList.SelectedIndex is -1) return;
        var idx = LB_ProfileList.SelectedIndex;
        config.Profiles.RemoveAt(idx);
        ReloadList();
        UpdateAddLabel();
    }

    private void LB_IDs_SelectedIndexChanged(object sender, EventArgs e)
    {
        var idx = LB_ProfileList.SelectedIndex;
        B_Remove.Enabled = idx >= 0;
        B_Select.Enabled = idx >= 0;
        if (idx is -1) return;
        var profile = config.Profiles[idx];
        if (profile is null) return;
        TB_Name.Text = profile.Name;
        TB_TID.Text = profile.TID;
        TB_SID.Text = profile.SID;
        CB_ShinyCharm.Checked = profile.HasShinyCharm;
        CB_MarkCharm.Checked = profile.HasMarkCharm;
        CB_Game.SelectedIndex = profile.GameVersion;
    }

    private void Profiles_FormClosing(object sender, FormClosingEventArgs e)
    {
        string output = JsonSerializer.Serialize(config);
        using StreamWriter sw = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"));
        sw.Write(output);

        parent.Config = config;
        parent.SeedSearchFormOpen = false;
    }

    private void ID_KeyPress(object sender, KeyPressEventArgs e)
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

    private void TB_Name_TextChanged(object sender, EventArgs e)
    {
        UpdateAddLabel();
    }

    private void UpdateAddLabel()
    {
        if (LB_ProfileList.SelectedIndex > -1 && TB_Name.Text == config.Profiles[LB_ProfileList.SelectedIndex].Name)
        {
            B_Add.Text = "Update";
        }
        else
        {
            B_Add.Text = "Add";
        }
    }
}
