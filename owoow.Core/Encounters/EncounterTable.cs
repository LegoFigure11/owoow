using owoow.Core.Interfaces;
using static owoow.Core.Encounters;

namespace owoow.Core.EncounterTable;

public class EncounterTable(string game, string tabName, string area, string weather, string leadAbility)
{
    public IDictionary<int, IEncounterTableEntry> MainTable { get; set; } = GetEncounterTable(game, tabName, area, weather)!;
    public IDictionary<int, IEncounterTableEntry> AbilityTable { get; set; } = GetAbilityTable(game, tabName, area, weather, leadAbility)!;

    private const byte EXPECTED_COUNT = 100;

    public static IDictionary<int, IEncounterTableEntry> GetEncounterTable(string game, string tabName, string area, string weather)
    {
        var enc = GetEncounters(game, tabName, area, weather);
        Dictionary<int, IEncounterTableEntry> table = [];

        foreach (var v in enc.Encounters!.Values)
        {
            if (Encounters.Personal is null) continue;
            for (var i = v.SlotMin; i <= v.SlotMax; i++)
            {
                var t = new EncounterTableEntry(Encounters.Personal[v.Species!], v, enc);
                table.Add(i, t);
            }
        }

        if (table.Count != EXPECTED_COUNT)
        {
            var ex = new Exception(
                @$"Uable to build encounter table! Please report this as a bug.
                Game: {game}
                Encounter Type: {tabName}
                Area: {area}
                Count: {weather} (Expected {EXPECTED_COUNT})"
                );
            throw ex;
        }

        return table;
    }

    public static IDictionary<int, IEncounterTableEntry> GetAbilityTable(string game, string tabName, string area, string weather, string ability)
    {
        var enc = GetEncounters(game, tabName, area, weather);
        Dictionary<int, IEncounterTableEntry> table = [];
        var type = RNG.Util.GetLeadAbilityType(ability);
        var i = 0;
        foreach (var v in enc.Encounters!.Values)
        {
            if (Encounters.Personal is null) continue;

            var t = new EncounterTableEntry(Encounters.Personal[v.Species!], v, enc);
            if (t.Types.Contains(type))
            {
                table.Add(i++, t);
            }
        }
        return table;
    }
}
