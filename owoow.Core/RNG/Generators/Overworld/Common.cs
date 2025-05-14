using owoow.Core.Enums;
using owoow.Core.Interfaces;
using PKHeX.Core;

namespace owoow.Core.RNG.Generators.Overworld;

public static class Common
{
    public static void DoPlacementRolls(ref Xoroshiro128Plus rng)
    {
        rng.NextInt(361);
        rng.Next();
    }

    public static ulong GenerateDexRecActivation(ref Xoroshiro128Plus rng)
    {
        return rng.NextInt(100);
    }

    public static ulong GenerateLeadAbilityActivation(ref Xoroshiro128Plus rng)
    {
        return rng.NextInt(100);
    }

    public static ulong GenerateEncounterRate(ref Xoroshiro128Plus rng, uint max = 100)
    {
        return rng.NextInt(max);
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

    public static char GenerateGender(ref Xoroshiro128Plus rng, short gender, bool isCC = false)
    {
        var roll = ' ';
        if (!isCC) roll = rng.NextInt(2) == 0 ? 'F' : 'M'; // 50/50 regardless of gender ratio
        return gender switch
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

    public static string GenerateAbility(ref Xoroshiro128Plus rng, string[]? abilities, bool locked = false, ulong roll = 2)
    {
        if (locked) return abilities![roll];
        roll = rng.NextInt(2);
        return abilities![1 - roll];
    }

    public static string GenerateItem(ref Xoroshiro128Plus rng, IEncounterTableEntry enc, AbilityType ab = AbilityType.NoEffect)
    {
        if (enc.HasItems)
        {
            if (enc.Items![0] != enc.Items![1])
            {
                var roll = rng.NextInt(100);
                if (ab == AbilityType.IncreaseItemRate)
                {
                    return roll switch
                    {
                        <= 59 => enc.Items![0] != "None" ? $"{enc.Items![0]} (60%)" : "None",
                        <= 79 => enc.Items![1] != "None" ? $"{enc.Items![1]} (20%)" : "None",
                        _ => "None",
                    };
                }
                else
                {
                    return roll switch
                    {
                        <= 49 => enc.Items![0] != "None" ? $"{enc.Items![0]} (50%)" : "None",
                        <= 54 => enc.Items![1] != "None" ? $"{enc.Items![1]} (5%)" : "None",
                        _ => "None",
                    };
                }
            }
            else
            {
                return $"{enc.Items![0]} (100%)";
            }
        }
        return "None";
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
