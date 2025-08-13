using owoow.Core.Enums;
using owoow.Core.Interfaces;
using static owoow.Core.Encounters;

namespace owoow.Core.EncounterTable;

public class EncounterTable(Game game, EncounterType EncounterType, string area, string weather, string leadAbility)
{
    public IDictionary<int, IEncounterTableEntry> MainTable { get; set; } = GetEncounterTable(game, EncounterType, area, weather)!;
    public IDictionary<int, IEncounterTableEntry> AbilityTable { get; set; } = GetAbilityTable(game, EncounterType, area, weather, leadAbility)!;
    public IDictionary<int, IEncounterStaticTableEntry> StaticTable { get; set; } = GetStaticTable(game, area, weather);

    private const byte EXPECTED_COUNT = 100;

    public static IDictionary<int, IEncounterTableEntry>? GetEncounterTable(Game game, EncounterType encounterType, string area, string weather)
    {
        try
        {
            var enc = GetEncounters(game, encounterType, area, weather);
            Dictionary<int, IEncounterTableEntry> table = [];

            if (enc is not null)
            {
                foreach (var v in enc.Encounters!.Values)
                {
                    if (Encounters.Personal is null) continue;
                    for (var i = v.SlotMin; i <= v.SlotMax; i++)
                    {
                        var t = new EncounterTableEntry(Encounters.Personal[v.Species!], v, enc);
                        table.Add(i, t);
                    }
                }

                if (table.Count == EXPECTED_COUNT) return table;

                var ex = new Exception(
                    @$"Unable to build encounter table! Please report this as a bug.
                       Game: {game}
                       Encounter Type: {encounterType}
                       Area: {area}
                       Count: {weather} (Expected {EXPECTED_COUNT})"
                );
                throw ex;
            }
            return table;
        }
        catch { return null; }
    }

    public static IDictionary<int, IEncounterTableEntry> GetAbilityTable(Game game, EncounterType encounterType, string area, string weather, string ability)
    {
        var enc = GetEncounters(game, encounterType, area, weather);
        Dictionary<int, IEncounterTableEntry> table = [];
        var type = RNG.Util.GetTypePullingLeadAbilityType(ability);
        var i = 0;
        if (enc is not null)
        {
            foreach (var v in enc.Encounters!.Values)
            {
                if (Encounters.Personal is null) continue;

                var t = new EncounterTableEntry(Encounters.Personal[v.Species!], v, enc);
                if (t.Types.Contains(type))
                {
                    table.Add(i++, t);
                }
            }
        }
        return table;
    }

    public static IDictionary<int, IEncounterStaticTableEntry> GetStaticTable(Game game, string area, string weather)
    {
        var enc = GetStaticEncounters(game, area, weather);
        Dictionary<int, IEncounterStaticTableEntry> table = [];
        var i = 0;
        if (enc is not null)
        {
            foreach (var v in enc.Encounters!.Values)
            {
                if (Encounters.Personal is null) continue;

                var t = new EncounterStaticTableEntry(Encounters.Personal[v.Species!], v);
                table.Add(i++, t);
            }
        }
        return table;
    }
}
