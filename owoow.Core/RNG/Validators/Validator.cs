using owoow.Core.Enums;
using PKHeX.Core;

namespace owoow.Core.RNG.Validators
{
    public static class Validator
    {
        public static bool CheckIsAura(bool aura, AuraType target) => target switch
        {
            AuraType.None => !aura,
            AuraType.Brilliant => aura,
            _ => true,
        };

        public static bool CheckNature(string nature, Nature target) => nature == "Sync" || target == Nature.Random || nature == Util.Natures[(int)target];

        public static bool CheckEC(uint ec, bool rare) => !rare || (rare && ec % 100 == 0);

        public static bool CheckIsShiny(uint xor, ShinyType target) => target switch
        {
            ShinyType.Square => xor == 0,
            ShinyType.Star => xor > 0 && xor < 16,
            ShinyType.Either => xor < 16,
            ShinyType.None => xor >= 16,
            _ => true,
        };

        public static bool CheckHeight(uint height, ScaleType target) => target switch
        {
            ScaleType.XXXS => height == 0,
            ScaleType.XXS => height >= 1 && height <= 24,
            ScaleType.XS => height >= 25 && height <= 59,
            ScaleType.S => height >= 60 && height <= 99,
            ScaleType.M => height >= 100 && height <= 155,
            ScaleType.L => height >= 156 && height <= 195,
            ScaleType.XL => height >= 196 && height <= 230,
            ScaleType.XXL => height >= 231 && height <= 254,
            ScaleType.XXXL => height == 255,
            ScaleType.MinOrMax => height == 0 || height == 255,
            _ => true,
        };

        public static bool CheckMark(RibbonIndex mark, RibbonIndex target) => target switch
        {
            RibbonIndex.MAX_COUNT + 1 => true, // Ignore
            RibbonIndex.MAX_COUNT + 2 => mark < RibbonIndex.MAX_COUNT, // Any Mark
            RibbonIndex.MAX_COUNT + 3 => mark >= RibbonIndex.MarkRowdy && mark <= RibbonIndex.MarkSlump, // Personality
            RibbonIndex.MAX_COUNT + 4 => (mark >= RibbonIndex.MarkSlump && mark <= RibbonIndex.MarkSlump) || mark == RibbonIndex.MarkRare, // Personality or Rare
            RibbonIndex.MAX_COUNT => mark == RibbonIndex.MAX_COUNT, // None
            _ => mark == target,
        };

        public static bool CheckLotoIDResult(LotoIDTargetType result, LotoIDTargetType target) => target == result || target == LotoIDTargetType.Any;

        public static bool CheckSuccessType(bool success, SuccessType target) => target switch
        {
            SuccessType.Yes => success,
            SuccessType.No => !success,
            _ => true,
        };
    }
}
