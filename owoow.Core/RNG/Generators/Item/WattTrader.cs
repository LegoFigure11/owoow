using owoow.Core.Interfaces;
using owoow.Core.RNG.Generators.Misc;
using PKHeX.Core;

namespace owoow.Core.RNG.Generators.Item;

public static class WattTrader
{
    public static Task<List<WattTraderFrame>> Generate(ulong s0, ulong s1, ulong start, ulong end, GeneratorConfig config)
    {
        return Task.Run(() =>
        {
            List<WattTraderFrame> frames = [];

            (s0, s1) = Util.XoroshiroJump(s0, s1, start);
            Xoroshiro128Plus outer = new(s0, s1);

            uint Jump = 0;

            for (ulong i = start; i <= end; i++)
            {
                var os = outer.GetState();
                var rng = new Xoroshiro128Plus(os.s0, os.s1);

                Jump = 0;

                if (config.ConsiderMenuClose)
                {
                    Jump += MenuClose.GetAdvances(ref rng, config.MenuCloseNPCs, config.MenuCloseIsHoldingDirection, config.Weather);
                }

                var Highlight = (uint)rng.NextInt(1000);
                if (Highlight < config.WattTraderSlotMin || Highlight > config.WattTraderSlotMax)
                {
                    outer.Next();
                    continue;
                }

                var Regular = (uint)rng.NextInt(9);

                frames.Add(new WattTraderFrame
                {
                    Advances = $"{i:N0}",
                    Jump = $"+{Jump}",
                    Animation = (os.s0 & 1 ^ os.s1 & 1) == 0 ? 'P' : 'S',
                    Highlight = $"{GetHighlightItem(Highlight)} ({Highlight:N0})",
                    Regular = $"{Regular}",
                    Seed0 = $"{os.s0:X16}",
                    Seed1 = $"{os.s1:X16}",
                });


                outer.Next();
            }
            return frames;
        });
    }

    private static string GetHighlightItem(uint Roll) => Roll switch
    {
        >= 980 => "King's Rock x1",
        >= 969 => "Chipped Pot x1",
        >= 939 => "Cracked Pot x1",
        >= 909 => "Magmarizer x1",
        >= 879 => "Electirizer x1",
        >= 869 => "Lucky Egg x1",
        >= 859 => "Starf Berry x1",
        >= 829 => "Lansat Berry x1",
        >= 828 => "Dream Ball x1",
        >= 827 => "Beast Ball x1",
        >= 797 => "Big Nugget x1",
        >= 782 => "Ribbon Sweet x1",
        >= 767 => "Star Sweet x1",
        >= 752 => "Flower Sweet x1",
        >= 737 => "Clover Sweet x1",
        >= 722 => "Berry Sweet x1",
        >= 707 => "Love Sweet x1",
        >= 692 => "Strawberry Sweet x1",
        >= 682 => "Galarica Twig x5",
        >= 642 => "Galarica Twig x3",
        >= 602 => "Max Elixir x1",
        >= 582 => "Max Mushrooms x1",
        >= 577 => "Dynite Ore x5",
        >= 557 => "Dynite Ore x1",
        >= 552 => "Armorite Ore x8",
        >= 532 => "Armorite Ore x3",
        >= 472 => "Armorite Ore x1",
        >= 422 => "Gigantamix x1",
        >= 382 => "Rare Candy x5",
        >= 332 => "Rare Candy x1",
        >= 327 => "PP Max x1",
        >= 302 => "PP Up x2",
        >= 252 => "PP Up x1",
        >= 242 => "Pink Apricorn x10",
        >= 232 => "Black Apricorn x10",
        >= 222 => "White Apricorn x10",
        >= 212 => "Green Apricorn x10",
        >= 202 => "Yellow Apricorn x10",
        >= 192 => "Blue Apricorn x10",
        >= 182 => "Red Apricorn x10",
        >= 165 => "Pink Apricorn x5",
        >= 148 => "Black Apricorn x5",
        >= 131 => "White Apricorn x5",
        >= 114 => "Green Apricorn x5",
        >= 97 => "Yellow Apricorn x5",
        >= 80 => "Blue Apricorn x5",
        >= 63 => "Red Apricorn x5",
        >= 60 => "Gold Bottle Cap x1",
        >= 50 => "Bottle Cap x3",
        _ => "Bottle Cap x1",
    };
}
