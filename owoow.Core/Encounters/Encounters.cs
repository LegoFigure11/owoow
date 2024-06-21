using owoow.Core.Interfaces;
using System.Text.Json;

namespace owoow.Core;

public static class Encounters
{
    public readonly static Dictionary<string, Personal>? Personal;

    public readonly static Dictionary<string, Dictionary<string, Encounter>>? SwordFishing;
    public readonly static Dictionary<string, Dictionary<string, Encounter>>? SwordHidden;
    public readonly static Dictionary<string, Dictionary<string, Encounter>>? SwordSymbol;

    public readonly static Dictionary<string, Dictionary<string, Encounter>>? ShieldHidden;
    public readonly static Dictionary<string, Dictionary<string, Encounter>>? ShieldFishing;
    public readonly static Dictionary<string, Dictionary<string, Encounter>>? ShieldSymbol;

    static Encounters()
    {
        Personal = JsonSerializer.Deserialize<Dictionary<string, Personal>>(Utils.GetStringResource("personal.json") ?? "{}");

        SwordFishing = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Encounter>>>(Utils.GetStringResource("sw_fishing.json") ?? "{}");
        SwordHidden = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Encounter>>>(Utils.GetStringResource("sw_hidden.json") ?? "{}");
        SwordSymbol = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Encounter>>>(Utils.GetStringResource("sw_symbol.json") ?? "{}");

        ShieldFishing = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Encounter>>>(Utils.GetStringResource("sh_fishing.json") ?? "{}");
        ShieldHidden = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Encounter>>>(Utils.GetStringResource("sh_hidden.json") ?? "{}");
        ShieldSymbol = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Encounter>>>(Utils.GetStringResource("sh_symbol.json") ?? "{}");
    }
}
