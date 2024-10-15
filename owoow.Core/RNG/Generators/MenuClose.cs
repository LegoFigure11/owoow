using owoow.Core.Enums;
using owoow.Core.Interfaces;
using PKHeX.Core;

namespace owoow.Core.RNG.Generators;

public static class MenuClose
{
    public static void Advance(ref Xoroshiro128Plus rng, uint NPCs = 0, MenuCloseType type = MenuCloseType.Regular)
    {
        for (int i = 0; i < NPCs; i++) rng.NextInt(91);

        if (type == MenuCloseType.Regular)
        {
            // Not accurate in all weathers, thanks @Lincoln-LM
            rng.Next();
            rng.NextInt(60);
        }
    }

    public static uint GetAdvances(ref Xoroshiro128Plus rng, uint NPCs = 0, MenuCloseType type = MenuCloseType.Regular)
    {
        (ulong s0, ulong s1) = rng.GetState();

        Advance(ref rng, NPCs, type);

        (ulong _s0, ulong _s1) = rng.GetState();

        return Util.GetAdvancesPassed(s0, s1, _s0, _s1);
    }

    public static uint GetAdvances(ref Xoroshiro128Plus rng, uint NPCs = 0, bool IsHoldingDirection = false)
    {
        return GetAdvances(ref rng, NPCs, IsHoldingDirection ? MenuCloseType.HoldingDirection : MenuCloseType.Regular);
    }

    public static Task<List<MenuCloseFrame>> Generate(ulong s0, ulong s1, ulong start, ulong end, GeneratorConfig config)
    {
        return Task.Run(() =>
        {
            List<MenuCloseFrame> frames = [];

            Xoroshiro128Plus rng = new(s0, s1);

            ulong advances = 0;
            for (; advances < start; advances++)
            {
                rng.Next();
            }

            while (advances < end)
            {
                var (_s0, _s1) = rng.GetState();
                var adv = GetAdvances(ref rng, config.MenuCloseNPCs, config.MenuCloseIsHoldingDirection);
                frames.Add(new MenuCloseFrame
                {
                    Advances = $"{advances:N0}",
                    Jump = $"+{adv}",
                    Seed0 = $"{_s0:X16}",
                    Seed1 = $"{_s1:X16}",
                });

                if (adv == 0)
                {
                    advances++;
                }
                else
                {
                    advances += adv;
                }
            }

            return frames;
        });
    }
}
