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

    public static RibbonIndex GetFilterMarkype(int selected) => selected switch
    {
        1 => RibbonIndex.MAX_COUNT, // None
        2 => RibbonIndex.MAX_COUNT + 2, // Any
        3 => RibbonIndex.MAX_COUNT + 3, // Personality
        4 => RibbonIndex.MAX_COUNT + 4, // Personality/Rare
        5 => RibbonIndex.MarkUncommon,
        6 => RibbonIndex.MarkLunchtime, // Time
        7 => RibbonIndex.MarkCloudy, // Weather
        8 => RibbonIndex.MarkFishing,
        9 => RibbonIndex.MarkRare,
        10 => RibbonIndex.MarkRowdy,
        11 => RibbonIndex.MarkAbsentMinded,
        12 => RibbonIndex.MarkJittery,
        13 => RibbonIndex.MarkExcited,
        14 => RibbonIndex.MarkCharismatic,
        15 => RibbonIndex.MarkCalmness,
        16 => RibbonIndex.MarkIntense,
        17 => RibbonIndex.MarkZonedOut,
        18 => RibbonIndex.MarkJoyful,
        19 => RibbonIndex.MarkAngry,
        20 => RibbonIndex.MarkSmiley,
        21 => RibbonIndex.MarkTeary,
        22 => RibbonIndex.MarkUpbeat,
        23 => RibbonIndex.MarkPeeved,
        24 => RibbonIndex.MarkIntellectual,
        25 => RibbonIndex.MarkFerocious,
        26 => RibbonIndex.MarkCrafty,
        27 => RibbonIndex.MarkScowling,
        28 => RibbonIndex.MarkKindly,
        29 => RibbonIndex.MarkFlustered,
        30 => RibbonIndex.MarkPumpedUp,
        31 => RibbonIndex.MarkZeroEnergy,
        32 => RibbonIndex.MarkPrideful,
        33 => RibbonIndex.MarkUnsure,
        34 => RibbonIndex.MarkHumble,
        35 => RibbonIndex.MarkThorny,
        36 => RibbonIndex.MarkVigor,
        37 => RibbonIndex.MarkSlump,
        _ => RibbonIndex.MAX_COUNT + 1, // Ignore
    };
}
