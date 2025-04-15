using owoow.Core.Enums;
using owoow.Core.Interfaces;
using PKHeX.Core;
using System.Globalization;

namespace owoow.WinForms.Subforms;

public partial class Cramomatic : Form
{
    readonly MainWindow MainWindow;
    readonly string Tab;
    List<CramomaticFrame> Frames = [];

    public Cramomatic(MainWindow f, string tab)
    {
        InitializeComponent();
        MainWindow = f;
        Tab = tab;

        f.SetTextBoxText(f.TB_Seed0.Text, TB_Seed0);
        f.SetTextBoxText(f.TB_Seed1.Text, TB_Seed1);
        f.SetTextBoxText(string.IsNullOrEmpty(f.TB_CurrentAdvances.Text) ? "0" : f.TB_CurrentAdvances.Text.Replace(",", string.Empty), TB_Cramomatic_Initial);
        f.SetTextBoxText(((TextBox)f.Controls.Find($"TB_{Tab}_NPCs", true).FirstOrDefault()!).Text, TB_Cramomatic_NPCs);
        f.SetCheckBoxCheckedState(((CheckBox)f.Controls.Find($"CB_{Tab}_MenuClose_Direction", true).FirstOrDefault()!).Checked, CB_Cramomatic_MenuClose_Direction);
        f.SetComboBoxSelectedIndex(0, CB_Target, CB_Item1, CB_Item2, CB_Item3, CB_Item4);

        TB_Seed0.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyHex!);
        TB_Seed1.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyHex!);

        TB_Cramomatic_Initial.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyNumerical!);
        TB_Cramomatic_NPCs.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyNumerical!);
        TB_Cramomatic_Advances.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyNumerical!);
    }

    private void B_Cramomatic_Search_Click(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(false, sender);

        if (string.IsNullOrEmpty(TB_Cramomatic_Initial.Text)) TB_Cramomatic_Initial.Text = "0";
        if (string.IsNullOrEmpty(TB_Cramomatic_Advances.Text) || TB_Cramomatic_Advances.Text is "0") TB_Cramomatic_Advances.Text = "1";

        if (string.IsNullOrEmpty(TB_Seed0.Text)) TB_Seed0.Text = "0";
        if (string.IsNullOrEmpty(TB_Seed1.Text)) TB_Seed1.Text = "0";
        if (TB_Seed0.Text is "0" && TB_Seed1.Text is "0")
        {
            TB_Seed0.Text = "1337";
            TB_Seed1.Text = "1390";
        }

        TB_Seed0.Text = TB_Seed0.Text.PadLeft(16, '0');
        TB_Seed1.Text = TB_Seed1.Text.PadLeft(16, '0');

        var initial = ulong.Parse(TB_Cramomatic_Initial.Text);
        var advances = ulong.Parse(TB_Cramomatic_Advances.Text);

        var s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
        var s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);

        Core.RNG.GeneratorConfig config = new()
        {
            ConsiderMenuClose = CB_Cramomatic_MenuClose.Checked,
            MenuCloseIsHoldingDirection = CB_Cramomatic_MenuClose_Direction.Checked,
            MenuCloseNPCs = uint.Parse(TB_Cramomatic_NPCs.Text),
            Weather = WeatherType.NormalWeather,
            CramomaticTargetType = Core.RNG.Util.GetCramomaticTargetType(CB_Target.SelectedIndex),
            CramomaticInputs =
            [
                Core.RNG.Util.GetCramomaticInputItemType(CB_Item1.SelectedIndex),
                Core.RNG.Util.GetCramomaticInputItemType(CB_Item2.SelectedIndex),
                Core.RNG.Util.GetCramomaticInputItemType(CB_Item3.SelectedIndex),
                Core.RNG.Util.GetCramomaticInputItemType(CB_Item4.SelectedIndex),
            ],
            BonusOnly = CB_BonusOnly.Checked,
        };

        var rng = new Xoroshiro128Plus(s0, s1);

        Task.Run(async () =>
        {
            Frames = await Task.Run(async () => await Core.RNG.Generators.Item.Cramomatic.Generate(s0, s1, initial, initial + advances, config));

            MainWindow.SetBindingSourceDataSource(Frames, CramomaticResultsSource);

            MainWindow.SetControlEnabledState(true, sender);
        });
    }

    private void MenuCloseTimeline_FormClosing(object sender, FormClosingEventArgs e)
    {
        MainWindow.CramomaticFormOpen = false;
        MainWindow.SetCheckBoxCheckedState(CB_Cramomatic_MenuClose_Direction.Checked, (CheckBox)MainWindow.Controls.Find($"CB_{Tab}_MenuClose_Direction", true).FirstOrDefault()!);
        MainWindow.SetTextBoxText(TB_Cramomatic_NPCs.Text, (TextBox)MainWindow.Controls.Find($"TB_{Tab}_NPCs", true).FirstOrDefault()!);
    }

    private void CB_Cramomatic_MenuClose_CheckedChanged(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(CB_Cramomatic_MenuClose.Checked, CB_Cramomatic_MenuClose_Direction, L_Cramomatic_NPCs, TB_Cramomatic_NPCs);
    }

    private void DGV_Results_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        var index = e.RowIndex;
        if (Frames.Count <= index) return;
        var row = DGV_Results.Rows[index];
        var result = Frames[index];

        if (result.Prize is "Safari Ball" or "Sport Ball") row.DefaultCellStyle.BackColor = Color.PapayaWhip;
        else row.DefaultCellStyle.BackColor = result.Bonus ? Color.LightCyan : row.Index % 2 == 0 ? Color.White : Color.WhiteSmoke;
    }

    private void CB_Leave(object sender, EventArgs e)
    {
        var cb = (ComboBox)sender;
        var last = cb.SelectedIndex;
        var text = cb.Text;
        var items = cb.Items.Cast<string>().ToList();
        var match = items.Find(e => e.Equals(text, StringComparison.CurrentCultureIgnoreCase));
        if (match is not null)
        {
            cb.SelectedIndex = items.IndexOf(match);
        }
        else
        {
            cb.SelectedIndex = Math.Max(last, 0);
        }
    }
}
