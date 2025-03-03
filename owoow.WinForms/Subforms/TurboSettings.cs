using System.Text.Json;

namespace owoow.WinForms.Subforms;

public partial class TurboSettings : Form
{
    private readonly ClientConfig c;
    private readonly BindingSource bs = [];
    private readonly MainWindow p;

    public static readonly IReadOnlyList<string> options = [
        "A", "B", "X", "Y", "Left Stick (L3)", "Right Stick (R3)", "L", "R", "ZL", "ZR", "+", "-",
        "Up (Hold)", "Down (Hold)", "Left (Hold)", "Right (Hold)", "Release Stick",
        "D-Pad Up", "D-Pad Down", "D-Pad Left", "D-Pad Right",
        "HOME", "Screenshot",
        "Wait (100ms)", "Wait (500ms)", "Wait (1000ms)"
    ];

    public TurboSettings(ref ClientConfig config, MainWindow parent)
    {
        c = config;
        p = parent;

        InitializeComponent();
    }

    private void TurboSettings_Load(object sender, EventArgs e)
    {
        CB_Input.Items.AddRange([.. options]);
        CB_Input.SelectedIndex = 0;

        CB_Loop.Checked = c.LoopTurbo;

        ReloadList();
    }

    private void ReloadList()
    {
        c.TurboSequence = c.TurboSequence.Where(item => options.Contains(item)).ToList();
        if (bs.DataSource is null)
        {
            bs.DataSource = c.TurboSequence;
            LB_List.DataSource = bs;
        }
        else
        {
            bs.DataSource = c.TurboSequence;
            bs.ResetBindings(false);
        }
    }

    private void TurboSettings_FormClosing(object sender, FormClosingEventArgs e)
    {
        string output = JsonSerializer.Serialize(c);
        using StreamWriter sw = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"));
        sw.Write(output);

        p.Config = c;
        p.TurboSettingsFormOpen = false;
    }

    private void B_Add_Click(object sender, EventArgs e)
    {
        c.TurboSequence.Add(CB_Input.Text);
        ReloadList();
        LB_List.SelectedIndex = c.TurboSequence.Count - 1;
    }

    private void B_Remove_Click(object sender, EventArgs e)
    {
        var i = LB_List.SelectedIndex;
        c.TurboSequence.RemoveAt(i);
        ReloadList();
    }

    private void B_Up_Click(object sender, EventArgs e)
    {
        if (c.TurboSequence.Count > 1)
        {
            var i = LB_List.SelectedIndex;
            (c.TurboSequence[i], c.TurboSequence[Math.Max(i - 1, 0)]) = (c.TurboSequence[Math.Max(i - 1, 0)], c.TurboSequence[i]);
            ReloadList();
            LB_List.SelectedIndex = Math.Max(i - 1, 0);
        }
    }

    private void B_Down_Click(object sender, EventArgs e)
    {
        if (c.TurboSequence.Count > 1)
        {
            var i = LB_List.SelectedIndex;
            (c.TurboSequence[i], c.TurboSequence[Math.Min(i + 1, c.TurboSequence.Count - 1)]) = (c.TurboSequence[Math.Min(i + 1, c.TurboSequence.Count - 1)], c.TurboSequence[i]);
            ReloadList();
            LB_List.SelectedIndex = Math.Min(i + 1, c.TurboSequence.Count - 1);
        }
    }

    private void CB_Loop_CheckedChanged(object sender, EventArgs e)
    {
        c.LoopTurbo = CB_Loop.Checked;
    }
}
