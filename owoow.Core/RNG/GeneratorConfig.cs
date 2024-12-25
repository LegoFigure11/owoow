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

    public bool RareEC { get; set; } = false;

    public string LeadAbility { get; set; } = string.Empty;
    public AbilityType AbilityType => LeadAbility switch
    {
        "Illuminate" or "Arena Trap" or "No Guard" => AbilityType.IncreaseEncounterRate,
        "Stench" or "Quick Feet" or "White Smoke" or "Infiltrator" => AbilityType.DecreseEncounterRate,
        "Synchronize" => AbilityType.Synchronize,
        "Cute Charm" => AbilityType.CuteCharm,
        "Magnet Pull" or "Lightning Rod" or "Static" or "Flash Fire" or "Storm Drain" or "Harvest" => AbilityType.TypePulling,
        "Super Luck" or "Compound Eyes" => AbilityType.IncreaseItemRate,
        _ => AbilityType.NoEffect,
    };

    public uint TID { get; set; } = 0;
    public uint SID { get; set; } = 0;
    public uint TSV => Util.GetShinyValue(TID, SID);

    public int ShinyRolls { get; set; } = 1;
    public int MarkRolls { get; set; } = 1;

    public int AuraKOs { get; set; } = 500;

    public int GuaranteedIVs { get; set; } = 0;

    public WeatherType Weather { get; set; } = WeatherType.AllWeather;
    public bool WeatherActive => Weather != WeatherType.NormalWeather;

    public bool IsDexRecActive { get; set; } = false;
    public uint[] DexRecSlots { get; set; } = [0, 0, 0, 0];

    public bool ConsiderMenuClose { get; set; } = false;
    public bool MenuCloseIsHoldingDirection { get; set; } = false;
    public uint MenuCloseNPCs { get; set; } = 0;

    public bool FiltersEnabled { get; set; } = false;
};
