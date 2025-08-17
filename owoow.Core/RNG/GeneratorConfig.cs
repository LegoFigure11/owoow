using owoow.Core.Enums;
using PKHeX.Core;

namespace owoow.Core.RNG;

public class GeneratorConfig
{
    public string TargetSpecies { get; set; } = string.Empty;

    public AuraType TargetAura { get; set; } = AuraType.Any;
    public Nature TargetNature { get; set; } = Nature.Random;
    public ShinyType TargetShiny { get; set; } = ShinyType.Any;
    public RibbonIndex TargetMark { get; set; } = RibbonIndex.MAX_COUNT + 1;
    public ScaleType TargetScale { get; set; } = ScaleType.Any;

    public uint[] TargetMinIVs { get; set; } = [0, 0, 0, 0, 0, 0];
    public uint[] TargetMaxIVs { get; set; } = [31, 31, 31, 31, 31, 31];
    public IVSearchType[] SearchTypes { get; set; } = [IVSearchType.Range, IVSearchType.Range, IVSearchType.Range, IVSearchType.Range, IVSearchType.Range, IVSearchType.Range];

    public bool RareEC { get; set; } = false;

    public string LeadAbility { get; set; } = string.Empty;
    public AbilityType AbilityType => LeadAbility switch
    {
        "Illuminate" or "Arena Trap" or "No Guard" => AbilityType.IncreaseEncounterRate,
        "Stench" or "Quick Feet" or "White Smoke" or "Infiltrator" => AbilityType.DecreaseEncounterRate,
        "Synchronize" => AbilityType.Synchronize,
        "Cute Charm" => AbilityType.CuteCharm,
        "Magnet Pull" or "Lightning Rod" or "Static" or "Flash Fire" or "Storm Drain" or "Harvest" => AbilityType.TypePulling,
        "Super Luck" or "Compound Eyes" => AbilityType.IncreaseItemRate,
        _ => AbilityType.NoEffect,
    };

    public uint TID { get; set; } = 0;
    public uint SID { get; set; } = 0;
    public uint TSV => Util.GetShinyValue(TID, SID);
    public List<string> IDs { get; set; } = [];

    public int ShinyRolls { get; set; } = 1;
    public int MarkRolls { get; set; } = 1;

    public int AuraKOs { get; set; } = 500;

    public int MaxStep { get; set; } = 0;

    public int GuaranteedIVs { get; set; } = 0;

    public WeatherType Weather { get; set; } = WeatherType.AllWeather;
    public bool WeatherActive => Weather != WeatherType.NormalWeather;

    public bool IsDexRecActive => DexRecSlots.Any(s => s != 0);
    public short[] DexRecSlots { get; set; } = [0, 0, 0, 0];

    public bool ConsiderMenuClose { get; set; } = false;
    public bool MenuCloseIsHoldingDirection { get; set; } = false;
    public uint MenuCloseNPCs { get; set; } = 0;

    public bool ConsiderFly { get; set; } = false;
    public uint AreaLoadAdvances { get; set; } = 0;
    public uint AreaLoadNPCs { get; set; } = 0;
    public bool ConsiderRain { get; set; } = false;

    // Party List -> Main Menu
    // When Flying, this value also includes the ticks consumed opening the map
    public uint RainTicksSummary => Weather switch
    {
        WeatherType.Raining => ConsiderFly ? (uint)6 : 3,
        WeatherType.Thunderstorm => ConsiderFly ? (uint)12 : 6,
        _ => 0,
    };

    // Main Menu -> Overworld
    // When Flying, takes place after the Memory Set rand100 and before the Fly rand100s
    public uint RainTicksAfterCloseMenu => Weather switch
    {
        WeatherType.Raining => ConsiderFly ? (uint)3 : 2,
        WeatherType.Thunderstorm => ConsiderFly ? (uint)6 : 4,
        _ => 0
    };

    // At end of Hidden step loop if no encounter can be generated
    public uint RainTicksOnHiddenStepFail => Weather switch
    {
        WeatherType.Raining => 3,
        WeatherType.Thunderstorm => 6,
        _ => 0
    };

    // Between Fly NPCs and "Menu Close" NPCs
    // Always 0 if not Flying
    public uint RainTicksAreaLoad { get; set; } = 0;

    // Between "Menu Close" NPCs and Encounter Generation
    // Always 0 if Flying
    public uint RainTicksEncounter { get; set; } = 0;

    public LotoIDTargetType LotoIDTargetType { get; set; } = LotoIDTargetType.Any;
    public SuccessType SuccessType { get; set; } = SuccessType.Any;

    public CramomaticTargetType CramomaticTargetType { get; set; } = CramomaticTargetType.Any;
    public CramomaticInputItemType[] CramomaticInputs { get; set; } = [0, 0, 0, 0];
    public bool BonusOnly { get; set; } = false;

    public uint WattTraderSlotMin { get; set; } = 0;
    public uint WattTraderSlotMax { get; set; } = 999;

    public ulong DiggingPaMinWatts { get; set; } = 0;

    public bool FiltersEnabled { get; set; } = false;

    public bool LogResultsToFile { get; set; } = false;
};
