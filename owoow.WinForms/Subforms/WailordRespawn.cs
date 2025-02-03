using owoow.Core.Enums;
using owoow.Core.Interfaces;
using owoow.Core.RNG.Generators.Item;
using PKHeX.Core;
using System.Globalization;

namespace owoow.WinForms.Subforms;

public partial class WailordRespawn : Form
{
    readonly MainWindow MainWindow;
    readonly string Tab;
    List<WailordFrame> Frames = [];

    public WailordRespawn(MainWindow f, string tab)
    {
        InitializeComponent();
        MainWindow = f;
        Tab = tab;

        f.SetTextBoxText(f.TB_Seed0.Text, TB_Seed0);
        f.SetTextBoxText(f.TB_Seed1.Text, TB_Seed1);
        f.SetTextBoxText(string.IsNullOrEmpty(f.TB_CurrentAdvances.Text) ? "0" : f.TB_CurrentAdvances.Text.Replace(",", string.Empty), TB_Wailord_Initial);
        f.SetTextBoxText(((TextBox)f.Controls.Find($"TB_{Tab}_NPCs", true).FirstOrDefault()!).Text, TB_Wailord_NPCs);
        f.SetCheckBoxCheckedState(((CheckBox)f.Controls.Find($"CB_{Tab}_MenuClose_Direction", true).FirstOrDefault()!).Checked, CB_Wailord_MenuClose_Direction);
        f.SetComboBoxSelectedIndex(0, CB_Target);

        TB_Seed0.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyHex!);
        TB_Seed1.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyHex!);

        TB_Wailord_Initial.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyNumerical!);
        TB_Wailord_NPCs.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyNumerical!);
        TB_Wailord_Advances.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyNumerical!);
    }

    private void B_Wailord_Search_Click(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(false, sender);

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

        var rng = new Xoroshiro128Plus(s0, s1);

        Task.Run(async () =>
        {
            Frames = await Task.Run(async () => await Wailord.Generate(s0, s1, initial, initial + advances, config));

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
        MainWindow.SetControlEnabledState(CB_Wailord_MenuClose.Checked, CB_Wailord_MenuClose_Direction, L_Wailord_NPCs, TB_Wailord_NPCs);
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
}
