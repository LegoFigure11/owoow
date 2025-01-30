using owoow.Core;
using owoow.Core.Connection;
using owoow.Core.EncounterTable;
using owoow.Core.Interfaces;
using owoow.Core.RNG.Generators.Misc;
using owoow.Core.RNG.Generators.Overworld;
using owoow.WinForms.Subforms;
using PKHeX.Core;
using PKHeX.Drawing.Misc;
using PKHeX.Drawing.PokeSprite;
using SysBot.Base;
using System.Globalization;
using System.Text;
using static owoow.Core.Encounters;
using static owoow.Core.RNG.FilterUtil;
using static owoow.Core.RNG.Util;

namespace owoow.WinForms;

public partial class MainWindow : Form
{
    private static CancellationTokenSource Source = new();
    private static readonly CancellationTokenSource AdvanceSource = new();
    private static readonly Lock _connectLock = new();

    //private readonly ClientConfig Config;
    private ConnectionWrapperAsync ConnectionWrapper = default!;
    private readonly SwitchConnectionConfig ConnectionConfig;

    private readonly GameStrings Strings = GameInfo.GetStrings(1);

    private bool stop;
    private bool reset;
    private bool readPause = false;
    private bool skipPause = false;
    private long total;

    private List<OverworldFrame> Frames = [];

    private PK8? CachedEncounter;

    public MainWindow()
    {
        ConnectionConfig = new()
        {
            IP = "192.168.0.0", // Config.IP,
            Port = 6000, // protocol is SwitchProtocol.WiFi ? 6000 : Config.UsbPort,
            Protocol = SwitchProtocol.WiFi // Config.Protocol,
        };

        InitializeComponent();
    }

    private void MainWindow_Load(object sender, EventArgs e)
    {
        TB_SwitchIP.Text = "192.168.0.0";

        SetTextBoxText("0", TB_Seed0, TB_Seed1);
        SetTextBoxText(string.Empty, TB_CurrentAdvances, TB_AdvancesIncrease, TB_CurrentS0, TB_CurrentS1, TB_Wild);
        SetComboBoxSelectedIndex(0, CB_Filter_Shiny, CB_Filter_Mark, CB_Filter_Aura, CB_Filter_Height, CB_Game);

        TB_Status.Text = "Not Connected.";
        SetAreaOptions();

        SetDexRecOptions();
    }

    #region Connection
    private void Connect(CancellationToken token)
    {
        Task.Run(
            async () =>
            {
                SetControlEnabledState(false, B_Connect);
                try
                {
                    ConnectionConfig.IP = TB_SwitchIP.Text;
                    (bool success, string err) = await ConnectionWrapper
                        .Connect(token)
                        .ConfigureAwait(false);
                    if (!success)
                    {
                        SetControlEnabledState(true, B_Connect);
                        this.DisplayMessageBox(err);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    SetControlEnabledState(true, B_Connect);
                    this.DisplayMessageBox(ex.Message);
                    return;
                }

                UpdateStatus("Detecting game version...");
                string id = await ConnectionWrapper.Connection
                    .GetTitleID(token)
                    .ConfigureAwait(false);
                var game = id switch
                {
                    Offsets.SwordID => "Sword",
                    Offsets.ShieldID => "Shield",
                    _ => "",
                };

                if (game is "")
                {
                    try
                    {
                        (bool success, string err) = await ConnectionWrapper
                            .DisconnectAsync(token)
                            .ConfigureAwait(false);
                        if (!success)
                        {
                            SetControlEnabledState(true, B_Connect);
                            this.DisplayMessageBox(err);
                            return;
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                    finally
                    {
                        SetControlEnabledState(true, B_Connect);
                        this.DisplayMessageBox("Unable to detect Pokémon Sword or Pokémon Shield running on your Switch!");
                    }
                    return;
                }

                SetCheckBoxCheckedState(ConnectionWrapper.GetHasShinyCharm(), CB_ShinyCharm);
                SetCheckBoxCheckedState(ConnectionWrapper.GetHasMarkCharm(), CB_MarkCharm);
                var (tid, sid) = ConnectionWrapper.GetIDs();
                SetTextBoxText(tid, TB_TID);
                SetTextBoxText(sid, TB_SID);

                SetComboBoxSelectedIndex(game == "Sword" ? 0 : 1, CB_Game);

                UpdateStatus("Reading Pokédex Recommendations...");
                try
                {
                    var DexRec = await ConnectionWrapper.ReadDexRecommendation(token).ConfigureAwait(false);
                    SetComboBoxSelectedIndex(CB_DexRec1.Items.IndexOf(GetDexRecommendation(DexRec[0])), CB_DexRec1);
                    SetComboBoxSelectedIndex(CB_DexRec2.Items.IndexOf(GetDexRecommendation(DexRec[1])), CB_DexRec2);
                    SetComboBoxSelectedIndex(CB_DexRec3.Items.IndexOf(GetDexRecommendation(DexRec[2])), CB_DexRec3);
                    SetComboBoxSelectedIndex(CB_DexRec4.Items.IndexOf(GetDexRecommendation(DexRec[3])), CB_DexRec4);
                }
                catch (Exception ex)
                {
                    this.DisplayMessageBox($"Error occurred while reading Pokédex Recommendations: {ex.Message}");
                    return;
                }

                UpdateStatus("Reading RNG State...");
                ulong _s0, _s1;
                try
                {
                    (_s0, _s1) = await ConnectionWrapper.ReadRNGState(token).ConfigureAwait(false);
                    SetTextBoxText($"{_s0:X16}", TB_Seed0, TB_CurrentS0);
                    SetTextBoxText($"{_s1:X16}", TB_Seed1, TB_CurrentS1);
                    SetTextBoxText("0", TB_CurrentAdvances, TB_AdvancesIncrease);

                }
                catch (Exception ex)
                {
                    this.DisplayMessageBox($"Error occurred while reading initial RNG state: {ex.Message}");
                    return;
                }

                SetControlEnabledState(true, B_Disconnect, B_CopyToInitial, B_ReadEncounter, B_RefreshDexRec, GB_SwitchControls);

                UpdateStatus("Monitoring RNG State...");
                try
                {
                    total = 0;
                    stop = false;
                    while (!stop)
                    {
                        if (ConnectionWrapper.Connected && !readPause)
                        {
                            var (s0, s1) = await ConnectionWrapper.ReadRNGState(token).ConfigureAwait(false);
                            var adv = GetAdvancesPassed(_s0, _s1, s0, s1);
                            if (adv > 0)
                            {
                                if (reset)
                                {
                                    total = 0;
                                    reset = false;
                                }
                                else
                                {
                                    total += adv;
                                }

                                _s0 = s0;
                                _s1 = s1;

                                SetTextBoxText($"{_s0:X16}", TB_CurrentS0);
                                SetTextBoxText($"{_s1:X16}", TB_CurrentS1);
                                SetTextBoxText($"{total:N0}", TB_CurrentAdvances);
                                SetTextBoxText($"{adv:N0}", TB_AdvancesIncrease);
                            }
                        }
                    }
                }
                catch
                {
                    // Ignored
                }
            },
            token
        );
    }

    private void Disconnect(CancellationToken token)
    {
        Task.Run(
            async () =>
            {
                SetControlEnabledState(false, B_Disconnect);
                stop = true;
                try
                {
                    (bool success, string err) = await ConnectionWrapper.DisconnectAsync(token).ConfigureAwait(false);
                    if (!success) this.DisplayMessageBox(err);
                }
                catch (Exception ex)
                {
                    this.DisplayMessageBox(ex.Message);
                }
                await Source.CancelAsync();
                Source = new();
                SetControlEnabledState(true, B_Connect);
            },
            token
        );
    }

    private void B_NTP_Click(object sender, EventArgs e)
    {
        Task.Run(
            async () =>
            {
                try
                {
                    await ConnectionWrapper.ResetTimeNTP(Source.Token);
                }
                catch (Exception ex)
                {
                    this.DisplayMessageBox(ex.Message);
                }
            }
        );
    }

    private void B_ResetStick_Click(object sender, EventArgs e)
    {
        Task.Run(
            async () =>
            {
                try
                {
                    await ConnectionWrapper.ResetStick(Source.Token);
                }
                catch (Exception ex)
                {
                    this.DisplayMessageBox(ex.Message);
                }
            }
        );
    }

    private void B_HoldUp_Click(object sender, EventArgs e)
    {
        Task.Run(
            async () =>
            {
                try
                {
                    await ConnectionWrapper.HoldUp(Source.Token);
                }
                catch (Exception ex)
                {
                    this.DisplayMessageBox(ex.Message);
                }
            }
        );
    }

    private void B_SkipAdvance_Click(object sender, EventArgs e)
    {
        Task.Run(
            async () =>
            {
                try
                {
                    SetControlEnabledState(false, B_SkipAdvance, B_SkipForward, B_SkipBack, B_HoldUp, B_ResetStick);
                    SetControlEnabledState(true, B_CancelSkip);
                    skipPause = false;
                    var skips = uint.Parse(TB_Skips.Text);
                    for (var i = 0; i < skips && !skipPause; i++)
                    {

                        SetButtonText($"{i + 1}", B_SkipAdvance);
                        await ConnectionWrapper.PressL3(AdvanceSource.Token).ConfigureAwait(false);
                        await Task.Delay(150, AdvanceSource.Token);
                    }
                    SetButtonText("Adv.", B_SkipAdvance);
                    SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_HoldUp, B_ResetStick);
                    SetControlEnabledState(false, B_CancelSkip);
                }
                catch (Exception ex)
                {
                    SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_HoldUp, B_ResetStick);
                    SetControlEnabledState(false, B_CancelSkip);
                    this.DisplayMessageBox(ex.Message);
                }
            }
        );
    }

    private void B_CancelSkip_Click(object sender, EventArgs e)
    {
        skipPause = true;
    }

    private void B_SkipForward_Click(object sender, EventArgs e)
    {
        Task.Run(
            async () =>
            {
                try
                {
                    SetControlEnabledState(false, B_SkipAdvance, B_SkipForward, B_SkipBack, B_HoldUp, B_ResetStick);
                    SetControlEnabledState(true, B_CancelSkip);
                    skipPause = false;
                    var skips = uint.Parse(TB_Skips.Text);
                    for (var i = 0; i < skips && !skipPause; i++)
                    {

                        SetButtonText($"{i + 1}", B_SkipForward);
                        await ConnectionWrapper.DaySkip(AdvanceSource.Token).ConfigureAwait(false);
                        await Task.Delay(360, AdvanceSource.Token);
                    }
                    SetButtonText("Days+", B_SkipForward);
                    SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_HoldUp, B_ResetStick);
                    SetControlEnabledState(false, B_CancelSkip);
                }
                catch (Exception ex)
                {
                    SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_HoldUp, B_ResetStick);
                    SetControlEnabledState(false, B_CancelSkip);
                    this.DisplayMessageBox(ex.Message);
                }
            }
        );
    }

    private void B_SkipBack_Click(object sender, EventArgs e)
    {
        Task.Run(
            async () =>
            {
                try
                {
                    SetControlEnabledState(false, B_SkipAdvance, B_SkipForward, B_SkipBack, B_HoldUp, B_ResetStick);
                    SetControlEnabledState(true, B_CancelSkip);
                    skipPause = false;
                    var skips = uint.Parse(TB_Skips.Text);
                    for (var i = 0; i < skips && !skipPause; i++)
                    {

                        SetButtonText($"{i + 1}", B_SkipBack);
                        await ConnectionWrapper.DaySkipBack(AdvanceSource.Token).ConfigureAwait(false);
                        await Task.Delay(360, AdvanceSource.Token);
                    }
                    SetButtonText("Days-", B_SkipBack);
                    SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_HoldUp, B_ResetStick);
                    SetControlEnabledState(false, B_CancelSkip);
                }
                catch (Exception ex)
                {
                    SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_HoldUp, B_ResetStick);
                    SetControlEnabledState(false, B_CancelSkip);
                    this.DisplayMessageBox(ex.Message);
                }
            }
        );
    }
    #endregion

    #region Searchers

    private void B_Symbol_Search_Click(object sender, EventArgs e)
    {
        SetControlEnabledState(false, sender);

        var table = new EncounterTable(CB_Game.Text, "Symbol", CB_Symbol_Area.Text, CB_Symbol_Weather.Text, CB_Symbol_LeadAbility.Text);

        var initial = ulong.Parse(TB_Symbol_Initial.Text);
        var advances = ulong.Parse(TB_Symbol_Advances.Text);

        var numTasks = (byte)(advances < 1_000 ? 1 : advances < 50_000 ? 2 : 4);
        var interval = advances / numTasks;

        var s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
        var s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);

        Core.RNG.GeneratorConfig config = new()
        {
            TargetSpecies = CB_Symbol_Species.Text,
            LeadAbility = CB_Symbol_LeadAbility.Text,

            Weather = GetWeatherType($"{CB_Symbol_Weather.SelectedItem}"),

            ShinyRolls = CB_ShinyCharm.Checked ? 3 : 1,
            MarkRolls = CB_MarkCharm.Checked ? 3 : 1,

            TargetShiny = GetFilterShinyType(CB_Filter_Shiny.SelectedIndex),
            TargetAura = GetFilterAuraType(CB_Filter_Aura.SelectedIndex),
            TargetMark = GetFilterMarkype(CB_Filter_Mark.SelectedIndex),
            TargetScale = GetFilterScaleType(CB_Filter_Height.SelectedIndex),

            TargetMinIVs = [(uint)NUD_HP_Min.Value, (uint)NUD_Atk_Min.Value, (uint)NUD_Def_Min.Value, (uint)NUD_SpA_Min.Value, (uint)NUD_SpD_Min.Value, (uint)NUD_Spe_Min.Value],
            TargetMaxIVs = [(uint)NUD_HP_Max.Value, (uint)NUD_Atk_Max.Value, (uint)NUD_Def_Max.Value, (uint)NUD_SpA_Max.Value, (uint)NUD_SpD_Max.Value, (uint)NUD_Spe_Max.Value],

            AuraKOs = int.Parse(TB_Symbol_KOs.Text),

            DexRecSlots =
            [
                GetDexRecommendation(CB_DexRec1.Text),
                GetDexRecommendation(CB_DexRec2.Text),
                GetDexRecommendation(CB_DexRec3.Text),
                GetDexRecommendation(CB_DexRec4.Text)
            ],

            ConsiderMenuClose = CB_Symbol_MenuClose.Checked,
            MenuCloseIsHoldingDirection = CB_Symbol_MenuClose_Direction.Checked,
            MenuCloseNPCs = uint.Parse(TB_Symbol_NPCs.Text),

            ConsiderFly = CB_ConsiderFlying.Checked,
            AreaLoadAdvances = (uint)NUD_AreaLoad.Value,
            AreaLoadNPCs = (uint)NUD_FlyNPCs.Value,
            ConsiderRain = CB_ConsiderRain.Checked,
            RainTicksAreaLoad = CB_ConsiderFlying.Checked && CB_ConsiderRain.Checked ? (uint)NUD_RainTick.Value : 0,
            RainTicksEncounter = CB_ConsiderFlying.Checked && CB_ConsiderRain.Checked ? 0 : (uint)NUD_RainTick.Value,

            FiltersEnabled = CB_EnableFilters.Checked,

            TID = uint.Parse(TB_TID.Text),
            SID = uint.Parse(TB_SID.Text),
        };

        var rng = new Xoroshiro128Plus(s0, s1);

        for (ulong i = 0; i < initial; i++) rng.Next();

        List<OverworldFrame>[] results = [];

        List<Task<List<OverworldFrame>>> tasks = [];
        for (byte i = 0; i < numTasks; i++)
        {
            var last = i == numTasks - 1;

            var (_s0, _s1) = rng.GetState();
            var start = initial + (i * interval);
            var end = initial + (interval * (i + (uint)1)) - 1;

            if (last) end += advances % interval;

            tasks.Add(Symbol.Generate(_s0, _s1, table, start, end, config));

            if (!last)
            {
                for (ulong j = 0; j < interval; j++)
                {
                    rng.Next();
                }
            }
        }

        Task.Run(async () =>
        {
            results = await Task.WhenAll(tasks);
            List<OverworldFrame> AllResults = [];
            foreach (var result in results)
            {
                AllResults.AddRange(result);
            }

            Frames = AllResults;
            SetBindingSourceDataSource(AllResults, ResultsSource);
            DGV_Results.SanitizeColumns(this);

            SetControlEnabledState(true, sender);
        });
    }

    private void B_Hidden_Search_Click(object sender, EventArgs e)
    {
        SetControlEnabledState(false, sender);

        var table = new EncounterTable(CB_Game.Text, "Hidden", CB_Hidden_Area.Text, CB_Hidden_Weather.Text, CB_Hidden_LeadAbility.Text);

        var initial = ulong.Parse(TB_Hidden_Initial.Text);
        var advances = ulong.Parse(TB_Hidden_Advances.Text);

        var numTasks = (byte)(advances < 1_000 ? 1 : advances < 50_000 ? 2 : 4);
        var interval = advances / numTasks;

        var s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
        var s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);

        Core.RNG.GeneratorConfig config = new()
        {
            TargetSpecies = CB_Hidden_Species.Text,
            LeadAbility = CB_Hidden_LeadAbility.Text,

            Weather = GetWeatherType($"{CB_Hidden_Weather.SelectedItem}"),

            ShinyRolls = CB_ShinyCharm.Checked ? 3 : 1,
            MarkRolls = CB_MarkCharm.Checked ? 3 : 1,

            TargetShiny = GetFilterShinyType(CB_Filter_Shiny.SelectedIndex),
            TargetMark = GetFilterMarkype(CB_Filter_Mark.SelectedIndex),
            TargetScale = GetFilterScaleType(CB_Filter_Height.SelectedIndex),

            TargetMinIVs = [(uint)NUD_HP_Min.Value, (uint)NUD_Atk_Min.Value, (uint)NUD_Def_Min.Value, (uint)NUD_SpA_Min.Value, (uint)NUD_SpD_Min.Value, (uint)NUD_Spe_Min.Value],
            TargetMaxIVs = [(uint)NUD_HP_Max.Value, (uint)NUD_Atk_Max.Value, (uint)NUD_Def_Max.Value, (uint)NUD_SpA_Max.Value, (uint)NUD_SpD_Max.Value, (uint)NUD_Spe_Max.Value],

            DexRecSlots =
            [
                GetDexRecommendation(CB_DexRec1.Text),
                GetDexRecommendation(CB_DexRec2.Text),
                GetDexRecommendation(CB_DexRec3.Text),
                GetDexRecommendation(CB_DexRec4.Text)
            ],

            ConsiderMenuClose = CB_Hidden_MenuClose.Checked,
            MenuCloseIsHoldingDirection = CB_Hidden_MenuClose_Direction.Checked,
            MenuCloseNPCs = uint.Parse(TB_Hidden_NPCs.Text),

            ConsiderFly = CB_ConsiderFlying.Checked,
            AreaLoadAdvances = (uint)NUD_AreaLoad.Value,
            AreaLoadNPCs = (uint)NUD_FlyNPCs.Value,
            ConsiderRain = CB_ConsiderRain.Checked,
            RainTicksAreaLoad = CB_ConsiderFlying.Checked && CB_ConsiderRain.Checked ? (uint)NUD_RainTick.Value : 0,
            RainTicksEncounter = CB_ConsiderFlying.Checked && CB_ConsiderRain.Checked ? 0 : (uint)NUD_RainTick.Value,

            FiltersEnabled = CB_EnableFilters.Checked,

            TID = uint.Parse(TB_TID.Text),
            SID = uint.Parse(TB_SID.Text),
        };

        var rng = new Xoroshiro128Plus(s0, s1);

        for (ulong i = 0; i < initial; i++) rng.Next();

        List<OverworldFrame>[] results = [];

        List<Task<List<OverworldFrame>>> tasks = [];
        for (byte i = 0; i < numTasks; i++)
        {
            var last = i == numTasks - 1;

            var (_s0, _s1) = rng.GetState();
            var start = initial + (i * interval);
            var end = initial + (interval * (i + (uint)1)) - 1;

            if (last) end += advances % interval;

            tasks.Add(Hidden.Generate(_s0, _s1, table, start, end, config));

            if (!last)
            {
                for (ulong j = 0; j < interval; j++)
                {
                    rng.Next();
                }
            }
        }

        Task.Run(async () =>
        {
            results = await Task.WhenAll(tasks);
            List<OverworldFrame> AllResults = [];
            foreach (var result in results)
            {
                AllResults.AddRange(result);
            }

            Frames = AllResults;
            SetBindingSourceDataSource(AllResults, ResultsSource);
            DGV_Results.SanitizeColumns(this);

            SetControlEnabledState(true, sender);
        });
    }

    private void B_Static_Search_Click(object sender, EventArgs e)
    {
        SetControlEnabledState(false, sender);

        var table = new EncounterTable(CB_Game.Text, "Static", CB_Static_Area.Text, CB_Static_Weather.Text, CB_Static_LeadAbility.Text);

        var initial = ulong.Parse(TB_Static_Initial.Text);
        var advances = ulong.Parse(TB_Static_Advances.Text);

        var numTasks = (byte)(advances < 1_000 ? 1 : advances < 50_000 ? 2 : 4);
        var interval = advances / numTasks;

        var s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
        var s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);

        Core.RNG.GeneratorConfig config = new()
        {
            TargetSpecies = CB_Static_Species.Text,
            LeadAbility = CB_Static_LeadAbility.Text,

            Weather = GetWeatherType($"{CB_Static_Weather.SelectedItem}"),

            ShinyRolls = CB_ShinyCharm.Checked ? 3 : 1,
            MarkRolls = CB_MarkCharm.Checked ? 3 : 1,

            TargetShiny = GetFilterShinyType(CB_Filter_Shiny.SelectedIndex),
            TargetMark = GetFilterMarkype(CB_Filter_Mark.SelectedIndex),
            TargetScale = GetFilterScaleType(CB_Filter_Height.SelectedIndex),

            TargetMinIVs = [(uint)NUD_HP_Min.Value, (uint)NUD_Atk_Min.Value, (uint)NUD_Def_Min.Value, (uint)NUD_SpA_Min.Value, (uint)NUD_SpD_Min.Value, (uint)NUD_Spe_Min.Value],
            TargetMaxIVs = [(uint)NUD_HP_Max.Value, (uint)NUD_Atk_Max.Value, (uint)NUD_Def_Max.Value, (uint)NUD_SpA_Max.Value, (uint)NUD_SpD_Max.Value, (uint)NUD_Spe_Max.Value],

            ConsiderMenuClose = CB_Static_MenuClose.Checked,
            MenuCloseIsHoldingDirection = CB_Static_MenuClose_Direction.Checked,
            MenuCloseNPCs = uint.Parse(TB_Static_NPCs.Text),

            ConsiderFly = CB_ConsiderFlying.Checked,
            AreaLoadAdvances = (uint)NUD_AreaLoad.Value,
            AreaLoadNPCs = (uint)NUD_FlyNPCs.Value,
            ConsiderRain = CB_ConsiderRain.Checked,
            RainTicksAreaLoad = CB_ConsiderFlying.Checked && CB_ConsiderRain.Checked ? (uint)NUD_RainTick.Value : 0,
            RainTicksEncounter = CB_ConsiderFlying.Checked && CB_ConsiderRain.Checked ? 0 : (uint)NUD_RainTick.Value,

            FiltersEnabled = CB_EnableFilters.Checked,

            TID = uint.Parse(TB_TID.Text),
            SID = uint.Parse(TB_SID.Text),
        };

        var rng = new Xoroshiro128Plus(s0, s1);

        for (ulong i = 0; i < initial; i++) rng.Next();

        List<OverworldFrame>[] results = [];

        List<Task<List<OverworldFrame>>> tasks = [];
        for (byte i = 0; i < numTasks; i++)
        {
            var last = i == numTasks - 1;

            var (_s0, _s1) = rng.GetState();
            var start = initial + (i * interval);
            var end = initial + (interval * (i + (uint)1)) - 1;

            if (last) end += advances % interval;

            tasks.Add(Static.Generate(_s0, _s1, table, start, end, config));

            if (!last)
            {
                for (ulong j = 0; j < interval; j++)
                {
                    rng.Next();
                }
            }
        }

        Task.Run(async () =>
        {
            results = await Task.WhenAll(tasks);
            List<OverworldFrame> AllResults = [];
            foreach (var result in results)
            {
                AllResults.AddRange(result);
            }

            Frames = AllResults;
            SetBindingSourceDataSource(AllResults, ResultsSource);
            DGV_Results.SanitizeColumns(this);

            SetControlEnabledState(true, sender);
        });
    }

    private void B_Fishing_Search_Click(object sender, EventArgs e)
    {
        SetControlEnabledState(false, sender);

        var table = new EncounterTable(CB_Game.Text, "Fishing", CB_Fishing_Area.Text, CB_Fishing_Weather.Text, CB_Fishing_LeadAbility.Text);

        var initial = ulong.Parse(TB_Fishing_Initial.Text);
        var advances = ulong.Parse(TB_Fishing_Advances.Text);

        var numTasks = (byte)(advances < 1_000 ? 1 : advances < 50_000 ? 2 : 4);
        var interval = advances / numTasks;

        var s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
        var s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);

        Core.RNG.GeneratorConfig config = new()
        {
            TargetSpecies = CB_Fishing_Species.Text,
            LeadAbility = CB_Fishing_LeadAbility.Text,

            Weather = GetWeatherType($"{CB_Fishing_Weather.SelectedItem}"),

            ShinyRolls = CB_ShinyCharm.Checked ? 3 : 1,
            MarkRolls = CB_MarkCharm.Checked ? 3 : 1,

            TargetShiny = GetFilterShinyType(CB_Filter_Shiny.SelectedIndex),
            TargetAura = GetFilterAuraType(CB_Filter_Aura.SelectedIndex),
            TargetMark = GetFilterMarkype(CB_Filter_Mark.SelectedIndex),
            TargetScale = GetFilterScaleType(CB_Filter_Height.SelectedIndex),

            TargetMinIVs = [(uint)NUD_HP_Min.Value, (uint)NUD_Atk_Min.Value, (uint)NUD_Def_Min.Value, (uint)NUD_SpA_Min.Value, (uint)NUD_SpD_Min.Value, (uint)NUD_Spe_Min.Value],
            TargetMaxIVs = [(uint)NUD_HP_Max.Value, (uint)NUD_Atk_Max.Value, (uint)NUD_Def_Max.Value, (uint)NUD_SpA_Max.Value, (uint)NUD_SpD_Max.Value, (uint)NUD_Spe_Max.Value],

            AuraKOs = int.Parse(TB_Fishing_KOs.Text),

            DexRecSlots =
            [
                GetDexRecommendation(CB_DexRec1.Text),
                GetDexRecommendation(CB_DexRec2.Text),
                GetDexRecommendation(CB_DexRec3.Text),
                GetDexRecommendation(CB_DexRec4.Text)
            ],

            ConsiderMenuClose = CB_Fishing_MenuClose.Checked,
            MenuCloseIsHoldingDirection = CB_Fishing_MenuClose_Direction.Checked,
            MenuCloseNPCs = uint.Parse(TB_Fishing_NPCs.Text),

            ConsiderFly = CB_ConsiderFlying.Checked,
            AreaLoadAdvances = (uint)NUD_AreaLoad.Value,
            AreaLoadNPCs = (uint)NUD_FlyNPCs.Value,
            ConsiderRain = CB_ConsiderRain.Checked,
            RainTicksAreaLoad = CB_ConsiderFlying.Checked && CB_ConsiderRain.Checked ? (uint)NUD_RainTick.Value : 0,
            RainTicksEncounter = CB_ConsiderFlying.Checked && CB_ConsiderRain.Checked ? 0 : (uint)NUD_RainTick.Value,

            FiltersEnabled = CB_EnableFilters.Checked,

            TID = uint.Parse(TB_TID.Text),
            SID = uint.Parse(TB_SID.Text),
        };

        var rng = new Xoroshiro128Plus(s0, s1);

        for (ulong i = 0; i < initial; i++) rng.Next();

        List<OverworldFrame>[] results = [];

        List<Task<List<OverworldFrame>>> tasks = [];
        for (byte i = 0; i < numTasks; i++)
        {
            var last = i == numTasks - 1;

            var (_s0, _s1) = rng.GetState();
            var start = initial + (i * interval);
            var end = initial + (interval * (i + (uint)1)) - 1;

            if (last) end += advances % interval;

            tasks.Add(Fishing.Generate(_s0, _s1, table, start, end, config));

            if (!last)
            {
                for (ulong j = 0; j < interval; j++)
                {
                    rng.Next();
                }
            }
        }

        Task.Run(async () =>
        {
            results = await Task.WhenAll(tasks);
            List<OverworldFrame> AllResults = [];
            foreach (var result in results)
            {
                AllResults.AddRange(result);
            }

            Frames = AllResults;
            SetBindingSourceDataSource(AllResults, ResultsSource);
            DGV_Results.SanitizeColumns(this);

            SetControlEnabledState(true, sender);
        });
    }

    private void B_CalculateRain_Click(object sender, EventArgs e)
    {
        var tab = TC_EncounterType.SelectedTab;
        if (tab != null)
        {
            var type = tab.Text;

            var table = new EncounterTable(
                CB_Game.Text,
                type,
                ((ComboBox)Controls.Find($"CB_{type}_Area", true).FirstOrDefault()!).Text,
                ((ComboBox)Controls.Find($"CB_{type}_Weather", true).FirstOrDefault()!).Text,
                ((ComboBox)Controls.Find($"CB_{type}_LeadAbility", true).FirstOrDefault()!).Text
                );

            var initial = ulong.Parse(((TextBox)Controls.Find($"TB_{type}_Initial", true).FirstOrDefault()!).Text);
            var advances = 0;

            var s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
            var s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);

            Core.RNG.GeneratorConfig config = new()
            {
                TargetSpecies = ((ComboBox)Controls.Find($"CB_{type}_Species", true).FirstOrDefault()!).Text,
                LeadAbility = ((ComboBox)Controls.Find($"CB_{type}_LeadAbility", true).FirstOrDefault()!).Text,

                Weather = GetWeatherType(((ComboBox)Controls.Find($"CB_{type}_Weather", true).FirstOrDefault()!).Text),

                ShinyRolls = CB_ShinyCharm.Checked ? 3 : 1,
                MarkRolls = CB_MarkCharm.Checked ? 3 : 1,

                TargetShiny = GetFilterShinyType(CB_Filter_Shiny.SelectedIndex),
                TargetMark = GetFilterMarkype(CB_Filter_Mark.SelectedIndex),
                TargetScale = GetFilterScaleType(CB_Filter_Height.SelectedIndex),

                TargetMinIVs = [(uint)NUD_HP_Min.Value, (uint)NUD_Atk_Min.Value, (uint)NUD_Def_Min.Value, (uint)NUD_SpA_Min.Value, (uint)NUD_SpD_Min.Value, (uint)NUD_Spe_Min.Value],
                TargetMaxIVs = [(uint)NUD_HP_Max.Value, (uint)NUD_Atk_Max.Value, (uint)NUD_Def_Max.Value, (uint)NUD_SpA_Max.Value, (uint)NUD_SpD_Max.Value, (uint)NUD_Spe_Max.Value],

                ConsiderMenuClose = ((CheckBox)Controls.Find($"CB_{type}_MenuClose", true).FirstOrDefault()!).Checked,
                MenuCloseIsHoldingDirection = ((CheckBox)Controls.Find($"CB_{type}_MenuClose_Direction", true).FirstOrDefault()!).Checked,
                MenuCloseNPCs = uint.Parse(((TextBox)Controls.Find($"TB_{type}_NPCs", true).FirstOrDefault()!).Text),

                ConsiderFly = CB_ConsiderFlying.Checked,
                ConsiderRain = CB_ConsiderRain.Checked,
                AreaLoadAdvances = (uint)NUD_AreaLoad.Value,
                AreaLoadNPCs = (uint)NUD_FlyNPCs.Value,
                RainTicksAreaLoad = CB_ConsiderFlying.Checked && CB_ConsiderRain.Checked ? (uint)NUD_RainTick.Value : 0,
                RainTicksEncounter = CB_ConsiderFlying.Checked && CB_ConsiderRain.Checked ? 0 : (uint)NUD_RainTick.Value,

                FiltersEnabled = true,

                TID = uint.Parse(TB_TID.Text),
                SID = uint.Parse(TB_SID.Text),
            };

            var rng = new Xoroshiro128Plus(s0, s1);

            for (ulong i = 0; i < initial; i++) rng.Next();

            var (_s0, _s1) = rng.GetState();

            List<uint> ticks = [];

            List<OverworldFrame> results = [];

            int max = 300;

            for (uint i = 0; i < max; i++) // search next 300 ticks
            {
                config.RainTicksEncounter = i;
                results = type switch
                {
                    "Static" => Task.Run(() => Static.Generate(_s0, _s1, table, initial, initial + (ulong)advances, config)).Result,
                    "Symbol" => Task.Run(() => Symbol.Generate(_s0, _s1, table, initial, initial + (ulong)advances, config)).Result,
                    "Fishing" => Task.Run(() => Fishing.Generate(_s0, _s1, table, initial, initial + (ulong)advances, config)).Result,
                    _ => Task.Run(() => Hidden.Generate(_s0, _s1, table, initial, initial + (ulong)advances, config)).Result,
                };
                if (results.Count != 0)
                    ticks.Add(i);
            }

            if (ticks.Count == max)
            {
                MessageBox.Show("Whoops! Max number of results found, please set the IV filters to be stricter and try again.");
            }
            else if (ticks.Count != 0)
            {
                SetNUDValue(ticks[0], NUD_RainTick);
                MessageBox.Show($"Found {ticks.Count} result{(ticks.Count != 1 ? "s" : string.Empty)}: {string.Join(", ", ticks)}");
            }
            else
            {
                MessageBox.Show("No results found.");
            }
        }
    }
    #endregion

    #region UI Methods
    private void UpdateStatus(string status)
    {
        SetTextBoxText(status, TB_Status);
    }

    private void SetDexRecOptions()
    {
        if (Encounters.Personal is not null)
        {
            // Special handling to keep Jangmo-o, Hakamo-o, Kommo-o, and Porygon-Z. Silvally-10+ also need to be filtered out
            List<string> range = ["(None)"];
            range.AddRange(Encounters.Personal.Keys.Where(k => !((k[^2] == '-' || k[^3] == '-') && k[^1] != 'o' && k[^1] != 'Z')));

            CB_DexRec1.Items.Clear();
            CB_DexRec2.Items.Clear();
            CB_DexRec3.Items.Clear();
            CB_DexRec4.Items.Clear();

            foreach (var entry in range)
            {
                CB_DexRec1.Items.Add(entry);
                CB_DexRec2.Items.Add(entry);
                CB_DexRec3.Items.Add(entry);
                CB_DexRec4.Items.Add(entry);
            }

            CB_DexRec1.SelectedIndex = 0;
            CB_DexRec2.SelectedIndex = 0;
            CB_DexRec3.SelectedIndex = 0;
            CB_DexRec4.SelectedIndex = 0;
        }
    }

    private void SetAreaOptions()
    {
        var tab = TC_EncounterType.SelectedTab;
        if (tab != null)
        {
            if (Controls.Find($"CB_{tab.Text}_Area", true).FirstOrDefault() is ComboBox target)
            {
                target.Items.Clear();
                var areas = GetAreaList(CB_Game.Text, tab.Text).ToArray();
                Array.Sort(areas);
                foreach (var area in areas)
                {
                    target.Items.Add(area);
                }
                target.SelectedIndex = 0;
            }
        }
    }

    private void SetWeatherOptions()
    {
        var tab = TC_EncounterType.SelectedTab;
        if (tab != null)
        {
            if (Controls.Find($"CB_{tab.Text}_Weather", true).FirstOrDefault() is ComboBox target)
            {
                if (Controls.Find($"CB_{tab.Text}_Area", true).FirstOrDefault() is ComboBox area)
                {
                    target.Items.Clear();
                    var weathers = GetWeatherList(CB_Game.Text, tab.Text, $"{area.SelectedItem}").ToArray();
                    foreach (var weather in weathers)
                    {
                        target.Items.Add(weather);
                    }
                    target.SelectedIndex = 0;
                }
            }
        }
    }

    private void SetSpeciesOptions()
    {
        var tab = TC_EncounterType.SelectedTab;
        if (tab != null)
        {
            if (Controls.Find($"CB_{tab.Text}_Species", true).FirstOrDefault() is ComboBox target)
            {
                if (Controls.Find($"CB_{tab.Text}_Weather", true).FirstOrDefault() is ComboBox weather)
                {
                    if (Controls.Find($"CB_{tab.Text}_Area", true).FirstOrDefault() is ComboBox area)
                    {
                        target.Items.Clear();
                        var species = GetSpeciesList(CB_Game.Text, tab.Text, $"{area.SelectedItem}", $"{weather.SelectedItem}").ToArray();
                        Array.Sort(species);
                        foreach (var specie in species)
                        {
                            target.Items.Add(specie);
                        }
                        target.SelectedIndex = 0;
                    }
                }
            }
        }
    }

    private void B_Connect_Click(object sender, EventArgs e)
    {
        lock (_connectLock)
        {
            if (ConnectionWrapper is { Connected: true })
                return;

            ConnectionWrapper = new(ConnectionConfig, UpdateStatus);
            Connect(Source.Token);
        }
    }

    private void B_Disconnect_Click(object sender, EventArgs e)
    {
        lock (_connectLock)
        {
            if (ConnectionWrapper is not { Connected: true })
                return;

            Disconnect(Source.Token);
        }
    }

    private void B_CopyToInitial_Click(object sender, EventArgs e)
    {
        if (ModifierKeys == Keys.Shift)
        {
            Task.Run(
                async () =>
                {
                    try
                    {
                        ulong s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
                        ulong s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);
                        await ConnectionWrapper.WriteRNGState(s0, s1, Source.Token);
                        reset = true;
                    }
                    catch (Exception ex)
                    {
                        this.DisplayMessageBox(ex.Message);
                    }
                }
            );
        }
        else
        {
            SetTextBoxText(TB_CurrentS0.Text, TB_Seed0);
            SetTextBoxText(TB_CurrentS1.Text, TB_Seed1);
        }
    }

    private void B_RetailUpdateSeeds_Click(object sender, EventArgs e)
    {
        SetTextBoxText("0", TB_CurrentAdvances);
        B_CopyToInitial_Click(sender, e);
    }

    private void B_RefreshDexRec_Click(object sender, EventArgs e)
    {
        Task.Run(
            async () =>
            {
                try
                {
                    readPause = true;
                    await Task.Delay(100, Source.Token);

                    var DexRec = await ConnectionWrapper.ReadDexRecommendation(Source.Token).ConfigureAwait(false);

                    SetComboBoxSelectedIndex(CB_DexRec1.Items.IndexOf(GetDexRecommendation(DexRec[0])), CB_DexRec1);
                    SetComboBoxSelectedIndex(CB_DexRec2.Items.IndexOf(GetDexRecommendation(DexRec[1])), CB_DexRec2);
                    SetComboBoxSelectedIndex(CB_DexRec3.Items.IndexOf(GetDexRecommendation(DexRec[2])), CB_DexRec3);
                    SetComboBoxSelectedIndex(CB_DexRec4.Items.IndexOf(GetDexRecommendation(DexRec[3])), CB_DexRec4);

                    readPause = false;
                }
                catch (Exception ex)
                {
                    readPause = false;
                    this.DisplayMessageBox($"Error occurred while reading Pokédex Recommendations: {ex.Message}");
                    return;
                }
            }
        );
    }

    private void B_ReadEncounter_Click(object sender, EventArgs e)
    {
        Task.Run(
            async () =>
            {
                try
                {
                    readPause = true;
                    await Task.Delay(100, Source.Token);
                    SetTextBoxText("Reading encounter...", TB_Wild);
                    var pk = await ConnectionWrapper.ReadWildPokemon(Source.Token);
                    if (pk.Valid && pk.Species > 0)
                    {
                        CachedEncounter = pk;
                        bool HasRibbon = Utils.HasMark(pk, out RibbonIndex mark);

                        var n = System.Environment.NewLine;

                        string form = pk.Form == 0 ? string.Empty : $"-{pk.Form}";
                        string gender = pk.Gender switch
                        {
                            0 => " (M)",
                            1 => " (F)",
                            _ => string.Empty,
                        };
                        string shiny = pk.ShinyXor switch
                        {
                            0 => "■ - ",
                            < 16 => "★ -",
                            _ => string.Empty,
                        };


                        string item = pk.HeldItem > 0 ? $" @ {Strings.Item[pk.HeldItem]}" : string.Empty;
                        string markString = HasRibbon ? $"{n}Mark: {mark.ToString().Replace("Mark", "")}" : string.Empty;

                        string scale = $"Height: {PokeSizeDetailedUtil.GetSizeRating(pk.HeightScalar)} ({pk.HeightScalar})";

                        string moves = string.Empty;


                        foreach (int move in pk.Moves)
                        {
                            if (move == 0) break;
                            moves += $"{n}- {Strings.Move[move]}";
                        }

                        string output = $"{shiny}{(Species)pk.Species}{form}{gender}{item}{n}EC: {pk.EncryptionConstant:X8}{n}PID: {pk.PID:X8}{n}{Strings.Natures[(int)pk.Nature]} Nature{n}Ability: {Strings.Ability[pk.Ability]}{n}IVs: {pk.IV_HP}/{pk.IV_ATK}/{pk.IV_DEF}/{pk.IV_SPA}/{pk.IV_SPD}/{pk.IV_SPE}{n}{scale}{markString}{moves}";

                        readPause = false;
                        SetPictureBoxImage(pk.Sprite(), PB_PokemonSprite);
                        if (HasRibbon) SetPictureBoxImage(RibbonSpriteUtil.GetRibbonSprite(mark)!, PB_MarkSprite);
                        SetTextBoxText(output, TB_Wild);
                        SetControlEnabledState(true, B_CopyToFilter);
                    }
                    else
                    {
                        readPause = false;
                        CachedEncounter = null;
                        PB_PokemonSprite.Image = null;
                        PB_MarkSprite.Image = null;
                        SetTextBoxText("No encounter present.", TB_Wild);
                        SetControlEnabledState(false, B_CopyToFilter);
                    }
                }
                catch (Exception ex)
                {
                    readPause = false;
                    CachedEncounter = null;
                    PB_PokemonSprite.Image = null;
                    PB_MarkSprite.Image = null;
                    SetTextBoxText(string.Empty, TB_Wild);
                    SetControlEnabledState(false, B_CopyToFilter);
                    this.DisplayMessageBox(ex.Message);
                }
            }
        );
    }

    private void B_CopyToFilter_Click(object sender, EventArgs e)
    {
        if (CachedEncounter is not null)
        {
            SetNUDValue(CachedEncounter.IV_HP, NUD_HP_Min, NUD_HP_Max);
            SetNUDValue(CachedEncounter.IV_ATK, NUD_Atk_Min, NUD_Atk_Max);
            SetNUDValue(CachedEncounter.IV_DEF, NUD_Def_Min, NUD_Def_Max);
            SetNUDValue(CachedEncounter.IV_SPA, NUD_SpA_Min, NUD_SpA_Max);
            SetNUDValue(CachedEncounter.IV_SPD, NUD_SpD_Min, NUD_SpD_Max);
            SetNUDValue(CachedEncounter.IV_SPE, NUD_Spe_Min, NUD_Spe_Max);
        }
    }

    private void TC_EncounterType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetAreaOptions();
    }

    private void CB_Area_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetWeatherOptions();
    }

    private void CB_Weather_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetSpeciesOptions();
    }


    private void B_IV_Max_Click(object sender, EventArgs e)
    {
        var stat = ((Button)sender).Name.Replace("B_", string.Empty).Replace("_Max", string.Empty);
        var min = (NumericUpDown)Controls.Find($"NUD_{stat}_Min", true).FirstOrDefault()!;
        var max = (NumericUpDown)Controls.Find($"NUD_{stat}_Max", true).FirstOrDefault()!;
        min.Value = 31;
        max.Value = 31;
    }

    private void B_IV_Min_Click(object sender, EventArgs e)
    {
        var stat = ((Button)sender).Name.Replace("B_", string.Empty).Replace("_Min", string.Empty);
        var min = (NumericUpDown)Controls.Find($"NUD_{stat}_Min", true).FirstOrDefault()!;
        var max = (NumericUpDown)Controls.Find($"NUD_{stat}_Max", true).FirstOrDefault()!;
        min.Value = 0;
        max.Value = 0;
    }


    private void IV_Label_Click(object sender, EventArgs e)
    {
        var stat = ((Label)sender).Name.Replace("L_", string.Empty);
        var min = (NumericUpDown)Controls.Find($"NUD_{stat}_Min", true).FirstOrDefault()!;
        var max = (NumericUpDown)Controls.Find($"NUD_{stat}_Max", true).FirstOrDefault()!;
        min.Value = 0;
        max.Value = 31;
    }

    private void CB_MenuClose_CheckedChanged(object sender, EventArgs e)
    {
        var cb = (CheckBox)sender;
        var tab = cb.Name.Replace("CB_", string.Empty).Replace("_MenuClose", string.Empty);
        SetControlEnabledState(cb.Checked, Controls.Find($"TB_{tab}_NPCs", true).FirstOrDefault()!, Controls.Find($"B_{tab}_MenuClose", true).FirstOrDefault()!, Controls.Find($"CB_{tab}_MenuClose_Direction", true).FirstOrDefault()!);
    }

    public void SetCheckBoxCheckedState(bool state, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not CheckBox cb)
                continue;

            if (InvokeRequired)
                Invoke(() => cb.Checked = state);
            else
                cb.Checked = state;
        }
    }

    public void SetControlEnabledState(bool state, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not Control c)
                continue;

            if (InvokeRequired)
                Invoke(() => c.Enabled = state);
            else
                c.Enabled = state;
        }
    }

    public void SetNUDValue(decimal value, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not NumericUpDown c)
                continue;

            if (InvokeRequired)
                Invoke(() => c.Value = value);
            else
                c.Value = value;
        }
    }

    public void SetTextBoxText(string text, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not TextBox tb)
                continue;
            if (InvokeRequired)
            {
                Invoke(() => tb.Text = text);
            }
            else tb.Text = text;
        }
    }

    public void SetButtonText(string text, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not Button b)
                continue;
            if (InvokeRequired)
            {
                Invoke(() => b.Text = text);
            }
            else b.Text = text;
        }
    }

    private void SetPictureBoxImage(Image img, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not PictureBox pb)
                continue;
            if (InvokeRequired)
            {
                Invoke(() => pb.Image = img);
            }
            else pb.Image = img;
        }
    }

    public void SetComboBoxSelectedIndex(int index, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not ComboBox cb)
                continue;
            if (InvokeRequired)
            {
                Invoke(() => cb.SelectedIndex = index);
            }
            else cb.SelectedIndex = index;
        }
    }

    public void SetBindingSourceDataSource(object source, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not BindingSource b)
                continue;
            if (InvokeRequired)
            {
                Invoke(() => b.DataSource = source);
            }
            else b.DataSource = source;
        }
    }

    public void SetDataGridViewColumnVisibility(bool visible, int index, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not DataGridView dgv)
                continue;
            if (InvokeRequired)
            {
                Invoke(() => dgv.Columns[index].Visible = visible);
            }
            else dgv.Columns[index].Visible = visible;
        }
    }

    private void CB_ConsiderFlying_CheckedChanged(object sender, EventArgs e)
    {
        SetControlEnabledState(((CheckBox)sender).Checked, L_AreaLoad, NUD_AreaLoad, L_FlyNPCs, NUD_FlyNPCs);
        SetControlEnabledState(!((CheckBox)sender).Checked && CB_ConsiderRain.Checked, B_CalculateRain);
    }

    private void CB_ConsiderRain_CheckedChanged(object sender, EventArgs e)
    {
        SetControlEnabledState(((CheckBox)sender).Checked, L_RainTick, NUD_RainTick);
        SetControlEnabledState(((CheckBox)sender).Checked && !CB_ConsiderFlying.Checked, B_CalculateRain);
    }

    private readonly static Font BoldFont = new("Microsoft Sans Serif", 8, FontStyle.Bold);
    private void DGV_Results_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        var index = e.RowIndex;
        if (Frames.Count <= index) return;
        var row = DGV_Results.Rows[index];
        var result = Frames[index];

        if (result.Shiny is "Square") row.DefaultCellStyle.BackColor = Color.LightCyan;
        else if (result.Shiny is "Star") row.DefaultCellStyle.BackColor = Color.Aqua;
        else if (result.Step == 1) row.DefaultCellStyle.BackColor = Color.Honeydew;
        else if (result.Brilliant == 'Y') row.DefaultCellStyle.BackColor = Color.PapayaWhip;
        else row.DefaultCellStyle.BackColor = row.Index % 2 == 0 ? Color.White : Color.WhiteSmoke;

        var iv = 11;
        byte[] ivs = [result.H, result.A, result.B, result.C, result.D, result.S];
        for (var i = 0; i < ivs.Length; i++)
        {
            if (ivs[i] == 0)
            {
                row.Cells[iv + i].Style.Font = BoldFont;
                row.Cells[iv + i].Style.ForeColor = Color.OrangeRed;
            }
            else if (ivs[i] == 31)
            {
                row.Cells[iv + i].Style.Font = BoldFont;
                row.Cells[iv + i].Style.ForeColor = Color.SeaGreen;
            }
            else
            {
                row.Cells[iv + i].Style.ForeColor = row.DefaultCellStyle.ForeColor;
                row.Cells[iv + i].Style.Font = row.DefaultCellStyle.Font;
            }
        }

        row.Cells[2].Style.Font = result.Step != 1 ? row.DefaultCellStyle.Font : BoldFont;
        row.Cells[6].Style.Font = result.Brilliant != 'Y' ? row.DefaultCellStyle.Font : BoldFont;
        row.Cells[17].Style.Font = result.Mark == "None" ? row.DefaultCellStyle.Font : BoldFont;
        row.Cells[20].Style.Font = result.Height is not "XXXL (255)" and not "XXXS (0)" ? row.DefaultCellStyle.Font : BoldFont;

    }
    #endregion

    #region Input Validation
    public void KeyPress_AllowOnlyHex(object sender, KeyPressEventArgs e)
    {
        var c = e.KeyChar;
        if (c != (char)Keys.Back && !char.IsControl(c))
        {
            if (
                !char.IsBetween(c, '0', '9') &&
                !char.IsBetween(c, 'a', 'f') &&
                !char.IsBetween(c, 'A', 'F')
            )
            {
                e.Handled = true;
            }
        }
    }

    public void KeyPress_AllowOnlyNumerical(object sender, KeyPressEventArgs e)
    {
        var c = e.KeyChar;
        if (c != (char)Keys.Back && !char.IsControl(c))
        {
            if (!char.IsBetween(c, '0', '9'))
            {
                e.Handled = true;
            }
        }
    }

    public void KeyPress_AllowOnlyIP(object sender, KeyPressEventArgs e)
    {
        var c = e.KeyChar;
        if (c != (char)Keys.Back && !char.IsControl(c))
        {
            if (!char.IsBetween(c, '0', '9') && c != '.')
            {
                e.Handled = true;
            }
        }
    }

    private void KeyPress_AllowOnlyBinary(object sender, KeyPressEventArgs e)
    {
        string s = string.Empty;

        if (e.KeyChar is ',' or 'p' or 'P')
        {
            e.KeyChar = '0';
        }
        else if (e.KeyChar is '.' or 's' or 'S')
        {
            e.KeyChar = '1';
        }

        s += e.KeyChar;

        byte[] b = Encoding.ASCII.GetBytes(s);

        if (e.KeyChar != (char)Keys.Back && !char.IsControl(e.KeyChar))
        {
            if (!(('0' <= b[0]) && (b[0] <= '1')))
            {
                e.Handled = true;
            }
        }
    }
    #endregion

    #region SubForms
    private void B_RetailSeedFinder_Click(object sender, EventArgs e)
    {
        using RetailSeedFinder rsf = new();
        if (rsf.ShowDialog() == DialogResult.OK)
        {
            TB_Seed0.Text = rsf.Seed0;
            TB_Seed1.Text = rsf.Seed1;
        }
    }

    public bool EncounterLookupFormOpen = false;
    EncounterLookup? EncounterLookupForm;
    private void TSMI_EncounterLookup_Click(object sender, EventArgs e)
    {
        if (!EncounterLookupFormOpen)
        {
            EncounterLookupFormOpen = true;
            EncounterLookupForm = new EncounterLookup(this, CB_Game.SelectedIndex);
            EncounterLookupForm.Show();
        }
        else
        {
            EncounterLookupForm?.Focus();
        }
    }

    public bool MenuCloseTimelineFormOpen = false;
    MenuCloseTimeline? MenuCloseTimelineForm;
    private void B_MenuClose_Click(object sender, EventArgs e)
    {
        if (!MenuCloseTimelineFormOpen)
        {
            MenuCloseTimelineFormOpen = true;
            MenuCloseTimelineForm = new MenuCloseTimeline(this, ((Button)sender).Name.Replace("B_", string.Empty).Replace("_MenuClose", string.Empty));
            MenuCloseTimelineForm.Show();
        }
        else
        {
            MenuCloseTimelineForm?.Focus();
        }
    }

    public bool LotoIDFormOpen = false;
    LotoID? LotoIDForm;
    private void TSMI_LotoID_Click(object sender, EventArgs e)
    {
        if (!LotoIDFormOpen)
        {
            LotoIDFormOpen = true;
            LotoIDForm = new LotoID(this, TC_EncounterType.SelectedTab!.Text);
            LotoIDForm.Show();
        }
        else
        {
            LotoIDForm?.Focus();
        }
    }

    public bool CramomaticFormOpen = false;
    Cramomatic? CramomaticForm;
    private void TSMI_Cramomatic_Click(object sender, EventArgs e)
    {
        if (!CramomaticFormOpen)
        {
            CramomaticFormOpen = true;
            CramomaticForm = new Cramomatic(this, TC_EncounterType.SelectedTab!.Text);
            CramomaticForm.Show();
        }
        else
        {
            CramomaticForm?.Focus();
        }
    }

    public bool WattTraderFormOpen = false;
    WattTrader? WattTraderForm;
    private void TSMI_WattTrader_Click(object sender, EventArgs e)
    {
        if (!WattTraderFormOpen)
        {
            WattTraderFormOpen = true;
            WattTraderForm = new WattTrader(this, TC_EncounterType.SelectedTab!.Text);
            WattTraderForm.Show();
        }
        else
        {
            WattTraderForm?.Focus();
        }
    }

    public bool WailordRespawnFormOpen = false;
    WailordRespawn? WailordRespawnForm;
    private void TSMI_WailordRespawn_Click(object sender, EventArgs e)
    {
        if (!WailordRespawnFormOpen)
        {
            WailordRespawnFormOpen = true;
            WailordRespawnForm = new WailordRespawn(this, TC_EncounterType.SelectedTab!.Text);
            WailordRespawnForm.Show();
        }
        else
        {
            WailordRespawnForm?.Focus();
        }
    }
    #endregion

    #region Retail
    private byte[] RetailSequence = [];
    private ulong RetailSeed0 = 0;
    private ulong RetailSeed1 = 0;
    private ulong RetailInitial = 0;
    private void B_GenerateRetailPattern_Click(object sender, EventArgs e)
    {
        var s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
        var s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);
        var ini = ulong.Parse(TB_RetailInitial.Text);
        var adv = ulong.Parse(TB_RetailRange.Text);
        RetailInitial = ini;
        (RetailSequence, RetailSeed0, RetailSeed1) = SeedFinder.GenerateAnimationSequence(s0, s1, ini, adv);
    }

    private void TB_Animations_TextChanged(object sender, EventArgs e)
    {
        var pattern = TB_Animations.Text;
        TB_AnimationLength.Text = $"{pattern.Length}";
        if (RetailSequence.Length > 5)
        {
            if (pattern.Length > 5)
            {
                var (hits, advances, s0, s1) = SeedFinder.ReidentifySeed(RetailSequence, RetailSeed0, RetailSeed1, pattern);
                if (hits == 1)
                {
                    TB_RetailAdvances.Text = $"{(uint)advances + RetailInitial:N0}";
                    TB_CurrentAdvances.Text = $"{(uint)advances + RetailInitial:N0}";
                    TB_CurrentS0.Text = $"{s0:X16}";
                    TB_CurrentS1.Text = $"{s1:X16}";
                }
                else if (hits == 0)
                {
                    TB_RetailAdvances.Text = "No results";
                }
                else
                {
                    TB_RetailAdvances.Text = $"{hits} results";
                }
            }
            else
            {
                TB_RetailAdvances.Text = "Need more inputs";
            }
        }
        else
        {
            TB_RetailAdvances.Text = "Generate First ↑";
        }
    }
    #endregion
}

public static class Extension
{
    public static void SanitizeColumns(this DataGridView dgv, MainWindow mw)
    {
        try
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col is not null)
                {
                    var row = dgv.Rows[0];
                    if (row is not null)
                    {
                        var fv = row.Cells[col.Index].FormattedValue!.ToString();
                        if (fv is null) continue;
                        var vis = string.IsNullOrEmpty(fv.Trim());
                        mw.SetDataGridViewColumnVisibility(!vis, col.Index, dgv);
                    }
                }
            }
        }
        catch
        {
            //mw.DisplayMessageBox(ex.Message);
            return;
        }
    }
}
