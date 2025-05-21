namespace owoow.Core.Interfaces;

public interface IEncounter
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

public interface IEncounterStaticEntry
{
    string? Species { get; }
    int Level { get; }
    bool IsAbilityLocked { get; }
    bool IsGenderLocked { get; }
    bool IsShinyLocked { get; }
    ulong Ability { get; }
    int GuaranteedIVs { get; }
}

public class Encounter : IEncounter
{
    public int MinLevel { get; set; } = 0;
    public int MaxLevel { get; set; } = 0;

    public Dictionary<string, EncounterEntry>? Encounters { get; set; } = [];
}

public class EncounterStatic
{
    public Dictionary<string, EncounterStaticEntry>? Encounters { get; set; } = [];
}

public class EncounterEntry : IEncounterEntry
{
    public string? Species { get; set; } = string.Empty;
    public int SlotMin { get; set; } = 0;
    public int SlotMax { get; set; } = 0;
}

public class EncounterStaticEntry : IEncounterStaticEntry
{
    public string? Species { get; set; } = string.Empty;
    public int Level { get; set; } = 0;
    public bool IsAbilityLocked { get; set; } = false;
    public bool IsGenderLocked { get; set; } = false;
    public bool IsShinyLocked { get; set; } = false;
    public ulong Ability { get; set; } = 0;
    public int GuaranteedIVs { get; set; } = 0;
}

public class EncounterLookupEntry : IEncounter, IEncounterEntry, IEncounterStaticEntry
{
    public string Species { get; set; } = string.Empty;
    public int SlotMin { get; set; } = 0;
    public int SlotMax { get; set; } = 0;
    public int Level { get; set; } = 0;
    public int MinLevel { get; set; } = 0;
    public int MaxLevel { get; set; } = 0;
    public int EncounterRate { get; set; } = 0;
    public bool IsAbilityLocked { get; set; } = false;
    public bool IsGenderLocked { get; set; } = false;
    public bool IsShinyLocked { get; set; } = false;
    public ulong Ability { get; set; } = 0;
    public int GuaranteedIVs { get; set; } = 0;
    public string Weather { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string EncounterType { get; set; } = string.Empty;
}
