using owoow.Core.Interfaces;
using PKHeX.Core;
using System.Globalization;

namespace owoow.WinForms.Subforms;

public partial class WattTrader : Form
{
    readonly MainWindow MainWindow;
    readonly string Tab;
    List<WattTraderFrame> Frames = [];

    public WattTrader(MainWindow f, string tab)
    {
        InitializeComponent();
        MainWindow = f;
        Tab = tab;

        f.SetTextBoxText(f.TB_Seed0.Text, TB_Seed0);
        f.SetTextBoxText(f.TB_Seed1.Text, TB_Seed1);
        f.SetTextBoxText(string.IsNullOrEmpty(f.TB_CurrentAdvances.Text) ? "0" : f.TB_CurrentAdvances.Text.Replace(",", string.Empty), TB_WattTrader_Initial);
        f.SetTextBoxText(((TextBox)f.Controls.Find($"TB_{Tab}_NPCs", true).FirstOrDefault()!).Text, TB_WattTrader_NPCs);
        f.SetCheckBoxCheckedState(((CheckBox)f.Controls.Find($"CB_{Tab}_MenuClose_Direction", true).FirstOrDefault()!).Checked, CB_WattTrader_MenuClose_Direction);
        f.SetComboBoxSelectedIndex(CB_WattTrader_Weather.Items.IndexOf($"{((ComboBox)f.Controls.Find($"CB_{Tab}_Weather", true).FirstOrDefault()!).SelectedItem}"), CB_WattTrader_Weather);
        f.SetComboBoxSelectedIndex(0, CB_Target);

        TB_Seed0.KeyPress += f.KeyPress_AllowOnlyHex!;
        TB_Seed1.KeyPress += f.KeyPress_AllowOnlyHex!;

        TB_SlotMin.KeyPress += f.KeyPress_AllowOnlyNumerical!;
        TB_SlotMax.KeyPress += f.KeyPress_AllowOnlyNumerical!;
        TB_WattTrader_Initial.KeyPress += f.KeyPress_AllowOnlyNumerical!;
        TB_WattTrader_NPCs.KeyPress += f.KeyPress_AllowOnlyNumerical!;
        TB_WattTrader_Advances.KeyPress += f.KeyPress_AllowOnlyNumerical!;
    }

    private void B_WattTrader_Search_Click(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(false, sender);

        if (string.IsNullOrEmpty(TB_WattTrader_Initial.Text)) TB_WattTrader_Initial.Text = "0";
        if (string.IsNullOrEmpty(TB_WattTrader_Advances.Text) || TB_WattTrader_Advances.Text is "0") TB_WattTrader_Advances.Text = "1";

        if (string.IsNullOrEmpty(TB_Seed0.Text)) TB_Seed0.Text = "0";
        if (string.IsNullOrEmpty(TB_Seed1.Text)) TB_Seed1.Text = "0";
        if (TB_Seed0.Text is "0" && TB_Seed1.Text is "0")
        {
            TB_Seed0.Text = "1337";
            TB_Seed1.Text = "1390";
        }

        TB_Seed0.Text = TB_Seed0.Text.PadLeft(16, '0');
        TB_Seed1.Text = TB_Seed1.Text.PadLeft(16, '0');

        var initial = ulong.Parse(TB_WattTrader_Initial.Text);
        var advances = ulong.Parse(TB_WattTrader_Advances.Text);

        var s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
        var s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);

        Core.RNG.GeneratorConfig config = new()
        {
            ConsiderMenuClose = CB_WattTrader_MenuClose.Checked,
            MenuCloseIsHoldingDirection = CB_WattTrader_MenuClose_Direction.Checked,
            MenuCloseNPCs = uint.Parse(TB_WattTrader_NPCs.Text),
            Weather = Core.RNG.Util.GetWeatherType($"{CB_WattTrader_Weather.SelectedItem}"),
            WattTraderSlotMin = uint.Parse(TB_SlotMin.Text),
            WattTraderSlotMax = uint.Parse(TB_SlotMax.Text),
        };

        var rng = new Xoroshiro128Plus(s0, s1);

        Task.Run(async () =>
        {
            Frames = await Task.Run(async () => await Core.RNG.Generators.Item.WattTrader.Generate(s0, s1, initial, initial + advances, config).ConfigureAwait(false));

            MainWindow.SetBindingSourceDataSource(Frames, WattTraderResultsSource);

            MainWindow.SetControlEnabledState(true, sender);
        });
    }

    private void MenuCloseTimeline_FormClosing(object sender, FormClosingEventArgs e)
    {
        MainWindow.WattTraderFormOpen = false;
        MainWindow.SetCheckBoxCheckedState(CB_WattTrader_MenuClose_Direction.Checked, (CheckBox)MainWindow.Controls.Find($"CB_{Tab}_MenuClose_Direction", true).FirstOrDefault()!);
        MainWindow.SetTextBoxText(TB_WattTrader_NPCs.Text, (TextBox)MainWindow.Controls.Find($"TB_{Tab}_NPCs", true).FirstOrDefault()!);
    }

    private void CB_WattTrader_MenuClose_CheckedChanged(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(CB_WattTrader_MenuClose.Checked, CB_WattTrader_MenuClose_Direction, L_WattTrader_NPCs, TB_WattTrader_NPCs);
    }

    private void CB_Target_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CB_Target.Text is null) return;
        var (min, max) = GetRangeFromItemName(CB_Target.Text);
        TB_SlotMin.Text = min.ToString();
        TB_SlotMax.Text = max.ToString();
    }

    private void DGV_Results_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        var index = e.RowIndex;
        if (Frames.Count <= index) return;
        var row = DGV_Results.Rows[index];
        var result = Frames[index];

        if (result.Highlight is "Beast Ball x1 (827)" or "Dream Ball x1 (828)") row.DefaultCellStyle.BackColor = Color.PapayaWhip;
        else row.DefaultCellStyle.BackColor = row.Index % 2 == 0 ? Color.White : Color.WhiteSmoke;
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

    private static (int, int) GetRangeFromItemName(string Item) => Item switch
    {
        "Bottle Cap x1" => (0, 49),
        "Bottle Cap x3" => (50, 59),
        "Gold Bottle Cap x1" => (60, 62),
        "Red Apricorn x5" => (63, 79),
        "Blue Apricorn x5" => (80, 96),
        "Yellow Apricorn x5" => (97, 113),
        "Green Apricorn x5" => (114, 130),
        "White Apricorn x5" => (131, 147),
        "Black Apricorn x5" => (148, 164),
        "Pink Apricorn x5" => (165, 181),
        "Red Apricorn x10" => (182, 191),
        "Blue Apricorn x10" => (192, 201),
        "Yellow Apricorn x10" => (202, 211),
        "Green Apricorn x10" => (212, 221),
        "White Apricorn x10" => (222, 231),
        "Black Apricorn x10" => (232, 241),
        "Pink Apricorn x10" => (242, 251),
        "PP Up x1" => (252, 301),
        "PP Up x2" => (302, 326),
        "PP Max x1" => (327, 331),
        "Rare Candy x1" => (332, 381),
        "Rare Candy x5" => (382, 421),
        "Gigantamix x1" => (422, 471),
        "Armorite Ore x1" => (472, 531),
        "Armorite Ore x3" => (532, 551),
        "Armorite Ore x8" => (552, 556),
        "Dynite Ore x1" => (557, 576),
        "Dynite Ore x5" => (577, 581),
        "Max Mushrooms x1" => (582, 601),
        "Max Elixir x1" => (602, 641),
        "Galarica Twig x3" => (642, 681),
        "Galarica Twig x5" => (682, 691),
        "Strawberry Sweet x1" => (692, 706),
        "Love Sweet x1" => (707, 721),
        "Berry Sweet x1" => (722, 736),
        "Clover Sweet x1" => (737, 751),
        "Flower Sweet x1" => (752, 766),
        "Star Sweet x1" => (767, 781),
        "Ribbon Sweet x1" => (782, 796),
        "Big Nugget x1" => (797, 826),
        "Beast Ball x1" => (827, 827),
        "Dream Ball x1" => (828, 828),
        "Lansat Berry x1" => (829, 858),
        "Starf Berry x1" => (859, 868),
        "Lucky Egg x1" => (869, 878),
        "Electirizer x1" => (879, 908),
        "Magmarizer x1" => (909, 938),
        "Cracked Pot x1" => (939, 968),
        "Chipped Pot x1" => (969, 979),
        "King's Rock x1" => (980, 999),
        "Beast or Dream Ball" => (827, 828),
        _ => (0, 999),
    };
}
