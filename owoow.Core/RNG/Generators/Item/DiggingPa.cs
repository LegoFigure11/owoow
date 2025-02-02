using owoow.Core.Interfaces;
using owoow.Core.RNG.Generators.Misc;
using PKHeX.Core;

namespace owoow.Core.RNG.Generators.Item;

public static class DiggingPa
{
    public static Task<List<DiggingPaFrame>> Generate(ulong s0, ulong s1, ulong start, ulong end, GeneratorConfig config)
    {
        // Derived from https://gist.github.com/Lusamine/9688ae65229c95a0b2ea22e803f3b017
        // Thanks Anubis!

        return Task.Run(() =>
        {
            List<DiggingPaFrame> frames = [];

            Xoroshiro128Plus outer = new(s0, s1);

            ulong advances = 0;
            uint Jump = 0;

            uint earned = 0;
            uint counted = 0;

            uint success = 0;

            for (; advances < start; advances++)
            {
                outer.Next();
            }

            for (ulong i = start; i <= end; i++)
            {
                var os = outer.GetState();
                var rng = new Xoroshiro128Plus(os.s0, os.s1);

                Jump = 0;

                if (config.ConsiderMenuClose)
                {
                    Jump += MenuClose.GetAdvances(ref rng, config.MenuCloseNPCs, config.MenuCloseIsHoldingDirection, config.Weather);
                }

                earned = 0;
                counted = 0;

                success = 8;

                HandleSafeRolls(ref rng, success, ref earned, ref counted);

                while (true)
                {
                    success += HandleRiskyRolls(ref rng, ref earned, ref counted);

                    // Check for a second wind
                    // Only eligible if there were at least 7 successful safe + risky rolls before this.
                    var roll = (uint)rng.NextInt(100);
                    if (roll >= 90 && success >= 7)
                    {
                        HandleSecondWind(ref rng, ref earned, ref counted);
                        success = 4;
                        HandleSafeRolls(ref rng, success, ref earned, ref counted);
                    }
                    else
                    {
                        break;
                    }
                }

                if (earned < config.DiggingPaMinWatts)
                {
                    outer.Next();
                    continue;
                }

                frames.Add(new DiggingPaFrame
                {
                    Advances = $"{i:N0}",
                    Jump = $"+{Jump}",
                    Animation = (os.s0 & 1 ^ os.s1 & 1) == 0 ? 'P' : 'S',
                    Watts = earned,
                    Actual = $"{earned:N0}",
                    Reported = $"{counted:N0}",
                    Seed0 = $"{os.s0:X16}",
                    Seed1 = $"{os.s1:X16}",
                });

                outer.Next();
            }
            return frames;
        });
    }

    private static void HandleSafeRolls(ref Xoroshiro128Plus rng, uint count, ref uint earned, ref uint counted)
    {
        for (var i = 0; i < count; i++)
        {
            var rand = (uint)rng.NextInt(100);
            var reward = FindItemInTable(rand, DiggingPaRewards);
            if (reward == 0)
                reward = 2000; // Danger Zone gives 2000 during safe attempts.
            earned += reward;
            counted += reward;
        }
    }

    // Returns number succeeded to know if we're eligible for more second wind bonus
    private static uint HandleRiskyRolls(ref Xoroshiro128Plus rng, ref uint earned, ref uint counted)
    {
        uint succeeded = 0;
        while (true)
        {
            var rand = (uint)rng.NextInt(100);
            var reward = FindItemInTable(rand, DiggingPaRewards);
            if (reward == 0)
                break; // Hit the danger zone.
            earned += reward;
            counted += reward;
            succeeded++;
        }
        return succeeded;
    }

    private static void HandleSecondWind(ref Xoroshiro128Plus rng, ref uint earned, ref uint counted)
    {
        earned += 5000 * 10;
        counted += 5000 * 10;
        var roll = (uint)rng.NextInt(100);
        if (roll < 90) // What kind of second wind? 10% to just give 50000, 90% for up to 101.
        {
            earned += 5000; // Give an additional for a total of 11 if successful
            counted += 5000;

            var bonus_count = 0;
            while (bonus_count < 90)
            {
                roll = (uint)rng.NextInt(100); // Roll for additional 5k caches
                if (roll >= 95)
                    break;
                earned += 5000;
                counted += 5000;
                bonus_count++;
            }

            // If he found at least 15 additional caches, he gets one last go.
            if (bonus_count >= 15)
            {
                var last_go = (uint)rng.NextInt(3);
                switch (last_go)
                {
                    // These are not recorded in his final displayed value so we don't add them to counted.
                    case 0:
                        earned += 20000;
                        break;
                    case 1:
                        earned += 50000;
                        break;
                    case 2:
                        earned += 70000;
                        break;
                }
            }
        }
    }

    public class RewardEntry(uint min, uint max, uint watts)
    {
        public uint Min = min;
        public uint Max = max;
        public uint Watts = watts;
    }

    public static readonly RewardEntry[] DiggingPaRewards =
    [
        new( 0, 29,    0), // Danger Zone
        new(30, 32,  800),
        new(33, 41, 1500),
        new(42, 66, 2000),
        new(67, 86, 3000),
        new(87, 96, 5000),
        new(97, 99, 8000),
    ];

    public static uint FindItemInTable(uint rand, RewardEntry[] table)
    {
        foreach (RewardEntry entry in table)
        {
            if (rand >= entry.Min && rand <= entry.Max)
                return entry.Watts;
        }
        return 0;
    }
}
