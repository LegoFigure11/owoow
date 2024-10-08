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

    public uint[] TargetMinIVs { get; set; } = [ 0,  0,  0,  0,  0,  0];
    public uint[] TargetMaxIVs { get; set; } = [31, 31, 31, 31, 31, 31];

    public bool RareEC { get; set; } = false;

    public string LeadAbility { get; set; } = string.Empty;
    public bool AbilityIsTypePulling => Util.GetLeadAbilityType(LeadAbility) != string.Empty;
    public bool AbilityIsSync => LeadAbility == "Synchronize";
    public bool AbilityIsCuteCharm => LeadAbility == "Cute Charm";

    public uint TID { get; set; } = 0;
    public uint SID { get; set; } = 0;
    public uint TSV => Util.GetShinyValue(TID, SID);

    public int ShinyRolls { get; set; } = 1;
    public int MarkRolls { get; set; } = 1;

    public int AuraKOs { get; set; } = 500;

    public int GuaranteedIVs { get; set; } = 0;

    public string Weather { get; set; } = string.Empty;
    public bool WeatherActive => Weather != "Normal Weather";

    public bool FiltersEnabled { get; set; } = false;
};
