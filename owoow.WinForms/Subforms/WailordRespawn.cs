using owoow.Core.Enums;
using owoow.Core.Interfaces;
using owoow.Core.RNG.Generators.Item;
using PKHeX.Core;
using System.Globalization;

namespace owoow.WinForms.Subforms;

public partial class WailordRespawn : Form
{
    readonly MainWindow MainWindow;
    readonly EncounterType Tab;
    List<WailordFrame> Frames = [];

    public WailordRespawn(MainWindow f, EncounterType tab)
    {
        InitializeComponent();
        MainWindow = f;
        Tab = tab;

        f.SetTextBoxText(f.TB_Seed0.Text, TB_Seed0);
        f.SetTextBoxText(f.TB_Seed1.Text, TB_Seed1);
        f.SetTextBoxText(string.IsNullOrEmpty(f.TB_CurrentAdvances.Text) ? "0" : f.TB_CurrentAdvances.Text.Replace(",", string.Empty), TB_Wailord_Initial);
        f.SetTextBoxText(((TextBox)f.Controls.Find($"TB_{Tab}_NPCs", true).FirstOrDefault()!).Text, TB_Wailord_NPCs);
        f.SetCheckBoxCheckedState(((CheckBox)f.Controls.Find($"CB_{Tab}_MenuClose", true).FirstOrDefault()!).Checked, CB_Wailord_MenuClose);
        f.SetCheckBoxCheckedState(((CheckBox)f.Controls.Find($"CB_{Tab}_MenuClose_Direction", true).FirstOrDefault()!).Checked, CB_Wailord_MenuClose_Direction);
        f.SetComboBoxSelectedIndex(0, CB_Target);

        TB_Seed0.KeyPress += f.KeyPress_AllowOnlyHex!;
        TB_Seed1.KeyPress += f.KeyPress_AllowOnlyHex!;

        TB_Wailord_Initial.KeyPress += f.KeyPress_AllowOnlyNumerical!;
        TB_Wailord_NPCs.KeyPress += f.KeyPress_AllowOnlyNumerical!;
        TB_Wailord_Advances.KeyPress += f.KeyPress_AllowOnlyNumerical!;
    }

    private void B_Wailord_Search_Click(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(false, sender);

        if (string.IsNullOrEmpty(TB_Wailord_Initial.Text)) TB_Wailord_Initial.Text = "0";
        if (string.IsNullOrEmpty(TB_Wailord_Advances.Text) || TB_Wailord_Advances.Text is "0") TB_Wailord_Advances.Text = "1";

        if (string.IsNullOrEmpty(TB_Seed0.Text)) TB_Seed0.Text = "0";
        if (string.IsNullOrEmpty(TB_Seed1.Text)) TB_Seed1.Text = "0";
        if (TB_Seed0.Text is "0" && TB_Seed1.Text is "0")
        {
            TB_Seed0.Text = "1337";
            TB_Seed1.Text = "1390";
        }

        TB_Seed0.Text = TB_Seed0.Text.PadLeft(16, '0');
        TB_Seed1.Text = TB_Seed1.Text.PadLeft(16, '0');

        var initial = ulong.Parse(TB_Wailord_Initial.Text);
        var advances = ulong.Parse(TB_Wailord_Advances.Text);

        var s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
        var s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);

        Core.RNG.GeneratorConfig config = new()
        {
            ConsiderMenuClose = CB_Wailord_MenuClose.Checked,
            MenuCloseIsHoldingDirection = CB_Wailord_MenuClose_Direction.Checked,
            MenuCloseNPCs = uint.Parse(TB_Wailord_NPCs.Text),
            Weather = WeatherType.NormalWeather,
            SuccessType = Core.RNG.Util.GetSuccessType(CB_Target.SelectedIndex),
        };

        Task.Run(async () =>
        {
            Frames = await Task.Run(async () => await Wailord.Generate(s0, s1, initial, initial + advances, config).ConfigureAwait(false));

            MainWindow.SetBindingSourceDataSource(Frames, WailordResultsSource);

            MainWindow.SetControlEnabledState(true, sender);
        });
    }

    private void MenuCloseTimeline_FormClosing(object sender, FormClosingEventArgs e)
    {
        MainWindow.WailordRespawnFormOpen = false;
        MainWindow.SetCheckBoxCheckedState(CB_Wailord_MenuClose_Direction.Checked, (CheckBox)MainWindow.Controls.Find($"CB_{Tab}_MenuClose_Direction", true).FirstOrDefault()!);
        MainWindow.SetTextBoxText(TB_Wailord_NPCs.Text, (TextBox)MainWindow.Controls.Find($"TB_{Tab}_NPCs", true).FirstOrDefault()!);
    }

    private void CB_Wailord_MenuClose_CheckedChanged(object sender, EventArgs e)
    {
        var c = CB_Wailord_MenuClose.Checked;
        MainWindow.SetControlEnabledState(c, CB_Wailord_MenuClose_Direction, L_Wailord_NPCs, TB_Wailord_NPCs);
        MainWindow.SetMenuClose(c);
    }

    private void DGV_Results_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        var index = e.RowIndex;
        if (Frames.Count <= index) return;
        var row = DGV_Results.Rows[index];
        var result = Frames[index];

        if (result.Respawn == 'Y') row.DefaultCellStyle.BackColor = Color.PapayaWhip;
        else row.DefaultCellStyle.BackColor = row.Index % 2 == 0 ? Color.White : Color.WhiteSmoke;
    }

    private void CB_Leave(object sender, EventArgs e)
    {
        var cb = (ComboBox)sender;
        var last = cb.SelectedIndex;
        var text = cb.Text;
        var items = cb.Items.Cast<string>().ToList();
        var match = items.Find(e => e.Equals(text, StringComparison.CurrentCultureIgnoreCase));
        cb.SelectedIndex = match is not null ? items.IndexOf(match) : Math.Max(last, 0);
    }

    public void SetSeeds(string s0, string s1)
    {
        MainWindow.SetTextBoxText(s0, TB_Seed0);
        MainWindow.SetTextBoxText(s1, TB_Seed1);
        Focus();
    }

    public void SetMenuClose(bool check)
    {
        MainWindow.SetCheckBoxCheckedState(check, CB_Wailord_MenuClose);
    }

    public void SetMenuCloseDirection(bool check)
    {
        MainWindow.SetCheckBoxCheckedState(check, CB_Wailord_MenuClose_Direction);
    }

    private void CB_Wailord_MenuClose_Direction_CheckedChanged(object sender, EventArgs e)
    {
        MainWindow.SetMenuCloseDirection(CB_Wailord_MenuClose_Direction.Checked);
    }
}
