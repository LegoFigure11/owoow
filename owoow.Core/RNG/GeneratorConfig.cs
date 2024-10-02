using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace owoow.Core.RNG;

public class GeneratorConfig
{
    public string TargetSpecies { get; set; } = string.Empty;

    public string LeadAbility { get; set; } = string.Empty;
    public bool AbilityIsTypePulling => Util.GetLeadAbilityType(LeadAbility) != string.Empty;
    public bool AbilityIsSync => LeadAbility == "Synchronize";
    public bool AbilityIsCuteCharm => LeadAbility == "Cute Charm";

    public uint TID { get; set; } = 0;
    public uint SID { get; set; } = 0;
    public uint TSV => Util.GetShinyValue(TID, SID);

    public int ShinyRolls { get; set; } = 1;
    public int MarkRolls { get; set; } = 1;

    public string Weather { get; set; } = string.Empty;
    public bool WeatherActive => Weather != "Normal Weather";
};
