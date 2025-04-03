using owoow.Core.Interfaces;
using System.Collections.Immutable;
using System.Text.Json;

namespace owoow.Core;

public static class Encounters
{
    public readonly static Dictionary<string, Personal>? Personal;

    private readonly static Dictionary<string, Dictionary<string, Encounter>>? SwordFishing;
    private readonly static Dictionary<string, Dictionary<string, Encounter>>? SwordHidden;
    private readonly static Dictionary<string, Dictionary<string, Encounter>>? SwordSymbol;
    private readonly static Dictionary<string, Dictionary<string, EncounterStatic>>? SwordStatic;

    private readonly static Dictionary<string, Dictionary<string, Encounter>>? ShieldHidden;
    private readonly static Dictionary<string, Dictionary<string, Encounter>>? ShieldFishing;
    private readonly static Dictionary<string, Dictionary<string, Encounter>>? ShieldSymbol;
    private readonly static Dictionary<string, Dictionary<string, EncounterStatic>>? ShieldStatic;

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

    public static ImmutableSortedDictionary<string, List<EncounterLookupEntry>> GetEncounterLookupForGame(string game)
    {
        var list = new Dictionary<string, List<EncounterLookupEntry>>();
        var fishing = game == "Sword" ? SwordFishing : ShieldFishing;
        var hidden = game == "Sword" ? SwordHidden : ShieldHidden;
        var symbol = game == "Sword" ? SwordSymbol : ShieldSymbol;
        var strong = game == "Sword" ? SwordStatic : ShieldStatic;

        foreach (var area in symbol!)
        {
            foreach (var weather in area.Value)
            {
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
                    });
                }
            }
        }

        foreach (var area in hidden!)
        {
            foreach (var weather in area.Value)
            {
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

    public static Encounter? GetEncounters(string game, string tabName, string area, string weather)
    {
        try
        {
            return game switch
            {
                "Sword" => tabName switch
                {
                    "Fishing" => SwordFishing![area][weather],
                    "Hidden" => SwordHidden![area][weather],
                    _ => SwordSymbol![area][weather],
                },
                _ => tabName switch
                {
                    "Fishing" => ShieldFishing![area][weather],
                    "Hidden" => ShieldHidden![area][weather],
                    _ => ShieldSymbol![area][weather],
                }
            };
        }
        catch { return null; }
    }

    public static EncounterStatic? GetStaticEncounters(string game, string area, string weather)
    {
        try
        {
            return game switch
            {
                "Sword" => SwordStatic![area][weather],
                _ => ShieldStatic![area][weather],
            };
        }
        catch { return null; }
    }

    public static IReadOnlyList<string> GetAreaList(string game, string tabName) => game switch
    {
        "Sword" => tabName switch
        {
            "Fishing" => [.. SwordFishing!.Keys],
            "Hidden" => [.. SwordHidden!.Keys],
            "Static" => [.. SwordStatic!.Keys],
            _ => [.. SwordSymbol!.Keys],
        },
        _ => tabName switch
        {
            "Fishing" => [.. ShieldFishing!.Keys],
            "Hidden" => [.. ShieldHidden!.Keys],
            "Static" => [.. ShieldStatic!.Keys],
            _ => [.. ShieldSymbol!.Keys],
        },
    };

    public static IReadOnlyList<string> GetWeatherList(string game, string tabName, string area) => game switch
    {
        "Sword" => tabName switch
        {
            "Fishing" => [.. SwordFishing![area].Keys],
            "Hidden" => [.. SwordHidden![area].Keys],
            "Static" => [.. SwordStatic![area].Keys],
            _ => [.. SwordSymbol![area].Keys],
        },
        _ => tabName switch
        {
            "Fishing" => [.. ShieldFishing![area].Keys],
            "Hidden" => [.. ShieldHidden![area].Keys],
            "Static" => [.. ShieldStatic![area].Keys],
            _ => [.. ShieldSymbol![area].Keys],
        },
    };

    public static IReadOnlyList<string> GetSpeciesList(string game, string tabName, string area, string weather)
    {
        if (tabName == "Static") return GetSpeciesListStatic(game, area, weather);

        var enc = GetEncounters(game, tabName, area, weather);
        IList<string> ret = [];
        if (enc is not null)
        {
            foreach (var v in enc.Encounters!.Values)
            {
                if (!ret.Contains(v.Species!))
                    ret.Add(v.Species!);
            }
        }
        return ret.AsReadOnly();
    }

    public static IReadOnlyList<string> GetSpeciesListStatic(string game, string area, string weather)
    {
        var enc = GetStaticEncounters(game, area, weather);
        IList<string> ret = [];
        if (enc is not null)
        {
            foreach (var v in enc.Encounters!.Values)
            {
                if (!ret.Contains(v.Species!))
                    ret.Add(v.Species!);
            }
        }
        return ret.AsReadOnly();
    }
}
