using owoow.Core.Enums;
using owoow.Core.Interfaces;
using System.Collections.Immutable;
using System.Text.Json;

namespace owoow.Core;

public static class Encounters
{
    public static readonly Dictionary<string, Personal>? Personal;

    public static readonly string ANY_SPECIES = "(Any)";

    private static readonly Dictionary<string, Dictionary<string, Encounter>>? SwordFishing;
    private static readonly Dictionary<string, Dictionary<string, Encounter>>? SwordHidden;
    private static readonly Dictionary<string, Dictionary<string, Encounter>>? SwordSymbol;
    private static readonly Dictionary<string, Dictionary<string, EncounterStatic>>? SwordStatic;

    private static readonly Dictionary<string, Dictionary<string, Encounter>>? ShieldHidden;
    private static readonly Dictionary<string, Dictionary<string, Encounter>>? ShieldFishing;
    private static readonly Dictionary<string, Dictionary<string, Encounter>>? ShieldSymbol;
    private static readonly Dictionary<string, Dictionary<string, EncounterStatic>>? ShieldStatic;

    static Encounters()
    {
        Personal = JsonSerializer.Deserialize<Dictionary<string, Personal>>(Utils.GetStringResource("personal.json") ?? "{}");

        SwordFishing = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Encounter>>>(Utils.GetStringResource("sw_fishing.json") ?? "{}");
        SwordHidden = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Encounter>>>(Utils.GetStringResource("sw_hidden.json") ?? "{}");
        SwordSymbol = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Encounter>>>(Utils.GetStringResource("sw_symbol.json") ?? "{}");
        SwordStatic = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, EncounterStatic>>>(Utils.GetStringResource("sw_static.json") ?? "{}");

        ShieldFishing = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Encounter>>>(Utils.GetStringResource("sh_fishing.json") ?? "{}");
        ShieldHidden = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Encounter>>>(Utils.GetStringResource("sh_hidden.json") ?? "{}");
        ShieldSymbol = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Encounter>>>(Utils.GetStringResource("sh_symbol.json") ?? "{}");
        ShieldStatic = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, EncounterStatic>>>(Utils.GetStringResource("sh_static.json") ?? "{}");
    }

    public static ImmutableSortedDictionary<string, List<EncounterLookupEntry>> GetEncounterLookupForGame(Game game)
    {
        var list = new Dictionary<string, List<EncounterLookupEntry>>();
        var fishing = game == Game.Sword ? SwordFishing : ShieldFishing;
        var hidden  = game == Game.Sword ? SwordHidden  : ShieldHidden ;
        var symbol  = game == Game.Sword ? SwordSymbol  : ShieldSymbol ;
        var strong  = game == Game.Sword ? SwordStatic  : ShieldStatic ;

        foreach (var area in symbol!)
        {
            foreach (var weather in area.Value)
            {
                if ((WeatherNameToValue(weather.Key) & WeathersByArea[area.Key]) == 0) continue; // Filter out impossible weathers
                foreach (var enc in weather.Value.Encounters!)
                {
                    var species = enc.Value.Species!;
                    if (!list.TryGetValue(species, out List<EncounterLookupEntry>? value))
                    {
                        list[species] = [];
                        value = list[species];
                    }

                    bool found = false;
                    foreach (var entry in value)
                    {
                        if (entry.Area == area.Key && entry.Weather == weather.Key && entry.EncounterType == "Symbol")
                        {
                            entry.EncounterRate += enc.Value.SlotMax - enc.Value.SlotMin + 1;
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        value.Add(new EncounterLookupEntry
                        {
                            Species = species,
                            MinLevel = weather.Value.MinLevel,
                            MaxLevel = weather.Value.MaxLevel,
                            EncounterType = "Symbol",
                            Weather = weather.Key,
                            Area = area.Key,
                            EncounterRate = enc.Value.SlotMax - enc.Value.SlotMin + 1,
                        });
                    }
                }
            }
        }

        foreach (var area in strong!)
        {
            foreach (var weather in area.Value)
            {
                if ((WeatherNameToValue(weather.Key) & WeathersByArea[area.Key]) == 0) continue; // Filter out impossible weathers
                foreach (var enc in weather.Value.Encounters!)
                {
                    var species = enc.Value.Species!;
                    if (!list.TryGetValue(species, out List<EncounterLookupEntry>? value))
                    {
                        list[species] = [];
                        value = list[species];
                    }
                    value.Add(new EncounterLookupEntry
                    {
                        Species = species,
                        MinLevel = enc.Value.Level,
                        MaxLevel = enc.Value.Level,
                        EncounterType = "Static",
                        Weather = weather.Key,
                        Area = area.Key,
                        EncounterRate = 100,
                        IsAbilityLocked = enc.Value.IsAbilityLocked,
                        Ability = enc.Value.Ability,
                        IsShinyLocked = enc.Value.IsShinyLocked,
                        GuaranteedIVs = enc.Value.GuaranteedIVs,
                        IsGenderLocked = enc.Value.IsGenderLocked,
                    });
                }
            }
        }

        foreach (var area in hidden!)
        {
            foreach (var weather in area.Value)
            {
                if ((WeatherNameToValue(weather.Key) & WeathersByArea[area.Key]) == 0) continue; // Filter out impossible weathers
                foreach (var enc in weather.Value.Encounters!)
                {
                    var species = enc.Value.Species!;
                    if (!list.TryGetValue(species, out List<EncounterLookupEntry>? value))
                    {
                        list[species] = [];
                        value = list[species];
                    }
                    bool found = false;
                    foreach (var entry in value)
                    {
                        if (entry.Area == area.Key && entry.Weather == weather.Key && entry.EncounterType == "Hidden")
                        {
                            entry.EncounterRate += enc.Value.SlotMax - enc.Value.SlotMin + 1;
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        value.Add(new EncounterLookupEntry
                        {
                            Species = species,
                            MinLevel = weather.Value.MinLevel,
                            MaxLevel = weather.Value.MaxLevel,
                            EncounterType = "Hidden",
                            Weather = weather.Key,
                            Area = area.Key,
                            EncounterRate = enc.Value.SlotMax - enc.Value.SlotMin + 1,
                        });
                    }
                }
            }
        }

        foreach (var area in fishing!)
        {
            foreach (var weather in area.Value)
            {
                if ((WeatherNameToValue(weather.Key) & WeathersByArea[area.Key]) == 0) continue; // Filter out impossible weathers
                foreach (var enc in weather.Value.Encounters!)
                {
                    var species = enc.Value.Species!;
                    if (!list.TryGetValue(species, out List<EncounterLookupEntry>? value))
                    {
                        list[species] = [];
                        value = list[species];
                    }
                    bool found = false;
                    foreach (var entry in value)
                    {
                        if (entry.Area == area.Key && entry.Weather == weather.Key && entry.EncounterType == "Fishing")
                        {
                            entry.EncounterRate += enc.Value.SlotMax - enc.Value.SlotMin + 1;
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        value.Add(new EncounterLookupEntry
                        {
                            Species = species,
                            MinLevel = weather.Value.MinLevel,
                            MaxLevel = weather.Value.MaxLevel,
                            EncounterType = "Fishing",
                            Weather = weather.Key,
                            Area = area.Key,
                            EncounterRate = enc.Value.SlotMax - enc.Value.SlotMin + 1,
                        });
                    }
                }
            }
        }

        return list.ToImmutableSortedDictionary();
    }

    public static Encounter? GetEncounters(Game game, EncounterType tabName, string area, string weather)
    {
        try
        {
            return game switch
            {
                Game.Sword => tabName switch
                {
                    EncounterType.Fishing => SwordFishing ![area][weather],
                    EncounterType.Hidden  => SwordHidden  ![area][weather],
                    _                     => SwordSymbol  ![area][weather],
                },
                _          => tabName switch
                {
                    EncounterType.Fishing => ShieldFishing![area][weather],
                    EncounterType.Hidden  => ShieldHidden ![area][weather],
                    _                     => ShieldSymbol ![area][weather],
                }
            };
        }
        catch { return null; }
    }

    public static EncounterStatic? GetStaticEncounters(Game game, string area, string weather)
    {
        try
        {
            return game switch
            {
                Game.Sword => SwordStatic ![area][weather],
                _          => ShieldStatic![area][weather],
            };
        }
        catch { return null; }
    }

    public static IReadOnlyList<string> GetAreaList(Game game, EncounterType tabName) => game switch
    {
        Game.Sword => tabName switch
        {
            EncounterType.Fishing => [.. SwordFishing !.Keys],
            EncounterType.Hidden  => [.. SwordHidden  !.Keys],
            EncounterType.Static  => [.. SwordStatic  !.Keys],
            _                     => [.. SwordSymbol  !.Keys],
        },
        _          => tabName switch
        {
            EncounterType.Fishing => [.. ShieldFishing!.Keys],
            EncounterType.Hidden  => [.. ShieldHidden !.Keys],
            EncounterType.Static  => [.. ShieldStatic !.Keys],
            _                     => [.. ShieldSymbol !.Keys],
        },
    };

    public static IReadOnlyList<string> GetWeatherList(Game game, EncounterType tabName, string area) => game switch
    {
        Game.Sword => tabName switch
        {
            EncounterType.Fishing => [.. SwordFishing ![area].Keys.Where(weather => (WeatherNameToValue(weather) & WeathersByArea[area]) != 0)],
            EncounterType.Hidden  => [.. SwordHidden  ![area].Keys.Where(weather => (WeatherNameToValue(weather) & WeathersByArea[area]) != 0)],
            EncounterType.Static  => [.. SwordStatic  ![area].Keys.Where(weather => (WeatherNameToValue(weather) & WeathersByArea[area]) != 0)],
            _                     => [.. SwordSymbol  ![area].Keys.Where(weather => (WeatherNameToValue(weather) & WeathersByArea[area]) != 0)],
        },
        _          => tabName switch
        {
            EncounterType.Fishing => [.. ShieldFishing![area].Keys.Where(weather => (WeatherNameToValue(weather) & WeathersByArea[area]) != 0)],
            EncounterType.Hidden  => [.. ShieldHidden ![area].Keys.Where(weather => (WeatherNameToValue(weather) & WeathersByArea[area]) != 0)],
            EncounterType.Static  => [.. ShieldStatic ![area].Keys.Where(weather => (WeatherNameToValue(weather) & WeathersByArea[area]) != 0)],
            _                     => [.. ShieldSymbol ![area].Keys.Where(weather => (WeatherNameToValue(weather) & WeathersByArea[area]) != 0)],
        },
    };

    public static IReadOnlyList<string> GetSpeciesList(Game game, EncounterType tabName, string area, string weather)
    {
        if (tabName == EncounterType.Static) return GetSpeciesListStatic(game, area, weather);

        var enc = GetEncounters(game, tabName, area, weather);
        IList<string> ret = [];
        if (enc is not null)
        {
            var encs = enc.Encounters!.Values.Where(v => !ret.Contains(v.Species!));
            foreach (var v in encs)
            {
                ret.Add(v.Species!);
            }
        }
        return ret.AsReadOnly();
    }

    public static IReadOnlyList<string> GetSpeciesListStatic(Game game, string area, string weather)
    {
        var enc = GetStaticEncounters(game, area, weather);
        IList<string> ret = [];
        if (enc is not null)
        {
            var encs = enc.Encounters!.Values.Where(v => !ret.Contains(v.Species!));
            foreach (var v in encs)
            {
                ret.Add(v.Species!);
            }
        }
        return ret.AsReadOnly();
    }

    private static AvailableWeather WeatherNameToValue(string name) => name switch
    {
        "Normal Weather" => AvailableWeather.Normal,
        "Overcast" => AvailableWeather.Overcast,
        "Raining" => AvailableWeather.Raining,
        "Thunderstorm" => AvailableWeather.Thunderstorm,
        "Intense Sun" => AvailableWeather.Intense_Sun,
        "Snowing" => AvailableWeather.Snowing,
        "Snowstorm" => AvailableWeather.Snowstorm,
        "Sandstorm" => AvailableWeather.Sandstorm,
        "Heavy Fog" => AvailableWeather.Heavy_Fog,
        _ => AvailableWeather.None,
    };

    // Adapted from https://github.com/kwsch/pkNX/blob/master/FlatBuffers/SWSH/Gen8/Wild/EncounterTableUtil.cs#L299
    private static readonly Dictionary<string, AvailableWeather> WeathersByArea = new()
    {
        { "Axew's Eye", AvailableWeather.All },
        { "Ballimere Lake", AvailableWeather.All_CT },
        { "Ballimere Lake (Surfing)", AvailableWeather.All_Ballimere },
        { "Brawlers' Cave", AvailableWeather.Normal },
        { "Brawlers' Cave (Surfing)", AvailableWeather.Normal },
        { "Bridge Field", AvailableWeather.All },
        { "Bridge Field (Flying)", AvailableWeather.All },
        { "Bridge Field (Surfing)", AvailableWeather.All },
        { "Challenge Beach", AvailableWeather.All_IoA },
        { "Challenge Beach (Beach)", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Challenge Beach (Surfing)", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Challenge Road", AvailableWeather.All_IoA },
        { "City of Motostoke", AvailableWeather.Normal },
        { "Courageous Cavern", AvailableWeather.Normal },
        { "Courageous Cavern (Surfing)", AvailableWeather.Normal },
        { "Dappled Grove", AvailableWeather.All },
        { "Dusty Bowl", AvailableWeather.All },
        { "Dusty Bowl (Flying)", AvailableWeather.All },
        { "Dusty Bowl and Giant's Mirror (Surfing)", AvailableWeather.All },
        { "Dyna Tree Hill", AvailableWeather.Normal },
        { "East Lake Axewell", AvailableWeather.All },
        { "East Lake Axewell (Flying)", AvailableWeather.All },
        { "East Lake Axewell (Surfing)", AvailableWeather.All },
        { "Fields of Honor", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Fields of Honor (Beach)", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Fields of Honor (Surfing)", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Forest of Focus", AvailableWeather.All_IoA },
        { "Forest of Focus (Surfing)", AvailableWeather.All_IoA },
        { "Frigid Sea", AvailableWeather.No_Sun_Sand },
        { "Frigid Sea (Surfing)", AvailableWeather.No_Sun_Sand },
        { "Frostpoint Field", AvailableWeather.All_CT },
        { "Galar Mine", AvailableWeather.Normal },
        { "Galar Mine No. 2", AvailableWeather.Normal },
        { "Giant's Bed", AvailableWeather.All_CT },
        { "Giant's Bed / Giant's Foot (Surfing)", AvailableWeather.All_CT },
        { "Giant's Cap", AvailableWeather.All },
        { "Giant's Cap (2)", AvailableWeather.All },
        { "Giant's Cap (3)", AvailableWeather.All },
        { "Giant's Cap (Ground)", AvailableWeather.All },
        { "Giant's Cap (Lunatone/Solrock)", AvailableWeather.All },
        { "Giant's Foot", AvailableWeather.All_CT },
        { "Giant's Mirror", AvailableWeather.All },
        { "Giant's Mirror (Flying)", AvailableWeather.All },
        { "Giant's Mirror (Ground)", AvailableWeather.All },
        { "Giant's Seat", AvailableWeather.All },
        { "Glimwood Tangle", AvailableWeather.Normal },
        { "Hammerlocke Hills", AvailableWeather.All },
        { "Hammerlocke Hills (Flying)", AvailableWeather.All },
        { "Honeycalm Island", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Honeycalm Island (Surfing)", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Honeycalm Sea", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Honeycalm Sea (Sharpedo)", AvailableWeather.All_IoA },
        { "Honeycalm Sea (Surfing)", AvailableWeather.All_IoA },
        { "Insular Sea", AvailableWeather.Normal },
        { "Insular Sea (Sharpedo)", AvailableWeather.Normal },
        { "Insular Sea (Surfing)", AvailableWeather.Normal },
        { "Lake of Outrage", AvailableWeather.All },
        { "Lake of Outrage (Surfing)", AvailableWeather.All },
        { "Lakeside Cave", AvailableWeather.All_Ballimere },
        { "Loop Lagoon", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Loop Lagoon (Beach)", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Loop Lagoon (Surfing)", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Motostoke Outskirts", AvailableWeather.Normal },
        { "Motostoke Riverbank", AvailableWeather.All },
        { "Motostoke Riverbank (Surfing)", AvailableWeather.All },
        { "North Lake Miloch", AvailableWeather.All },
        { "North Lake Miloch (Surfing)", AvailableWeather.All },
        { "Old Cemetery", AvailableWeather.All_CT },
        { "Path to the Peak", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Intense_Sun | AvailableWeather.Icy | AvailableWeather.Heavy_Fog },
        { "Potbottom Desert", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Raining | AvailableWeather.Sandstorm | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Roaring-Sea Caves", AvailableWeather.All_CT },
        { "Roaring-Sea Caves (Surfing)", AvailableWeather.Overcast },
        { "Rolling Fields", AvailableWeather.All },
        { "Rolling Fields (2)", AvailableWeather.All },
        { "Rolling Fields (Flying)", AvailableWeather.All },
        { "Rolling Fields (Ground)", AvailableWeather.All },
        { "Route 1", AvailableWeather.Normal },
        { "Route 2", AvailableWeather.Normal },
        { "Route 2 (High Level)", AvailableWeather.Normal },
        { "Route 2 (Surfing)", AvailableWeather.Normal },
        { "Route 3", AvailableWeather.Normal },
        { "Route 3 (Garbage)", AvailableWeather.Normal },
        { "Route 4", AvailableWeather.Normal },
        { "Route 5", AvailableWeather.Normal },
        { "Route 6", AvailableWeather.Intense_Sun },
        { "Route 7", AvailableWeather.Normal },
        { "Route 8", AvailableWeather.Normal },
        { "Route 8 (on Steamdrift Way)", AvailableWeather.Snowing },
        { "Route 9", AvailableWeather.Snowing },
        { "Route 9  (Surfing)", AvailableWeather.Snowing },
        { "Route 9 (in Circhester Bay)", AvailableWeather.Snowing },
        { "Route 9 (in Circhester Bay) (Surfing)", AvailableWeather.Snowing },
        { "Route 9 (in Outer Spikemuth)", AvailableWeather.Overcast },
        { "Route 10", AvailableWeather.Snowstorm },
        { "Route 10 (Near Station)", AvailableWeather.Snowstorm },
        { "Slippery Slope", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Intense_Sun | AvailableWeather.Icy | AvailableWeather.Heavy_Fog },
        { "Slumbering Weald", AvailableWeather.Normal },
        { "Slumbering Weald (High Level)", AvailableWeather.Normal },
        { "Slumbering Weald (Low Level)", AvailableWeather.Normal },
        { "Snowslide Slope", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Intense_Sun | AvailableWeather.Icy | AvailableWeather.Heavy_Fog },
        { "Soothing Wetlands", AvailableWeather.All_IoA },
        { "Soothing Wetlands (Puddles)", AvailableWeather.All_IoA },
        { "South Lake Miloch", AvailableWeather.All },
        { "South Lake Miloch (2)", AvailableWeather.All },
        { "South Lake Miloch (Flying)", AvailableWeather.All },
        { "South Lake Miloch (Surfing)", AvailableWeather.All },
        { "Stepping-Stone Sea", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Stepping-Stone Sea (Sharpedo)", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Stepping-Stone Sea (Surfing)", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Stony Wilderness", AvailableWeather.All },
        { "Stony Wilderness (2)", AvailableWeather.All },
        { "Stony Wilderness (3)", AvailableWeather.All },
        { "Stony Wilderness (Flying)", AvailableWeather.All },
        { "Three-Point Pass", AvailableWeather.All_CT },
        { "Town of Hulbury", AvailableWeather.Normal },
        { "Training Lowlands", AvailableWeather.All_IoA },
        { "Training Lowlands (Beach)", AvailableWeather.All_IoA },
        { "Training Lowlands (Surfing)", AvailableWeather.All_IoA },
        { "Tunnel to the Top", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Intense_Sun | AvailableWeather.Icy | AvailableWeather.Heavy_Fog },
        { "Turffield", AvailableWeather.Normal },
        { "Warm-up Tunnel", AvailableWeather.All_IoA },
        { "Watchtower Ruins", AvailableWeather.All },
        { "Watchtower Ruins (Flying)", AvailableWeather.All },
        { "West Lake Axewell", AvailableWeather.All },
        { "West Lake Axewell (Surfing)", AvailableWeather.All },
        { "Workout Sea", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Workout Sea (Sharpedo)", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
        { "Workout Sea (Surfing)", AvailableWeather.Normal | AvailableWeather.Overcast | AvailableWeather.Stormy | AvailableWeather.Intense_Sun | AvailableWeather.Heavy_Fog },
    };
}
