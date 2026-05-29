using owoow.Core;
using owoow.Core.Connection;
using owoow.Core.Structures;
using PKHeX.Core;

namespace owoow.WinForms.Subforms;

public partial class DexRecSearcher : Form
{
    readonly MainWindow MainWindow;
    readonly ConnectionWrapperAsync ConnectionWrapper;
    bool stop = false;

    public DexRecSearcher(MainWindow f, ConnectionWrapperAsync c)
    {
        InitializeComponent();

        MainWindow = f;
        ConnectionWrapper = c;

        MainWindow.SetTextBoxText(string.Empty, TB_Map, TB_S1, TB_S2, TB_S3, TB_S4, TB_Seed);

        CB_Target.Items.Clear();
        var list = Encounters.GetDexRecOptions(false);
        foreach (var item in list) CB_Target.Items.Add(item);
        CB_Target.SelectedIndex = 0;

        CB_IgnoreMap.Items.Clear();
        CB_IgnoreMap.Items.Add("(None)");
        foreach (var item in Encounters.Zones)
        {
            var val = item.Value;
            if (
                val.Contains("Surfing") ||
                val.Contains("Sharpedo") ||
                val.Contains("Flying") ||
                val.Contains("Ground") ||
                val.Contains("Puddle") ||
                val.Contains("Garbage") ||
                val.Contains("(2)") ||
                val.Contains("(3)") ||
                val.Contains("High Level") ||
                val.Contains("Beach") ||
                val.Contains("Lunatone")
                )
            {
                continue;
            }

            if (CB_IgnoreMap.Items.Contains(val)) continue;
            CB_IgnoreMap.Items.Add(val);
        }
        CB_IgnoreMap.SelectedIndex = 0;

        if (ConnectionWrapper.Connected)
        {
            try
            {
                Task.Run(
                    async () =>
                    {
                        MainWindow.readPause = true;
                        await Task.Delay(100, MainWindow.Source.Token);
                        var dr = await ConnectionWrapper.ReadDexRecommendationFull(MainWindow.Source.Token);
                        SetDexRecDetails(dr);
                        MainWindow.readPause = false;
                    }
                );
            }
            catch
            {
                // Ignored
            }
        }
    }

    private void SetDexRecDetails(PokedexRecommendation dr)
    {
        MainWindow.SetTextBoxText(dr.Location, TB_Map);
        MainWindow.SetTextBoxText($"{dr.Seed:X16}", TB_Seed);
        MainWindow.SetTextBoxText(Core.RNG.Util.GetDexRecommendation(dr.Species1), TB_S1);
        MainWindow.SetTextBoxText(Core.RNG.Util.GetDexRecommendation(dr.Species2), TB_S2);
        MainWindow.SetTextBoxText(Core.RNG.Util.GetDexRecommendation(dr.Species3), TB_S3);
        MainWindow.SetTextBoxText(Core.RNG.Util.GetDexRecommendation(dr.Species4), TB_S4);
    }

    private void CB_SpecificSlot_CheckedChanged(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(CB_SpecificSlot.GetIsChecked(), RB_S1, RB_S2, RB_S3, RB_S4);
    }

    private int GetSpecificSlot()
    {
        if (RB_S4.GetIsChecked()) return 4;
        if (RB_S3.GetIsChecked()) return 3;
        if (RB_S2.GetIsChecked()) return 2;
        else return 1;
    }

    private void B_Search_Click(object sender, EventArgs e)
    {
        Task.Run(async () =>
        {
            try
            {
                MainWindow.readPause = true;
                await Task.Delay(100, MainWindow.Source.Token);
                MainWindow.SetControlEnabledState(false, B_Search);
                MainWindow.SetControlEnabledState(true, B_Cancel);
                stop = false;
                bool first = true;
                while (!stop)
                {
                    MainWindow.readPause = true;
                    await ConnectionWrapper.OpenPokedex(first, MainWindow.Source.Token);
                    var dr = await ConnectionWrapper.ReadDexRecommendationFull(MainWindow.Source.Token);
                    MainWindow.readPause = false;
                    var target = (ushort)Core.RNG.Util.GetDexRecommendation(CB_Target.GetText());
                    var slotSpecified = CB_SpecificSlot.GetIsChecked();
                    var slot = GetSpecificSlot();
                    var exclArea = CB_IgnoreMap.GetText();

                    SetDexRecDetails(dr);
                    var found = true;
                    if (exclArea != dr.Location)
                    {
                        if (slotSpecified)
                        {
                            switch (slot)
                            {
                                case 4:
                                    if (dr.Species4 != target) found = false;
                                    break;
                                case 3:
                                    if (dr.Species3 != target) found = false;
                                    break;
                                case 2:
                                    if (dr.Species2 != target) found = false;
                                    break;
                                case 1:
                                    if (dr.Species1 != target) found = false;
                                    break;
                            }
                        }
                        else
                        {
                            if (dr.Species1 != target && dr.Species2 != target && dr.Species3 != target && dr.Species4 != target) found = false;
                        }
                        if (found)
                        {
                            this.DisplayMessageBox("Matching Pokedex Recommendation Found!", "Pokedex Recommendatino Search Result");
                            break;
                        }
                    }
                    await MainWindow.AdvanceDate(1);
                    await ConnectionWrapper.ClosePokedexAndSave(MainWindow.Source.Token);
                }
                MainWindow.SetControlEnabledState(true, B_Search);
                MainWindow.SetControlEnabledState(false, B_Cancel);
                MainWindow.readPause = false;
            }
            catch (Exception ex)
            {
                MainWindow.readPause = false;
                MainWindow.SetControlEnabledState(true, B_Search);
                MainWindow.SetControlEnabledState(false, B_Cancel);
                this.DisplayMessageBox(ex.Message);
            }
        });
    }

    private void B_Cancel_Click(object sender, EventArgs e)
    {
        stop = true;
    }
}
