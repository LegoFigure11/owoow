using owoow.Core.Connection;
using owoow.Core.Enums;
using owoow.Core.Interfaces;
using PKHeX.Core;
using System.Globalization;
using static owoow.Core.RNG.Util;

namespace owoow.WinForms.Subforms;

public partial class CaptureCalc : Form
{
    readonly MainWindow MainWindow;
    readonly ConnectionWrapperAsync? ConnectionWrapper;
    readonly uint Offset;
    readonly ClientConfig cfg;
    private readonly string text;

    public bool SubformOpen = false;

    CancellationTokenSource Source = new();

    List<CaptureFrame> Frames = [];

    private bool reset = true;
    public bool readPause;

    public CaptureCalc(MainWindow f, ConnectionWrapperAsync? c, ref ClientConfig o)
    {
        InitializeComponent();

        MainWindow = f;
        ConnectionWrapper = c;
        cfg = o;
        Offset = o.BattleRNGOffset;

        text = Text;

        var readPause = false;
        var stop = false;
        ulong total = 0;

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

        MainWindow.SetCheckBoxCheckedState(o.HashCatchingCharm, CB_Charm);
        MainWindow.SetCheckBoxCheckedState(o.BeatRaihan, CB_8thBadge);
        MainWindow.SetNUDValue(o.SpeciesRegisteredInDex, NUD_ZukanCaught);

        MainWindow.SetControlText(string.Empty, TB_AdvancesIncrease, TB_CurrentAdvances, TB_CurrentS0, TB_CurrentS1);

        TB_Seed0.KeyPress += MainWindow.KeyPress_AllowOnlyHex!;
        TB_Seed1.KeyPress += MainWindow.KeyPress_AllowOnlyHex!;

        if (ConnectionWrapper is not null)
        {
            try
            {
                Task.Run(
                    async () =>
                    {
                        MainWindow.readPause = true;
                        await Task.Delay(100, Source.Token);
                        try
                        {
                            total = 0;
                            stop = false;
                            var (_s0, _s1) = await ConnectionWrapper.ReadRNGState(Offset, Source.Token).ConfigureAwait(false);
                            MainWindow.SetControlText($"{_s0:X16}", TB_Seed0);
                            MainWindow.SetControlText($"{_s1:X16}", TB_Seed1);
                            while (!stop)
                            {
                                if (ConnectionWrapper.Connected && !readPause)
                                {
                                    var (s0, s1) = await ConnectionWrapper.ReadRNGState(Offset, Source.Token).ConfigureAwait(false);
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
                );
            }
            catch
            {
                // Ignored
            }
        }
    }

    private void DexRecSearcher_FormClosing(object sender, FormClosingEventArgs e)
    {
        Source.Cancel();
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

            case BallType.BeastBall or BallType.DreamBall or BallType.FastBall or BallType.FriendBall or
                BallType.SafariBall or BallType.HealBall or BallType.HeavyBall or BallType.LevelBall or
                BallType.LoveBall or BallType.LuxuryBall or BallType.MoonBall or BallType.NestBall or
                BallType.NetBall or BallType.PokeBall or BallType.PremierBall or BallType.SafariBall or
                BallType.SportBall or BallType.UltraBall:
            default:
                MainWindow.SetControlEnabledState(false, CB_Fishing, CB_Surfing, CB_Registered, CB_Dusk, CB_First, L_Turns, NUD_Turns);
                break;
        }
    }

    private async void B_ReadParty_Click(object sender, EventArgs e)
    {
        if (ConnectionWrapper is not null && ConnectionWrapper.Connected)
        {
            try
            {
                readPause = true;
                var partyMon = await ConnectionWrapper.ReadPartyPokemon((byte)(NUD_PartySlot.GetValue() - 1), Source.Token).ConfigureAwait(false);
                readPause = false;
                MainWindow.SetNUDValue(partyMon.CurrentLevel, NUD_Active_Level);
                MainWindow.SetComboBoxSelectedIndex(partyMon.Gender, CB_Active_Gender);
                MainWindow.SetComboBoxSelectedIndex(partyMon.Species - 1, CB_Active_Species);
            }
            catch (Exception ex)
            {
                readPause = false;
                this.DisplayMessageBox(ex.Message);
            }
        }
    }

    private async void B_ReadWild_Click(object sender, EventArgs e)
    {
        if (ConnectionWrapper is not null && ConnectionWrapper.Connected)
        {
            try
            {
                readPause = true;
                var wildMon = await ConnectionWrapper.ReadWildPokemon(Source.Token).ConfigureAwait(false);
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
                readPause = false;
            }
            catch (Exception ex)
            {
                readPause = false;
                this.DisplayMessageBox(ex.Message);
            }
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

    private PK8 _pk = new();
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
            Frames = await Task.Run(async () => await Core.RNG.Generators.Misc.Capture.Generate(s0, s1, initial, initial + advances, config).ConfigureAwait(false));

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
                        if (ConnectionWrapper is not null && ConnectionWrapper.Connected)
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
        cfg.HashCatchingCharm = CB_Charm.GetIsChecked();
    }

    private void CB_8thBadge_CheckedChanged(object sender, EventArgs e)
    {
        cfg.BeatRaihan = CB_8thBadge.GetIsChecked();
    }

    private void NUD_ZukanCaught_ValueChanged(object sender, EventArgs e)
    {
        cfg.SpeciesRegisteredInDex = NUD_ZukanCaught.GetValue();
    }
}
