using PKHeX.Core;

namespace owoow.Core.RNG;

public static class Util
{
    public static readonly string[] PersonalityMarks = ["Rowdy", "Absent-Minded", "Jittery", "Excited", "Charismatic", "Calmness", "Intense", "Zoned-Out", "Joyful", "Angry", "Smiley", "Teary", "Upbeat", "Peeved", "Intellectual", "Ferocious", "Crafty", "Scowling", "Kindly", "Flustered", "Pumped-Up", "Zero Energy", "Prideful", "Unsure", "Humble", "Thorny", "Vigor", "Slump"];
    public static readonly IReadOnlyList<string> Natures = GameInfo.GetStrings(1).Natures;

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

    public static (uint threshold, uint rolls) GetBrilliantInfo(int KOs) => KOs switch
    {
        >= 500 => (30, 6),
        >= 300 => (30, 5),
        >= 200 => (30, 4),
        >= 100 => (30, 3),
        >= 50 => (25, 2),
        >= 20 => (20, 1),
        >= 1 => (15, 1),
        _ => (0, 0),
    };

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

    public static uint GetShinyXOR(uint pid, uint tsv) => GetShinyValue(GetShinyValue(pid), tsv);
}
