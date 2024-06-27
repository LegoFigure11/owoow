namespace owoow.Core.Interfaces;

public partial interface IEncounter
{
    int MinLevel { get; }
    int MaxLevel { get; }
}

public interface IEncounterEntry
{
    string? Species { get; }
    int SlotMin { get; }
    int SlotMax { get; }
}

public class Encounter : IEncounter
{
    public int MinLevel { get; set; } = 0;
    public int MaxLevel { get; set; } = 0;

    public Dictionary<string, EncounterEntry>? Encounters { get; set; } = [];
}

public class EncounterEntry : IEncounterEntry
{
    public string? Species { get; set; } = string.Empty;
    public int SlotMin { get; set; } = 0;
    public int SlotMax { get; set; } = 0;
}
