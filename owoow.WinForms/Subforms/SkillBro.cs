using owoow.Core.Interfaces;
using PKHeX.Core;
using System.Globalization;
using owoow.Core.Enums;

namespace owoow.WinForms.Subforms;

public partial class SkillBro : Form
{
    readonly MainWindow MainWindow;
    readonly EncounterType Tab;
    List<SkillBroFrame> Frames = [];

    public SkillBro(MainWindow f, EncounterType tab)
    {
        InitializeComponent();
        MainWindow = f;
        Tab = tab;

        f.SetTextBoxText(f.TB_Seed0.Text, TB_Seed0);
        f.SetTextBoxText(f.TB_Seed1.Text, TB_Seed1);
        f.SetTextBoxText(string.IsNullOrEmpty(f.TB_CurrentAdvances.Text) ? "0" : f.TB_CurrentAdvances.Text.Replace(",", string.Empty), TB_SkillBro_Initial);
        f.SetTextBoxText(((TextBox)f.Controls.Find($"TB_{Tab}_NPCs", true).FirstOrDefault()!).Text, TB_SkillBro_NPCs);
        f.SetCheckBoxCheckedState(((CheckBox)f.Controls.Find($"CB_{Tab}_MenuClose", true).FirstOrDefault()!).Checked, CB_SkillBro_MenuClose);
        f.SetCheckBoxCheckedState(((CheckBox)f.Controls.Find($"CB_{Tab}_MenuClose_Direction", true).FirstOrDefault()!).Checked, CB_SkillBro_MenuClose_Direction);
        f.SetComboBoxSelectedIndex(CB_SkillBro_Weather.Items.IndexOf($"{((ComboBox)f.Controls.Find($"CB_{Tab}_Weather", true).FirstOrDefault()!).SelectedItem}"), CB_SkillBro_Weather);
        f.SetComboBoxSelectedIndex(f.CB_Game.SelectedIndex, CB_SkillBro_Game);

        TB_Seed0.KeyPress += f.KeyPress_AllowOnlyHex!;
        TB_Seed1.KeyPress += f.KeyPress_AllowOnlyHex!;
        TB_Seed0.KeyDown += f.State_HandlePaste!;
        TB_Seed1.KeyDown += f.State_HandlePaste!;

        TB_SkillBro_Initial.KeyPress += f.KeyPress_AllowOnlyNumerical!;
        TB_SkillBro_NPCs.KeyPress += f.KeyPress_AllowOnlyNumerical!;
        TB_SkillBro_Advances.KeyPress += f.KeyPress_AllowOnlyNumerical!;
        TB_SkillBro_Initial.KeyDown += f.Dec_HandlePaste!;
        TB_SkillBro_NPCs.KeyDown += f.Dec_HandlePaste!;
        TB_SkillBro_Advances.KeyDown += f.Dec_HandlePaste!;
    }

    private void B_SkillBro_Search_Click(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(false, sender);

        if (string.IsNullOrEmpty(TB_SkillBro_Initial.Text)) TB_SkillBro_Initial.Text = "0";
        if (string.IsNullOrEmpty(TB_SkillBro_Advances.Text) || TB_SkillBro_Advances.Text is "0") TB_SkillBro_Advances.Text = "1";

        if (string.IsNullOrEmpty(TB_Seed0.Text)) TB_Seed0.Text = "0";
        if (string.IsNullOrEmpty(TB_Seed1.Text)) TB_Seed1.Text = "0";
        if (TB_Seed0.Text is "0" && TB_Seed1.Text is "0")
        {
            TB_Seed0.Text = "1337";
            TB_Seed1.Text = "1390";
        }

        TB_Seed0.Text = TB_Seed0.Text.PadLeft(16, '0');
        TB_Seed1.Text = TB_Seed1.Text.PadLeft(16, '0');

        var initial = ulong.Parse(TB_SkillBro_Initial.Text);
        var advances = ulong.Parse(TB_SkillBro_Advances.Text);

        var s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
        var s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);

        Core.RNG.GeneratorConfig config = new()
        {
            ConsiderMenuClose = CB_SkillBro_MenuClose.Checked,
            MenuCloseIsHoldingDirection = CB_SkillBro_MenuClose_Direction.Checked,
            MenuCloseNPCs = uint.Parse(TB_SkillBro_NPCs.Text),
            Weather = Core.RNG.Util.GetWeatherType($"{CB_SkillBro_Weather.SelectedItem}"),
            SkillBroItemsMin = [
                (byte)NUD_GoldBottleCap.Value,
                (byte)NUD_BottleCap.Value,
                (byte)NUD_NormalGem.Value,
                (byte)NUD_StickyBarb.Value,
                (byte)NUD_LightClay.Value,
                (byte)NUD_LaggingTail.Value,
                (byte)NUD_IronBall.Value,
                (byte)NUD_MetalCoat.Value,
                (byte)NUD_IceStone.Value,
                (byte)NUD_DawnStone.Value,
                (byte)NUD_DuskStone.Value,
                (byte)NUD_ShinyStone.Value,
                (byte)NUD_MoonStone.Value,
                (byte)NUD_SunStone.Value,
                (byte)NUD_FossilizedFish.Value,
                (byte)NUD_FossilizedDrake.Value,
                (byte)NUD_FossilizedDino.Value,
                (byte)NUD_FossilizedBird.Value,
                (byte)NUD_WishingPiece.Value,
                (byte)NUD_CometShard.Value,
                (byte)NUD_RareBone.Value,
                ],
            SkillBroItemsMinCount = (int)NUD_MinTotal.Value,
        };

        Task.Run(async () =>
        {
            Frames = await Task.Run(async () => await Core.RNG.Generators.Item.SkillBro.Generate(s0, s1, initial, initial + advances, config).ConfigureAwait(false));

            MainWindow.SetBindingSourceDataSource(Frames, SkillBroResultsSource);

            MainWindow.SetControlEnabledState(true, sender);
        });
    }

    private void MenuCloseTimeline_FormClosing(object sender, FormClosingEventArgs e)
    {
        MainWindow.SkillBroFormOpen = false;
        MainWindow.SetCheckBoxCheckedState(CB_SkillBro_MenuClose_Direction.Checked, (CheckBox)MainWindow.Controls.Find($"CB_{Tab}_MenuClose_Direction", true).FirstOrDefault()!);
        MainWindow.SetTextBoxText(TB_SkillBro_NPCs.Text, (TextBox)MainWindow.Controls.Find($"TB_{Tab}_NPCs", true).FirstOrDefault()!);
    }

    private void CB_SkillBro_MenuClose_CheckedChanged(object sender, EventArgs e)
    {
        var c = CB_SkillBro_MenuClose.Checked;
        MainWindow.SetControlEnabledState(c, CB_SkillBro_MenuClose_Direction, L_SkillBro_NPCs, TB_SkillBro_NPCs);
        MainWindow.SetMenuClose(c);
    }

    private void DGV_Results_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        var index = e.RowIndex;
        if (Frames.Count <= index) return;
        var row = DGV_Results.Rows[index];
        var result = Frames[index];
        int[] results =
        [
            result.GoldBottleCap,
            result.BottleCap,
            result.NormalGem,
            result.StickyBarb,
            result.LightClay,
            result.LaggingTail,
            result.IronBall,
            result.MetalCoat,
            result.IceStone,
            result.DawnStone,
            result.DuskStone,
            result.ShinyStone,
            result.MoonStone,
            result.SunStone,
            result.FossilizedFish,
            result.FossilizedDrake,
            result.FossilizedDino,
            result.FossilizedBird,
            result.WishingPiece,
            result.CometShard,
            result.RareBone,
        ];

        row.DefaultCellStyle.BackColor = result.Total switch
        {
            >= 10 => Color.LightCyan,
            >= 7 => Color.PapayaWhip,
            _ => row.Index % 2 == 0 ? Color.White : Color.WhiteSmoke,
        };
        const int first = 4;
        for (var i = 0; i < results.Length; i++)
        {
            if (results[i] > 0)
            {
                row.Cells[first + i].Style.Font = MainWindow.BoldFont;
            }
            else
            {
                row.Cells[first + i].Style.ForeColor = row.DefaultCellStyle.ForeColor;
                row.Cells[first + i].Style.Font = row.DefaultCellStyle.Font;
            }
        }
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
        MainWindow.SetCheckBoxCheckedState(check, CB_SkillBro_MenuClose);
    }
    public void SetMenuCloseDirection(bool check)
    {
        MainWindow.SetCheckBoxCheckedState(check, CB_SkillBro_MenuClose_Direction);
    }

    private void CB_SkillBro_MenuClose_Direction_CheckedChanged(object sender, EventArgs e)
    {
        MainWindow.SetMenuCloseDirection(CB_SkillBro_MenuClose_Direction.Checked);
    }

    private void L_Filter_Click(object sender, EventArgs e)
    {
        string name = ((Label)sender).Name.Replace("L_", "NUD_");
        var nud = (NumericUpDown)Controls.Find(name, true).FirstOrDefault()!;
        nud.Value = 0;
    }
}
