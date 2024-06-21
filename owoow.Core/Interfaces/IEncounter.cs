namespace owoow.Core.Interfaces;

public interface IEncounter
{
    int MinLevel { get; }
    int MaxLevel { get; }
    Dictionary<string, EncounterEntry> Encounters { get; }
}

public interface IEncounterEntry
{
    int SlotMin { get; }
    int SlotMax { get; }
}

public class Encounter : IEncounter
{
    public int MinLevel { get; set; } = 0;
    public int MaxLevel { get; set; } = 0;

    public Dictionary<string, EncounterEntry> Encounters { get; set; } = [];
}

public class EncounterEntry : IEncounterEntry
{
    public int SlotMin { get; set; } = 0;
    public int SlotMax { get; set; } = 0;
}
