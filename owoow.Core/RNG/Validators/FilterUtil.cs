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
        4 => RibbonIndex.MarkUncommon,
        5 => RibbonIndex.MarkLunchtime, // Time
        6 => RibbonIndex.MarkCloudy, // Weather
        7 => RibbonIndex.MarkFishing,
        8 => RibbonIndex.MarkRare,
        9 => RibbonIndex.MarkRowdy,
        10 => RibbonIndex.MarkAbsentMinded,
        11 => RibbonIndex.MarkJittery,
        12 => RibbonIndex.MarkExcited,
        13 => RibbonIndex.MarkCharismatic,
        14 => RibbonIndex.MarkCalmness,
        15 => RibbonIndex.MarkIntense,
        16 => RibbonIndex.MarkZonedOut,
        17 => RibbonIndex.MarkJoyful,
        18 => RibbonIndex.MarkAngry,
        19 => RibbonIndex.MarkSmiley,
        20 => RibbonIndex.MarkTeary,
        21 => RibbonIndex.MarkUpbeat,
        22 => RibbonIndex.MarkPeeved,
        23 => RibbonIndex.MarkIntellectual,
        24 => RibbonIndex.MarkFerocious,
        25 => RibbonIndex.MarkCrafty,
        26 => RibbonIndex.MarkScowling,
        27 => RibbonIndex.MarkKindly,
        28 => RibbonIndex.MarkFlustered,
        29 => RibbonIndex.MarkPumpedUp,
        30 => RibbonIndex.MarkZeroEnergy,
        31 => RibbonIndex.MarkPrideful,
        32 => RibbonIndex.MarkUnsure,
        33 => RibbonIndex.MarkHumble,
        34 => RibbonIndex.MarkThorny,
        35 => RibbonIndex.MarkVigor,
        36 => RibbonIndex.MarkSlump,
        _ => RibbonIndex.MAX_COUNT + 1, // Ignore
    };
}
