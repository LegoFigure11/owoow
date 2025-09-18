namespace owoow.Core.Enums;

public enum WeatherType
{
    AllWeather = -1, // Failsafe/Fallback, should never be used
    NormalWeather,
    Overcast,
    Raining,
    Thunderstorm,
    IntenseSun,
    Snowing,
    Snowstorm,
    Sandstorm,
    HeavyFog,
};


// From https://github.com/kwsch/pkNX/blob/master/FlatBuffers/SWSH/Gen8/Wild/EncounterTableUtil.cs#L144
[Flags]
public enum AvailableWeather
{
    None = 0,
    Normal = 1,
    Overcast = 1 << 1,
    Raining = 1 << 2,
    Thunderstorm = 1 << 3,
    Intense_Sun = 1 << 4,
    Snowing = 1 << 5,
    Snowstorm = 1 << 6,
    Sandstorm = 1 << 7,
    Heavy_Fog = 1 << 8,
    Shaking_Trees = 1 << 9,
    Fishing = 1 << 10,

    All = Normal | Overcast | Raining | Thunderstorm | Intense_Sun | Snowing | Snowstorm | Sandstorm | Heavy_Fog,
    Stormy = Raining | Thunderstorm,
    Icy = Snowing | Snowstorm,
    All_IoA = Normal | Overcast | Stormy | Intense_Sun | Sandstorm | Heavy_Fog,         // IoA can have everything but snow
    All_CT = Normal | Overcast | Stormy | Intense_Sun | Icy | Heavy_Fog,                // CT can have everything but sand
    No_Sun_Sand = Normal | Overcast | Stormy | Icy | Heavy_Fog,                         // Everything but sand and sun
    All_Ballimere = Normal | Overcast | Stormy | Intense_Sun | Snowing | Heavy_Fog,     // All Ballimere Lake weather
}
