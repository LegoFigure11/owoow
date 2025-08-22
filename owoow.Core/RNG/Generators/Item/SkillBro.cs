using owoow.Core.Enums;
using owoow.Core.Interfaces;
using owoow.Core.RNG.Generators.Misc;
using PKHeX.Core;

namespace owoow.Core.RNG.Generators.Item;

public static class SkillBro
{
    public static Task<List<SkillBroFrame>> Generate(ulong s0, ulong s1, ulong start, ulong end, GeneratorConfig config)
    {
        // Derived from https://gist.github.com/Lusamine/3f162101798dca855e6b97807c2ccf1d
        // Thanks Anubis!

        return Task.Run(() =>
        {
            List<SkillBroFrame> frames = [];

            (s0, s1) = Util.XoroshiroJump(s0, s1, start);
            Xoroshiro128Plus outer = new(s0, s1);

            uint Jump;

            byte[] rewards;


            for (ulong i = start; i <= end; i++)
            {
                var os = outer.GetState();
                var rng = new Xoroshiro128Plus(os.s0, os.s1);
                rewards = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

                Jump = 0;

                if (config.ConsiderMenuClose)
                {
                    Jump += MenuClose.GetAdvances(ref rng, config.MenuCloseNPCs, config.MenuCloseIsHoldingDirection, config.Weather);
                }

                HandleSafeRolls(ref rng, 1, config.Game, ref rewards);

                while (true)
                {
                    HandleRiskyRolls(ref rng, config.Game, ref rewards); // Exits when we hit the danger zone

                    // Check for second wind
                    var roll = (uint)rng.NextInt(100);
                    if (roll < 5)
                    {
                        HandleSafeRolls(ref rng, 1, config.Game, ref rewards); // 1 safe roll then back to risky ones
                    }
                    else
                    {
                        break;
                    }

                }

                // Filters
                var pass = !rewards.Where((t, k) => t < config.SkillBroItemsMin[k]).Any();
                var count = rewards.Aggregate(0, (current, count) => current + count);
                if (!pass || count < config.SkillBroItemsMinCount)
                {
                    outer.Next();
                    continue;
                }

                frames.Add(new SkillBroFrame
                {
                    Advances        = $"{i:N0}",
                    Jump            = $"+{Jump}",
                    Animation       = (os.s0 & 1 ^ os.s1 & 1) == 0 ? 'P' : 'S',

                    Total           = count,

                    GoldBottleCap   = rewards[(byte)SkillBroReward.GoldBottleCap],
                    BottleCap       = rewards[(byte)SkillBroReward.BottleCap],
                    NormalGem       = rewards[(byte)SkillBroReward.NormalGem],
                    StickyBarb      = rewards[(byte)SkillBroReward.StickyBarb],
                    LightClay       = rewards[(byte)SkillBroReward.LightClay],
                    LaggingTail     = rewards[(byte)SkillBroReward.LaggingTail],
                    IronBall        = rewards[(byte)SkillBroReward.IronBall],
                    MetalCoat       = rewards[(byte)SkillBroReward.MetalCoat],
                    IceStone        = rewards[(byte)SkillBroReward.IceStone],
                    DawnStone       = rewards[(byte)SkillBroReward.DawnStone],
                    DuskStone       = rewards[(byte)SkillBroReward.DuskStone],
                    ShinyStone      = rewards[(byte)SkillBroReward.ShinyStone],
                    MoonStone       = rewards[(byte)SkillBroReward.MoonStone],
                    SunStone        = rewards[(byte)SkillBroReward.SunStone],
                    FossilizedFish  = rewards[(byte)SkillBroReward.FossilizedFish], 
                    FossilizedDrake = rewards[(byte)SkillBroReward.FossilizedDrake],
                    FossilizedDino  = rewards[(byte)SkillBroReward.FossilizedDino],
                    FossilizedBird  = rewards[(byte)SkillBroReward.FossilizedBird],
                    WishingPiece    = rewards[(byte)SkillBroReward.WishingPiece],
                    CometShard      = rewards[(byte)SkillBroReward.CometShard],
                    RareBone        = rewards[(byte)SkillBroReward.RareBone],

                    Seed0           = $"{os.s0:X16}",
                    Seed1           = $"{os.s1:X16}",
                });

                outer.Next();
            }
            return frames;
        });
    }

    private static void HandleSafeRolls(ref Xoroshiro128Plus rng, uint count, Game game, ref byte[] rewards)
    {
        for (var i = 0; i < count; i++)
        {
            var rand = (uint)rng.NextInt(10000);
            var reward = GetReward(rand, game);
            if (reward == SkillBroReward.DangerZone) reward = SkillBroReward.RareBone; // Danger Zone gives Rare Bone during safe attempts.

            rewards[(byte)reward] += 1;
        }
    }

    private static void HandleRiskyRolls(ref Xoroshiro128Plus rng, Game game, ref byte[] rewards)
    {
        while (true)
        {
            var rand = (uint)rng.NextInt(10000);
            var reward = GetReward(rand, game);
            if (reward == SkillBroReward.DangerZone) break; // Hit the danger zone.

            rewards[(byte)reward] += 1;
        }
    }

    private static SkillBroReward GetReward(uint roll, Game game) => game == Game.Sword ? RewardsSword(roll) : RewardsShield(roll);


    private static SkillBroReward RewardsSword(uint roll) => roll switch
    {
        >= 6000 => SkillBroReward.DangerZone,
        >= 5999 => SkillBroReward.GoldBottleCap,
        >= 5850 => SkillBroReward.BottleCap,
        >= 5650 => SkillBroReward.NormalGem,
        >= 5450 => SkillBroReward.StickyBarb,
        >= 5250 => SkillBroReward.LightClay,
        >= 4950 => SkillBroReward.LaggingTail,
        >= 4650 => SkillBroReward.IronBall,
        >= 4350 => SkillBroReward.MetalCoat,
        >= 4050 => SkillBroReward.IceStone,
        >= 3750 => SkillBroReward.DawnStone,
        >= 3450 => SkillBroReward.DuskStone,
        >= 3150 => SkillBroReward.ShinyStone,
        >= 2850 => SkillBroReward.MoonStone,
        >= 2550 => SkillBroReward.SunStone,
        >= 2450 => SkillBroReward.FossilizedFish,
        >= 2350 => SkillBroReward.FossilizedDrake,
        >= 1850 => SkillBroReward.FossilizedDino,
        >= 1350 => SkillBroReward.FossilizedBird,
        >= 1050 => SkillBroReward.WishingPiece,
        >=  550 => SkillBroReward.CometShard,
        _       => SkillBroReward.RareBone,
    };

    private static SkillBroReward RewardsShield(uint roll) => roll switch
    {
        >= 6000 => SkillBroReward.DangerZone,
        >= 5999 => SkillBroReward.GoldBottleCap,
        >= 5850 => SkillBroReward.BottleCap,
        >= 5650 => SkillBroReward.NormalGem,
        >= 5450 => SkillBroReward.StickyBarb,
        >= 5250 => SkillBroReward.LightClay,
        >= 4950 => SkillBroReward.LaggingTail,
        >= 4650 => SkillBroReward.IronBall,
        >= 4350 => SkillBroReward.MetalCoat,
        >= 4050 => SkillBroReward.IceStone,
        >= 3750 => SkillBroReward.DawnStone,
        >= 3450 => SkillBroReward.DuskStone,
        >= 3150 => SkillBroReward.ShinyStone,
        >= 2850 => SkillBroReward.MoonStone,
        >= 2550 => SkillBroReward.SunStone,
        >= 2050 => SkillBroReward.FossilizedFish,
        >= 1550 => SkillBroReward.FossilizedDrake,
        >= 1450 => SkillBroReward.FossilizedDino,
        >= 1350 => SkillBroReward.FossilizedBird,
        >= 1050 => SkillBroReward.WishingPiece,
        >= 550  => SkillBroReward.CometShard,
        _       => SkillBroReward.RareBone,
    };
}
