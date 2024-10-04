using owoow.Core.Interfaces;
using PKHeX.Core;
using static owoow.Core.RNG.Generators.Common;
using static owoow.Core.RNG.Generators.Fixed;
using static owoow.Core.RNG.Validators.Validator;

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

            bool IsAura;
            bool IsShiny;
            uint PID;
            uint EC;

            char Gender;
            string Ability;
            string Nature;
            string Item;

            ulong AuraIVs;
            string AuraEggMove;

            bool PassIVs;
            uint[] IVs;

            uint Height;

            string Mark;

            (uint AuraThreshold, uint AuraRolls) = Util.GetBrilliantInfo(config.AuraKOs);

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
                    EncounterSlot = GenerateEncounterSlot(ref rng, (uint)ActiveTable.Count);
                    EncounterSlotChosen = true;
                }
                else
                {
                    ActiveTable = table.MainTable;
                }

                if (!EncounterSlotChosen)
                {
                    EncounterSlot = GenerateEncounterSlot(ref rng);
                }

                Encounter = ActiveTable[(int)EncounterSlot];
                if (Encounter.Species != config.TargetSpecies)
                {
                    outer.Next();
                    continue;
                }

                // LEVEL
                Level = GenerateLevel(ref rng, Encounter);

                // MARK -- DISCARDED
                _ = GenerateMark(ref rng, config.MarkRolls, config.WeatherActive);

                // BRILLIANT AURA
                IsAura = GenerateIsAura(ref rng, AuraThreshold);
                if (!CheckIsAura(IsAura, config.TargetAura))
                {
                    outer.Next();
                    continue;
                }

                // SHINY
                IsShiny = GenerateIsShiny(ref rng, config.ShinyRolls, config.TSV);

                // GENDER
                Gender = GenerateGender(ref rng, Encounter, CuteCharm);

                // NATURE
                Nature = GenerateNature(ref rng, config.AbilityIsSync);
                if (!CheckNature(Nature, config.TargetNature))
                {
                    outer.Next();
                    continue;
                }

                // ABILITY
                Ability = GenerateAbility(ref rng, Encounter);

                // HELD ITEM
                Item = GenerateItem(ref rng, Encounter);

                // AURA IVS/EMS
                AuraIVs = 0;
                AuraEggMove = string.Empty;
                if (IsAura)
                {
                    Level = (ulong)Encounter.MaxLevel;
                    AuraIVs = GenerateAuraIVs(ref rng);
                    AuraEggMove = GenerateAuraEggMove(ref rng, Encounter);
                }

                // FIXED SEED
                var go = new Xoroshiro128Plus(GenerateFixedSeed(ref rng), 0x82A2B175229D6A5B);

                // ENCRYPTION CONSTANT
                EC = GenerateEC(ref go);
                if (!CheckEC(EC, config.RareEC))
                {
                    outer.Next();
                    continue;
                }

                // PID
                PID = GeneratePID(ref go, IsShiny, config.TSV);
                if (!CheckIsShiny(Util.GetShinyXOR(PID, config.TSV), config.TargetShiny))
                {
                    outer.Next();
                    continue;
                }

                // IVS
                (PassIVs, IVs) = GenerateIVs(ref go, AuraIVs, config);
                if (!PassIVs)
                {
                    outer.Next();
                    continue;
                }

                // HEIGHT
                Height = GenerateHeightWeightScale(ref rng);

                // MARK
                Mark = GenerateMark(ref rng, config.MarkRolls, config.WeatherActive);

                // Matches, keep!
                frames.Add(new Frame()
                {
                    Advances = i,

                    Animation = (os.s0 & 1 ^ os.s1 & 1) == 1 ? 'P' : 'S',

                    Species = Encounter.Species!,
                    Level = (byte)Level,

                    Gender = Gender,
                    Nature = Nature,
                    Ability = Ability,
                    Item = Item,

                    Seed0 = $"{os.s0:X16}",
                    Seed1 = $"{os.s1:X16}",
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
