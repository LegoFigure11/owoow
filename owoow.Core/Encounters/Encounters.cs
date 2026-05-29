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
    };
}
