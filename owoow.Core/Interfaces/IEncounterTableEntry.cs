namespace owoow.Core.Interfaces;

public interface IEncounterTableEntry : IEncounter, IEncounterEntry, IPersonal
{
}

public interface IEncounterStaticTableEntry : IEncounterStaticEntry, IPersonal
{
}


public class EncounterTableEntry(IPersonal personal, IEncounterEntry entry, IEncounter encounter) : IEncounterTableEntry
{
    public string Species { get; } = entry.Species!;

    public byte EggMoveCount { get; } = personal.EggMoveCount;
    public string[] EggMoves { get; } = personal.EggMoves!;
    public bool HasItems { get; } = personal.HasItems;
    public string[] Items { get; } = personal.Items!;
    public short DevId { get; } = personal.DevId;
    public short Gender { get; } = personal.Gender;
    public string[] Types { get; } = personal.Types!;
    public string[] Abilities { get; } = personal.Abilities!;

    public int SlotMin { get; } = entry.SlotMin;
    public int SlotMax { get; } = entry.SlotMax;

    public int MinLevel { get; } = encounter.MinLevel;
    public int MaxLevel { get; } = encounter.MaxLevel;
}

public class EncounterStaticTableEntry(IPersonal personal, IEncounterStaticEntry entry) : IEncounterStaticTableEntry
{
    public string Species { get; } = entry.Species!;

    public byte EggMoveCount { get; } = personal.EggMoveCount;
    public string[] EggMoves { get; } = personal.EggMoves!;
    public bool HasItems { get; } = personal.HasItems;
    public string[] Items { get; } = personal.Items!;
    public short DevId { get; } = personal.DevId;
    public short Gender { get; } = personal.Gender;
    public string[] Types { get; } = personal.Types!;
    public string[] Abilities { get; } = personal.Abilities!;

    public bool IsAbilityLocked { get; } = entry.IsAbilityLocked;
    public bool IsShinyLocked { get; } = entry.IsShinyLocked;

    public int GuaranteedIVs { get; } = entry.GuaranteedIVs;
    public ulong Ability { get; } = entry.Ability;

    public int Level { get; } = entry.Level;
}
