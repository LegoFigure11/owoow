using owoow.Core.Interfaces;
using PKHeX.Core;

namespace owoow.Core.RNG.Generators;

public static class Common
{
    public static ulong GenerateLeadAbilityActivation(ref Xoroshiro128Plus rng)
    {
        return rng.NextInt(100);
    }

    public static ulong GenerateEncounterSlot(ref Xoroshiro128Plus rng, uint max = 100)
    {
        return rng.NextInt(max);
    }

    public static ulong GenerateLevel(ref Xoroshiro128Plus rng, IEncounterTableEntry enc)
    {
        ulong delta = (ulong)(enc.MaxLevel - enc.MinLevel) + 1;
        return rng.NextInt(delta) + (uint)enc.MinLevel;
    }

    public static RibbonIndex GenerateMark(ref Xoroshiro128Plus rng, int rolls, bool isWeather = false, bool isFishing = false)
    {
        for (int i = 0; i < rolls; i++)
        {
            uint rare = (uint)rng.NextInt(1000);
            uint personality = (uint)rng.NextInt(100);
            uint uncommon = (uint)rng.NextInt(50);
            uint weather = (uint)rng.NextInt(50); RibbonIndex.MarkRare.GetPropertyName();
            uint time = (uint)rng.NextInt(50);
            uint fishing = (uint)rng.NextInt(25);

            if (rare == 0) return RibbonIndex.MarkRare;
            if (personality == 0) return (RibbonIndex)((ulong)RibbonIndex.MarkRowdy + rng.NextInt(28));
            if (uncommon == 0) return RibbonIndex.MarkUncommon;
            if (weather == 0 && isWeather) return RibbonIndex.MarkCloudy;
            if (time == 0) return RibbonIndex.MarkLunchtime;
            if (fishing == 0 && isFishing) return RibbonIndex.MarkFishing;
        }
        return RibbonIndex.MAX_COUNT;
    }

    public static bool GenerateIsAura(ref Xoroshiro128Plus rng, uint thresh)
    {
        return rng.NextInt(1000) < thresh;
    }

    public static bool GenerateIsShiny(ref Xoroshiro128Plus rng, int rolls, uint tsv)
    {
        bool shiny = false;
        for (int i = 0; i < rolls; i++)
        {
            var pid = (uint)rng.Next();
            shiny = Util.GetShinyXOR(pid, tsv) < 16;
            if (shiny) break;
        }
        return shiny;
    }

    public static char GenerateGender(ref Xoroshiro128Plus rng, IEncounterTableEntry enc, bool isCC = false)
    {
        var roll = ' ';
        if (!isCC) roll = rng.NextInt(2) == 0 ? 'F' : 'M'; // 50/50 regardless of gender ratio
        return enc.Gender switch
        {
            >= 255 => '-',
            >= 254 => 'F',
            >= 1 => isCC ? 'C' : roll,
            _ => 'M',
        };
    }

    public static string GenerateNature(ref Xoroshiro128Plus rng, bool sync)
    {
        var nature = Util.Natures[(int)rng.NextInt(25)];
        if (sync) nature = "Sync";
        return nature;
    }

    public static string GenerateAbility(ref Xoroshiro128Plus rng, IEncounterTableEntry enc, bool locked = false)
    {
        ulong roll = 2;
        if (!locked) roll = rng.NextInt(2);
        return enc.Abilities![roll];
    }

    public static string GenerateItem(ref Xoroshiro128Plus rng, IEncounterTableEntry enc)
    {
        string item = "None";
        if (enc.HasItems)
        {
            var roll = rng.NextInt(100);
            item = roll switch
            {
                <= 49 => enc.Items![0] != "None" ? $"{enc.Items![0]} (50%)" : "None",
                <= 54 => enc.Items![1] != "None" ? $"{enc.Items![1]} (5%)" : "None",
                <= 55 => enc.Items![2] != "None" ? $"{enc.Items![2]} (1%)" : "None",
                _ => "None",
            };
        }
        return item;
    }

    public static int GenerateAuraIVs(ref Xoroshiro128Plus rng)
    {
        return (int)rng.NextInt(2) | 2;
    }

    public static string GenerateAuraEggMove(ref Xoroshiro128Plus rng, IEncounterTableEntry enc)
    {
        var move = "None";
        if (enc.EggMoveCount > 1) move = enc.EggMoves![rng.NextInt(enc.EggMoveCount)];
        return move;
    }
}
