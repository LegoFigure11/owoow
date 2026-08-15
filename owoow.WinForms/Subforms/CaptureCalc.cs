using owoow.Core.Connection;
using owoow.Core.Enums;
using owoow.Core.Interfaces;
using owoow.Core.Structures;
using PKHeX.Core;
using SysBot.Base;
using System.Globalization;
using static owoow.Core.RNG.Util;

namespace owoow.WinForms.Subforms;

public partial class CaptureCalc : Form
{
    readonly MainWindow MainWindow;
    readonly uint Offset;
    readonly ClientConfig cfg;

    private static readonly Lock _connectLock = new();

    private ConnectionWrapperAsync ConnectionWrapper = default!;
    private SwitchConnectionConfig ConnectionConfig;

    public bool SubformOpen = false;

    CancellationTokenSource Source = new();

    List<CaptureFrame> Frames = [];

    private bool reset = true;
    private bool stop;
    public bool readPause;

    public CaptureCalc(MainWindow f, ref ClientConfig o)
    {
        InitializeComponent();

        MainWindow = f;
        cfg = o;
        Offset = o.BattleRNGOffset;

        ConnectionConfig = new()
        {
            IP = cfg.IP,
            Port = cfg.Protocol is SwitchProtocol.WiFi ? 6000 : cfg.UsbPort,
            Protocol = cfg.Protocol,
        };

        TB_Seed0.KeyPress += MainWindow.KeyPress_AllowOnlyHex!;
        TB_Seed1.KeyPress += MainWindow.KeyPress_AllowOnlyHex!;
        TB_Seed0.KeyDown += MainWindow.State_HandlePaste!;
        TB_Seed1.KeyDown += MainWindow.State_HandlePaste!;

        TB_Capture_Initial.KeyPress += MainWindow.KeyPress_AllowOnlyNumerical!;
        TB_Capture_Advances.KeyPress += MainWindow.KeyPress_AllowOnlyNumerical!;
        TB_Capture_Initial.KeyDown += MainWindow.Dec_HandlePaste!;
        TB_Capture_Advances.KeyDown += MainWindow.Dec_HandlePaste!;
    }

    private void CaptureCalc_Load(object sender, EventArgs e)
    {
        CenterToScreen();

        if (cfg.Protocol is SwitchProtocol.WiFi)
        {
            TB_SwitchIP.Text = cfg.IP;
        }
        else
        {
            L_SwitchIP.Text = "USB Port:";
            TB_SwitchIP.Text = $"{cfg.UsbPort}";
        }

        CB_Charm.Checked = cfg.HasCatchingCharm;

        MainWindow.SetControlText("0", TB_Seed0, TB_Seed1);
        MainWindow.SetControlText(string.Empty, TB_CurrentAdvances, TB_AdvancesIncrease, TB_CurrentS0, TB_CurrentS1);

        TB_Status.Text = "Not Connected.";

        for (ushort i = 1; i < 899; i++)
        {
            var name = SpeciesName.GetSpeciesNameGeneration(i, 2, 8);
            CB_Active_Species.Items.Add(name);
            CB_Wild_Species.Items.Add(name);
        }

        MainWindow.SetComboBoxSelectedIndex(0,
            CB_Active_Species, CB_Active_Gender, CB_Wild_Species, CB_Wild_Gender, CB_Wild_Status,
            CB_Ball, CB_TargetCrit, CB_TargetSuccess
            );

        MainWindow.SetCheckBoxCheckedState(cfg.BeatRaihan, CB_8thBadge);
        MainWindow.SetNUDValue(cfg.SpeciesRegisteredInDex, NUD_ZukanCaught);
    }

    private async void DexRecSearcher_FormClosing(object sender, FormClosingEventArgs e)
    {
        stop = true;
        await Source.CancelAsync().ConfigureAwait(false);
        Source.Dispose();
        Source = new();
    }

    private void CB_Ball_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch ((BallType)CB_Ball.GetSelectedIndex())
        {
            case BallType.DiveBall:
                MainWindow.SetControlEnabledState(true, CB_Fishing, CB_Surfing);
                MainWindow.SetControlEnabledState(false, CB_Registered, CB_Dusk, CB_First, L_Turns, NUD_Turns);
                break;

            case BallType.DuskBall:
                MainWindow.SetControlEnabledState(true, CB_Dusk);
                MainWindow.SetControlEnabledState(false, CB_Fishing, CB_Surfing, CB_Registered, CB_First, L_Turns, NUD_Turns);
                break;

            case BallType.LureBall:
                MainWindow.SetControlEnabledState(true, CB_Fishing);
                MainWindow.SetControlEnabledState(false, CB_Surfing, CB_Registered, CB_Dusk, CB_First, L_Turns, NUD_Turns);
                break;

            case BallType.QuickBall:
                MainWindow.SetControlEnabledState(true, CB_First);
                MainWindow.SetControlEnabledState(false, CB_Fishing, CB_Surfing, CB_Registered, CB_Dusk, L_Turns, NUD_Turns);
                break;

            case BallType.RepeatBall:
                MainWindow.SetControlEnabledState(true, CB_Registered);
                MainWindow.SetControlEnabledState(false, CB_Fishing, CB_Surfing, CB_Dusk, CB_First, L_Turns, NUD_Turns);
                break;

            case BallType.TimerBall:
                MainWindow.SetControlEnabledState(true, L_Turns, NUD_Turns);
                MainWindow.SetControlEnabledState(false, CB_Fishing, CB_Surfing, CB_Registered, CB_Dusk, CB_First);
                break;

            // All other balls don't use any of these, so we can disable them by default
            default:
                MainWindow.SetControlEnabledState(false, CB_Fishing, CB_Surfing, CB_Registered, CB_Dusk, CB_First, L_Turns, NUD_Turns);
                break;
        }
    }

    private async void B_ReadParty_Click(object sender, EventArgs e)
    {
        try
        {
            readPause = true;
            MainWindow.SetControlEnabledState(false, B_ReadWild, B_ReadParty);
            await Task.Delay(100, Source.Token);
            var partyMon = await ConnectionWrapper.ReadPartyPokemon((byte)(NUD_PartySlot.GetValue() - 1), Source.Token).ConfigureAwait(false);
            
            MainWindow.SetNUDValue(partyMon.CurrentLevel, NUD_Active_Level);
            MainWindow.SetComboBoxSelectedIndex(partyMon.Gender, CB_Active_Gender);
            MainWindow.SetComboBoxSelectedIndex(Math.Max(partyMon.Species - 1, 0), CB_Active_Species);

            readPause = false;
            MainWindow.SetControlEnabledState(true, B_ReadWild, B_ReadParty);
        }
        catch (Exception ex)
        {
            readPause = false;
            this.DisplayMessageBox(ex.Message);
            MainWindow.SetControlEnabledState(true, B_ReadWild, B_ReadParty);
        }

    }

    private async void B_ReadWild_Click(object sender, EventArgs e)
    {
        try
        {
            readPause = true;
            MainWindow.SetControlEnabledState(false, B_ReadParty, B_ReadWild);
            await Task.Delay(100, Source.Token);
            var wildMon = await ConnectionWrapper.ReadWildPokemon(Source.Token).ConfigureAwait(false);
            
            if (wildMon is { Valid: true, Species: > 0 })
            {
                wildMon.ResetPartyStats();

                var currHP = await ConnectionWrapper.ReadWildPokemonCurrentHP(Source.Token).ConfigureAwait(false);
                var status = await ConnectionWrapper.ReadWildPokemonStatusCondition(Source.Token).ConfigureAwait(false);

                MainWindow.SetNUDValue(wildMon.CurrentLevel, NUD_Wild_Level);
                MainWindow.SetNUDValue(wildMon.Stat_HPMax, NUD_Wild_MaxHP);
                MainWindow.SetNUDValue(currHP, NUD_Wild_CurHP);

                MainWindow.SetNUDValue(wildMon.PersonalInfo.CatchRate, NUD_Wild_Rate);
                MainWindow.SetNUDValue(wildMon.PersonalInfo.Weight, NUD_Wild_Weight);
                MainWindow.SetComboBoxSelectedIndex(wildMon.Gender, CB_Wild_Gender);
                MainWindow.SetComboBoxSelectedIndex(GetSelectedIndexForStatusType(status), CB_Wild_Status);
                MainWindow.SetComboBoxSelectedIndex(wildMon.Species - 1, CB_Wild_Species);
                MainWindow.SetNUDValue(wildMon.Form, NUD_Wild_Form);
            }
            else
            {
                this.DisplayMessageBox("No encounter present.");
            }
            readPause = false;
            MainWindow.SetControlEnabledState(true, B_ReadParty, B_ReadWild);

        }
        catch (Exception ex)
        {
            readPause = false;
            this.DisplayMessageBox(ex.Message);
            MainWindow.SetControlEnabledState(true, B_ReadParty, B_ReadWild);
        }
    }

    private static int GetSelectedIndexForStatusType(StatusType type) => type switch
    {
        StatusType.Poison => 5,
        StatusType.Paralysis => 4,
        StatusType.Freeze => 3,
        StatusType.Burn => 2,
        StatusType.Sleep => 1,
        _ => 0,
    };

    private static StatusType GetStatusTypeForSelectedIndex(int idx) => idx switch
    {
        5 => StatusType.Poison,
        4 => StatusType.Paralysis,
        3 => StatusType.Freeze,
        2 => StatusType.Burn,
        1 => StatusType.Sleep,
        _ => StatusType.None,
    };

    private readonly PK8 _pk = new();
    private void CB_Wild_Species_SelectedIndexChanged(object sender, EventArgs e)
    {
        var idx = CB_Wild_Species.GetSelectedIndex();
        var f = NUD_Wild_Form.GetValue();
        _pk.Species = (ushort)(idx + 1);
        _pk.Form = (byte)f;
        MainWindow.SetNUDValue(_pk.PersonalInfo.CatchRate, NUD_Wild_Rate);
        MainWindow.SetNUDValue(_pk.PersonalInfo.Weight, NUD_Wild_Weight);
    }

    private void B_Search_Click(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(false, sender);

        if (string.IsNullOrEmpty(TB_Capture_Initial.Text)) TB_Capture_Initial.Text = "0";
        if (string.IsNullOrEmpty(TB_Capture_Advances.Text) || TB_Capture_Advances.Text is "0") TB_Capture_Advances.Text = "1";

        if (string.IsNullOrEmpty(TB_Seed0.Text)) TB_Seed0.Text = "0";
        if (string.IsNullOrEmpty(TB_Seed1.Text)) TB_Seed1.Text = "0";
        if (TB_Seed0.Text is "0" && TB_Seed1.Text is "0")
        {
            TB_Seed0.Text = "1337";
            TB_Seed1.Text = "1390";
        }

        TB_Seed0.Text = TB_Seed0.Text.PadLeft(16, '0');
        TB_Seed1.Text = TB_Seed1.Text.PadLeft(16, '0');

        var initial = ulong.Parse(TB_Capture_Initial.Text);
        var advances = ulong.Parse(TB_Capture_Advances.Text);

        var s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
        var s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);

        var pk = new PK8()
        {
            Species = (ushort)(CB_Wild_Species.GetSelectedIndex() + 1),
            Form = (byte)(NUD_Wild_Form.GetValue()),
        };

        (s0, s1) = XoroshiroJump(s0, s1, initial);

        Core.RNG.GeneratorConfig config = new()
        {
            Species = pk.Species,
            PartySpecies = CB_Active_Species.GetSelectedIndex(),
            Level = (byte)NUD_Wild_Level.GetValue(),
            PartyLevel = (byte)NUD_Active_Level.GetValue(),
            Gender = (byte)CB_Wild_Gender.GetSelectedIndex(),
            PartyGender = (byte)CB_Active_Gender.GetSelectedIndex(),

            MaxHP = (ushort)NUD_Wild_MaxHP.GetValue(),
            CurrHP = (ushort)NUD_Wild_CurHP.GetValue(),
            CatchRate = (byte)NUD_Wild_Rate.GetValue(),
            Weight = (ushort)NUD_Wild_Weight.GetValue(),

            HasCatchingCharm = CB_Charm.GetIsChecked(),
            ZukanCount = (ushort)NUD_ZukanCaught.GetValue(),
            Ball = (BallType)CB_Ball.GetSelectedIndex(),
            IsUltraBeast = pk.IsUltraBeast(),
            Status = GetStatusTypeForSelectedIndex(CB_Wild_Status.GetSelectedIndex()),

            Fishing = CB_Fishing.GetIsChecked(),
            Surfing = CB_Surfing.GetIsChecked(),
            Registered = CB_Registered.GetIsChecked(),
            Dusk = CB_Dusk.GetIsChecked(),
            FirstTurn = CB_First.GetIsChecked(),
            Turns = (byte)NUD_Turns.GetValue(),
            MoonStoneEvo = pk.IsMoonBallBoosted(),
            BaseSpeed = (byte)pk.PersonalInfo.SPE,
            BeatRaihan = CB_8thBadge.GetIsChecked(),
            IsBugOrWaterType = pk.PersonalInfo.IsType((byte)MoveType.Water) || pk.PersonalInfo.IsType((byte)MoveType.Bug),

            TargetCrit = (AuraType)CB_TargetCrit.GetSelectedIndex(),
            TargetSuccess = (AuraType)CB_TargetSuccess.GetSelectedIndex(),
            TargetMinRolls = NUD_ShakesMin.GetValue(),
            TargetMaxRolls = NUD_ShakesMax.GetValue(),
        };

        Task.Run(async () =>
        {
            Frames = await Core.RNG.Generators.Misc.Capture.Generate(s0, s1, initial, initial + advances, config).ConfigureAwait(false);

            MainWindow.SetBindingSourceDataSource(Frames, CaptureResultsSource);

            MainWindow.SetControlEnabledState(true, sender);
        });
    }

    private void NUD_Wild_Form_ValueChanged(object sender, EventArgs e)
    {
        var idx = CB_Wild_Species.GetSelectedIndex();
        var f = NUD_Wild_Form.GetValue();
        _pk.Species = (ushort)(idx + 1);
        _pk.Form = (byte)f;
        MainWindow.SetNUDValue(_pk.PersonalInfo.CatchRate, NUD_Wild_Rate);
        MainWindow.SetNUDValue(_pk.PersonalInfo.Weight, NUD_Wild_Weight);
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
                        if (ConnectionWrapper.Connected)
                            await ConnectionWrapper.WriteRNGState(s0, s1, Offset, Source.Token).ConfigureAwait(false);
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
                var s0 = TB_CurrentS0.Text;
                var s1 = TB_CurrentS1.Text;

                MainWindow.SetControlText(s0, TB_Seed0);
                MainWindow.SetControlText(s1, TB_Seed1);

                reset = true;
            }
#if DEBUG
        }
#endif
    }

    private void CB_Charm_CheckedChanged(object sender, EventArgs e)
    {
        cfg.HasCatchingCharm = CB_Charm.GetIsChecked();
    }

    private void CB_8thBadge_CheckedChanged(object sender, EventArgs e)
    {
        cfg.BeatRaihan = CB_8thBadge.GetIsChecked();
    }

    private void NUD_ZukanCaught_ValueChanged(object sender, EventArgs e)
    {
        cfg.SpeciesRegisteredInDex = NUD_ZukanCaught.GetValue();
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

    private void UpdateStatus(string status)
    {
        MainWindow.SetControlText(status, TB_Status);
    }

    private void Connect(CancellationToken token)
    {
        Task.Run(
            async () =>
            {
                MainWindow.SetControlEnabledState(false, B_Connect);
                try
                {
                    ConnectionConfig = new()
                    {
                        IP = TB_SwitchIP.GetText(),
                        Protocol = cfg.Protocol,
                        Port = cfg.Protocol is SwitchProtocol.WiFi ? 6000 : cfg.UsbPort,
                    };
                    ConnectionWrapper = new(ConnectionConfig, UpdateStatus);
                    (bool success, string err) = await ConnectionWrapper
                        .Connect(token)
                        .ConfigureAwait(false);
                    if (!success)
                    {
                        MainWindow.SetControlEnabledState(true, B_Connect);
                        this.DisplayMessageBox(err);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MainWindow.SetControlEnabledState(true, B_Connect);
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
                var skippedGameCheck = false;
                if ((ModifierKeys & Keys.Shift) != Keys.Shift)
                {
                    if (game is "")
                    {
                        try
                        {
                            (bool success, string err) = await ConnectionWrapper
                                .DisconnectAsync(token)
                                .ConfigureAwait(false);
                            if (!success)
                            {
                                MainWindow.SetControlEnabledState(true, B_Connect);
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
                            MainWindow.SetControlEnabledState(true, B_Connect);
                            this.DisplayMessageBox(
                                "Unable to detect Pokémon Sword or Pokémon Shield running on your Switch!");
                        }

                        return;
                    }
                }
                else
                {
                    if (game is "")
                    {
                        skippedGameCheck = true;
                        UpdateStatus("Connected! (forced)");
                        this.DisplayMessageBox(
                            "Unable to detect Pokémon Sword or Pokémon Shield running on your Switch, but forcing connection anyway as Shift was held.");
                    }
                    MainWindow.SetControlEnabledState(true, B_Disconnect);

                }
                if (!skippedGameCheck)
                {
                    MainWindow.SetCheckBoxCheckedState(ConnectionWrapper.GetHasCatchingCharm(), CB_Charm);

                    UpdateStatus("Reading RNG State...");
                    ulong _s0, _s1;
                    try
                    {
                        (_s0, _s1) = await ConnectionWrapper.ReadRNGState(Offset, token).ConfigureAwait(false);
                        MainWindow.SetControlText($"{_s0:X16}", TB_Seed0, TB_CurrentS0);
                        MainWindow.SetControlText($"{_s1:X16}", TB_Seed1, TB_CurrentS1);
                        MainWindow.SetControlText("0", TB_CurrentAdvances, TB_AdvancesIncrease);

                    }
                    catch (Exception ex)
                    {
                        this.DisplayMessageBox($"Error occurred while reading initial RNG state: {ex.Message}");
                        return;
                    }

                    MainWindow.SetControlEnabledState(true, B_Disconnect, B_CopyToInitial, B_ReadParty, B_ReadWild);

                    UpdateStatus("Monitoring RNG State...");
                    try
                    {
                        long total = 0;
                        stop = false;
                        while (!stop)
                        {
                            if (ConnectionWrapper.Connected && !readPause)
                            {
                                var (s0, s1) = await ConnectionWrapper.ReadRNGState(Offset, token).ConfigureAwait(false);
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

                                    MainWindow.SetControlText($"{_s0:X16}", TB_CurrentS0);
                                    MainWindow.SetControlText($"{_s1:X16}", TB_CurrentS1);
                                    MainWindow.SetControlText($"{total:N0}", TB_CurrentAdvances);
                                    MainWindow.SetControlText($"{adv:N0}", TB_AdvancesIncrease);
                                }
                            }
                        }
                    }
                    catch
                    {
                        // Ignored
                    }
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
                MainWindow.SetControlEnabledState(false, B_Disconnect, B_ReadWild, B_ReadParty);
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
                MainWindow.SetControlEnabledState(true, B_Connect);
            },
            token
        );
    }

    private void CMS_RightClick_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = !(DGV_Results.CurrentRow?.Index >= 0);
    }

    private void TSMI_CopySeeds_Click(object sender, EventArgs e)
    {
        try
        {
            var s0 = DGV_Results.CurrentRow!.Cells[23].Value;
            var s1 = DGV_Results.CurrentRow!.Cells[24].Value;
            Clipboard.SetText($"{s0}{Environment.NewLine}{s1}");
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
            var s0 = DGV_Results.CurrentRow!.Cells[4].Value;
            var s1 = DGV_Results.CurrentRow!.Cells[5].Value;
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
            MainWindow.SetControlText($"{adv}".Replace(",", string.Empty), TB_Capture_Initial);
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
            var hti = DGV_Results.HitTest(e.X, e.Y);
            if (hti.RowIndex is not -1)
            {
                DGV_Results.CurrentCell = DGV_Results.Rows[hti.RowIndex].Cells[hti.ColumnIndex];
            }
        }
    }
}
