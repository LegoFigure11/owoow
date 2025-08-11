using System.Numerics;
using owoow.Core.Enums;
using PKHeX.Core;
using static owoow.Core.Encounters;

namespace owoow.Core.RNG;

public static class Util
{
    public static readonly IReadOnlyList<string> Natures = GameInfo.GetStrings(1).Natures;

    public const uint MAX_TRACKED_ADVANCES = 50_000; // 50,000 chosen arbitrarily to prevent an infinite loop

    public static uint GetAdvancesPassed(ulong s0, ulong s1, ulong _s0, ulong _s1, ulong limit = MAX_TRACKED_ADVANCES)
    {
        if (s0 == _s0 && s1 == _s1) return 0;
        var rng = new Xoroshiro128Plus(s0, s1);
        uint i = 0;
        do
        {
            i++;
            rng.Next();

            var (cur0, cur1) = rng.GetState();
            if (cur0 == _s0 && cur1 == _s1) break;

        } while (i < limit);

        return i;
    }

    public static (ulong s0, ulong s1) XoroshiroJump(ulong s0, ulong s1, ulong jump)
    {
        do
        {
            var interval = (uint)Math.Min(jump, uint.MaxValue);
            (s0, s1) = XoroshiroTableJump(s0, s1, interval);
            jump -= interval;
        } while (jump > 0);
        return (s0, s1);
    }

    // https://github.com/Admiral-Fish/PokeFinder/blob/2734dd27bd072a914dcdcc47db36a0067e51069b/Source/Core/RNG/Xoroshiro.cpp
    private static readonly ulong[,] XoroshiroJumpTable = {
        { 0x0008828e513b43d5, 0x095b8f76579aa001 }, { 0x7a8ff5b1c465a931, 0x162ad6ec01b26eae },
        { 0xb18b0d36cd81a8f5, 0xb4fbaa5c54ee8b8f }, { 0x23ac5e0ba1cecb29, 0x1207a1706bebb202 },
        { 0xbb18e9c8d463bb1b, 0x2c88ef71166bc53d }, { 0xe3fbe606ef4e8e09, 0xc3865bb154e9be10 },
        { 0x28faaaebb31ee2db, 0x1a9fc99fa7818274 }, { 0x30a7c4eef203c7eb, 0x588abd4c2ce2ba80 },
        { 0xa425003f3220a91d, 0x9c90debc053e8cef }, { 0x81e1dd96586cf985, 0xb82ca99a09a4e71e },
        { 0x4f7fd3dfbb820bfb, 0x35d69e118698a31d }, { 0xfee2760ef3a900b3, 0x49613606c466efd3 },
        { 0xf0df0531f434c57d, 0xbd031d011900a9e5 }, { 0x442576715266740c, 0x235e761b3b378590 },
        { 0x1e8bae8f680d2b35, 0x3710a7ae7945df77 }, { 0xfd7027fe6d2f6764, 0x75d8e7dbceda609c },
        { 0x28eff231ad438124, 0xde2cba60cd3332b5 }, { 0x1808760d0a0909a1, 0x377e64c4e80a06fa },
        { 0xb9a362fafedfe9d2, 0x0cf0a2225da7fb95 }, { 0xf57881ab117349fd, 0x2bab58a3cadfc0a3 },
        { 0x849272241425c996, 0x8d51ecdb9ed82455 }, { 0xf1ccb8898cbc07cd, 0x521b29d0a57326c1 },
        { 0x61179e44214caafa, 0xfbe65017abec72dd }, { 0xd9aa6b1e93fbb6e4, 0x6c446b9bc95c267b },
        { 0x86e3772194563f6d, 0x64f80248d23655c6 },
    };

    // https://github.com/Admiral-Fish/PokeFinder/blob/2734dd27bd072a914dcdcc47db36a0067e51069b/Source/Core/RNG/Xoroshiro.cpp
    private static (ulong s0, ulong s1) XoroshiroTableJump(ulong s0, ulong s1, uint jump)
    {
        var rng = new Xoroshiro128Plus(s0, s1);
        var j = jump & 0x7f;
        for (var i = 0; i < j; i++) rng.Next();
        jump >>= 7;

        for (var i = 0; jump != 0; jump >>= 1, i++)
        {
            if ((jump & 1) == 0) continue;
            var jumps = new ulong[2];
            for (var k = 1; k >= 0; k--)
            {
                var val = XoroshiroJumpTable[i, k];
                for (var l = 0; l < 64; l++, val >>= 1)
                {
                    if ((val & 1) != 0)
                    {
                        var (_s0, _s1) = rng.GetState();
                        jumps[0] ^= _s0;
                        jumps[1] ^= _s1;
                    }

                    rng.Next();
                }
            }

            rng = new Xoroshiro128Plus(jumps[0], jumps[1]);
        }

        return rng.GetState();
    }

    public static (uint threshold, int rolls) GetBrilliantInfo(int KOs) => KOs switch
    {
        >= 500 => (30, 6),
        >= 300 => (30, 5),
        >= 200 => (30, 4),
        >= 100 => (30, 3),
        >= 50 => (25, 2),
        >= 20 => (20, 1),
        >= 1 => (15, 1),
        _ => (0, 0),
    };

    public static string GetTypePullingLeadAbilityType(string ability) => ability switch
    {
        "Magnet Pull" => "Steel",
        "Lightning Rod" or "Static" => "Electric",
        "Flash Fire" => "Fire",
        "Storm Drain" => "Water",
        "Harvest" => "Grass",
        _ => string.Empty,
    };

    public static uint GetHiddenEncounterModifiedRate(uint step, AbilityType ability)
    {
        var rate = Math.Min((step + 1) * 22, 100);
        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (ability)
        {
            case AbilityType.DecreaseEncounterRate:
                rate >>= 3;
                break;

            case AbilityType.IncreaseEncounterRate:
                rate <<= 1;
                break;
        }
        return Math.Min(rate, 100);
    }

    public static uint GetShinyValue(uint x, uint y) => x ^ y;
    public static uint GetShinyValue(uint x) => (x >> 16) ^ (x & 0xFFFF);

    public static uint GetShinyXOR(uint pid, uint tsv) => GetShinyValue(GetShinyValue(pid), tsv);

    public static string GetShinyType(uint xor) => xor switch
    {
        0 => "Square",
        < 16 => $"Star ({xor})",
        _ => "No",
    };

    public static string GetHeightString(uint height) => height switch
    {
        >= 255 => $"XXXL ({height})",
        >= 231 => $"XXL ({height})",
        >= 196 => $"XL ({height})",
        >= 156 => $"L ({height})",
        >= 100 => $"M ({height})",
        >= 60 => $"S ({height})",
        >= 25 => $"XS ({height})",
        >= 1 => $"XXS ({height})",
        _ => $"XXXS ({height})",
    };

    public static string GetRibbonName(RibbonIndex rib) => rib switch
    {
        RibbonIndex.MAX_COUNT => "None",
        RibbonIndex.MarkLunchtime => "Time",
        RibbonIndex.MarkCloudy => "Weather",
        _ => rib.GetPropertyName().Replace("RibbonMark", string.Empty),
    };

    public static WeatherType GetWeatherType(string weather) => weather switch
    {
        "Normal Weather" => WeatherType.NormalWeather,
        "Overcast" => WeatherType.Overcast,
        "Raining" => WeatherType.Raining,
        "Thunderstorm" => WeatherType.Thunderstorm,
        "Intense Sun" => WeatherType.IntenseSun,
        "Snowing" => WeatherType.Snowing,
        "Snowstorm" => WeatherType.Snowstorm,
        "Heavy Fog" => WeatherType.HeavyFog,
        _ => WeatherType.AllWeather,
    };

    public static IVSearchType GetIVSearchType(string labelText) =>
        labelText == "||" ? IVSearchType.Or : IVSearchType.Range;

    public static string GetLotoIDPrizeName(LotoIDTargetType item) => item switch
    {
        LotoIDTargetType.MasterBall => "Master Ball",
        LotoIDTargetType.RareCandy => "Rare Candy",
        LotoIDTargetType.PPMax => "PP Max",
        LotoIDTargetType.PPUp => "PP Up",
        LotoIDTargetType.MoomooMilk => "Moomoo Milk",
        _ => "None",
    };

    public static LotoIDTargetType GetLotoIDTargetType(int selected) => (LotoIDTargetType)selected;
    public static CramomaticTargetType GetCramomaticTargetType(int selected) => (CramomaticTargetType)selected;
    public static CramomaticInputItemType GetCramomaticInputItemType(int selected) => (CramomaticInputItemType)selected;
    public static SuccessType GetSuccessType(int selected) => (SuccessType)selected;

    public static short GetDexRecommendation(string species)
    {
        if (Personal is not null)
        {
            try
            {
                if (Personal[species] is not null) return Personal[species].DevId;
            }
            catch { return 0; } // (None) selected, or user entered garbage
        }
        return 0;
    }

    public static string GetDexRecommendation(ushort species)
    {
        if (Personal is not null)
        {
            foreach (var (k, v) in Personal)
            {
                if (v.DevId == species) return k;
            }
        }
        return "(None)";
    }
}
