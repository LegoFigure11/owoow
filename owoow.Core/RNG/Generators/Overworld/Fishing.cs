using owoow.Core.Enums;
using owoow.Core.Interfaces;
using owoow.Core.RNG.Generators.Misc;
using PKHeX.Core;
using static owoow.Core.RNG.Generators.Overworld.Common;
using static owoow.Core.RNG.Generators.Fixed;
using static owoow.Core.RNG.Validators.Validator;
using Environment = owoow.Core.RNG.Generators.Misc.Environment;

namespace owoow.Core.RNG.Generators.Overworld;

public class Fishing
{
    public static Task<List<OverworldFrame>> Generate(ulong s0, ulong s1, EncounterTable.EncounterTable table, ulong start, ulong end, GeneratorConfig config)
    {
        return Task.Run(() =>
        {

            List<OverworldFrame> frames = [];
            var outer = new Xoroshiro128Plus(s0, s1);

            bool FiltersEnabled = config.FiltersEnabled;

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

            (uint AuraThreshold, int AuraRolls) = Util.GetBrilliantInfo(config.AuraKOs);

            for (ulong i = start; i <= end && frames.Count < 1_000; i++)
            {
                var os = outer.GetState();
                var rng = new Xoroshiro128Plus(os.s0, os.s1);

                EncounterSlotChosen = false;
                CuteCharm = false;
                Jump = 0;

                #region Rain, Thunderstorm, Fly, Menu Close
                if (config.ConsiderRain) Jump += Environment.GetRainAdvances(ref rng, config.RainTicksSummary);

                if (config.ConsiderFly) Jump += Environment.GetMapMemoryRollAdvances(ref rng);

                if (config.ConsiderRain) Jump += Environment.GetRainAdvances(ref rng, config.RainTicksAfterCloseMenu);

                if (config.ConsiderFly)
                {
                    Jump += Environment.GetAreaLoadAdvances(ref rng, config.AreaLoadAdvances);

                    Jump += Environment.GetAreaLoadNPCAdvances(ref rng, config.AreaLoadNPCs);
                }

                if (config.ConsiderRain) Jump += Environment.GetRainAdvances(ref rng, config.RainTicksAreaLoad);

                if (config.ConsiderMenuClose)
                {
                    Jump += MenuClose.GetAdvances(ref rng, config.MenuCloseNPCs, config.MenuCloseIsHoldingDirection, config.Weather);
                }

                if (config.ConsiderRain) Jump += Environment.GetRainAdvances(ref rng, config.RainTicksEncounter);
                #endregion

                // LEAD ABILITY ACTIVATION & ENCOUNTER SLOT
                Lead = GenerateLeadAbilityActivation(ref rng);

                if (Lead + 1 <= 66 && config.AbilityType == AbilityType.CuteCharm)
                {
                    CuteCharm = true;
                }
                if (Lead >= 49 && config.AbilityType == AbilityType.TypePulling && table.AbilityTable.Count > 0)
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
                    if (DexRec > 49 && config.IsDexRecActive)
                    {
                        DexRecSlot = config.DexRecSlots[GenerateEncounterSlot(ref rng, 4)];
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
                if (FiltersEnabled && Encounter.Species != config.TargetSpecies)
                {
                    outer.Next();
                    continue;
                }

                // LEVEL
                Level = GenerateLevel(ref rng, Encounter);

                // MARK -- DISCARDED
                _ = GenerateMark(ref rng, config.MarkRolls, config.WeatherActive, true);

                // BRILLIANT AURA
                IsAura = GenerateIsAura(ref rng, AuraThreshold);
                if (FiltersEnabled && !CheckIsAura(IsAura, config.TargetAura))
                {
                    outer.Next();
                    continue;
                }

                // SHINY
                IsShiny = GenerateIsShiny(ref rng, config.ShinyRolls + (IsAura ? AuraRolls : 0), config.TSV);

                // GENDER
                Gender = GenerateGender(ref rng, Encounter.Gender, CuteCharm);

                // NATURE
                Nature = GenerateNature(ref rng, config.AbilityType == AbilityType.Synchronize);
                if (FiltersEnabled && !CheckNature(Nature, config.TargetNature))
                {
                    outer.Next();
                    continue;
                }

                // ABILITY
                Ability = GenerateAbility(ref rng, Encounter.Abilities);

                // HELD ITEM
                Item = GenerateItem(ref rng, Encounter, config.AbilityType);

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
                var go = new Xoroshiro128Plus(GenerateFixedSeed(ref rng), 0x82A2B175229D6A5B);

                // ENCRYPTION CONSTANT
                EC = GenerateEC(ref go);
                if (FiltersEnabled && !CheckEC(EC, config.RareEC))
                {
                    outer.Next();
                    continue;
                }

                // PID
                PID = GeneratePID(ref go, IsShiny, config.TSV);
                ShinyXOR = Util.GetShinyXOR(PID, config.TSV);
                if (FiltersEnabled && !CheckIsShiny(ShinyXOR, config.TargetShiny))
                {
                    outer.Next();
                    continue;
                }

                // IVS
                (PassIVs, IVs) = GenerateIVs(ref go, AuraIVs, config);
                if (!PassIVs) // FiltersEnabled check takes place in GenerateIVs
                {
                    outer.Next();
                    continue;
                }

                // HEIGHT
                Height = GenerateHeightWeightScale(ref go);
                if (FiltersEnabled && !CheckHeight(Height, config.TargetScale))
                {
                    outer.Next();
                    continue;
                }

                // MARK
                Mark = GenerateMark(ref rng, config.MarkRolls, config.WeatherActive, true);
                if (FiltersEnabled && !CheckMark(Mark, config.TargetMark))
                {
                    outer.Next();
                    continue;
                }

                // Matches, keep!
                frames.Add(new OverworldFrame()
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
                });
                outer.Next();
            }
            return frames;
        });
    }
}
