using owoow.Core.Enums;
using PKHeX.Core;
using static owoow.Core.Encounters;

namespace owoow.Core.RNG;

public static class Util
{
    public static readonly IReadOnlyList<string> Natures = GameInfo.GetStrings(1).Natures;

    public const uint MAX_TRACKED_ADVANCES = 50_000; // 50,000 chosen arbitrarily to prevent an infinite loop

    public static uint GetAdvancesPassed(ulong s0, ulong s1, ulong _s0, ulong _s1)
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

        } while (i < MAX_TRACKED_ADVANCES);

        return i;
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
            foreach ((var k, var v) in Personal)
            {
                if (v.DevId == species) return k;
            }
        }
        return "(None)";
    }
}
