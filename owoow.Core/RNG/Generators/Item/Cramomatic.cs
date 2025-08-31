using owoow.Core.Enums;
using owoow.Core.Interfaces;
using owoow.Core.RNG.Generators.Misc;
using PKHeX.Core;
using static owoow.Core.RNG.Validators.Validator;

namespace owoow.Core.RNG.Generators.Item;

public static class Cramomatic
{
    public static Task<List<CramomaticFrame>> Generate(ulong s0, ulong s1, ulong start, ulong end, GeneratorConfig config)
    {
        return Task.Run(() =>
        {
            List<CramomaticFrame> frames = [];

            bool isSweet = config.CramomaticInputs.Any(i => i is CramomaticInputItemType.SweetIngredient);
            bool allSame = config.CramomaticInputs.All(i => i == config.CramomaticInputs[0]);

            uint Jump = 0;

            (s0, s1) = Util.XoroshiroJump(s0, s1, start);

            Xoroshiro128Plus outer = new(s0, s1);

            for (ulong i = start; i <= end; i++)
            {
                var os = outer.GetState();
                var rng = new Xoroshiro128Plus(os.s0, os.s1);

                Jump = 0;

                if (config.ConsiderMenuClose)
                {
                    Jump += MenuClose.GetAdvances(ref rng, config.MenuCloseNPCs, config.MenuCloseIsHoldingDirection, config.Weather);
                }

                uint slot = 4;
                uint itemRoll = 0;
                bool isSportSafari = false;
                if (isSweet)
                {
                    itemRoll = (uint)rng.NextInt(100);
                }
                else
                {
                    slot -= (uint)rng.NextInt(4); // reverse order
                    itemRoll = (uint)rng.NextInt(100);
                    isSportSafari = rng.NextInt(1000) == 0;
                }

                CramomaticGenerateType item = GetItemType(itemRoll, isSportSafari, !isSweet);

                if (!CheckCramomaticResult(item, config.CramomaticTargetType, allSame))
                {
                    outer.Next();
                    continue;
                }

                bool isBonus = rng.NextInt((uint)(item is CramomaticGenerateType.SportSafari or CramomaticGenerateType.Apricorn ? 1000 : 100)) == 0;

                if (!isSweet && config.BonusOnly && !isBonus)
                {
                    outer.Next();
                    continue;
                }

                frames.Add(new CramomaticFrame
                {
                    Advances = $"{i:N0}",
                    Jump = $"+{Jump}",
                    Animation = (os.s0 & 1 ^ os.s1 & 1) == 0 ? 'P' : 'S',
                    Prize = GetItemName(item, config.CramomaticInputs[slot - 1], allSame),
                    Bonus = isBonus && !isSweet,
                    Seed0 = $"{os.s0:X16}",
                    Seed1 = $"{os.s1:X16}",
                });


                outer.Next();
            }
            return frames;
        });
    }

    private static CramomaticGenerateType GetItemType(uint roll, bool isSportSafari = false, bool isBall = false)
    {
        if (isBall)
        {
            return isSportSafari ? CramomaticGenerateType.SportSafari : GetBallType(roll);
        }
        return GetSweetType(roll);
    }

    private static CramomaticGenerateType GetBallType(uint roll) => roll switch
    {
        >= 99 => CramomaticGenerateType.Apricorn,
        >= 75 => CramomaticGenerateType.Shop2,
        >= 50 => CramomaticGenerateType.Shop1,
        >= 25 => CramomaticGenerateType.GreatBall,
        _ => CramomaticGenerateType.PokeBall,
    };

    private static CramomaticGenerateType GetSweetType(uint roll) => roll switch
    {
        >= 89 => CramomaticGenerateType.RibbonSweet,
        >= 79 => CramomaticGenerateType.StarSweet,
        _ => CramomaticGenerateType.StrawberrySweet,
    };

    private static string GetItemName(CramomaticGenerateType item, CramomaticInputItemType color, bool isSafari) => item switch
    {
        CramomaticGenerateType.StarSweet       => "Star Sweet",
        CramomaticGenerateType.RibbonSweet     => "Ribbon Sweet",
        CramomaticGenerateType.StrawberrySweet => "Strawberry Sweet",
        CramomaticGenerateType.PokeBall        => "Poké Ball",
        CramomaticGenerateType.GreatBall       => "Great Ball",
        CramomaticGenerateType.SportSafari     => isSafari ? "Safari Ball" : "Sport Ball",
        _                                      => BallList[color][(int)item - 1],
    };

    private static readonly OrderedDictionary<CramomaticInputItemType, string[]> BallList = new() {
        { CramomaticInputItemType.BlackApricorn,  [ "Heavy Ball",  "Luxury Ball", "Dusk Ball"    ] },
        { CramomaticInputItemType.BlueApricorn,   [ "Lure Ball",   "Dive Ball",   "Net Ball"     ] },
        { CramomaticInputItemType.GreenApricorn,  [ "Friend Ball", "Nest Ball",   "Ultra Ball"   ] },
        { CramomaticInputItemType.PinkApricorn,   [ "Love Ball",   "Heal Ball",   "Ultra Ball"   ] },
        { CramomaticInputItemType.RedApricorn,    [ "Level Ball",  "Repeat Ball", "Ultra Ball"   ] },
        { CramomaticInputItemType.WhiteApricorn,  [ "Fast Ball",   "Timer Ball",  "Premier Ball" ] },
        { CramomaticInputItemType.YellowApricorn, [ "Moon Ball",   "Quick Ball",  "Ultra Ball"   ] },
    };
}
