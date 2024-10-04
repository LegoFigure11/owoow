using owoow.Core.Enums;
using PKHeX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace owoow.Core.RNG;

public class GeneratorConfig
{
    public string TargetSpecies { get; set; } = string.Empty;

    public AuraType TargetAura { get; set; } = AuraType.Any;
    public Nature TargetNature { get; set; } = Nature.Random;
    public ShinyType TargetShiny { get; set; } = ShinyType.Any;

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
};
