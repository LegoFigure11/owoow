using owoow.Core.Enums;
using owoow.Core.Interfaces;
using owoow.Core.RNG.Generators.Misc;
using PKHeX.Core;
using static owoow.Core.RNG.Generators.Fixed;
using static owoow.Core.RNG.Generators.Overworld.Common;
using static owoow.Core.RNG.Validators.Validator;
using Environment = owoow.Core.RNG.Generators.Misc.Environment;

namespace owoow.Core.RNG.Generators.Overworld;

public class Static
{
    public static Task<List<OverworldFrame>> Generate(ulong s0, ulong s1, EncounterTable.EncounterTable table, ulong start, ulong end, GeneratorConfig config)
    {
        return Task.Run(() =>
        {

            List<OverworldFrame> frames = [];
            var outer = new Xoroshiro128Plus(s0, s1);

            bool FiltersEnabled = config.FiltersEnabled;

            ulong Lead;
            bool CuteCharm = false;

            bool IsShiny;
            uint ShinyXOR;
            uint PID;
            uint EC;

            char Gender;
            string Ability;
            string Nature;

            bool PassIVs;
            byte[] IVs;

            uint Height;

            uint Jump = 0;

            IEncounterStaticTableEntry Encounter = table.StaticTable.Values.Where(enc => enc.Species == config.TargetSpecies).FirstOrDefault()!;

            RibbonIndex Mark;

            for (ulong i = start; i <= end && frames.Count < 1_000; i++)
            {
                var os = outer.GetState();
                var rng = new Xoroshiro128Plus(os.s0, os.s1);

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

                // LEAD ABILITY ACTIVATION
                Lead = GenerateLeadAbilityActivation(ref rng);

                if (config.AbilityType == AbilityType.CuteCharm && Lead + 1 <= 66)
                {
                    CuteCharm = true;
                }

                // SHINY
                IsShiny = !Encounter.IsShinyLocked && GenerateIsShiny(ref rng, config.ShinyRolls, config.TSV);

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
                Ability = GenerateAbility(ref rng, Encounter.Abilities, Encounter.IsAbilityLocked, Encounter.Ability);

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
                (PassIVs, IVs) = GenerateIVs(ref go, Encounter.GuaranteedIVs, config);
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
                Mark = GenerateMark(ref rng, config.MarkRolls, config.WeatherActive);
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

                    Species = Encounter.Species!,
                    Shiny = Util.GetShinyType(ShinyXOR),
                    Level = (byte)Encounter.Level,

                    Gender = Gender,
                    Nature = Nature,
                    Ability = Ability,

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

                    Seed0 = $"{os.s0:X16}",
                    Seed1 = $"{os.s1:X16}",
                });
                outer.Next();
            }
            return frames;
        });
    }
}
