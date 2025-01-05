using owoow.Core.Enums;
using owoow.Core.Interfaces;
using PKHeX.Core;

namespace owoow.Core.RNG.Generators;

public static class Environment
{
    private static void AdvanceRain(ref Xoroshiro128Plus rng, uint ticks)
    {
        for (int i = 0; i < ticks; i++)
        {
            rng.NextInt(20_001);
            rng.NextInt(20_001); // rand invoked twice per tick
        }
    }

    public static uint GetRainAdvances(ref Xoroshiro128Plus rng, uint advances)
    {
        (ulong s0, ulong s1) = rng.GetState();

        AdvanceRain(ref rng, advances);

        (ulong _s0, ulong _s1) = rng.GetState();

        return Util.GetAdvancesPassed(s0, s1, _s0, _s1);
    }

    private static void AdvanceMapMemoryRoll(ref Xoroshiro128Plus rng)
    {
        rng.NextInt(100);
    }

    public static uint GetMapMemoryRollAdvances(ref Xoroshiro128Plus rng)
    {
        (ulong s0, ulong s1) = rng.GetState();

        AdvanceMapMemoryRoll(ref rng);

        (ulong _s0, ulong _s1) = rng.GetState();

        return Util.GetAdvancesPassed(s0, s1, _s0, _s1);
    }

    private static void AdvanceAreaLoad(ref Xoroshiro128Plus rng, uint advances)
    {
        for (int i = 0; i < advances; i++) rng.NextInt(100);
    }

    public static uint GetAreaLoadAdvances(ref Xoroshiro128Plus rng, uint advances)
    {
        (ulong s0, ulong s1) = rng.GetState();

        AdvanceAreaLoad(ref rng, advances);

        (ulong _s0, ulong _s1) = rng.GetState();

        return Util.GetAdvancesPassed(s0, s1, _s0, _s1);
    }

    private static void AdvanceAreaLoadNPCs(ref Xoroshiro128Plus rng, uint npcs)
    {
        for (int i = 0; i < npcs; i++) rng.NextInt(91);
    }

    public static uint GetAreaLoadNPCAdvances(ref Xoroshiro128Plus rng, uint npcs)
    {
        (ulong s0, ulong s1) = rng.GetState();

        AdvanceAreaLoadNPCs(ref rng, npcs);

        (ulong _s0, ulong _s1) = rng.GetState();

        return Util.GetAdvancesPassed(s0, s1, _s0, _s1);
    }
}
