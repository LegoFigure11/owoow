using owoow.Core.Enums;
using owoow.Core.Interfaces;
using PKHeX.Core;
using System.Globalization;
using System.Text.Json;

namespace owoow.WinForms.Subforms;

public partial class LotoID : Form
{
    readonly MainWindow MainWindow;
    readonly string Tab;
    public List<string> IDs = [];
    List<LotoIDFrame> Frames = [];

    public LotoID(MainWindow f, string tab)
    {
        InitializeComponent();
        MainWindow = f;
        Tab = tab;

        var idsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "id-list.json");
        if (File.Exists(idsPath))
            IDs = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(idsPath)) ?? [];

        f.SetTextBoxText(f.TB_Seed0.Text, TB_Seed0);
        f.SetTextBoxText(f.TB_Seed1.Text, TB_Seed1);
        f.SetTextBoxText(string.IsNullOrEmpty(f.TB_CurrentAdvances.Text) ? "0" : f.TB_CurrentAdvances.Text.Replace(",", string.Empty), TB_LotoID_Initial);
        f.SetTextBoxText(((TextBox)f.Controls.Find($"TB_{Tab}_NPCs", true).FirstOrDefault()!).Text, TB_LotoID_NPCs);
        f.SetCheckBoxCheckedState(((CheckBox)f.Controls.Find($"CB_{Tab}_MenuClose_Direction", true).FirstOrDefault()!).Checked, CB_LotoID_MenuClose_Direction);
        f.SetComboBoxSelectedIndex(0, CB_Target);

        for (var i = 0; i < IDs.Count; i++)
        {
            IDs[i] = IDs[i].PadLeft(6, '0');
            if (IDs[i].Length > 6) IDs[i] = IDs[i][..6];
        }

        L_LoadedIDs.Text = $"Loaded IDs: {IDs.Count}";

        TB_Seed0.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyHex!);
        TB_Seed1.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyHex!);

        TB_LotoID_Initial.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyNumerical!);
        TB_LotoID_NPCs.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyNumerical!);
        TB_LotoID_Advances.KeyPress += new KeyPressEventHandler(f.KeyPress_AllowOnlyNumerical!);
    }

    private string Target = string.Empty;
    private void B_LotoID_Search_Click(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(false, sender);

        var initial = ulong.Parse(TB_LotoID_Initial.Text);
        var advances = ulong.Parse(TB_LotoID_Advances.Text);

        var s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
        var s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);

        Core.RNG.GeneratorConfig config = new()
        {
            ConsiderMenuClose = CB_LotoID_MenuClose.Checked,
            MenuCloseIsHoldingDirection = CB_LotoID_MenuClose_Direction.Checked,
            MenuCloseNPCs = uint.Parse(TB_LotoID_NPCs.Text),
            Weather = WeatherType.NormalWeather,
            LotoIDTargetType = Core.RNG.Util.GetLotoIDTargetType(CB_Target.SelectedIndex),
            IDs = IDs,
        };

        Target = Core.RNG.Util.GetLotoIDPrizeName(config.LotoIDTargetType);

        var rng = new Xoroshiro128Plus(s0, s1);

        Task.Run(async () =>
        {
            Frames = await Task.Run(async () => await Core.RNG.Generators.Item.LotoID.Generate(s0, s1, initial, initial + advances, config));

            MainWindow.SetBindingSourceDataSource(Frames, LotoIDResultsSource);

            MainWindow.SetControlEnabledState(true, sender);
        });
    }

    private void MenuCloseTimeline_FormClosing(object sender, FormClosingEventArgs e)
    {
        MainWindow.LotoIDFormOpen = false;
        MainWindow.SetCheckBoxCheckedState(CB_LotoID_MenuClose_Direction.Checked, (CheckBox)MainWindow.Controls.Find($"CB_{Tab}_MenuClose_Direction", true).FirstOrDefault()!);
        MainWindow.SetTextBoxText(TB_LotoID_NPCs.Text, (TextBox)MainWindow.Controls.Find($"TB_{Tab}_NPCs", true).FirstOrDefault()!);
    }

    private void CB_LotoID_MenuClose_CheckedChanged(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(CB_LotoID_MenuClose.Checked, CB_LotoID_MenuClose_Direction, L_LotoID_NPCs, TB_LotoID_NPCs);
    }

    private void DGV_Results_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        var index = e.RowIndex;
        if (Frames.Count <= index) return;
        var row = DGV_Results.Rows[index];
        var result = Frames[index];

        if (result.Prize == Target && Target != "None") row.DefaultCellStyle.BackColor = Color.PapayaWhip;
        else row.DefaultCellStyle.BackColor = result.Prize == "Master Ball" ? Color.LightCyan : row.Index % 2 == 0 ? Color.White : Color.WhiteSmoke;
    }

    public bool SubformOpen = false;
    IDList? Idl;
    private void B_IDList_Click(object sender, EventArgs e)
    {
        if (!SubformOpen)
        {
            SubformOpen = true;
            Idl = new IDList(ref IDs, this);
            Idl.Show();
        }
        else
        {
            Idl?.Focus();
        }
    }
}
