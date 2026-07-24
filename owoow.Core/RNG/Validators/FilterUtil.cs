using owoow.Core.Enums;
using PKHeX.Core;

namespace owoow.Core.RNG;

public static class FilterUtil
{
    public static ShinyType GetFilterShinyType(int selected) => selected switch
    {
        1 => ShinyType.Either,
        2 => ShinyType.Square,
        3 => ShinyType.Star,
        4 => ShinyType.None,
        _ => ShinyType.Any,
    };

    public static AuraType GetFilterAuraType(int selected) => selected switch
    {
        1 => AuraType.Brilliant,
        2 => AuraType.None,
        _ => AuraType.Any,
    };

    public static ScaleType GetFilterScaleType(int selected) => (ScaleType)selected;

    public static RibbonIndex GetFilterMarkType(int selected) => selected switch
    {
        1 => RibbonIndex.MAX_COUNT, // None
        2 => RibbonIndex.MAX_COUNT + 2, // Any
        3 => RibbonIndex.MAX_COUNT + 3, // Personality
        4 => RibbonIndex.MAX_COUNT + 4, // Personality/Rare
        5 => RibbonIndex.MAX_COUNT + 5, // Any except Uncommon
        6 => RibbonIndex.MarkUncommon,
        7 => RibbonIndex.MarkLunchtime, // Time
        8 => RibbonIndex.MarkCloudy, // Weather
        9 => RibbonIndex.MarkFishing,
        10 => RibbonIndex.MarkRare,
        11 => RibbonIndex.MarkRowdy,
        12 => RibbonIndex.MarkAbsentMinded,
        13 => RibbonIndex.MarkJittery,
        14 => RibbonIndex.MarkExcited,
        15 => RibbonIndex.MarkCharismatic,
        16 => RibbonIndex.MarkCalmness,
        17 => RibbonIndex.MarkIntense,
        18 => RibbonIndex.MarkZonedOut,
        19 => RibbonIndex.MarkJoyful,
        20 => RibbonIndex.MarkAngry,
        21 => RibbonIndex.MarkSmiley,
        22 => RibbonIndex.MarkTeary,
        23 => RibbonIndex.MarkUpbeat,
        24 => RibbonIndex.MarkPeeved,
        25 => RibbonIndex.MarkIntellectual,
        26 => RibbonIndex.MarkFerocious,
        27 => RibbonIndex.MarkCrafty,
        28 => RibbonIndex.MarkScowling,
        29 => RibbonIndex.MarkKindly,
        30 => RibbonIndex.MarkFlustered,
        31 => RibbonIndex.MarkPumpedUp,
        32 => RibbonIndex.MarkZeroEnergy,
        33 => RibbonIndex.MarkPrideful,
        34 => RibbonIndex.MarkUnsure,
        35 => RibbonIndex.MarkHumble,
        36 => RibbonIndex.MarkThorny,
        37 => RibbonIndex.MarkVigor,
        38 => RibbonIndex.MarkSlump,
        _ => RibbonIndex.MAX_COUNT + 1, // Ignore
    };
}
