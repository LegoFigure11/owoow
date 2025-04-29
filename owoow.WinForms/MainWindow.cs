using owoow.Core;
using owoow.Core.Connection;
using owoow.Core.Discord;
using owoow.Core.EncounterTable;
using owoow.Core.Enums;
using owoow.Core.Interfaces;
using owoow.Core.RNG.Generators.Misc;
using owoow.Core.RNG.Generators.Overworld;
using owoow.WinForms.Subforms;
using PKHeX.Core;
using PKHeX.Drawing.Misc;
using PKHeX.Drawing.PokeSprite;
using SysBot.Base;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.Json;
using static owoow.Core.Encounters;
using static owoow.Core.RNG.FilterUtil;
using static owoow.Core.RNG.Util;

namespace owoow.WinForms;

public partial class MainWindow : Form
{
    private static CancellationTokenSource Source = new();
    private static CancellationTokenSource AdvanceSource = new();
    private static CancellationTokenSource ResetSource = new();
    private static readonly Lock _connectLock = new();

    public ClientConfig Config;
    private ConnectionWrapperAsync ConnectionWrapper = default!;
    private readonly SwitchConnectionConfig ConnectionConfig;

    public readonly GameStrings Strings = GameInfo.GetStrings(1);

    private bool stop;
    private bool reset;
    private bool readPause = false;
    private bool skipPause = false;
    private bool resetPause = false;
    private long total;

    private List<OverworldFrame> Frames = [];

    private PK8? CachedEncounter;

    public WebhookHandler Webhook;

    public MainWindow()
    {
        Config = new ClientConfig();
        var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        if (File.Exists(configPath))
        {
            var text = File.ReadAllText(configPath);
            Config = JsonSerializer.Deserialize<ClientConfig>(text)!;
        }
        else
        {
            Config = new();
        }

        ConnectionConfig = new()
        {
            IP = Config.IP,
            Port = Config.Protocol is SwitchProtocol.WiFi ? 6000 : Config.UsbPort,
            Protocol = Config.Protocol,
        };

        Webhook = new(Config);


        var v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version!;
#if DEBUG
        var build = "";

        var asm = System.Reflection.Assembly.GetEntryAssembly();
        var gitVersionInformationType = asm?.GetType("GitVersionInformation");
        var sha = gitVersionInformationType?.GetField("ShortSha");

        if (sha is not null) build += $"#{sha.GetValue(null)}";

        var date = File.GetLastWriteTime(AppContext.BaseDirectory);
        build += $" (dev-{date:yyyyMMdd})";

#else
        var build = "";
#endif

        Text = $"owoow (´・ω・`) v{v.Major}.{v.Minor}.{v.Build}{build}";

        InitializeComponent();
    }

    private void MainWindow_Load(object sender, EventArgs e)
    {
        CenterToScreen();

        if (Config.Protocol is SwitchProtocol.WiFi)
        {
            TB_SwitchIP.Text = Config.IP;
        }
        else
        {
            L_SwitchIP.Text = "USB Port:";
            TB_SwitchIP.Text = $"{Config.UsbPort}";
        }

        TB_TID.Text = $"{Config.TID:D5}";
        TB_SID.Text = $"{Config.SID:D5}";

        CB_Game.SelectedIndex = Config.Game;

        CB_ShinyCharm.Checked = Config.HasShinyCharm;
        CB_MarkCharm.Checked = Config.HasMarkCharm;

        CB_PlayTone.Checked = Config.PlayTone;
        CB_FocusWindow.Checked = Config.FocusWindow;

        SetTextBoxText("0", TB_Seed0, TB_Seed1);
        SetTextBoxText(string.Empty, TB_CurrentAdvances, TB_AdvancesIncrease, TB_CurrentS0, TB_CurrentS1, TB_Wild);
        SetComboBoxSelectedIndex(0, CB_Filter_Shiny, CB_Filter_Mark, CB_Filter_Aura, CB_Filter_Height);

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

                SetControlEnabledState(true, B_Disconnect, B_CopyToInitial, B_ReadEncounter, B_RefreshDexRec, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_NTP, L_Skips, TB_Skips, B_SeedSearch);

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
                            if (reset || adv > 0)
                            {
                                if (reset || adv == 50_000)
                                {
                                    total = 0;
                                    reset = false;
                                    adv = 0;
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
                await Source.CancelAsync().ConfigureAwait(false);
                Source = new();
                await AdvanceSource.CancelAsync().ConfigureAwait(false);
                AdvanceSource = new();
                await ResetSource.CancelAsync().ConfigureAwait(false);
                ResetSource = new();
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
                    await SafeResetTimeNTP(AdvanceSource.Token).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    this.DisplayMessageBox(ex.Message);
                }
            }
        );
    }

    private void B_Turbo_Click(object sender, EventArgs e)
    {
        Task.Run(
            async () =>
            {
                try
                {
                    SetControlEnabledState(false, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_SeedSearch);
                    SetControlEnabledState(true, B_CancelSkip);
                    skipPause = false;
                    var i = 0;
                    bool flag = false;
                    if (Config.TurboSequence.Count > 0)
                    {
                        do
                        {
                            if (!AdvanceSource.IsCancellationRequested) await ConnectionWrapper.DoTurboCommand(Config.TurboSequence[i], AdvanceSource.Token).ConfigureAwait(false);
                            i = (i + 1) % Config.TurboSequence.Count;
                            if (!AdvanceSource.IsCancellationRequested) await Task.Delay(200, AdvanceSource.Token).ConfigureAwait(false);
                            if (i == 0) flag = true;
                        } while (!AdvanceSource.IsCancellationRequested && (Config.LoopTurbo || !flag) && !skipPause && Config.TurboSequence.Count > 0);
                    }
                    SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_SeedSearch);
                    SetControlEnabledState(false, B_CancelSkip);
                }
                catch (Exception ex)
                {
                    SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_SeedSearch);
                    SetControlEnabledState(false, B_CancelSkip);
                    if (ex is not OperationCanceledException) this.DisplayMessageBox(ex.Message);
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
                    SetControlEnabledState(false, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_SeedSearch);
                    SetControlEnabledState(true, B_CancelSkip);
                    skipPause = false;
                    var skips = uint.Parse(TB_Skips.Text);
                    for (var i = 0; i < skips && !skipPause; i++)
                    {
                        SetButtonText($"{i + 1}", B_SkipAdvance);
                        await ConnectionWrapper.PressL3(AdvanceSource.Token).ConfigureAwait(false);
                        await Task.Delay(150, AdvanceSource.Token).ConfigureAwait(false);
                    }
                    SetButtonText("Adv.", B_SkipAdvance);
                    SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_SeedSearch);
                    SetControlEnabledState(false, B_CancelSkip);
                }
                catch (Exception ex)
                {
                    SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_SeedSearch);
                    SetControlEnabledState(false, B_CancelSkip);
                    SetButtonText("Adv.", B_SkipAdvance);
                    if (ex is not OperationCanceledException) this.DisplayMessageBox(ex.Message);
                }
            }
        );
    }

    private async void B_CancelSkip_Click(object sender, EventArgs e)
    {
        skipPause = true;
        resetPause = true;
        AdvanceSource.Cancel();
        ResetSource.Cancel();
        AdvanceSource = new();
        ResetSource = new();
        try
        {
            if (ConnectionWrapper is { Connected: true }) await ConnectionWrapper.ResetStick(AdvanceSource.Token).ConfigureAwait(false);
        }
        catch { }
    }

    private static bool canCallResetTimeNTP = true;
    private static readonly Lock resetTimeNTPLock = new();

    /// <summary>
    /// Safely resets the time on the Nintendo Switch using the Network Time Protocol (NTP).
    /// This method ensures that the reset operation is not called multiple times in a short
    /// amount of time, which causes bad behavior with sys-botbase or usb-botbase.
    /// </summary>
    /// <param name="token">A cancellation token to handle task cancellation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task SafeResetTimeNTP(CancellationToken token)
    {
        lock (resetTimeNTPLock)
        {
            // ResetTimeNTP is on cooldown, so do nothing.
            if (!canCallResetTimeNTP)
                return;
            canCallResetTimeNTP = false;
        }

        _ = Task.Run(async () =>
        {
            try
            {
                await ConnectionWrapper.ResetTimeNTP(token).ConfigureAwait(false);
                SetControlEnabledState(false, B_NTP);
                await Task.Delay(2000).ConfigureAwait(false); // 2 second cooldown, don't allow it to be cancelled.
            }
            catch (Exception ex)
            {
                this.DisplayMessageBox($"Error during ResetTimeNTP: {ex.Message}\nPlease report this error.");
            }
            finally
            {
                lock (resetTimeNTPLock)
                {
                    canCallResetTimeNTP = true;
                    // Only enable the NTP button if the skip advance button is enabled, which means we're not currently skipping.
                    if (B_SkipAdvance.Enabled)
                        SetControlEnabledState(true, B_NTP);
                }
            }
        });
        await Task.Delay(200, AdvanceSource.Token).ConfigureAwait(false);
    }

    private void B_SkipForward_Click(object sender, EventArgs e)
    {
        Task.Run(
            async () =>
            {
                try
                {
                    SetControlEnabledState(false, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_SeedSearch, B_NTP);
                    SetControlEnabledState(true, B_CancelSkip);
                    skipPause = false;
                    var skips = uint.Parse(TB_Skips.Text);
                    for (var i = 0; i < skips && !skipPause; i++)
                    {
                        // Attempt to reset the time every 366 skips if NTP isn't on cooldown.
                        // If we're less than 366 from the end, then wait until then to NTP.
                        if (i > 0 && i % 366 == 0 && skips - i > 366)
                            await SafeResetTimeNTP(AdvanceSource.Token).ConfigureAwait(false);
                        SetButtonText($"{i + 1}", B_SkipForward);
                        await ConnectionWrapper.DaySkip(AdvanceSource.Token).ConfigureAwait(false);
                        await Task.Delay(360, AdvanceSource.Token).ConfigureAwait(false);
                    }

                    // Will only NTP if not on cooldown.
                    await SafeResetTimeNTP(AdvanceSource.Token).ConfigureAwait(false);

                    SetButtonText("Days+", B_SkipForward);
                    SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_SeedSearch);
                    // Only reset the NTP button if it's not on cooldown.
                    if (canCallResetTimeNTP)
                        SetControlEnabledState(true, B_NTP);
                    SetControlEnabledState(false, B_CancelSkip);
                }
                catch (Exception ex)
                {
                    SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_SeedSearch);
                    // Only reset the NTP button if it's not on cooldown.
                    if (canCallResetTimeNTP)
                        SetControlEnabledState(true, B_NTP);
                    SetControlEnabledState(false, B_CancelSkip);
                    SetButtonText("Days+", B_SkipForward);
                    if (ex is not OperationCanceledException) this.DisplayMessageBox(ex.Message);
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
                    SetControlEnabledState(false, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_SeedSearch, B_NTP);
                    SetControlEnabledState(true, B_CancelSkip);
                    skipPause = false;
                    var skips = uint.Parse(TB_Skips.Text);
                    for (var i = 0; i < skips && !skipPause; i++)
                    {
                        SetButtonText($"{i + 1}", B_SkipBack);
                        await ConnectionWrapper.DaySkipBack(AdvanceSource.Token).ConfigureAwait(false);
                        await Task.Delay(360, AdvanceSource.Token).ConfigureAwait(false);
                    }
                    SetButtonText("Days-", B_SkipBack);
                    SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_SeedSearch);
                    // Only reset the NTP button if it's not on cooldown.
                    if (canCallResetTimeNTP)
                        SetControlEnabledState(true, B_NTP);
                    SetControlEnabledState(false, B_CancelSkip);
                }
                catch (Exception ex)
                {
                    SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_SeedSearch);
                    // Only reset the NTP button if it's not on cooldown.
                    if (canCallResetTimeNTP)
                        SetControlEnabledState(true, B_NTP);
                    SetControlEnabledState(false, B_CancelSkip);
                    SetButtonText("Days-", B_SkipBack);
                    if (ex is not OperationCanceledException) this.DisplayMessageBox(ex.Message);
                }
            }
        );
    }
    #endregion

    #region Searchers

    private void B_Symbol_Search_Click(object sender, EventArgs e)
    {
        SetControlEnabledState(false, sender);

        ValidateInputs("Symbol");

        var table = new EncounterTable(CB_Game.Text, "Symbol", CB_Symbol_Area.Text, CB_Symbol_Weather.Text, CB_Symbol_LeadAbility.Text);

        var initial = ulong.Parse(TB_Symbol_Initial.Text);
        var advances = ulong.Parse(TB_Symbol_Advances.Text);

        var numTasks = (byte)(advances < 1_000 ? 1 : advances < 50_000 ? 2 : Math.Max(4, 1 << Config.MaxSearchTasksNthPowerOfTwo));
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

            RareEC = CB_RareEC.Checked,

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
            results = await Task.WhenAll(tasks).ConfigureAwait(false);
            List<OverworldFrame> AllResults = [];
            foreach (var result in results)
            {
                AllResults.AddRange(result);
            }

            Frames = AllResults;
            if (AllResults.Count > 1000) AllResults = AllResults[0..1000];
            SetBindingSourceDataSource(AllResults, ResultsSource);
            DGV_Results.SanitizeColumns(this);

            SetControlEnabledState(true, sender);
        }).ContinueWith(_ =>
        {
            if (CB_FocusWindow.Checked) ActivateWindow();
            if (CB_PlayTone.Checked) System.Media.SystemSounds.Asterisk.Play();
            if (Frames.Count >= 1_000) MessageBox.Show($"Too many results found, displayed results capped at 1000. Please re-run the search with more restrictive filters or a smaller range of advances.");
        });
    }

    private void B_Hidden_Search_Click(object sender, EventArgs e)
    {
        SetControlEnabledState(false, sender);

        ValidateInputs("Hidden");

        var table = new EncounterTable(CB_Game.Text, "Hidden", CB_Hidden_Area.Text, CB_Hidden_Weather.Text, CB_Hidden_LeadAbility.Text);

        var initial = ulong.Parse(TB_Hidden_Initial.Text);
        var advances = ulong.Parse(TB_Hidden_Advances.Text);

        var numTasks = (byte)(advances < 1_000 ? 1 : advances < 50_000 ? 2 : Math.Max(4, 1 << Config.MaxSearchTasksNthPowerOfTwo));
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

            MaxStep = CB_Hidden_MaxStep.SelectedIndex,

            TargetShiny = GetFilterShinyType(CB_Filter_Shiny.SelectedIndex),
            TargetMark = GetFilterMarkype(CB_Filter_Mark.SelectedIndex),
            TargetScale = GetFilterScaleType(CB_Filter_Height.SelectedIndex),

            TargetMinIVs = [(uint)NUD_HP_Min.Value, (uint)NUD_Atk_Min.Value, (uint)NUD_Def_Min.Value, (uint)NUD_SpA_Min.Value, (uint)NUD_SpD_Min.Value, (uint)NUD_Spe_Min.Value],
            TargetMaxIVs = [(uint)NUD_HP_Max.Value, (uint)NUD_Atk_Max.Value, (uint)NUD_Def_Max.Value, (uint)NUD_SpA_Max.Value, (uint)NUD_SpD_Max.Value, (uint)NUD_Spe_Max.Value],

            RareEC = CB_RareEC.Checked,

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
            results = await Task.WhenAll(tasks).ConfigureAwait(false);
            List<OverworldFrame> AllResults = [];
            foreach (var result in results)
            {
                AllResults.AddRange(result);
            }

            Frames = AllResults;
            if (AllResults.Count > 1000) AllResults = AllResults[0..1000];
            SetBindingSourceDataSource(AllResults, ResultsSource);
            DGV_Results.SanitizeColumns(this);

            SetControlEnabledState(true, sender);
        }).ContinueWith(_ =>
        {
            if (CB_FocusWindow.Checked) ActivateWindow();
            if (CB_PlayTone.Checked) System.Media.SystemSounds.Asterisk.Play();
            if (Frames.Count >= 1_000) MessageBox.Show($"Too many results found, displayed results capped at 1000. Please re-run the search with more restrictive filters or a smaller range of advances.");
        });
    }

    private void B_Static_Search_Click(object sender, EventArgs e)
    {
        SetControlEnabledState(false, sender);

        ValidateInputs("Static");

        var table = new EncounterTable(CB_Game.Text, "Static", CB_Static_Area.Text, CB_Static_Weather.Text, CB_Static_LeadAbility.Text);

        var initial = ulong.Parse(TB_Static_Initial.Text);
        var advances = ulong.Parse(TB_Static_Advances.Text);

        var numTasks = (byte)(advances < 1_000 ? 1 : advances < 50_000 ? 2 : Math.Max(4, 1 << Config.MaxSearchTasksNthPowerOfTwo));
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

            RareEC = CB_RareEC.Checked,

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
            results = await Task.WhenAll(tasks).ConfigureAwait(false);
            List<OverworldFrame> AllResults = [];
            foreach (var result in results)
            {
                AllResults.AddRange(result);
            }

            Frames = AllResults;
            if (AllResults.Count > 1000) AllResults = AllResults[0..1000];
            SetBindingSourceDataSource(AllResults, ResultsSource);
            DGV_Results.SanitizeColumns(this);

            SetControlEnabledState(true, sender);
        }).ContinueWith(_ =>
        {
            if (CB_FocusWindow.Checked) ActivateWindow();
            if (CB_PlayTone.Checked) System.Media.SystemSounds.Asterisk.Play();
            if (Frames.Count >= 1_000) MessageBox.Show($"Too many results found, displayed results capped at 1000. Please re-run the search with more restrictive filters or a smaller range of advances.");
        });
    }

    private void B_Fishing_Search_Click(object sender, EventArgs e)
    {
        SetControlEnabledState(false, sender);

        ValidateInputs("Fishing");

        var table = new EncounterTable(CB_Game.Text, "Fishing", CB_Fishing_Area.Text, CB_Fishing_Weather.Text, CB_Fishing_LeadAbility.Text);

        var initial = ulong.Parse(TB_Fishing_Initial.Text);
        var advances = ulong.Parse(TB_Fishing_Advances.Text);

        var numTasks = (byte)(advances < 1_000 ? 1 : advances < 50_000 ? 2 : Math.Max(4, 1 << Config.MaxSearchTasksNthPowerOfTwo));
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

            RareEC = CB_RareEC.Checked,

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
            results = await Task.WhenAll(tasks).ConfigureAwait(false);
            List<OverworldFrame> AllResults = [];
            foreach (var result in results)
            {
                AllResults.AddRange(result);
            }

            Frames = AllResults;
            if (AllResults.Count > 1000) AllResults = AllResults[0..1000];
            SetBindingSourceDataSource(AllResults, ResultsSource);
            DGV_Results.SanitizeColumns(this);

            SetControlEnabledState(true, sender);
        }).ContinueWith(_ =>
        {
            if (CB_FocusWindow.Checked) ActivateWindow();
            if (CB_PlayTone.Checked) System.Media.SystemSounds.Asterisk.Play();
            if (Frames.Count >= 1_000) MessageBox.Show($"Too many results found, displayed results capped at 1000. Please re-run the search with more restrictive filters or a smaller range of advances.");
        });
    }

    private void B_CalculateRain_Click(object sender, EventArgs e)
    {
        var tab = TC_EncounterType.SelectedTab;
        if (tab != null)
        {
            var type = tab.Text;

            ValidateInputs(type);

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

                AuraKOs = type is "Symbol" or "Fishing" ? int.Parse(GetControlText((TextBox)Controls.Find($"TB_{type}_KOs", true).FirstOrDefault()!)) : 0,

                Weather = GetWeatherType(((ComboBox)Controls.Find($"CB_{type}_Weather", true).FirstOrDefault()!).Text),

                ShinyRolls = CB_ShinyCharm.Checked ? 3 : 1,
                MarkRolls = CB_MarkCharm.Checked ? 3 : 1,

                MaxStep = type is "Hidden" ? CB_Hidden_MaxStep.SelectedIndex : 0,

                TargetShiny = GetFilterShinyType(CB_Filter_Shiny.SelectedIndex),
                TargetMark = GetFilterMarkype(CB_Filter_Mark.SelectedIndex),
                TargetScale = GetFilterScaleType(CB_Filter_Height.SelectedIndex),

                TargetMinIVs = [(uint)NUD_HP_Min.Value, (uint)NUD_Atk_Min.Value, (uint)NUD_Def_Min.Value, (uint)NUD_SpA_Min.Value, (uint)NUD_SpD_Min.Value, (uint)NUD_Spe_Min.Value],
                TargetMaxIVs = [(uint)NUD_HP_Max.Value, (uint)NUD_Atk_Max.Value, (uint)NUD_Def_Max.Value, (uint)NUD_SpA_Max.Value, (uint)NUD_SpD_Max.Value, (uint)NUD_Spe_Max.Value],

                RareEC = CB_RareEC.Checked,

                DexRecSlots =
                [
                    GetDexRecommendation(CB_DexRec1.Text),
                    GetDexRecommendation(CB_DexRec2.Text),
                    GetDexRecommendation(CB_DexRec3.Text),
                    GetDexRecommendation(CB_DexRec4.Text)
                ],

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

    private void B_SeedSearch_Click(object sender, EventArgs e)
    {
        var tab = TC_EncounterType.SelectedTab;
        Task.Run(async () =>
        {
            try
            {
                SetControlEnabledState(false, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_SeedSearch, B_ReadEncounter);
                SetControlEnabledState(true, B_CancelSkip);
                skipPause = true;
                readPause = true;
                bool found = false;
                await Task.Delay(150, ResetSource.Token).ConfigureAwait(false);
                bool first = true;
                var ct = 0;
                var sw = Stopwatch.GetTimestamp();
                while (ConnectionWrapper.Connected && !ResetSource.IsCancellationRequested && !resetPause && !found)
                {
                    ulong prevs0 = 0;
                    ulong prevs1 = 0;
                    var (s0, s1) = await ConnectionWrapper.ReadRNGState(ResetSource.Token).ConfigureAwait(false);

                    if (!(s0 == prevs0 && s1 == prevs1))
                    {
                        SetTextBoxText($"{s0:X16}", TB_Seed0, TB_CurrentS0);
                        SetTextBoxText($"{s1:X16}", TB_Seed1, TB_CurrentS1);
                        SetTextBoxText("0", TB_CurrentAdvances, TB_AdvancesIncrease);


                        if (tab != null)
                        {
                            var type = tab.Text;

                            ValidateInputs(type);

                            var table = new EncounterTable(
                                GetControlText(CB_Game),
                                type,
                                GetControlText((ComboBox)Controls.Find($"CB_{type}_Area", true).FirstOrDefault()!),
                                GetControlText((ComboBox)Controls.Find($"CB_{type}_Weather", true).FirstOrDefault()!),
                                GetControlText((ComboBox)Controls.Find($"CB_{type}_LeadAbility", true).FirstOrDefault()!)
                                );

                            var initial = ulong.Parse(GetControlText((TextBox)Controls.Find($"TB_{type}_Initial", true).FirstOrDefault()!));
                            var advances = ulong.Parse(GetControlText((TextBox)Controls.Find($"TB_{type}_Advances", true).FirstOrDefault()!));

                            var numTasks = (byte)(advances < 1_000 ? 1 : advances < 50_000 ? 2 : Math.Max(4, 1 << Config.MaxSearchTasksNthPowerOfTwo));
                            var interval = advances / numTasks;

                            Core.RNG.GeneratorConfig config = new()
                            {
                                TargetSpecies = GetControlText((ComboBox)Controls.Find($"CB_{type}_Species", true).FirstOrDefault()!),
                                LeadAbility = GetControlText((ComboBox)Controls.Find($"CB_{type}_LeadAbility", true).FirstOrDefault()!),

                                AuraKOs = type is "Symbol" or "Fishing" ? int.Parse(GetControlText((TextBox)Controls.Find($"TB_{type}_KOs", true).FirstOrDefault()!)) : 0,
                                TargetAura = type is "Symbol" or "Fishing" ? GetFilterAuraType(GetComboBoxSelectedIndex(CB_Filter_Aura)) : AuraType.Any,

                                Weather = GetWeatherType(GetControlText((ComboBox)Controls.Find($"CB_{type}_Weather", true).FirstOrDefault()!)),

                                ShinyRolls = GetCheckBoxIsChecked(CB_ShinyCharm) ? 3 : 1,
                                MarkRolls = GetCheckBoxIsChecked(CB_MarkCharm) ? 3 : 1,

                                MaxStep = type is "Hidden" ? GetComboBoxSelectedIndex(CB_Hidden_MaxStep) : 0,

                                TargetShiny = GetFilterShinyType(GetComboBoxSelectedIndex(CB_Filter_Shiny)),
                                TargetMark = GetFilterMarkype(GetComboBoxSelectedIndex(CB_Filter_Mark)),
                                TargetScale = GetFilterScaleType(GetComboBoxSelectedIndex(CB_Filter_Height)),

                                TargetMinIVs = [GetNUDValue(NUD_HP_Min), GetNUDValue(NUD_Atk_Min), GetNUDValue(NUD_Def_Min), GetNUDValue(NUD_SpA_Min), GetNUDValue(NUD_SpD_Min), GetNUDValue(NUD_Spe_Min)],
                                TargetMaxIVs = [GetNUDValue(NUD_HP_Max), GetNUDValue(NUD_Atk_Max), GetNUDValue(NUD_Def_Max), GetNUDValue(NUD_SpA_Max), GetNUDValue(NUD_SpD_Max), GetNUDValue(NUD_Spe_Max)],

                                RareEC = GetCheckBoxIsChecked(CB_RareEC),

                                DexRecSlots =
                                [
                                    GetDexRecommendation(GetControlText(CB_DexRec1)),
                                    GetDexRecommendation(GetControlText(CB_DexRec2)),
                                    GetDexRecommendation(GetControlText(CB_DexRec3)),
                                    GetDexRecommendation(GetControlText(CB_DexRec4))
                                ],

                                ConsiderMenuClose = GetCheckBoxIsChecked((CheckBox)Controls.Find($"CB_{type}_MenuClose", true).FirstOrDefault()!),
                                MenuCloseIsHoldingDirection = GetCheckBoxIsChecked((CheckBox)Controls.Find($"CB_{type}_MenuClose_Direction", true).FirstOrDefault()!),
                                MenuCloseNPCs = uint.Parse(GetControlText((TextBox)Controls.Find($"TB_{type}_NPCs", true).FirstOrDefault()!)),

                                ConsiderFly = GetCheckBoxIsChecked(CB_ConsiderFlying),
                                ConsiderRain = GetCheckBoxIsChecked(CB_ConsiderRain),
                                AreaLoadAdvances = GetNUDValue(NUD_AreaLoad),
                                AreaLoadNPCs = GetNUDValue(NUD_FlyNPCs),
                                RainTicksAreaLoad = GetCheckBoxIsChecked(CB_ConsiderFlying) && GetCheckBoxIsChecked(CB_ConsiderRain) ? GetNUDValue(NUD_RainTick) : 0,
                                RainTicksEncounter = GetCheckBoxIsChecked(CB_ConsiderFlying) && GetCheckBoxIsChecked(CB_ConsiderRain) ? 0 : GetNUDValue(NUD_RainTick),

                                FiltersEnabled = true,

                                TID = uint.Parse(GetControlText(TB_TID)),
                                SID = uint.Parse(GetControlText(TB_SID)),
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

                                var t = type switch
                                {
                                    "Static" => Static.Generate(_s0, _s1, table, start, end, config),
                                    "Symbol" => Symbol.Generate(_s0, _s1, table, start, end, config),
                                    "Fishing" => Fishing.Generate(_s0, _s1, table, start, end, config),
                                    _ => Hidden.Generate(_s0, _s1, table, start, end, config),
                                };

                                tasks.Add(t);

                                if (!last)
                                {
                                    for (ulong j = 0; j < interval; j++)
                                    {
                                        rng.Next();
                                    }
                                }
                            }

                            await Task.Run(async () =>
                            {
                                results = await Task.WhenAll(tasks).ConfigureAwait(false);
                                List<OverworldFrame> AllResults = [];
                                foreach (var result in results)
                                {
                                    AllResults.AddRange(result);
                                }

                                Frames = AllResults;
                                if (AllResults.Count > 0) found = true;
                                if (AllResults.Count > 1000) AllResults = AllResults[0..1000];
                                //AllResults.Sort();
                                SetBindingSourceDataSource(AllResults, ResultsSource);
                                DGV_Results.SanitizeColumns(this);
                            }).ConfigureAwait(false);
                        }
                    }
                    else
                    {
                        await Task.Delay(5_000, ResetSource.Token).ConfigureAwait(false);
                    }

                    prevs0 = s0;
                    prevs1 = s1;

                    if (!found && Config is ISeedResetConfig cfg)
                    {
                        ct++;
                        if (first)
                        {
                            await ConnectionWrapper.PressL3(ResetSource.Token).ConfigureAwait(false); // First input doesn't always go through
                            first = false;
                        }
                        await ConnectionWrapper.CloseGame(cfg, ResetSource.Token).ConfigureAwait(false);
                        await ConnectionWrapper.OpenGame(cfg, ResetSource.Token).ConfigureAwait(false);
                        UpdateStatus($"Searching... ({ct})");
                    }

                    if (found)
                    {
                        SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_SeedSearch, B_ReadEncounter);
                        SetControlEnabledState(false, B_CancelSkip);
                        if (GetCheckBoxIsChecked(CB_FocusWindow)) ActivateWindow();
                        if (GetCheckBoxIsChecked(CB_PlayTone)) System.Media.SystemSounds.Asterisk.Play();
                        await ConnectionWrapper.PressHome(ResetSource.Token).ConfigureAwait(false);
                        var timeSpan = Stopwatch.GetElapsedTime(sw);
                        var time = $"{timeSpan.Days:00}:{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
                        await Webhook.SendNotification(Frames[0], time, ct, Frames.Count, Frames.Any(x => x.Shiny != "No"), ResetSource.Token).ConfigureAwait(false);
                        await Task.Delay(100, ResetSource.Token).ConfigureAwait(false);
                        Disconnect(ResetSource.Token);
                        if (Frames.Count >= 1_000) MessageBox.Show($"Too many results found, displayed results capped at 1000. Please re-run the search with more restrictive filters or a smaller range of advances.");
                        MessageBox.Show($"Seed result found in {ct:N0} reset{(ct == 1 ? string.Empty : "s")}! Total search time: {time}.{System.Environment.NewLine}Disconnecting Switch.");
                    }
                }
            }
            catch (Exception ex)
            {
                readPause = false;
                skipPause = false;
                resetPause = false;
                reset = true;
                SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_SeedSearch, B_ReadEncounter);
                SetControlEnabledState(false, B_CancelSkip);
                if (ex is not TaskCanceledException)
                {
                    try
                    {
                        await Webhook.SendErrorNotification(ex.Message, "Seed Reset Error", CancellationToken.None).ConfigureAwait(false);
                    }
                    catch { }
                    this.DisplayMessageBox($"Error occurred during Seed Reset routine: {ex.Message}");
                }
                return;
            }

            SetControlEnabledState(true, B_SkipAdvance, B_SkipForward, B_SkipBack, B_Turbo, B_SeedSearch, B_ReadEncounter);
            SetControlEnabledState(false, B_CancelSkip);
            readPause = false;
            skipPause = false;
            resetPause = false;
            reset = true;
        });
    }
    #endregion

    #region UI Methods
    private void UpdateStatus(string status)
    {
        SetTextBoxText(status, TB_Status);
    }

    private void ValidateInputs(string tab)
    {
        // Initial
        var initial = (TextBox)Controls.Find($"TB_{tab}_Initial", true).FirstOrDefault()!;
        if (string.IsNullOrEmpty(GetControlText(initial))) SetTextBoxText("0", initial);

        // Advances
        var advances = (TextBox)Controls.Find($"TB_{tab}_Advances", true).FirstOrDefault()!;
        var adv = GetControlText(advances);
        if (string.IsNullOrEmpty(adv) || adv is "0") SetTextBoxText("1", advances);

        // Seed
        if (string.IsNullOrEmpty(GetControlText(TB_Seed0))) SetTextBoxText("0", TB_Seed0);
        if (string.IsNullOrEmpty(GetControlText(TB_Seed1))) SetTextBoxText("0", TB_Seed1);

        if (GetControlText(TB_Seed0) is "0" && GetControlText(TB_Seed1) is "0")
        {
            SetTextBoxText("1337", TB_Seed0);
            SetTextBoxText("1390", TB_Seed1);
        }
        SetTextBoxText(GetControlText(TB_Seed0).PadLeft(16, '0'), TB_Seed0);
        SetTextBoxText(GetControlText(TB_Seed1).PadLeft(16, '0'), TB_Seed1);

        // IDs
        if (string.IsNullOrEmpty(GetControlText(TB_TID))) SetTextBoxText("0", TB_TID);
        if (string.IsNullOrEmpty(GetControlText(TB_SID))) SetTextBoxText("0", TB_SID);
        SetTextBoxText(GetControlText(TB_TID).PadLeft(5, '0'), TB_TID);
        SetTextBoxText(GetControlText(TB_SID).PadLeft(5, '0'), TB_SID);

        // NPCs
        var npc = (TextBox)Controls.Find($"TB_{tab}_NPCs", true).FirstOrDefault()!;
        if (string.IsNullOrEmpty(GetControlText(npc))) SetTextBoxText("0", npc);

        // KO Count
        var ko = (TextBox?)Controls.Find($"TB_{tab}_KOs", true).FirstOrDefault();
        if (ko is not null && string.IsNullOrEmpty(GetControlText(ko))) SetTextBoxText("0", ko);

        // Marked Advance
        marked = null;
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
#if DEBUG
        if (((Button)sender).Name == "B_CopyToInitial" && ModifierKeys == Keys.Shift)
        {
            Task.Run(
                async () =>
                {
                    try
                    {
                        ulong s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
                        ulong s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);
                        await ConnectionWrapper.WriteRNGState(s0, s1, Source.Token).ConfigureAwait(false);
                        reset = true;
                    }
                    catch (Exception ex)
                    {
                        this.DisplayMessageBox($"Something went wrong when writing the RNG state: {ex.Message}");
                    }
                }
            );
        }
        else
        {
#endif
        if (TB_CurrentS0.Text != string.Empty && TB_CurrentS1.Text != string.Empty)
        {
            SetTextBoxText(TB_CurrentS0.Text, TB_Seed0);
            SetTextBoxText(TB_CurrentS1.Text, TB_Seed1);
            reset = true;
        }
#if DEBUG
        }
#endif
    }

    private void B_RetailUpdateSeeds_Click(object sender, EventArgs e)
    {
        SetTextBoxText("0", TB_CurrentAdvances);
        B_CopyToInitial_Click(sender, e);
    }

    private void TB_SwitchIP_TextChanged(object sender, EventArgs e)
    {
        if (Config.Protocol is SwitchProtocol.WiFi)
        {
            Config.IP = TB_SwitchIP.Text;
            ConnectionConfig.IP = TB_SwitchIP.Text;
        }
        else
        {
            if (int.TryParse(TB_SwitchIP.Text, out int port) && port >= 0)
            {
                Config.UsbPort = port;
                ConnectionConfig.Port = port;
                return;
            }

            MessageBox.Show("Please enter a valid numerical USB port.");
        }
    }

    private void NUD_Leave(object sender, EventArgs e)
    {
        var nud = (NumericUpDown)sender;
        if (string.IsNullOrEmpty(nud.Text))
        {
            nud.Text = "0";
        }
    }

    private void TID_TextChanged(object sender, EventArgs e)
    {
        if (TB_TID.Text.Length > 0)
        {
            var tid = int.Parse(TB_TID.Text);
            Config.TID = tid;
        }
    }

    private void SID_TextChanged(object sender, EventArgs e)
    {
        if (TB_SID.Text.Length > 0)
        {
            var sid = int.Parse(TB_SID.Text);
            Config.SID = sid;
        }
    }

    private void CB_Game_SelectedIndexChanged(object sender, EventArgs e)
    {
        Config.Game = CB_Game.SelectedIndex;
    }

    private void CB_ShinyCharm_CheckedChanged(object sender, EventArgs e)
    {
        Config.HasShinyCharm = CB_ShinyCharm.Checked;
    }

    private void CB_MarkCharm_CheckedChanged(object sender, EventArgs e)
    {
        Config.HasMarkCharm = CB_MarkCharm.Checked;
    }

    private void CB_PlayTone_CheckedChanged(object sender, EventArgs e)
    {
        Config.PlayTone = CB_PlayTone.Checked;
    }

    private void CB_FocusWindow_CheckedChanged(object sender, EventArgs e)
    {
        Config.FocusWindow = CB_FocusWindow.Checked;
    }

    private readonly JsonSerializerOptions options = new() { WriteIndented = true };
    private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
        var configpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        string output = JsonSerializer.Serialize(Config, options);
        using StreamWriter sw = new(configpath);
        sw.Write(output);

        if (ConnectionWrapper is { Connected: true })
        {
            try
            {
                _ = ConnectionWrapper.DisconnectAsync(Source.Token).ConfigureAwait(false);
            }
            catch
            {
                // ignored
            }
        }

        Source.Cancel();
        AdvanceSource.Cancel();
        ResetSource.Cancel();
        Source = new();
        AdvanceSource = new();
        ResetSource = new();
    }

    private void B_RefreshDexRec_Click(object sender, EventArgs e)
    {
        Task.Run(
            async () =>
            {
                try
                {
                    readPause = true;
                    await Task.Delay(100, Source.Token).ConfigureAwait(false);

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

    public bool OverworldScannerFormOpen = false;
    OverworldScanner? OverworldScannerForm;
    private void B_ReadEncounter_Click(object sender, EventArgs e)
    {
        if (ModifierKeys == Keys.Shift)
        {
            try
            {
                if (!OverworldScannerFormOpen)
                {
                    SetControlEnabledState(false, B_ReadEncounter);
                    readPause = true;

                    Task.Run(
                        async () =>
                        {
                            await Task.Delay(100, Source.Token).ConfigureAwait(false);
                            await ConnectionWrapper.ReadKCoordinatesAsync(Source.Token).ConfigureAwait(false);
                            readPause = false;
                            SetControlEnabledState(true, B_ReadEncounter);
                            var (pk8s, x, y, z, map) = ConnectionWrapper.GetOverworldPK8FromKCoordinates();

                            if (pk8s.Count > 0)
                            {
                                OverworldScannerFormOpen = true;
                                OverworldScannerForm = new OverworldScanner(this, [.. pk8s], x, y, z, map);
                                OverworldScannerForm.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("No Pokémon found in KCoordinates block! If you think there should be any, save the game in an area with wild spawns and try again.");
                            }
                        }
                    );
                }
                else
                {
                    FocusControl(OverworldScannerForm);
                }
            }
            catch (Exception ex)
            {
                readPause = false;
                SetControlEnabledState(true, B_ReadEncounter);
                this.DisplayMessageBox($"Error occurred while attempting to read KCoordinates block: {ex.Message}");
                return;
            }
        }
        else
        {
            Task.Run(
                async () =>
                {
                    try
                    {
                        readPause = true;
                        await Task.Delay(100, Source.Token).ConfigureAwait(false);
                        SetTextBoxText("Reading encounter...", TB_Wild);
                        var pk = await ConnectionWrapper.ReadWildPokemon(Source.Token).ConfigureAwait(false);
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
                                < 16 => "★ - ",
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
                            if (HasRibbon)
                            {
                                SetPictureBoxImage(RibbonSpriteUtil.GetRibbonSprite(mark)!, PB_MarkSprite);
                            }
                            else
                            {
                                PB_MarkSprite.Image = null;
                            }
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

    private EncounterTable GetCurrentTable()
    {
        var type = TC_EncounterType.SelectedTab!.Text;

        return new EncounterTable(
            GetControlText(CB_Game),
            type,
            GetControlText((ComboBox)Controls.Find($"CB_{type}_Area", true).FirstOrDefault()!),
            GetControlText((ComboBox)Controls.Find($"CB_{type}_Weather", true).FirstOrDefault()!),
            GetControlText((ComboBox)Controls.Find($"CB_{type}_LeadAbility", true).FirstOrDefault()!)
        );
    }

    private void CB_Target_TextChanged(object sender, EventArgs e)
    {
        var type = TC_EncounterType.SelectedTab!.Text;

        CB_Filter_Shiny.Enabled = true;
        CB_Filter_Aura.Enabled = true;
        CB_ConsiderFlying.Enabled = true;

        if (type == "Static")
        {
            var species = ((ComboBox)sender).Text;
            var enc = GetCurrentTable().StaticTable.Where(e => e.Value.Species == species).FirstOrDefault();
            if (species != string.Empty && enc.Value is not null) CB_Filter_Shiny.Enabled = !enc.Value.IsShinyLocked;

            CB_Filter_Aura.Enabled = false;
        }
        else if (type == "Hidden")
        {
            CB_Filter_Aura.Enabled = false;
            CB_ConsiderFlying.Checked = false;
            CB_ConsiderFlying.Enabled = false;
        }
        else if (type == "Fishing")
        {
            CB_ConsiderFlying.Enabled = false;
            CB_ConsiderFlying.Enabled = false;
        }
    }

    private void B_IV_Max_Click(object sender, EventArgs e)
    {
        var st = ((Button)sender).Name.Replace("B_", string.Empty).Replace("_Max", string.Empty);
        List<string> stats = ModifierKeys == Keys.Shift ? ["HP", "Atk", "Def", "SpA", "SpD", "Spe"] : [st];
        foreach (var stat in stats)
        {
            var min = (NumericUpDown)Controls.Find($"NUD_{stat}_Min", true).FirstOrDefault()!;
            var max = (NumericUpDown)Controls.Find($"NUD_{stat}_Max", true).FirstOrDefault()!;
            min.Value = 31;
            max.Value = 31;
        }
    }

    private void B_IV_Min_Click(object sender, EventArgs e)
    {
        var st = ((Button)sender).Name.Replace("B_", string.Empty).Replace("_Min", string.Empty);
        List<string> stats = ModifierKeys == Keys.Shift ? ["HP", "Atk", "Def", "SpA", "SpD", "Spe"] : [st];
        foreach (var stat in stats)
        {
            var min = (NumericUpDown)Controls.Find($"NUD_{stat}_Min", true).FirstOrDefault()!;
            var max = (NumericUpDown)Controls.Find($"NUD_{stat}_Max", true).FirstOrDefault()!;
            min.Value = 0;
            max.Value = 0;
        }
    }


    private void IV_Label_Click(object sender, EventArgs e)
    {
        var st = ((Label)sender).Name.Replace("L_", string.Empty);
        List<string> stats = ModifierKeys == Keys.Shift ? ["HP", "Atk", "Def", "SpA", "SpD", "Spe"] : [st];
        foreach (var stat in stats)
        {
            var min = (NumericUpDown)Controls.Find($"NUD_{stat}_Min", true).FirstOrDefault()!;
            var max = (NumericUpDown)Controls.Find($"NUD_{stat}_Max", true).FirstOrDefault()!;
            min.Value = 0;
            max.Value = 31;
        }
    }

    private void CB_MenuClose_CheckedChanged(object sender, EventArgs e)
    {
        var cb = (CheckBox)sender;
        var tab = cb.Name.Replace("CB_", string.Empty).Replace("_MenuClose", string.Empty);
        SetControlEnabledState(cb.Checked, Controls.Find($"TB_{tab}_NPCs", true).FirstOrDefault()!, Controls.Find($"B_{tab}_MenuClose", true).FirstOrDefault()!, Controls.Find($"CB_{tab}_MenuClose_Direction", true).FirstOrDefault()!);
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

    private void L_ResetComboBox(object sender, EventArgs e)
    {
        var name = ((Label)sender).Name;
        var cbName = name.Replace("L_", "CB_");
        var cb = (ComboBox?)Controls.Find(cbName, true).FirstOrDefault();
        if (cb is not null)
        {
            if (cb.Enabled) SetComboBoxSelectedIndex(0, cb);
        }
    }

    private void ActivateWindow()
    {
        if (InvokeRequired)
            Invoke(Activate);
        else
            Activate();
    }

    private void FocusControl(params Control?[] obj)
    {
        foreach (Control? c in obj)
        {
            if (InvokeRequired)
            {
                Invoke(() => c?.Focus());
            }
            else
            {
                c?.Focus();
            }
        }
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
                Invoke(() => tb.Text = text);
            else
                tb.Text = text;
        }
    }

    public void SetButtonText(string text, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not Button b)
                continue;

            if (InvokeRequired)
                Invoke(() => b.Text = text);
            else
                b.Text = text;
        }
    }

    private void SetPictureBoxImage(Image img, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not PictureBox pb)
                continue;

            if (InvokeRequired)
                Invoke(() => pb.Image = img);
            else
                pb.Image = img;
        }
    }

    public void SetComboBoxSelectedIndex(int index, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not ComboBox cb)
                continue;

            if (InvokeRequired)
                Invoke(() => cb.SelectedIndex = index);
            else
                cb.SelectedIndex = index;
        }
    }

    public void SetBindingSourceDataSource(object source, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not BindingSource b)
                continue;

            if (InvokeRequired)
                Invoke(() => b.DataSource = source);
            else
                b.DataSource = source;
        }
    }

    public void SetDataGridViewColumnVisibility(bool visible, int index, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not DataGridView dgv)
                continue;

            if (InvokeRequired)
                Invoke(() => dgv.Columns[index].Visible = visible);
            else
                dgv.Columns[index].Visible = visible;
        }
    }

    public string GetControlText(Control c)
    {
        return (InvokeRequired ? Invoke(() => c.Text) : c.Text);
    }

    public TabPage? GetTabControlTab(TabControl c)
    {
        return (InvokeRequired ? Invoke(() => c.SelectedTab) : c.SelectedTab);
    }

    public bool GetCheckBoxIsChecked(CheckBox c)
    {
        return (InvokeRequired ? Invoke(() => c.Checked) : c.Checked);
    }

    public uint GetNUDValue(NumericUpDown c)
    {
        return (uint)(InvokeRequired ? Invoke(() => c.Value) : c.Value);
    }

    public int GetComboBoxSelectedIndex(ComboBox c)
    {
        return (InvokeRequired ? Invoke(() => c.SelectedIndex) : c.SelectedIndex);
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

    public static readonly Font BoldFont = new("Microsoft Sans Serif", 8, FontStyle.Bold);
    public static readonly Color DefaultBackBlue = Color.FromName("Highlight");
    private void DGV_Results_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        var index = e.RowIndex;
        if (Frames.Count <= index) return;
        var row = DGV_Results.Rows[index];
        var result = Frames[index];

        if (marked is not null && result.Advances == marked)
        {
            row.DefaultCellStyle.BackColor = Color.LightPink;
            row.DefaultCellStyle.SelectionBackColor = Color.MediumVioletRed;
        }
        else
        {
            row.DefaultCellStyle.SelectionBackColor = DefaultBackBlue;
            if (result.Shiny is "Square") row.DefaultCellStyle.BackColor = Color.LightCyan;
            else if (result.Shiny is "Star") row.DefaultCellStyle.BackColor = Color.Aqua;
            else if (result.Step == 1) row.DefaultCellStyle.BackColor = Color.Honeydew;
            else if (result.Brilliant == 'Y') row.DefaultCellStyle.BackColor = Color.PapayaWhip;
            else row.DefaultCellStyle.BackColor = row.Index % 2 == 0 ? Color.White : Color.WhiteSmoke;
        }

        const int iv = 11;
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
                System.Media.SystemSounds.Asterisk.Play();
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
                System.Media.SystemSounds.Asterisk.Play();
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
                System.Media.SystemSounds.Asterisk.Play();
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
                System.Media.SystemSounds.Asterisk.Play();
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
            TB_CurrentS0.Text = rsf.Seed0;
            TB_CurrentS1.Text = rsf.Seed1;
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

    public bool SpreadFinderFormOpen = false;
    Subforms.SpreadFinder? SpreadFinderForm;
    private void TSMI_SpreadFinder_Click(object sender, EventArgs e)
    {
        if (!SpreadFinderFormOpen)
        {
            SpreadFinderFormOpen = true;
            SpreadFinderForm = new Subforms.SpreadFinder(this);
            SpreadFinderForm.Show();
        }
        else
        {
            SpreadFinderForm?.Focus();
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

    public bool DiggingPaFormOpen = false;
    DiggingPa? DiggingPaForm;
    private void TSMI_DiggingPa_Click(object sender, EventArgs e)
    {
        if (!DiggingPaFormOpen)
        {
            DiggingPaFormOpen = true;
            DiggingPaForm = new DiggingPa(this, TC_EncounterType.SelectedTab!.Text);
            DiggingPaForm.Show();
        }
        else
        {
            DiggingPaForm?.Focus();
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

    public bool SeedSearchFormOpen = false;
    SeedResetSettings? SeedResetSettingsForm;
    private void B_SeedSearch_Settings_Click(object sender, EventArgs e)
    {
        if (!SeedSearchFormOpen)
        {
            SeedSearchFormOpen = true;
            SeedResetSettingsForm = new SeedResetSettings(ref Config, this);
            SeedResetSettingsForm?.Show();
        }
        else
        {
            SeedResetSettingsForm?.Focus();
        }
    }

    public bool TurboSettingsFormOpen = false;
    TurboSettings? TurboSettingsForm;
    private void B_TurboSettings_Click(object sender, EventArgs e)
    {
        if (!TurboSettingsFormOpen)
        {
            TurboSettingsFormOpen = true;
            TurboSettingsForm = new TurboSettings(ref Config, this);
            TurboSettingsForm?.Show();
        }
        else
        {
            TurboSettingsForm?.Focus();
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
        if (string.IsNullOrEmpty(TB_RetailInitial.Text)) TB_RetailInitial.Text = "0";
        if (string.IsNullOrEmpty(TB_RetailRange.Text) || TB_RetailRange.Text is "0") TB_RetailRange.Text = "99999";

        if (string.IsNullOrEmpty(TB_Seed0.Text)) TB_Seed0.Text = "0";
        if (string.IsNullOrEmpty(TB_Seed1.Text)) TB_Seed1.Text = "0";
        if (TB_Seed0.Text is "0" && TB_Seed1.Text is "0")
        {
            TB_Seed0.Text = "1337";
            TB_Seed1.Text = "1390";
        }

        TB_Seed0.Text = TB_Seed0.Text.PadLeft(16, '0');
        TB_Seed1.Text = TB_Seed1.Text.PadLeft(16, '0');

        var s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
        var s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);
        var ini = ulong.Parse(TB_RetailInitial.Text);
        var adv = ulong.Parse(TB_RetailRange.Text);
        RetailInitial = ini;
        (RetailSequence, RetailSeed0, RetailSeed1) = SeedFinder.GenerateAnimationSequence(s0, s1, ini, adv);
        System.Media.SystemSounds.Asterisk.Play();
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

    #region Context Menu
    private void CMS_RightClick_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = !(DGV_Results.
            CurrentRow?.Index >= 0);
    }

    private void TSMI_CopySeeds_Click(object sender, EventArgs e)
    {
        try
        {
            var s0 = DGV_Results.CurrentRow!.Cells[23].Value;
            var s1 = DGV_Results.CurrentRow!.Cells[24].Value;
            Clipboard.SetText($"{s0}{System.Environment.NewLine}{s1}");
        }
        catch (NullReferenceException)
        {
            this.DisplayMessageBox("No row selected!");
        }
    }

    private void TSMI_SetAsInitial_Click(object sender, EventArgs e)
    {
        try
        {
            var s0 = DGV_Results.CurrentRow!.Cells[23].Value;
            var s1 = DGV_Results.CurrentRow!.Cells[24].Value;
            TB_Seed0.Text = $"{s0}";
            TB_Seed1.Text = $"{s1}";
        }
        catch (NullReferenceException)
        {
            this.DisplayMessageBox("No row selected!");
        }
    }

    private void TSMI_SetAdvances_Click(object sender, EventArgs e)
    {
        try
        {
            var adv = DGV_Results.CurrentRow!.Cells[0].Value;
            var tab = TC_EncounterType.SelectedTab!.Text;
            ((TextBox)Controls.Find($"TB_{tab}_Initial", true).FirstOrDefault()!).Text = $"{adv}";
        }
        catch (NullReferenceException)
        {
            this.DisplayMessageBox("No row selected!");
        }
    }

    private string? marked;
    private void TSMI_MarkAdvance_Click(object sender, EventArgs e)
    {
        try
        {
            var val = $"{DGV_Results.CurrentRow!.Cells[0].Value}";
            if (marked is not null && marked == val) marked = null;
            else marked = val;
        }
        catch (NullReferenceException)
        {
            this.DisplayMessageBox("No row selected!");
        }
    }

    private void DGV_Results_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button is MouseButtons.Right)
        {
            var row = DGV_Results.HitTest(e.X, e.Y).RowIndex;
            if (row is not -1)
            {
                DGV_Results.ClearSelection();
                DGV_Results.Rows[row].Selected = true;
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
            var tab = mw.GetTabControlTab(mw.TC_EncounterType);
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

                    if (tab is not null)
                    {
                        var text = mw.GetControlText(tab);
                        if (col.HeaderText is "Step")
                        {
                            mw.SetDataGridViewColumnVisibility(text is "Hidden", col.Index, dgv);
                        }

                        if (col.HeaderText is "Jump")
                        {
                            var mc = (CheckBox?)mw.Controls.Find($"CB_{text}_MenuClose", true).FirstOrDefault();
                            var check = false;
                            if (mc is not null) check = mw.GetCheckBoxIsChecked(mc);
                            var vis = mw.GetCheckBoxIsChecked(mw.CB_ConsiderFlying) || mw.GetCheckBoxIsChecked(mw.CB_ConsiderRain) || check;
                            mw.SetDataGridViewColumnVisibility(vis, col.Index, dgv);
                        }
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
