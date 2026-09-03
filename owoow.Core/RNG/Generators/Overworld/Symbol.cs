using owoow.Core.Enums;
using owoow.Core.Interfaces;
using owoow.Core.RNG.Generators.Misc;
using PKHeX.Core;
using SysBot.Base;
using static owoow.Core.RNG.Generators.Fixed;
using static owoow.Core.RNG.Generators.Overworld.Common;
using static owoow.Core.RNG.Validators.Validator;
using Environment = owoow.Core.RNG.Generators.Misc.Environment;

namespace owoow.Core.RNG.Generators.Overworld;

public class Symbol
{
    public static Task<List<OverworldFrame>> Generate(ulong s0, ulong s1, EncounterTable.EncounterTable table, ulong start, ulong end, GeneratorConfig config)
    {
        return Task.Run(() =>
        {
            List<OverworldFrame> frames = [];
            var outer = new Xoroshiro128Plus(s0, s1);

            #region Config Variable Setup
            var FiltersEnabled = config.FiltersEnabled;
            var ConsiderRain = config.ConsiderRain;
            var RainTicksSummary = config.RainTicksSummary;
            var ConsiderFly = config.ConsiderFly;
            var RainTicksAfterCloseMenu = config.RainTicksAfterCloseMenu;
            var AreaLoadAdvances = config.AreaLoadAdvances;
            var AreaLoadNPCs = config.AreaLoadNPCs;
            var ConsiderMenuClose = config.ConsiderMenuClose;
            var RainTicksAreaLoad = config.RainTicksAreaLoad;
            var MenuCloseNPCs = config.MenuCloseNPCs;
            var MenuCloseIsHoldingDirection = config.MenuCloseIsHoldingDirection;
            var Weather = config.Weather;
            var RainTicksEncounter = config.RainTicksEncounter;

            var AbilityType = config.AbilityType;
            var ShinyRolls = config.ShinyRolls;
            var TSV = config.TSV;
            var TargetNature = config.TargetNature;
            var RareEC = config.RareEC;
            var TargetShiny = config.TargetShiny;
            var TargetScale = config.TargetScale;
            var MarkRolls = config.MarkRolls;
            var WeatherActive = config.WeatherActive;
            var TargetMark = config.TargetMark;
            var AuraKOs = config.AuraKOs;
            var TargetAura = config.TargetAura;

            var IsDexRecActive = config.IsDexRecActive;
            var DexRecSlots = config.DexRecSlots;

            var TargetSpecies = config.TargetSpecies;

            var SearchForwards = config.SearchForwards;
            var LogResultsToFile = config.LogResultsToFile;
            #endregion

            ulong Lead;
            ulong DexRec;
            IDictionary<int, IEncounterTableEntry> ActiveTable;
            IEncounterTableEntry Encounter;
            bool CuteCharm = false;

            ulong EncounterSlot = 0;
            short DexRecSlot = 0;
            bool EncounterSlotChosen = false;

            ulong Level;

            bool IsAura;
            bool IsShiny;
            uint ShinyXOR;
            uint PID;
            uint EC;

            char Gender;
            string Ability;
            string Nature;
            string Item;

            int AuraIVs;
            string AuraEggMove;

            bool PassIVs;
            byte[] IVs;

            uint Height;

            uint Jump;

            RibbonIndex Mark;

            (uint AuraThreshold, int AuraRolls) = Util.GetBrilliantInfo(AuraKOs);

            for (ulong i = start; i <= end && frames.Count < 1_000; i++)
            {
                var os = outer.GetState();
                var rng = new Xoroshiro128Plus(os.s0, os.s1);
                _ = SearchForwards ? outer.Next() : outer.Prev();

                EncounterSlotChosen = false;
                CuteCharm = false;
                Jump = 0;

                #region Rain, Thunderstorm, Fly, Menu Close
                if (ConsiderRain) Jump += Environment.GetRainAdvances(ref rng, RainTicksSummary);

                if (ConsiderFly) Jump += Environment.GetMapMemoryRollAdvances(ref rng);

                if (ConsiderRain) Jump += Environment.GetRainAdvances(ref rng, RainTicksAfterCloseMenu);

                if (ConsiderFly)
                {
                    Jump += Environment.GetAreaLoadAdvances(ref rng, AreaLoadAdvances);

                    Jump += Environment.GetAreaLoadNPCAdvances(ref rng, AreaLoadNPCs);
                }

                if (ConsiderRain) Jump += Environment.GetRainAdvances(ref rng, RainTicksAreaLoad);

                if (ConsiderMenuClose)
                {
                    Jump += MenuClose.GetAdvances(ref rng, MenuCloseNPCs, MenuCloseIsHoldingDirection, Weather);
                }

                if (ConsiderRain) Jump += Environment.GetRainAdvances(ref rng, RainTicksEncounter);
                #endregion

                // PLACEMENT (ASSUME SUCCESS)

                DoPlacementRolls(ref rng);

                // LEAD ABILITY ACTIVATION & ENCOUNTER SLOT
                Lead = GenerateLeadAbilityActivation(ref rng);

                if (Lead + 1 <= 66 && AbilityType == AbilityType.CuteCharm)
                {
                    CuteCharm = true;
                }
                if (Lead >= 49 && AbilityType == AbilityType.TypePulling && table.AbilityTable.Count > 0)
                {
                    ActiveTable = table.AbilityTable;
                    var TableSize = (uint)ActiveTable.Count;
                    EncounterSlot = TableSize == 1 ? 0 : GenerateEncounterSlot(ref rng, TableSize);
                    EncounterSlotChosen = true;
                }
                else
                {
                    ActiveTable = table.MainTable;
                }

                // POKEDEX RECOMMENDATION
                if (!EncounterSlotChosen)
                {
                    DexRec = GenerateDexRecActivation(ref rng);
                    if (DexRec < 50 && IsDexRecActive)
                    {
                        DexRecSlot = DexRecSlots[GenerateEncounterSlot(ref rng, 4)];
                        var DexRecMatchingSpecies = ActiveTable.Where(enc => enc.Value.DevId == DexRecSlot);
                        if (DexRecMatchingSpecies.Any())
                        {
                            EncounterSlot = (ulong)DexRecMatchingSpecies.First().Key;
                            EncounterSlotChosen = true;
                        }
                    }
                }

                if (!EncounterSlotChosen)
                {
                    EncounterSlot = GenerateEncounterSlot(ref rng);
                }

                Encounter = ActiveTable[(int)EncounterSlot];
                if (FiltersEnabled && TargetSpecies != Encounters.ANY_SPECIES && Encounter.Species != TargetSpecies) continue;

                // LEVEL
                Level = GenerateLevel(ref rng, Encounter);

                // MARK -- DISCARDED
                _ = GenerateMark(ref rng, MarkRolls, WeatherActive);

                // BRILLIANT AURA
                IsAura = GenerateIsAura(ref rng, AuraThreshold);
                if (FiltersEnabled && !CheckIsAura(IsAura, TargetAura)) continue;

                // SHINY
                IsShiny = GenerateIsShiny(ref rng, ShinyRolls + (IsAura ? AuraRolls : 0), TSV);

                // GENDER
                Gender = GenerateGender(ref rng, Encounter.Gender, CuteCharm);

                // NATURE
                Nature = GenerateNature(ref rng, AbilityType == AbilityType.Synchronize);
                if (FiltersEnabled && !CheckNature(Nature, TargetNature)) continue;

                // ABILITY
                Ability = GenerateAbility(ref rng, Encounter.Abilities);

                // HELD ITEM
                Item = GenerateItem(ref rng, Encounter, AbilityType);

                // AURA IVS/EMS
                AuraIVs = 0;
                AuraEggMove = "-";
                if (IsAura)
                {
                    Level = (ulong)Encounter.MaxLevel;
                    AuraIVs = GenerateAuraIVs(ref rng);
                    AuraEggMove = GenerateAuraEggMove(ref rng, Encounter);
                }

                // FIXED SEED
                var go = new Xoroshiro128Plus(GenerateFixedSeed(ref rng));

                // ENCRYPTION CONSTANT
                EC = GenerateEC(ref go);
                if (FiltersEnabled && !CheckEC(EC, RareEC)) continue;

                // PID
                PID = GeneratePID(ref go, IsShiny, TSV);
                ShinyXOR = Util.GetShinyXOR(PID, TSV);
                if (FiltersEnabled && !CheckIsShiny(ShinyXOR, TargetShiny)) continue;

                // IVS
                (PassIVs, IVs) = GenerateIVs(ref go, AuraIVs, config);
                if (!PassIVs) continue; // FiltersEnabled check takes place in GenerateIVs

                // HEIGHT
                Height = GenerateHeightWeightScale(ref go);
                if (FiltersEnabled && !CheckHeight(Height, TargetScale)) continue;

                // MARK
                Mark = GenerateMark(ref rng, MarkRolls, WeatherActive);
                if (FiltersEnabled && !CheckMark(Mark, TargetMark)) continue;

                // Matches, keep!
                var f = new OverworldFrame()
                {
                    Advances = $"{i:N0}",

                    Jump = $"+{Jump}",

                    Animation = (os.s0 & 1 ^ os.s1 & 1) == 0 ? 'P' : 'S',

                    Brilliant = IsAura ? 'Y' : 'N',

                    Species = Encounter.Species!,
                    Shiny = Util.GetShinyType(ShinyXOR),
                    Level = (byte)Level,

                    Gender = Gender,
                    Nature = Nature,
                    Ability = Ability,
                    Item = Item,

                    PID = $"{PID:X8}",
                    EC = $"{EC:X8}",

                    H = IVs[0],
                    A = IVs[1],
                    B = IVs[2],
                    C = IVs[3],
                    D = IVs[4],
                    S = IVs[5],

                    Height = Util.GetHeightString(Height),

                    Mark = Util.GetRibbonName(Mark),

                    EggMove = AuraEggMove,

                    Seed0 = $"{os.s0:X16}",
                    Seed1 = $"{os.s1:X16}",
                };
                frames.Add(f);
                if (LogResultsToFile) LogUtil.LogText($"Result found! {f}");
            }
            return frames;
        });
    }
}
