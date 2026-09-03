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

public class Static
{
    public static Task<List<OverworldFrame>> Generate(ulong s0, ulong s1, EncounterTable.EncounterTable table, ulong start, ulong end, GeneratorConfig config)
    {
        return Task.Run(() =>
        {

            List<OverworldFrame> frames = [];
            var outer = new Xoroshiro128Plus(s0, s1);

            #region Config Variable Setup
            IEncounterStaticTableEntry Encounter = table.StaticTable.Values.FirstOrDefault(enc => enc.Species == config.TargetSpecies)!;

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

            var IsShinyLocked = Encounter.IsShinyLocked;
            var EncGender = Encounter.Gender;
            var IsGenderLocked = Encounter.IsGenderLocked;
            var Abilities = Encounter.Abilities;
            var IsAbilityLocked = Encounter.IsAbilityLocked;
            var EncAbility = Encounter.Ability;
            var GuaranteedIVs = Encounter.GuaranteedIVs;
            var Level = (byte)Encounter.Level;
            var Species = Encounter.Species!;

            var SearchForwards = config.SearchForwards;
            var LogResultsToFile = config.LogResultsToFile;
            #endregion

            ulong Lead;
            bool CuteCharm;

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

            RibbonIndex Mark;

            for (ulong i = start; i <= end && frames.Count < 1_000; i++)
            {
                var os = outer.GetState();
                var rng = new Xoroshiro128Plus(os.s0, os.s1);
                _ = SearchForwards ? outer.Next() : outer.Prev();

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

                // LEAD ABILITY ACTIVATION
                Lead = GenerateLeadAbilityActivation(ref rng);

                if (AbilityType == AbilityType.CuteCharm && Lead + 1 <= 66)
                {
                    CuteCharm = true;
                }

                // SHINY
                IsShiny = !IsShinyLocked && GenerateIsShiny(ref rng, ShinyRolls, TSV);

                // GENDER
                Gender = GenerateGender(ref rng, EncGender, CuteCharm, IsGenderLocked);

                // NATURE
                Nature = GenerateNature(ref rng, AbilityType == AbilityType.Synchronize);
                if (FiltersEnabled && !CheckNature(Nature, TargetNature)) continue;

                // ABILITY
                Ability = GenerateAbility(ref rng, Abilities, IsAbilityLocked, EncAbility);

                // FIXED SEED
                var go = new Xoroshiro128Plus(GenerateFixedSeed(ref rng));

                // ENCRYPTION CONSTANT
                EC = GenerateEC(ref go);
                if (FiltersEnabled && !CheckEC(EC, RareEC)) continue;

                // PID
                PID = GeneratePID(ref go, IsShiny, TSV);
                ShinyXOR = Util.GetShinyXOR(PID, TSV);
                if (FiltersEnabled && !CheckIsShiny(ShinyXOR, TargetShiny) && !IsShinyLocked) continue;

                // IVS
                (PassIVs, IVs) = GenerateIVs(ref go, GuaranteedIVs, config);
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

                    Species = Species,
                    Shiny = Util.GetShinyType(ShinyXOR),
                    Level = Level,

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
                };
                frames.Add(f);
                if (LogResultsToFile) LogUtil.LogText($"Result found! {f}");
            }
            return frames;
        });
    }
}
