using PKHeX.Core;

namespace owoow.Core;

public static class Util
{
    public static uint GetAdvancesPassed(ulong s0, ulong s1, ulong _s0, ulong _s1)
    {
        if (s0 == _s0 && s1 == _s1) return 0;
        var xoro = new Xoroshiro128Plus(s0, s1);
        uint i = 0;
        do
        {
            i++;
            xoro.Next();

            var (cur0, cur1) = xoro.GetState();
            if (cur0 == _s0 && cur1 == _s1) break;

        } while (i < 50_000); // 50,000 chosen arbitrarily to prevent an infinite loop

        return i;
    }
}
