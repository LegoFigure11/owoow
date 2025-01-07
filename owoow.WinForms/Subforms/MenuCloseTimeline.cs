using owoow.Core.Interfaces;
using owoow.Core.RNG.Generators;
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
        f.SetCheckBoxCheckedState(((CheckBox)f.Controls.Find($"CB_{Tab}_MenuClose_Direction", true).FirstOrDefault()!).Checked, CB_Timeline_MenuClose_Direction);
        f.SetComboBoxSelectedIndex(CB_Timeline_Weather.Items.IndexOf($"{((ComboBox)f.Controls.Find($"CB_{Tab}_Weather", true).FirstOrDefault()!).SelectedItem}"), CB_Timeline_Weather);
    }

    private void B_Symbol_Search_Click(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(false, sender);

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
            List<MenuCloseFrame> results = await Task.Run(async () => await MenuClose.Generate(s0, s1, initial, initial + advances, config));

            MainWindow.SetBindingSourceDataSource(results, MenuCloseResultsSource);

            MainWindow.SetControlEnabledState(true, sender);
        });
    }

    private void MenuCloseTimeline_FormClosing(object sender, FormClosingEventArgs e)
    {
        MainWindow.MenuCloseTimelineFormOpen = false;
        MainWindow.SetCheckBoxCheckedState(CB_Timeline_MenuClose_Direction.Checked, (CheckBox)MainWindow.Controls.Find($"CB_{Tab}_MenuClose_Direction", true).FirstOrDefault()!);
        MainWindow.SetTextBoxText(TB_Timeline_NPCs.Text, (TextBox)MainWindow.Controls.Find($"TB_{Tab}_NPCs", true).FirstOrDefault()!);
    }
}
