using owoow.Core.Enums;
using owoow.Core.Interfaces;
using PKHeX.Core;

namespace owoow.Core.RNG.Generators;

public static class MenuClose
{
    public static void Advance(ref Xoroshiro128Plus rng, uint NPCs = 0, MenuCloseType type = MenuCloseType.NotHoldingDirection, WeatherType weather = WeatherType.NormalWeather)
    {
        for (int i = 0; i < NPCs; i++) rng.NextInt(91);

        if (type is not MenuCloseType.HoldingDirection)
        {
            if (weather is WeatherType.NormalWeather or WeatherType.Overcast or WeatherType.HeavyFog)
            {
                rng.NextInt(); // technically a rand(2) but functionally the same
            }

            rng.NextInt(61);
        }
    }

    public static uint GetAdvances(ref Xoroshiro128Plus rng, uint NPCs = 0, MenuCloseType type = MenuCloseType.NotHoldingDirection, WeatherType weather = WeatherType.NormalWeather)
    {
        (ulong s0, ulong s1) = rng.GetState();

        Advance(ref rng, NPCs, type, weather);

        (ulong _s0, ulong _s1) = rng.GetState();

        return Util.GetAdvancesPassed(s0, s1, _s0, _s1);
    }

    public static uint GetAdvances(ref Xoroshiro128Plus rng, uint NPCs = 0, bool IsHoldingDirection = false, WeatherType weather = WeatherType.NormalWeather)
    {
        return GetAdvances(ref rng, NPCs, IsHoldingDirection ? MenuCloseType.HoldingDirection : MenuCloseType.NotHoldingDirection, weather);
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
                var adv = GetAdvances(ref rng, config.MenuCloseNPCs, config.MenuCloseIsHoldingDirection, config.Weather);
                frames.Add(new MenuCloseFrame
                {
                    Advances = $"{advances:N0}",
                    Jump = $"+{adv}",
                    Seed0 = $"{_s0:X16}",
                    Seed1 = $"{_s1:X16}",
                });

                if (adv == 0)
                {
                    advances++; // fail-safe in case of seed (0, 0)
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
