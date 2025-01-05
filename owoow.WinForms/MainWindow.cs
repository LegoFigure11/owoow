using owoow.Core;
using owoow.Core.Connection;
using owoow.Core.EncounterTable;
using owoow.Core.Interfaces;
using owoow.Core.RNG.Generators.Overworld;
using owoow.WinForms.Subforms;
using PKHeX.Core;
using PKHeX.Drawing.PokeSprite;
using SysBot.Base;
using System.Globalization;
using static owoow.Core.Encounters;
using static owoow.Core.RNG.FilterUtil;
using static owoow.Core.RNG.Util;

namespace owoow.WinForms;

public partial class MainWindow : Form
{
    private static CancellationTokenSource Source = new();
    private static readonly Lock _connectLock = new();

    //private readonly ClientConfig Config;
    private ConnectionWrapperAsync ConnectionWrapper = default!;
    private readonly SwitchConnectionConfig ConnectionConfig;

    private readonly GameStrings Strings = GameInfo.GetStrings(1);

    bool stop;
    bool reset;
    bool pause = false;
    long total;

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

                SetControlEnabledState(true, B_Disconnect, B_CopyToInitial, B_ReadEncounter);

                UpdateStatus("Monitoring RNG State...");
                try
                {
                    total = 0;
                    stop = false;
                    while (!stop)
                    {
                        if (ConnectionWrapper.Connected && !pause)
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
            RainTicksAreaLoad = (uint)NUD_RainFly.Value,
            RainTicksEncounter = (uint)NUD_RainEncounter.Value,

            FiltersEnabled = CB_EnableFilters.Checked,

            TID = uint.Parse(TB_TID.Text),
            SID = uint.Parse(TB_SID.Text),
        };

        var rng = new Xoroshiro128Plus(s0, s1);

        for (ulong i = 0; i < initial; i++) rng.Next();

        List<Frame>[] results = [];

        List<Task<List<Frame>>> tasks = [];
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
            List<Frame> AllResults = [];
            foreach (var result in results)
            {
                AllResults.AddRange(result);
            }

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
            RainTicksAreaLoad = (uint)NUD_RainFly.Value,
            RainTicksEncounter = (uint)NUD_RainEncounter.Value,

            FiltersEnabled = CB_EnableFilters.Checked,

            TID = uint.Parse(TB_TID.Text),
            SID = uint.Parse(TB_SID.Text),
        };

        var rng = new Xoroshiro128Plus(s0, s1);

        for (ulong i = 0; i < initial; i++) rng.Next();

        List<Frame>[] results = [];

        List<Task<List<Frame>>> tasks = [];
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
            List<Frame> AllResults = [];
            foreach (var result in results)
            {
                AllResults.AddRange(result);
            }

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
            RainTicksAreaLoad = (uint)NUD_RainFly.Value,
            RainTicksEncounter = (uint)NUD_RainEncounter.Value,

            FiltersEnabled = CB_EnableFilters.Checked,

            TID = uint.Parse(TB_TID.Text),
            SID = uint.Parse(TB_SID.Text),
        };

        var rng = new Xoroshiro128Plus(s0, s1);

        for (ulong i = 0; i < initial; i++) rng.Next();

        List<Frame>[] results = [];

        List<Task<List<Frame>>> tasks = [];
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
            List<Frame> AllResults = [];
            foreach (var result in results)
            {
                AllResults.AddRange(result);
            }

            SetBindingSourceDataSource(AllResults, ResultsSource);
            DGV_Results.SanitizeColumns(this);

            SetControlEnabledState(true, sender);
        });
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

    private void B_ReadEncounter_Click(object sender, EventArgs e)
    {
        Task.Run(
            async () =>
            {
                try
                {
                    pause = true;
                    await Task.Delay(100, Source.Token);
                    SetTextBoxText("Reading encounter...", TB_Wild);
                    var pk = await ConnectionWrapper.ReadWildPokemon(Source.Token);
                    if (pk.Valid)
                    {
                        bool HasRibbon = Utils.HasMark(pk, out RibbonIndex mark);

                        var n = Environment.NewLine;

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

                        pause = false;
                        SetPictureBoxImage(pk.Sprite(), PB_PokemonSprite);
                        SetTextBoxText(output, TB_Wild);
                    }
                    else
                    {
                        pause = false;
                        PB_PokemonSprite.Image = null;
                        SetTextBoxText("No encounter present.", TB_Wild);
                    }
                }
                catch (Exception ex)
                {
                    pause = false;
                    PB_PokemonSprite.Image = null;
                    SetTextBoxText(string.Empty, TB_Wild);
                    this.DisplayMessageBox(ex.Message);
                }
            }
        );
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
            MenuCloseTimelineForm!.Focus();
        }
    }

    private void CB_ConsiderFlying_CheckedChanged(object sender, EventArgs e)
    {
        SetControlEnabledState(((CheckBox)sender).Checked, L_AreaLoad, NUD_AreaLoad, L_FlyNPCs, NUD_FlyNPCs);
        SetControlEnabledState(((CheckBox)sender).Checked && CB_ConsiderRain.Checked, L_RainFly, NUD_RainFly);
    }

    private void CB_ConsiderRain_CheckedChanged(object sender, EventArgs e)
    {
        SetControlEnabledState(((CheckBox)sender).Checked, L_RainEncounter, NUD_RainEncounter);
        SetControlEnabledState(((CheckBox)sender).Checked && CB_ConsiderFlying.Checked, L_RainFly, NUD_RainFly);
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
