using owoow.Core.Interfaces;
using PKHeX.Core;
using System.Globalization;

namespace owoow.WinForms.Subforms;

public partial class DiggingPa : Form
{
    readonly MainWindow MainWindow;
    readonly string Tab;
    List<DiggingPaFrame> Frames = [];

    public DiggingPa(MainWindow f, string tab)
    {
        InitializeComponent();
        MainWindow = f;
        Tab = tab;

        f.SetTextBoxText(f.TB_Seed0.Text, TB_Seed0);
        f.SetTextBoxText(f.TB_Seed1.Text, TB_Seed1);
        f.SetTextBoxText(string.IsNullOrEmpty(f.TB_CurrentAdvances.Text) ? "0" : f.TB_CurrentAdvances.Text.Replace(",", string.Empty), TB_DiggingPa_Initial);
        f.SetTextBoxText(((TextBox)f.Controls.Find($"TB_{Tab}_NPCs", true).FirstOrDefault()!).Text, TB_DiggingPa_NPCs);
        f.SetCheckBoxCheckedState(((CheckBox)f.Controls.Find($"CB_{Tab}_MenuClose_Direction", true).FirstOrDefault()!).Checked, CB_DiggingPa_MenuClose_Direction);
        f.SetComboBoxSelectedIndex(CB_DiggingPa_Weather.Items.IndexOf($"{((ComboBox)f.Controls.Find($"CB_{Tab}_Weather", true).FirstOrDefault()!).SelectedItem}"), CB_DiggingPa_Weather);

        TB_Seed0.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyHex!);
        TB_Seed1.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyHex!);
        
        TB_Target.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyNumerical!);
        TB_DiggingPa_Initial.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyNumerical!);
        TB_DiggingPa_NPCs.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyNumerical!);
        TB_DiggingPa_Advances.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyNumerical!);
    }

    private void B_DiggingPa_Search_Click(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(false, sender);

        if (string.IsNullOrEmpty(TB_DiggingPa_Initial.Text)) TB_DiggingPa_Initial.Text = "0";
        if (string.IsNullOrEmpty(TB_DiggingPa_Advances.Text) || TB_DiggingPa_Advances.Text is "0") TB_DiggingPa_Advances.Text = "1";

        if (string.IsNullOrEmpty(TB_Seed0.Text)) TB_Seed0.Text = "0";
        if (string.IsNullOrEmpty(TB_Seed1.Text)) TB_Seed1.Text = "0";
        if (TB_Seed0.Text is "0" && TB_Seed1.Text is "0")
        {
            TB_Seed0.Text = "1337";
            TB_Seed1.Text = "1390";
        }

        TB_Seed0.Text = TB_Seed0.Text.PadLeft(16, '0');
        TB_Seed1.Text = TB_Seed1.Text.PadLeft(16, '0');

        var initial = ulong.Parse(TB_DiggingPa_Initial.Text);
        var advances = ulong.Parse(TB_DiggingPa_Advances.Text);

        var s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
        var s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);

        Core.RNG.GeneratorConfig config = new()
        {
            ConsiderMenuClose = CB_DiggingPa_MenuClose.Checked,
            MenuCloseIsHoldingDirection = CB_DiggingPa_MenuClose_Direction.Checked,
            MenuCloseNPCs = uint.Parse(TB_DiggingPa_NPCs.Text),
            Weather = Core.RNG.Util.GetWeatherType($"{CB_DiggingPa_Weather.SelectedItem}"),
            DiggingPaMinWatts = uint.Parse(TB_Target.Text),
        };

        var rng = new Xoroshiro128Plus(s0, s1);

        Task.Run(async () =>
        {
            Frames = await Task.Run(async () => await Core.RNG.Generators.Item.DiggingPa.Generate(s0, s1, initial, initial + advances, config));

            MainWindow.SetBindingSourceDataSource(Frames, DiggingPaResultsSource);

            MainWindow.SetControlEnabledState(true, sender);
        });
    }

    private void MenuCloseTimeline_FormClosing(object sender, FormClosingEventArgs e)
    {
        MainWindow.DiggingPaFormOpen = false;
        MainWindow.SetCheckBoxCheckedState(CB_DiggingPa_MenuClose_Direction.Checked, (CheckBox)MainWindow.Controls.Find($"CB_{Tab}_MenuClose_Direction", true).FirstOrDefault()!);
        MainWindow.SetTextBoxText(TB_DiggingPa_NPCs.Text, (TextBox)MainWindow.Controls.Find($"TB_{Tab}_NPCs", true).FirstOrDefault()!);
    }

    private void CB_DiggingPa_MenuClose_CheckedChanged(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(CB_DiggingPa_MenuClose.Checked, CB_DiggingPa_MenuClose_Direction, L_DiggingPa_NPCs, TB_DiggingPa_NPCs);
    }

    private void DGV_Results_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        var index = e.RowIndex;
        if (Frames.Count <= index) return;
        var row = DGV_Results.Rows[index];
        var result = Frames[index];

        if (result.Watts >= 1_000_000) row.DefaultCellStyle.BackColor = Color.LightCyan;
        else if (result.Watts >= 700_000) row.DefaultCellStyle.BackColor = Color.PapayaWhip;
        else row.DefaultCellStyle.BackColor = row.Index % 2 == 0 ? Color.White : Color.WhiteSmoke;
    }
}
