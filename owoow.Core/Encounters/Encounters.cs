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

    public static List<string> GetDexRecOptions(bool includeNone = true)
    {
        List<string> range = includeNone ? ["(None)"] : [];
        if (Personal is not null)
        {
            // Special handling to keep Jangmo-o, Hakamo-o, Kommo-o, and Porygon-Z. Silvally-10+ also need to be filtered out
            range.AddRange(Personal.Keys.Where(k => !((k[^2] == '-' || k[^3] == '-') && k[^1] != 'o' && k[^1] != 'Z')));
        }
        return range;
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
        { "Route 10 (Near Station)", AvailableWeather.Snowing },
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

    // From https://github.com/kwsch/pkNX/blob/3425f968c8fb3e4b81e64535a1bfa3bb0cc6aaa8/pkNX.Game/Misc/SWSHInfo.cs#L216
    public static readonly IReadOnlyDictionary<ulong, string> Zones = new Dictionary<ulong, string>
    {
        { 0x078BC1FF1A657844, "Route 1" },
        { 0x10355EFF1F4DB0B5, "Route 2" },
        { 0x776776717EA4483E, "Rolling Fields" },
        { 0x776777717EA449F1, "Dappled Grove" },
        { 0x776778717EA44BA4, "Watchtower Ruins" },
        { 0x776779717EA44D57, "East Lake Axewell" },
        { 0x77677A717EA44F0A, "West Lake Axewell" },
        { 0x77677B717EA450BD, "Axew's Eye" },
        { 0x77676C717EA43740, "South Lake Miloch" },
        { 0x77676D717EA438F3, "Giant's Seat" },
        { 0x776AFA717EA75E61, "North Lake Miloch" },
        { 0x194B97FF2492111A, "Route 3" },
        { 0x776E81717EAA799D, "Motostoke Riverbank" },
        { 0x776E7E717EAA7484, "Bridge Field" },
        { 0xDBCF5CFF0180B073, "Route 4" },
        { 0x8F67CD45F405D66E, "Slumbering Weald (Low Level)" },
        { 0xE0D6E5E78C91F4A7, "City of Motostoke" },
        { 0xE4E595FF06C510D8, "Route 5" },
        { 0x1C7150C0594994E5, "Town of Hulbury" },
        { 0x7D3B7A45E97D4A51, "Galar Mine No. 2" },
        { 0x75D83E45E5AA7953, "Galar Mine" },
        { 0x7D3B7745E97D4538, "Motostoke Outskirts" },
        { 0xA88AC04602050B95, "Glimwood Tangle" },
        { 0xEDFC32FF0C0A1B29, "Route 6" },
        { 0xF55F6BFF0FDCE70E, "Route 7" },
        { 0x449AE0FF3D19D777, "Route 8" },
        { 0x4BFDF9FF40EC6CFC, "Route 8 (on Steamdrift Way)" },
        { 0x4BFDFCFF40EC7215, "Route 9" },
        { 0x4BFDF6FF40EC67E3, "Route 9 (in Circhester Bay)" },
        { 0x4BFDFBFF40EC7062, "Route 9 (in Outer Spikemuth)" },
        { 0xB332930807F9D48A, "Route 10 (Near Station)" },
        { 0x7771E5717EAD5960, "Stony Wilderness" },
        { 0x7771E8717EAD5E79, "Dusty Bowl" },
        { 0x7771E7717EAD5CC6, "Giant's Mirror" },
        { 0x7771EA717EAD61DF, "Hammerlocke Hills" },
        { 0x7771E9717EAD602C, "Giant's Cap" },
        { 0x7771EC717EAD6545, "Lake of Outrage" },
        { 0x10355BFF1F4DAB9C, "Route 2 (High Level)" },
        { 0xB332920807F9D2D7, "Route 10" },
        { 0x8F67CB45F405D308, "Slumbering Weald (High Level)" },
        { 0xCD6E4FBCE1466F32, "Route 1" },
        { 0xDF686EC613544BD1, "Route 2" },
        { 0xD602B2A66C268F7C, "Rolling Fields" },
        { 0x458C9CA2C0087385, "Dappled Grove" },
        { 0xE20E6AE30AAA57D2, "Watchtower Ruins" },
        { 0xEEEEAC06BAC8D0B3, "East Lake Axewell" },
        { 0xF8D1E527F7B21FA0, "West Lake Axewell" },
        { 0xB6CFE90E0378FD79, "Axew's Eye" },
        { 0x520D8DD522E9A4C6, "South Lake Miloch" },
        { 0xBC7237A0392D8837, "Giant's Seat" },
        { 0xB67C706F5BAE9E35, "North Lake Miloch" },
        { 0xDA910F69A1B92FED, "West Lake Axewell (Surfing)" },
        { 0x7C17DB1B430F9543, "South Lake Miloch (Surfing)" },
        { 0xCC0F8A437312B8AC, "East Lake Axewell (Surfing)" },
        { 0x8BE2F6160986FB8E, "North Lake Miloch (Surfing)" },
        { 0x0E8392C0A57D5830, "Route 3" },
        { 0x82A7A328A26B9057, "Galar Mine" },
        { 0x5B2BC38E044EC2B7, "Route 4" },
        { 0x8D68276C03A332BE, "Route 5" },
        { 0x16D2FC4840A658A5, "Galar Mine No. 2" },
        { 0x3D6D58A96894575E, "Motostoke Outskirts" },
        { 0x6AA652641154B119, "Motostoke Riverbank" },
        { 0x36A5DC94335E1E72, "Bridge Field" },
        { 0xE503416A1C05765D, "Route 6" },
        { 0x201EF8E9D2A32D71, "Glimwood Tangle" },
        { 0x42312695C904658C, "Route 7" },
        { 0x1B95A78295F6F213, "Route 8" },
        { 0xAADAC3CB6A1DFE8A, "Route 8 (on Steamdrift Way)" },
        { 0x9116B224702CDCF1, "Route 9" },
        { 0xCDD3B5660D2E5E67, "Route 9 (in Circhester Bay)" },
        { 0x5A3B8F8147272058, "Route 9 (in Outer Spikemuth)" },
        { 0xA93101EA38598995, "Route 9  (Surfing)" },
        { 0x0181225223DE5420, "Route 10 (Near Station)" },
        { 0x1F0F1AE1818C4326, "Stony Wilderness" },
        { 0xAD11B3F3B2AC662D, "Dusty Bowl" },
        { 0xCD9719B2E64F2AA4, "Giant's Mirror" },
        { 0xCD48625EDC10CBFB, "Hammerlocke Hills" },
        { 0x712F3056573E23FA, "Giant's Cap" },
        { 0x593196758BA16B61, "Lake of Outrage" },
        { 0xF79DE930E6F50533, "Route 10" },
        { 0xA26A4595F72EDAEA, "Route 2 (High Level)" },
        { 0x56580C94EDFCE664, "Route 3 (Garbage)" },
        { 0xCB38FEA3F71C3958, "Rolling Fields (Flying)" },
        { 0x1F174D36062B8C38, "Rolling Fields (Ground)" },
        { 0x23017513039A78E7, "Rolling Fields (2)" },
        { 0xF1BA4AAD9AAB2C1A, "Watchtower Ruins (Flying)" },
        { 0x3D2E746F9D3F5CB5, "East Lake Axewell (Flying)" },
        { 0x6E121A9CE4F58F1E, "East Lake Axewell (Flying)" },
        { 0x3171A0C61793816E, "South Lake Miloch (Flying)" },
        { 0x198E4023A1B2DDEF, "South Lake Miloch (2)" },
        { 0xFAB1C08E70C0F1CA, "Motostoke Riverbank (Surfing)" },
        { 0xB9F76CEE459CEC07, "Bridge Field (Surfing)" },
        { 0x5F4E0AB29FD3F13A, "Bridge Field (Flying)" },
        { 0xF603DEA4177200EA, "Stony Wilderness (2)" },
        { 0x76EE4E28DD28374E, "Stony Wilderness (Flying)" },
        { 0x3F264B6FCB5647B4, "Giant's Mirror (Flying)" },
        { 0x2D887A1CA9B1B99A, "Dusty Bowl (Flying)" },
        { 0x2BE7E6A8901ECC20, "Giant's Mirror (Ground)" },
        { 0x39F0170769BF4524, "Dusty Bowl and Giant's Mirror (Surfing)" },
        { 0xB2067FBCF8D5C7BA, "Giant's Cap (Ground)" },
        { 0x48B9525945EE48B5, "Stony Wilderness (3)" },
        { 0xB5756B87989661E1, "Giant's Cap (2)" },
        { 0x7AB83D18C831DDEB, "Giant's Cap (3)" },
        { 0xDBEF8A8593377AAA, "Giant's Cap (Lunatone/Solrock)" },
        { 0x066F97F8765BC22D, "Hammerlocke Hills (Flying)" },
        { 0x87A97AFF94BC6CF2, "Lake of Outrage (Surfing)" },
        { 0x94289204B628522C, "Slumbering Weald (Low Level)" },
        { 0x5D02F15C043B872E, "Slumbering Weald (High Level)" },
        { 0xA4945486A2B97DFF, "Route 2 (Surfing)" },
        { 0xAC1187E9EC166853, "Route 9 (in Circhester Bay) (Surfing)" },

        // DLC 1 - Isle of Armor
        { 0x908A64718CA374E6, "Fields of Honor" },
        { 0x908A63718CA37333, "Soothing Wetlands" },
        { 0x908A62718CA37180, "Forest of Focus" },
        { 0x908A69718CA37D65, "Challenge Beach" },
        { 0x908A68718CA37BB2, "Brawlers' Cave" },
        { 0x908A67718CA379FF, "Challenge Road" },
        { 0x908A66718CA3784C, "Courageous Cavern" },
        { 0x908A6D718CA38431, "Loop Lagoon" },
        { 0x908A6C718CA3827E, "Training Lowlands" },
        { 0x90875F718CA13690, "Warm-Up Tunnel" },
        { 0x908760718CA13843, "Potbottom Desert" },
        { 0x909170718CA9A7F8, "Workout Sea" },
        { 0x909173718CA9AD11, "Stepping-Stone Sea" },
        { 0x909172718CA9AB5E, "Insular Sea" },
        { 0x909175718CA9B077, "Honeycalm Sea" },
        { 0x908DEC718CA691D5, "Honeycalm Island" },

        { 0x525D03DF0309D804, "Fields of Honor" },
        { 0xB0621052994A5089, "Fields of Honor (Surfing)" },
        { 0x91B1D1436BAF5871, "Fields of Honor (Beach)" },
        { 0xC449DFAB894F632C, "Loop Lagoon (Beach)" },
        { 0x273693DD91D7BD10, "Challenge Beach (Beach)" },
        { 0xD61582D408C39E60, "Challenge Beach (Surfing - River)" },
        { 0xBECC9623CD3E8C77, "Soothing Wetlands" },
        { 0x1C051CB6F97C2068, "Soothing Wetlands (Puddles)" },
        { 0xBC028EF260AD9406, "Forest of Focus" },
        { 0x32AB88FC9797DC83, "Forest of Focus (Surfing)" },
        { 0x39D078468AA0DCC1, "Challenge Beach" },
        { 0x3BFB22D0FB5B42D2, "Challenge Beach (Surfing - Ocean)" },
        { 0x2B1DF6E85F9BAE28, "Brawlers' Cave" },
        { 0x36FE81B956D0DCB5, "Brawlers' Cave (Surfing)" },
        { 0xBBAA199D0705405B, "Challenge Road" },
        { 0xFB9A7FD6D979C6DA, "Courageous Cavern" },
        { 0xBC0E1701C0276FCF, "Courageous Cavern (Surfing)" },
        { 0xAC2ED08E980FCFC5, "Loop Lagoon" },
        { 0x7D2E205E8E300EE1, "Loop Lagoon (Surfing)" },
        { 0x67E3FF10EB64FB79, "Training Lowlands (Beach)" },
        { 0x85E286D82C666BBC, "Training Lowlands" },
        { 0x95E125D2EE3ED656, "Warm-up Tunnel" },
        { 0xA7F495799F209587, "Potbottom Desert" },
        { 0x30AAD92559FCE81E, "Workout Sea" },
        { 0x6F748A46C8E3802C, "Workout Sea (Surfing)" },
        { 0x97A3E0687E3C5B01, "Stepping-Stone Sea (Surfing)" },
        { 0xDDDFF88957FD5B5C, "Insular Sea" },
        { 0xF3036CD294CE9365, "Stepping-Stone Sea" },
        { 0xFB9BB438425D58DA, "Insular Sea (Surfing)" },
        { 0xC16C1E2A1B5FFE87, "Honeycalm Sea (Surfing)" },
        { 0x081D7EF6A1C192B1, "Honeycalm Island" },
        { 0x86EFBF49516B5555, "Honeycalm Island (Surfing)" },
        { 0x39AB700A9F1AB71F, "Training Lowlands (Surfing)" },
        { 0x96C6A2A36131F383, "Stepping-Stone Sea (Sharpedo)" },
        { 0xC92D06352150C78A, "Insular Sea (Sharpedo)" },
        { 0xED1F9772AA35C3CD, "Workout Sea (Sharpedo)" },
        { 0x9C0049D3E6129924, "Honeycalm Sea (Sharpedo)" },

        // DLC 2 - Crown Tundra
        { 0x87E14B7187BC1CC1, "Slippery Slope" },
        { 0x87E1487187BC17A8, "Freezington" },
        { 0x87E1497187BC195B, "Frostpoint Field" },
        { 0x87E14E7187BC21DA, "Giant's Bed" },
        { 0x87E14F7187BC238D, "Old Cemetery" },
        { 0x87E14C7187BC1E74, "Snowslide Slope" },
        { 0x87E14D7187BC2027, "Tunnel to the Top" },
        { 0x87E1427187BC0D76, "Path to the Peak" },
        { 0x87E1437187BC0F29, "Crown Shrine" },
        { 0x87E4507187BE5B17, "Giant's Foot" },
        { 0x87E44F7187BE5964, "Roaring-Sea Caves" },
        { 0x87E4527187BE5E7D, "Frigid Sea" },
        { 0x87E4517187BE5CCA, "Three-Point Pass" },
        { 0x87DA3F7187B5E9AF, "Ballimere Lake" },
        { 0x87DA407187B5EB62, "Lakeside Cave" },
        { 0x87DA417187B5ED15, "Dyna Tree Hill" },

        { 0xD6EA3DE40B009E55, "Slippery Slope" },
        { 0xADF616908BD308DF, "Frostpoint Field" },
        { 0x308C5EB6A846D1F0, "Giant's Bed" },
        { 0x50E781F91B97C049, "Old Cemetery" },
        { 0xC303110BF1EC3322, "Snowslide Slope" },
        { 0xB768660B0BF4C0C3, "Tunnel to the Top" },
        { 0xFCB78AFCCECAF094, "Path to the Peak" },
        { 0xA345459C03EA6673, "Giant's Foot" },
        { 0xE4A982819ACF7292, "Roaring-Sea Caves" },
        { 0x18AAF85178C7B839, "Frigid Sea" },
        { 0x3EC6FCDC0C77D460, "Three-Point Pass" },
        { 0xE5225F9325CCA74B, "Ballimere Lake" },
        { 0x2F1B41507D695958, "Lakeside Cave" },

        { 0xF8A59FCA719D1EAE, "Giant's Bed / Giant's Foot (Surfing)" },
        { 0x55D8F226A42368B7, "Roaring-Sea Caves (Surfing)" },
        { 0x78536116469DC44D, "Frigid Sea (Surfing)" },
        { 0x9BDD6D11FFBEDA3F, "Ballimere Lake (Surfing)" },

        // Extra -- No Encounters and/or Weather, not in pkNX
        { 0x019B5DC049D8CB3E, "Town of Postwick" },
        { 0xC4811BAAA2CB7B86, "Your House" },
        { 0xA9AB2CAA935AB8AB, "Hop's House (Downstairs)" },
        { 0xA9AB2DAA935ABA5E, "Hop's House (Upstairs)" },
        { 0xE80BCEC03B7D6E23, "Town of Wedgehurst" },
        { 0xAE507D07A6B3CCFD, "Wedgehurst Station" },
        { 0xB019E4C35D0B6FBF, "Pokémon Research Lab" },
        { 0xC026BAC365C67415, "Town of Wedgehurst (House)" },
        { 0x94D771C34D3EADB8, "Town of Wedgehurst (House)" },
        { 0x9458C085A640AA9A, "Wedgehurst Boutique" },
        { 0x9A018D90DC5F1D15, "Wedgehurst Pokémon Center" },
        { 0xC93CF3C36B0AD47A, "Magnolia's House (Downstairs)" },
        { 0xC93CF2C36B0AD2C7, "Magnolia's House (Upstairs)" },
        { 0x490D42709F87B935, "Wild Area Station" },
        { 0xBC0019AF7C29A129, "Motostoke Pokémon Center" },
        { 0x9EB6C735FAD546D8, "Motostoke Hair Salon" },
        { 0x3527A3BF292595F6, "Motostoke Boutique" },
        { 0xAD65494023FE34A1, "Motostoke Battle Café" },
        { 0x44F96292EF9E3D04, "Motostoke Stadium" },
        { 0xB2E97CAF76E496D8, "Motostoke Pokémon Center (West)" },
        { 0xCFFBA69AFDF6F879, "Motostoke Station" },
        { 0xB60B5F4028E351D6, "Budew Drop Inn (Marnie's Room)" },
        { 0xB60EE14028E66493, "Budew Drop Inn (Upstairs)" },
        { 0xB60B604028E35389, "Budew Drop Inn (Guest Room)" },
        { 0xB60B5E4028E35023, "Budew Drop Inn (Guest Room)" },
        { 0xB60B5D4028E34E70, "Budew Drop Inn (Guest Room)" },
        { 0xB60EE24028E66646, "Budew Drop Inn (Downstairs)" },
        { 0xF12207C040C1CE88, "Town of Turffield" },
        { 0xB641F5C0EAA2E42C, "Turffield Pokémon Center" },
        { 0x49529A72E22FC56E, "Turffield Stadium" },
        { 0xD828AAE89C5F139C, "Route 5 (Nursery)" },
        { 0xADBFD15574453573, "Hulbury Pokémon Center" },
        { 0x32EBA5829BB4FAAC, "Town of Hulbury (House)" },
        { 0x29D58C829670D0A7, "Town of Hulbury (House)" },
        { 0x22724F82929DFDF6, "Town of Hulbury (House)" },
        { 0x19C8B6828DB5CC51, "Town of Hulbury (House)" },
        { 0x079C6082832D3B1B, "Town of Hulbury (House)" },
        { 0xFE85C3827DE830CA, "Town of Hulbury (House)" },
        { 0x3EC246133B01279F, "Hulbury Station" },
        { 0x2B7C86C0D370D3C3, "Hulbury Stadium" },
        { 0x3C01E282A0F961DD, "Hulbury Seafood Restaurant" },
        { 0xFC1954E79C5EAFE2, "City of Hammerlocke" },
        { 0x5B4476E6BCF58EFF, "Hammerlocke Pokémon Center" },
        { 0x62A7AFE6C0C85AE4, "Hammerlocke Pokémon Center (East)" },
        { 0x746785E6CAF4F3BA, "Hammerlocke Pokémon Center (West)" },
        { 0x7D7712562B0A0535, "Hammerlocke Salon" },
        { 0x7A75174C99405DA7, "Hammerlocke Battle Café" },
        { 0xD3D652AC0B932A57, "Hammerlocke Boutique" },
        { 0x838B304C9E8487AC, "City of Hammerlocke (House)" },
        { 0x6A68414C90855951, "City of Hammerlocke (House)" },
        { 0x7311DA4C956D8AF6, "City of Hammerlocke (House)" },
        { 0x583BEB4C85FCC81B, "City of Hammerlocke (House)" },
        { 0x6152044C8B40F220, "City of Hammerlocke (House)" },
        { 0xB898162A0A0A6BD4, "Hammerlocke Station" },
        { 0xCE8AD9F93158D698, "Hammerlocke Vault (Lobby)" },
        { 0xCE8ADCF93158DBB1, "Hammerlocke Vault (Tapestry Room)" },
        { 0xD7A176F9369DE0E9, "Hammerlocke Stadium" },
        { 0x987DEA45F94A073F, "Energy Plant" },
        { 0x987DEB45F94A08F2, "Tower Summit" },
        { 0x23D489C05D1C60CA, "Town of Stow-on-Side" },
        { 0x22A25C8EE6321E22, "Stow-on-Side Pokémon Center" },
        { 0xACB9D35459EF52EA, "Town of Stow-on-Side (House)" },
        { 0xB76358BE492CE4C1, "Stow-on-Side Stadium (Shield)" },
        { 0x0AB1FAC04F1DD58F, "Town of Ballonlea" },
        { 0x3283A35F4B590F11, "Ballonlea Pokémon Center" },
        { 0x4ED6DFA8D9E5EE53, "Town of Ballonlea (House)" },
        { 0x68666EA8E8414B6E, "Town of Ballonlea (House)" },
        { 0x5DA93B6AA5FCE7ED, "Ballonlea Stadium" },
        { 0xB1A0F94607496BFA, "Route 9 Tunnel" },
        { 0x13C833C0546235F4, "City of Circhester" },
        { 0xCE0D5D7C6A438FE8, "Circhester Pokémon Center" },
        { 0x108FC73491CDABD1, "City of Circhester (House)" },
        { 0x1939603496B5DD76, "City of Circhester (House)" },
        { 0x209C9D349A88B027, "City of Circhester (House)" },
        { 0x07798A348C8944A0, "Bob's Your Uncle" },
        { 0x93A8B6F339FE13BF, "Circhester Stadium (Shield)" },
        { 0x7654A65E387422C1, "Circhester Boutique" },
        { 0x29B2B6349FCCDA2C, "City of Circhester (House)" },
        { 0x32C8F334A511415D, "City of Circhester (House)" },
        { 0x3BDF0C34AA556B62, "City of Circhester (House)" },
        { 0xEC8E9A6929639FF3, "Circhester Hair Salon" },
        { 0xB984193460611DA3, "Hotel Ionia (West Lobby)" },
        { 0xC29A523465A57E08, "Hotel Ionia (East Lobby)" },
        { 0xB9841A3460611F56, "Hotel Ionia (West, Upstairs)" },
        { 0xB9879D34606433C6, "Hotel Ionia (West, Guest Room)" },
        { 0xB9879E3460643579, "Hotel Ionia (West, Guest Room)" },
        { 0xB9879C3460643213, "Hotel Ionia (Morimoto's Room)" },
        { 0xB9879B3460643060, "Hotel Ionia (Director's Room)" },
        { 0xC29A553465A58321, "Hotel Ionia (East, Upstairs)" },
        { 0xC296CB3465A262CC, "Hotel Ionia (East, Guest Room)" },
        { 0xC296CC3465A2647F, "Hotel Ionia (East, Guest Room)" },
        { 0xC296CE3465A267E5, "Hotel Ionia (East, Guest Room)" },
        { 0xC296CD3465A26632, "Hotel Ionia (East, Guest Room)" },
        { 0x3F177CC06CE9FC51, "Town of Spikemuth" },
        { 0xEFD167216DCE3D6F, "Spikemuth Pokémon Center" },
        { 0x265558880850C4CC, "White Hill Station" },
        { 0xB332910807F9D124, "Route 10 (Wyndon Outskirts)" },
        { 0xF3033BE7971A85DD, "City of Wyndon" },
        { 0xF6B617AC79AFAEEB, "Wyndon Pokémon Center" },
        { 0xA36BA36CD4D60347, "Wyndon Station" },
        { 0xD191F45AD6680385, "Wyndon Battle Café" },
        { 0xC5C383AF7C163AE4, "Wyndon Boutique" },
        { 0x856CE35A394548A2, "Wyndon Hair Salon" },
        { 0x7E87BD6C177B41F2, "Wyndon Stadium" },
        { 0x118C06AC892071C6, "Wyndon Pokémon Center (Stadium)" },
        { 0x5B6C9963B1159C1F, "City of Wyndon (House)" },
        { 0x52567C63ABD16B4E, "City of Wyndon (House)" },
        { 0x4AF34363A7FE9F69, "City of Wyndon (House)" },
        { 0x41DCA663A2B99518, "City of Wyndon (House)" },
        { 0x38C66D639D7534B3, "City of Wyndon (House)" },
        { 0xBB216063E7698272, "City of Wyndon (House)" },
        { 0xB20B4763E225586D, "City of Wyndon (House)" },
        { 0xBFD21E5ACC3B6AAF, "City of Wyndon (House)" },
        { 0xC8E8575AD17FCB14, "City of Wyndon (House)" },
        { 0x6D2C6F63BB4234F5, "Rose of the Rondelands (Lobby)" },
        { 0xF30334E7971A79F8, "City of Wyndon (Battle Tower)" },
        { 0xA25B9B913B464830, "Battle Tower (Lobby)" },

        { 0xA5B7301A16292030, "Armor Station" },
        { 0xFA9A0FC1C1A0026E, "Master Dojo" },
        { 0xFA968AC1C19CEA98, "Master Dojo (Stadium)" },
        { 0xE10A80C1B344A553, "Tower of Darkness (1F)" },
        { 0xE10A81C1B344A706, "Tower of Darkness (2F)" },
        { 0xE10A82C1B344A8B9, "Tower of Darkness (3F)" },
        { 0xE10A83C1B344AA6C, "Tower of Darkness (4F)" },
        { 0xE10A84C1B344AC1F, "Tower of Darkness (5F)" },
        { 0xE86DB9C1B7177138, "Tower of Waters (1F)" },
        { 0xE86DBCC1B7177651, "Tower of Waters (2F)" },
        { 0xE86DBBC1B717749E, "Tower of Waters (3F)" },
        { 0xE86DBEC1B71779B7, "Tower of Waters (4F)" },
        { 0xE86DBDC1B7177804, "Tower of Waters (5F)" },

        { 0x29AEBA83AE8DD323, "Crown Tundra Station" },
        { 0xD3DA05461A8CCB26, "Max Lair" },
        { 0xBEA37F2C0F2FE532, "Freezington (Cosmog House)" },
        { 0x93543A2BF6A825A1, "Freezington (Mayor's House)" },
        { 0x8127E42BEC1F946B, "Freezington (Sonia's House)" },
        { 0x8A3DFD2BF163BE70, "Freezington (Peony's House)" },
        { 0x9FE12345FD1CD324, "Crown Shrine (Inside)" },
        { 0xCB30EA4615A56F9B, "Iron Ruins" },
        { 0xCB30EC4615A57301, "Rock Peak Ruins" },
        { 0xCB30E94615A56DE8, "Iceburg Ruins" },
        { 0xCB33F04615A7AFA4, "Split-Decision Ruins" },

        // Unobserved, with corresponding AHTB entry
     // { 0xADA2BC0BA0B72162, "z_wr0307_camlimit" },
     // { 0x4802A4A9ABBA7856, "z_btl76_wr03" },
     // { 0x9220AA3EF4282491, "z_btl75_wr03" },
     // { 0x9D3A721C080275B4, "z_btl74_wr03" },
     // { 0x2B019EAB8A239C3F, "z_btl73_wr03" },
     // { 0xB4C3BC4B8333CCB2, "z_btl72_d0901" },
     // { 0x0655FA6133A11FA5, "z_btl71_d0701" },
     // { 0x90AE33E9F8407F26, "z_btl57_wr02" },
     // { 0xB3F77A88FD6DFA8F, "z_btl56_wr02" },
     // { 0xADB787AFC994B048, "z_btl55_wr02" },
     // { 0x477CE5983F86E931, "z_btl54_wr02" },
     // { 0xE6CCA2023F26D9A2, "z_btl53_wr02" },
     // { 0xF07980BB06852CAD, "z_btl52_i0301" },
     // { 0x4215A6C5AAD5C9DB, "z_btl51_i0201" },
     // { 0xD3DA04461A8CC973, "z_d0902" },
     // { 0x776775717EA4468B, "z_wr0101" },
     // { 0x645233F24410810B, "z_btl39_cm02" },
     // { 0x2960BAD449012C4F, "z_btl38_cm01" },
     // { 0x95534ECF43FE7B17, "z_btl37_wr03" },
     // { 0x2A4A7B671F531D27, "z_btl36_vs02" },
     // { 0x643E8385D05CF611, "z_btl35_vs01" },
     // { 0xDBC1F5A6A7FA6388, "z_btl34_r1001" },
     // { 0x5EC5517D22D3A9F0, "z_btl33_d0102" },
     // { 0x92043BFB09248475, "z_btl32_d0603" },
     // { 0x85B8829700057513, "z_btl31_d0602" },
     // { 0xEEDB58F408C61351, "z_btl30_d0601" },
     // { 0x56CC5A8F6A6F28E0, "z_btl29_r0202" },
     // { 0x5ED0E2B822403E62, "z_btl28_t0801" },
     // { 0x3F10B7E3443D2DCC, "z_btl27_t0101" },
     // { 0x47E101D07F5563A6, "z_btl26_wr02" },
     // { 0xFEAFB07EC69BC898, "z_btl25_wr01" },
     // { 0xE7A1A796123FBF02, "z_btl24_c0301" },
     // { 0x09FE088F86670D96, "z_btl23_t0802" },
     // { 0x0EDDB858E46845BB, "z_btl22_r0701" },
     // { 0x828F6C6B6FE96351, "z_btl21_r0902" },
     // { 0x04EEEABA67E9321E, "z_btl20_t0702" },
     // { 0x5B5324A6CB05074F, "z_btl19_t0701" },
     // { 0xB882C8AD0412190C, "z_btl18_r0901" },
     // { 0x1E40E94711594882, "z_btl17_r0801" },
     // { 0x7E20B70DB1709CE5, "z_btl16_t0601" },
     // { 0x8FC3263078DE5E3E, "z_btl15_d0401" },
     // { 0xA81ABFEF1B9D14CB, "z_btl14_t0502" },
     // { 0xFA64AD26838522D3, "z_btl13_t0501" },
     // { 0x46216B83FC100BBF, "z_btl12_r0601" },
     // { 0xAA30F29D3F47A0B4, "z_btl11_c0101" },
     // { 0x3829765D81A5108D, "z_btl10_t0401" },
     // { 0x4A5223597BB476BF, "z_btl09_r0502" },
     // { 0x80A5101F8C2D8F7D, "z_btl08_r0501" },
     // { 0xDDC117C68D8CAF84, "z_btl07_t0301" },
     // { 0x5E107D70F500AEC4, "z_btl06_r0401" },
     // { 0xA8C0190F360F66D9, "z_btl05_d0201" },
     // { 0x60C2C51868030B3F, "z_btl04_r0301" },
     // { 0xF5AA2A8F6214D90D, "z_btl03_r0201" },
     // { 0x341AC96DA119AA07, "z_btl02_r0101" },
     // { 0xE39AC4F1EF8A19F4, "z_btl01_d0101" },
     // { 0x5C6F8500CD1E0A3A, "TZ_performance" },
     // { 0xE3B6DDB74FFAA9F4, "z_tr0101" },
     // { 0x93AC3BF33A012B95, "z_t0701_g0210" },
     // { 0x93A8B7F339FE1572, "z_t0701_g0202" },
     // { 0xACC840F347FA60A4, "z_t0701_g0110" },
     // { 0xACCBC4F347FD76C7, "z_t0701_g0102" },
     // { 0xACCBC5F347FD787A, "z_t0701_g0101" },
     // { 0x5DACA06AA5FFC963, "z_t0601_g0110" },
     // { 0x5DA9366AA5FCDF6E, "z_t0601_g0104" },
     // { 0x5DA9396AA5FCE487, "z_t0601_g0103" },
     // { 0x5DA9386AA5FCE2D4, "z_t0601_g0102" },
     // { 0x91E3C4544A7E59AF, "z_t0501_i0201" },
     // { 0xB7665DBE492F2317, "z_t0501_g0210" },
     // { 0xB76355BE492CDFA8, "z_t0501_g0202" },
     // { 0xAFFCB6BE4557309A, "z_t0501_g0110" },
     // { 0xB0001EBE455A1729, "z_t0501_g0102" },
     // { 0xB0001BBE455A1210, "z_t0501_g0101" },
     // { 0x10B2798288716520, "z_t0401_i0601" },
     // { 0x2B7F8BC0D3731219, "z_t0401_g0110" },
     // { 0x2B7C87C0D370D576, "z_t0401_g0102" },
     // { 0x494F1572E22CAD98, "z_t0301_g0110" },
     // { 0x49529972E22FC3BB, "z_t0301_g0102" },
     // { 0x7E84B86C1779039C, "z_pl0110" },
     // { 0x7E87BC6C177B403F, "z_pl0102" },
     // { 0x8F67CC45F405D4BB, "z_d0102" },
     // { 0x647F4D63B656E4AE, "z_c0301_i0210" },
     // { 0x6482D563B65A019D, "z_c0301_i0202" },
     // { 0x6482D263B659FC84, "z_c0301_i0201" },
     // { 0xD7A47BF936A01F3F, "z_c0201_g0210" },
     // { 0x44F5DD92EF9B252E, "z_c0101_g0210" },
     // { 0x4DA2FC92F486705C, "z_c0101_g0102" },
     // { 0x4DA2FF92F4867575, "z_c0101_g0101" },
     // { 0xA25836913B4366BA, "z_bt0110" },

        // Unobserved, without corresponding AHTB entry
     // { 0x8974F277DC8A6276, "8974F277DC8A6276" },
     // { 0xAE507A07A6B3C7E4, "AE507A07A6B3C7E4" },
     // { 0x95B7864CA90D18E2, "95B7864CA90D18E2" },
     // { 0xA25B9E913B464D49, "A25B9E913B464D49" },
     // { 0xF30336E7971A7D5E, "F30336E7971A7D5E" },
     // { 0xF30339E7971A8277, "F30339E7971A8277" },
     // { 0xF30338E7971A80C4, "F30338E7971A80C4" },
    };
}
