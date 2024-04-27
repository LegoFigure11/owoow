using PKHeX.Core;

namespace owoow.Core.RNG;

public static class Util
{
    private static readonly string[] PersonalityMarks = ["Rowdy", "Absent-Minded", "Jittery", "Excited", "Charismatic", "Calmness", "Intense", "Zoned-Out", "Joyful", "Angry", "Smiley", "Teary", "Upbeat", "Peeved", "Intellectual", "Ferocious", "Crafty", "Scowling", "Kindly", "Flustered", "Pumped-Up", "Zero Energy", "Prideful", "Unsure", "Humble", "Thorny", "Vigor", "Slump"];

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

    public static string GenerateMark(ref Xoroshiro128Plus rng, bool Weather, bool Fishing, int MarkRolls)
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
}
