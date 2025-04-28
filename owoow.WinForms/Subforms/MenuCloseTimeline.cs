using owoow.Core.Interfaces;
using owoow.Core.RNG.Generators.Misc;
using PKHeX.Core;
using System.Globalization;

namespace owoow.WinForms.Subforms;

public partial class MenuCloseTimeline : Form
{
    readonly MainWindow MainWindow;
    readonly string Tab;

    public MenuCloseTimeline(MainWindow f, string tab)
    {
        InitializeComponent();
        MainWindow = f;
        Tab = tab;

        f.SetTextBoxText(f.TB_Seed0.Text, TB_Seed0);
        f.SetTextBoxText(f.TB_Seed1.Text, TB_Seed1);
        f.SetTextBoxText(string.IsNullOrEmpty(f.TB_CurrentAdvances.Text) ? "0" : f.TB_CurrentAdvances.Text.Replace(",", string.Empty), TB_Timeline_Initial);
        f.SetTextBoxText(((TextBox)f.Controls.Find($"TB_{Tab}_NPCs", true).FirstOrDefault()!).Text, TB_Timeline_NPCs);
        f.SetComboBoxSelectedIndex(CB_Timeline_Weather.Items.IndexOf($"{((ComboBox)f.Controls.Find($"CB_{Tab}_Weather", true).FirstOrDefault()!).SelectedItem}"), CB_Timeline_Weather);

        TB_Seed0.KeyPress += f.KeyPress_AllowOnlyHex!;
        TB_Seed1.KeyPress += f.KeyPress_AllowOnlyHex!;

        TB_Timeline_Initial.KeyPress += f.KeyPress_AllowOnlyNumerical!;
        TB_Timeline_NPCs.KeyPress += f.KeyPress_AllowOnlyNumerical!;
        TB_Timeline_Advances.KeyPress += f.KeyPress_AllowOnlyNumerical!;
    }

    private void B_Timeline_Search_Click(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(false, sender);

        if (string.IsNullOrEmpty(TB_Timeline_Initial.Text)) TB_Timeline_Initial.Text = "0";
        if (string.IsNullOrEmpty(TB_Timeline_Advances.Text) || TB_Timeline_Advances.Text is "0") TB_Timeline_Advances.Text = "1";

        if (string.IsNullOrEmpty(TB_Seed0.Text)) TB_Seed0.Text = "0";
        if (string.IsNullOrEmpty(TB_Seed1.Text)) TB_Seed1.Text = "0";
        if (TB_Seed0.Text is "0" && TB_Seed1.Text is "0")
        {
            TB_Seed0.Text = "1337";
            TB_Seed1.Text = "1390";
        }

        TB_Seed0.Text = TB_Seed0.Text.PadLeft(16, '0');
        TB_Seed1.Text = TB_Seed1.Text.PadLeft(16, '0');

        var initial = ulong.Parse(TB_Timeline_Initial.Text);
        var advances = ulong.Parse(TB_Timeline_Advances.Text);

        var s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
        var s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);

        Core.RNG.GeneratorConfig config = new()
        {
            MenuCloseIsHoldingDirection = CB_Timeline_MenuClose_Direction.Checked,
            MenuCloseNPCs = uint.Parse(TB_Timeline_NPCs.Text),
            Weather = Core.RNG.Util.GetWeatherType($"{CB_Timeline_Weather.SelectedItem}"),
        };

        var rng = new Xoroshiro128Plus(s0, s1);

        Task.Run(async () =>
        {
            List<MenuCloseFrame> results = await Task.Run(async () => await MenuClose.Generate(s0, s1, initial, initial + advances, config).ConfigureAwait(false));

            MainWindow.SetBindingSourceDataSource(results, MenuCloseResultsSource);

            MainWindow.SetControlEnabledState(true, sender);
        });
    }

    private void MenuCloseTimeline_FormClosing(object sender, FormClosingEventArgs e)
    {
        MainWindow.MenuCloseTimelineFormOpen = false;
        if (!MainWindow.CB_ConsiderFlying.Checked) MainWindow.SetTextBoxText(TB_Timeline_NPCs.Text, (TextBox)MainWindow.Controls.Find($"TB_{Tab}_NPCs", true).FirstOrDefault()!);
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
