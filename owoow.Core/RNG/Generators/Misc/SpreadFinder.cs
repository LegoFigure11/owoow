using owoow.Core.Interfaces;
using PKHeX.Core;
using System.Diagnostics;
using static owoow.Core.RNG.Generators.Fixed;
using static owoow.Core.RNG.Validators.Validator;

namespace owoow.Core.RNG.Generators.Misc;

public static class SpreadFinder
{
    public static Task<List<SpreadFinderFrame>> Generate(uint start, uint end, GeneratorConfig config)
    {
        return Task.Run(() =>
        {
            List<SpreadFinderFrame> frames = [];

            uint EC;

            bool PassIVs;
            byte[] IVs;

            uint Height;

            for (ulong FixedSeed = start; FixedSeed <= end/* && frames.Count < 1_000*/; FixedSeed++)
            {
                if (FixedSeed % 0x100000 == 0) Debug.Print($"{FixedSeed:X8}");

                Xoroshiro128Plus rng = new(FixedSeed, 0x82A2B175229D6A5B);

                EC = GenerateEC(ref rng);
                if (!CheckEC(EC, config.RareEC)) continue;

                _ = GenerateEC(ref rng); // PID, can use this method because EC makes the same calls and we don't care for the result

                (PassIVs, IVs) = GenerateIVs(ref rng, 0, config);
                if (!PassIVs) continue;

                // HEIGHT
                Height = GenerateHeightWeightScale(ref rng);
                if (!CheckHeight(Height, config.TargetScale)) continue;

                frames.Add(new()
                {
                    Seed = $"{FixedSeed:X8}",

                    EC = $"{EC:X8}",

                    H = IVs[0],
                    A = IVs[1],
                    B = IVs[2],
                    C = IVs[3],
                    D = IVs[4],
                    S = IVs[5],

                    Height = Util.GetHeightString(Height),
                });
            }

            return frames;
        });
    }
}
