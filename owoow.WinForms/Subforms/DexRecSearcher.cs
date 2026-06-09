using owoow.Core;
using owoow.Core.Connection;
using owoow.Core.Structures;
using System.Text.Json;
using static owoow.Core.RNG.Util;

namespace owoow.WinForms.Subforms;

public partial class DexRecSearcher : Form
{
    readonly MainWindow MainWindow;
    readonly ConnectionWrapperAsync ConnectionWrapper;
    private bool stop = false;
    private readonly string text;

    public bool SubformOpen = false;
    public List<string> Maps = [];

    CancellationTokenSource Source = new();

    public DexRecSearcher(MainWindow f, ConnectionWrapperAsync c)
    {
        InitializeComponent();

        MainWindow = f;
        ConnectionWrapper = c;

        text = Text;

        var mapsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ignore-locations-list.json");
        if (File.Exists(mapsPath))
            Maps = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(mapsPath)) ?? [];

        L_IgnoredMaps.Text = $"Excluded Maps: {Maps.Count}";

        MainWindow.SetControlText(string.Empty, TB_Map, TB_S1, TB_S2, TB_S3, TB_S4, TB_Seed);

        CB_Target.Items.Clear();
        var list = Encounters.GetDexRecOptions(false);
        foreach (var item in list) CB_Target.Items.Add(item);
        CB_Target.SelectedIndex = 0;

        if (ConnectionWrapper.Connected)
        {
            try
            {
                Task.Run(
                    async () =>
                    {
                        MainWindow.readPause = true;
                        await Task.Delay(100, Source.Token);
                        var dr = await ConnectionWrapper.ReadDexRecommendationFull(Source.Token);
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
        MainWindow.SetControlText(dr.Location, TB_Map);
        MainWindow.SetControlText($"{dr.Seed:X16}", TB_Seed);
        MainWindow.SetControlText(GetDexRecommendation(dr.Species1), TB_S1);
        MainWindow.SetControlText(GetDexRecommendation(dr.Species2), TB_S2);
        MainWindow.SetControlText(GetDexRecommendation(dr.Species3), TB_S3);
        MainWindow.SetControlText(GetDexRecommendation(dr.Species4), TB_S4);

        MainWindow.SetComboBoxSelectedIndex(MainWindow.CB_DexRec1.Items.IndexOf(GetDexRecommendation(dr.Species1)), MainWindow.CB_DexRec1);
        MainWindow.SetComboBoxSelectedIndex(MainWindow.CB_DexRec2.Items.IndexOf(GetDexRecommendation(dr.Species2)), MainWindow.CB_DexRec2);
        MainWindow.SetComboBoxSelectedIndex(MainWindow.CB_DexRec3.Items.IndexOf(GetDexRecommendation(dr.Species3)), MainWindow.CB_DexRec3);
        MainWindow.SetComboBoxSelectedIndex(MainWindow.CB_DexRec4.Items.IndexOf(GetDexRecommendation(dr.Species4)), MainWindow.CB_DexRec4);
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
                await Task.Delay(100, Source.Token);
                MainWindow.SetControlEnabledState(false, B_Search);
                MainWindow.SetControlEnabledState(true, B_Cancel);
                var tick = await ConnectionWrapper.GetCurrentTime(Source.Token).ConfigureAwait(false);
                stop = false;
                bool first = true;
                while (!stop)
                {
                    MainWindow.readPause = true;
                    MainWindow.SetControlText(text + " - Opening Pokédex...", this);
                    await ConnectionWrapper.OpenPokedex(first, Source.Token).ConfigureAwait(false);
                    MainWindow.SetControlText(text + " - Reading Pokédex Recommendation...", this);
                    var dr = await ConnectionWrapper.ReadDexRecommendationFull(Source.Token).ConfigureAwait(false);
                    MainWindow.readPause = false;
                    var target = (ushort)GetDexRecommendation(CB_Target.GetText());
                    var slotSpecified = CB_SpecificSlot.GetIsChecked();
                    var slot = GetSpecificSlot();

                    SetDexRecDetails(dr);
                    var found = true;
                    if (!Maps.Contains(dr.Location))
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
                            this.DisplayMessageBox("Matching Pokédex Recommendation Found!", "Pokédex Recommendation Search Result");
                            break;
                        }
                    }

                    MainWindow.SetControlText(text + " - Closing Pokédex and saving the game...", this);
                    await ConnectionWrapper.ClosePokedexAndSave(Source.Token).ConfigureAwait(false);

                    MainWindow.SetControlText(text + " - Advancing date...", this);
                    await MainWindow.AdvanceDate(1).ConfigureAwait(false);
                    if (CB_Date.GetIsChecked())
                    {
                        MainWindow.SetControlText(text + " - Resetting date...", this);
                        await ConnectionWrapper.SetCurrentTime(tick, Source.Token).ConfigureAwait(false);
                    }
                    first = false;
                }
                MainWindow.SetControlText(text, this);
                MainWindow.SetControlEnabledState(true, B_Search);
                MainWindow.SetControlEnabledState(false, B_Cancel);
                MainWindow.readPause = false;
            }
            catch (Exception ex)
            {
                MainWindow.SetControlText(text, this);
                MainWindow.readPause = false;
                MainWindow.SetControlEnabledState(true, B_Search);
                MainWindow.SetControlEnabledState(false, B_Cancel);
                if (ex is not TaskCanceledException) this.DisplayMessageBox(ex.Message);
            }
        });
    }

    private void B_Cancel_Click(object sender, EventArgs e)
    {
        stop = true;
        Source.Cancel();
        Source.Dispose();
        Source = new();
    }

    DexRecLocationList? _drl;
    private void B_ManageMaps_Click(object sender, EventArgs e)
    {
        if (!SubformOpen)
        {
            SubformOpen = true;
            _drl = new DexRecLocationList(ref Maps, this);
            _drl.Show();
        }
        else
        {
            _drl?.Focus();
        }
    }

    private void DexRecSearcher_FormClosing(object sender, FormClosingEventArgs e)
    {
        Source.Cancel();
        Source.Dispose();
        Source = new();
    }
}
