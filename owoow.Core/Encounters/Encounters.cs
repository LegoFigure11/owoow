using owoow.Core.Interfaces;
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

    public static Encounter GetEncounters(string game, string tabName, string area, string weather) => game switch
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
        },
    };

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
        foreach (var v in enc.Encounters!.Values)
        {
            if (!ret.Contains(v.Species!))
                ret.Add(v.Species!);
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
