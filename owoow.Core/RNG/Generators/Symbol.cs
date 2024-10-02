using owoow.Core.Interfaces;
using PKHeX.Core;

namespace owoow.Core.RNG.Generators;

public class Symbol
{
    public static Task<List<Frame>> Generate(ulong s0, ulong s1, EncounterTable.EncounterTable table, ulong start, ulong end, GeneratorConfig config)
    {
        return Task.Run(() => {

            List<Frame> frames = [];
            var outer = new Xoroshiro128Plus(s0, s1);

            ulong Lead;
            IDictionary<int, IEncounterTableEntry> ActiveTable;
            IEncounterTableEntry Encounter;
            bool CuteCharm = false;

            ulong EncounterSlot = 0;
            bool EncounterSlotChosen = false;

            ulong Level;
            uint LevelDelta;

            ulong Aura;
            bool IsShiny;
            uint PID;
            uint EC;

            char Gender;
            string Ability;
            string Nature;
            string Item;

            string Mark;

            for (ulong i = start; i <= end; i++)
            {
                var os = outer.GetState();
                var rng = new Xoroshiro128Plus(os.s0, os.s1);

                EncounterSlotChosen = false;
                CuteCharm = false;

                // LEAD ABILITY ACTIVATION & ENCOUNTER SLOT
                Lead = rng.NextInt(100);

                if ((Lead + 1) < 66 && config.AbilityIsCuteCharm)
                {
                    CuteCharm = true;
                }
                if (Lead >= 49 && config.AbilityIsTypePulling && table.AbilityTable.Count > 0)
                {
                    ActiveTable = table.AbilityTable;
                    EncounterSlot = rng.NextInt((ulong)ActiveTable.Count);
                    EncounterSlotChosen = true;
                }
                else
                {
                    ActiveTable = table.MainTable;
                }

                if (!EncounterSlotChosen)
                {
                    EncounterSlot = rng.NextInt(100);
                }

                Encounter = ActiveTable[(int)EncounterSlot];
                if (Encounter.Species != config.TargetSpecies)
                {
                    outer.Next();
                    continue;
                }

                // LEVEL
                LevelDelta = (uint)(Encounter.MaxLevel - Encounter.MinLevel);
                Level = rng.NextInt(LevelDelta) + (uint)Encounter.MinLevel;

                // MARK -- DISCARDED
                Util.GenerateMark(ref rng, config.MarkRolls,config.WeatherActive);

                // BRILLIANT AURA
                Aura = rng.NextInt(1000);

                // SHINY
                IsShiny = Util.GenerateIsShiny(ref rng, config.ShinyRolls, config.TSV);

                // GENDER
                Gender = Util.GenerateGender(ref rng, Encounter, CuteCharm);

                // NATURE
                Nature = Util.GenerateNature(ref rng, config.AbilityIsSync);

                // ABILITY
                Ability = Util.GenerateAbility(ref rng, Encounter);

                // HELD ITEM
                Item = Util.GenerateItem(ref rng, Encounter);

                // AURA IVS/EMS


                var (_s0, _s1) = outer.GetState();
                // Matches, keep!
                frames.Add(new Frame()
                {
                    Advances = i,

                    Animation = (_s0 & 1 ^ _s1 & 1) == 1 ? 'P' : 'S',

                    Species = Encounter.Species!,
                    Level = (byte)Level,

                    Gender = Gender,
                    Nature = Nature,
                    Ability = Ability,
                    Item = Item,

                    Seed0 = $"{_s0:X16}",
                    Seed1 = $"{_s1:X16}",
                }) ;
                outer.Next();
            }
            return frames;
        });
    }
    // Lead Rand(100)
    // CC if r + 1 <= 66;
    // Type if r >= 49

    // if KO_Respawn
    // Rand(100) > 49

    // if (TypePullingAbility)
    // total = slots where type is ability.type
    // rand(total)

    // if (DexRec) 
    // active = rand(100)
    // if active > 49
    // rand(4), if exists in table then use that as slot

    // if no slot by now, slot = rand(100);
}
