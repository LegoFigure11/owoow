using owoow.Core.Interfaces;
using PKHeX.Core;

namespace owoow.Core.RNG;

public static class Util
{
    private static readonly string[] PersonalityMarks = ["Rowdy", "Absent-Minded", "Jittery", "Excited", "Charismatic", "Calmness", "Intense", "Zoned-Out", "Joyful", "Angry", "Smiley", "Teary", "Upbeat", "Peeved", "Intellectual", "Ferocious", "Crafty", "Scowling", "Kindly", "Flustered", "Pumped-Up", "Zero Energy", "Prideful", "Unsure", "Humble", "Thorny", "Vigor", "Slump"];
    private static readonly IReadOnlyList<string> Natures = GameInfo.GetStrings(1).Natures;

    public static uint GetAdvancesPassed(ulong s0, ulong s1, ulong _s0, ulong _s1)
    {
        if (s0 == _s0 && s1 == _s1) return 0;
        var rng = new Xoroshiro128Plus(s0, s1);
        uint i = 0;
        do
        {
            i++;
            rng.Next();

            var (cur0, cur1) = rng.GetState();
            if (cur0 == _s0 && cur1 == _s1) break;

        } while (i < 50_000); // 50,000 chosen arbitrarily to prevent an infinite loop

        return i;
    }

    public static string GenerateMark(ref Xoroshiro128Plus rng, int MarkRolls, bool Weather = false, bool Fishing = false)
    {
        for (int i = 0; i < MarkRolls; i++)
        {
            uint rare        = (uint)rng.NextInt(1000);
            uint personality = (uint)rng.NextInt(100);
            uint uncommon    = (uint)rng.NextInt(50);
            uint weather     = (uint)rng.NextInt(50);
            uint time        = (uint)rng.NextInt(50);
            uint fishing     = (uint)rng.NextInt(25);

            if (rare == 0) return "Rare";
            if (personality == 0) return PersonalityMarks[rng.NextInt(28)]; // PersonalityMarks.Size
            if (uncommon == 0) return "Uncommon";
            if (weather == 0 && Weather) return "Weather";
            if (time == 0) return "Time";
            if (fishing == 0 && Fishing) return "Fishing";
        }
        return "None";
    }

    public static bool GenerateIsShiny(ref Xoroshiro128Plus rng, int rolls, uint tsv)
    {
        bool shiny = false;
        for (int i = 0; i < rolls; i++)
        {
            var pid = (uint)rng.Next();
            shiny = GetShinyValue(GetShinyValue(pid), tsv) < 16;
            if (shiny) break;
        }
        return shiny;
    }

    public static char GenerateGender(ref Xoroshiro128Plus rng, IEncounterTableEntry enc, bool isCC = false)
    {
        var roll = ' ';
        if (!isCC) roll = rng.NextInt(2) == 0 ? 'F' : 'M'; // 50/50 regardless of gender ratio
        return enc.Gender switch
        {
            >= 255 => '-',
            >= 254 => 'F',
            >= 1   => isCC ? 'C' : roll, 
            _      => 'M',
        };
    }

    public static string GenerateNature(ref Xoroshiro128Plus rng, bool sync)
    {
        var nature = Natures[(int)rng.NextInt(25)];
        if (sync) nature = "Sync";
        return nature;
    }

    public static string GenerateAbility(ref Xoroshiro128Plus rng, IEncounterTableEntry enc, bool locked = false)
    {
        ulong roll = 2;
        if (!locked) roll = rng.NextInt(2);
        return enc.Abilities![roll];
    }

    public static string GenerateItem(ref Xoroshiro128Plus rng, IEncounterTableEntry enc)
    {
        string item = "None";
        if (enc.HasItems)
        {
            var roll = rng.NextInt(100);
            item = roll switch
            {
                <= 49 => enc.Items![0] != "None" ? $"{enc.Items![0]} (50%)" : "None",
                <= 54 => enc.Items![1] != "None" ? $"{enc.Items![1]} (5%)" : "None",
                <= 55 => enc.Items![2] != "None" ? $"{enc.Items![2]} (1%)" : "None",
                _ => "None",
            };
        }
        return item;
    }

    public static string GetLeadAbilityType(string ability) => ability switch
    {
        "Magnet Pull" => "Steel",
        "Lightning Rod/Static" => "Electric",
        "Flash Fire" => "Fire",
        "Storm Drain" => "Water",
        "Harvest" => "Grass",
        _ => string.Empty,
    };

    public static uint GetShinyValue(uint x, uint y) => x ^ y;
    public static uint GetShinyValue(uint x) => (x >> 16) ^ (x & 0xFFFF);
}
