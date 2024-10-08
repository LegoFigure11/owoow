﻿using PKHeX.Core;

namespace owoow.Core.RNG.Generators;

public static class Fixed
{
    public static uint GenerateFixedSeed(ref Xoroshiro128Plus rng)
    {
        return (uint)rng.Next();
    }

    public static uint GenerateEC(ref Xoroshiro128Plus rng)
    {
        return (uint)rng.Next();
    }

    public static uint GeneratePID(ref Xoroshiro128Plus rng, bool shiny, uint tsv)
    {
        uint pid = (uint)rng.Next();
        uint xor = Util.GetShinyXOR(pid, tsv);
        if (shiny)
        {
            if (xor >= 16)
                pid = (((tsv ^ (pid & 0xFFFF)) << 16) | (pid & 0xFFFF)) & 0xFFFFFFFF;
        }
        else
        {
            if (xor < 16)
                pid ^= 0x10000000; // Antishiny
        }
        return pid;
    }

    public static uint GenerateGuaranteedIVIndex(ref Xoroshiro128Plus rng)
    {
        return (uint)rng.NextInt(6);
    }

    public static byte GenerateIV(ref Xoroshiro128Plus rng)
    {
        return (byte)rng.NextInt(32);
    }

    public static (bool, byte[]) GenerateIVs(ref Xoroshiro128Plus rng, ulong aura, GeneratorConfig config)
    {
        byte[] ivs = [32, 32, 32, 32, 32, 32];
        var g = (int)aura + config.GuaranteedIVs;
        // Guaranteed
        for (var i = 0; i < g; i++)
        {
            uint idx;
            do
            {
                idx = GenerateGuaranteedIVIndex(ref rng);
            } while (ivs[idx] != 32);

            ivs[idx] = 31;
            
            if (config.FiltersEnabled && ivs[idx] > config.TargetMaxIVs[idx]) return (false, ivs);
        }
        // Random
        for (var i = 0; i < 6; i++)
        {
            if (ivs[i] == 32) ivs[i] = GenerateIV(ref rng);

            if (config.FiltersEnabled && (ivs[i] < config.TargetMinIVs[i] || ivs[i] > config.TargetMaxIVs[i])) return (false, ivs);
        }
        return (true, ivs);
    }

    public static uint GenerateHeightWeightScale(ref Xoroshiro128Plus rng)
    {
        return (uint)(rng.NextInt(129) + rng.NextInt(128));
    }
}
