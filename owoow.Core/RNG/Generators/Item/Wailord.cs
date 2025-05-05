using owoow.Core.Interfaces;
using owoow.Core.RNG.Generators.Misc;
using PKHeX.Core;
using static owoow.Core.RNG.Validators.Validator;

namespace owoow.Core.RNG.Generators.Item;

public static class Wailord
{
    public static Task<List<WailordFrame>> Generate(ulong s0, ulong s1, ulong start, ulong end, GeneratorConfig config)
    {
        // Derived from https://gist.github.com/Lusamine/753caa2ca81dc498357df13100cd752b
        // Thanks Anubis!

        return Task.Run(() =>
        {
            List<WailordFrame> frames = [];

            Xoroshiro128Plus outer = new(s0, s1);

            ulong advances = 0;
            uint Jump = 0;
            uint SpawnAttempt = 0;
            List<uint> Locations = [];

            for (; advances < start; advances++)
            {
                outer.Next();
            }

            for (ulong i = start; i <= end; i++)
            {
                var os = outer.GetState();
                var rng = new Xoroshiro128Plus(os.s0, os.s1);

                Jump = 0;
                SpawnAttempt = 0;
                Locations.Clear();

                if (config.ConsiderMenuClose)
                {
                    Jump += MenuClose.GetAdvances(ref rng, config.MenuCloseNPCs, config.MenuCloseIsHoldingDirection, config.Weather);
                }

                while (Locations.Count < 5 && ++SpawnAttempt < 100)
                {
                    var NPC = (uint)rng.NextInt(51);
                    if (Locations.Contains(NPC)) continue;
                    Locations.Add(NPC);
                }

                Locations.Clear();

                while (Locations.Count < 3 && ++SpawnAttempt < 100)
                {
                    var NPC = (uint)rng.NextInt(45);
                    if (Locations.Contains(NPC)) continue;
                    Locations.Add(NPC);
                    rng.NextInt(10);
                }

                var Wailord = rng.NextInt(100);
                var result = Wailord == 0;
                if (!CheckSuccessType(result, config.SuccessType))
                {
                    outer.Next();
                    continue;
                }

                frames.Add(new WailordFrame
                {
                    Advances = $"{i:N0}",
                    Jump = $"+{Jump}",
                    Animation = (os.s0 & 1 ^ os.s1 & 1) == 0 ? 'P' : 'S',
                    Respawn = result ? 'Y' : 'N',
                    Seed0 = $"{os.s0:X16}",
                    Seed1 = $"{os.s1:X16}",
                });


                outer.Next();
            }
            return frames;
        });
    }
}
