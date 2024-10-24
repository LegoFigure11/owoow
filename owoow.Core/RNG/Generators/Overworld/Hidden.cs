﻿using owoow.Core.Enums;
using owoow.Core.Interfaces;
using PKHeX.Core;
using static owoow.Core.RNG.Generators.Common;
using static owoow.Core.RNG.Generators.Fixed;
using static owoow.Core.RNG.Validators.Validator;

namespace owoow.Core.RNG.Generators;

public class Hidden
{
    public static Task<List<Frame>> Generate(ulong s0, ulong s1, EncounterTable.EncounterTable table, ulong start, ulong end, GeneratorConfig config)
    {
        return Task.Run(() =>
        {

            List<Frame> frames = [];
            var outer = new Xoroshiro128Plus(s0, s1);

            bool FiltersEnabled = config.FiltersEnabled;

            ulong Lead = 0;
            IDictionary<int, IEncounterTableEntry> ActiveTable;
            IEncounterTableEntry Encounter;
            bool CuteCharm = false;

            byte step = 0;
            ulong EncounterSlot = 0;
            ulong BaseEncounterRate = 0;
            ulong EncounterRate = 0;
            bool CanGenerate = false;
            bool EncounterSlotChosen = false;

            ulong Level;

            bool IsShiny;
            uint ShinyXOR;
            uint PID;
            uint EC;

            char Gender;
            string Ability;
            string Nature;
            string Item;

            bool PassIVs;
            byte[] IVs;

            uint Height;

            uint Jump = 0;

            RibbonIndex Mark;

            for (ulong i = start; i <= end; i++)
            {
                var os = outer.GetState();
                var rng = new Xoroshiro128Plus(os.s0, os.s1);

                EncounterSlotChosen = false;
                CanGenerate = false;
                CuteCharm = false;
                step = 0;

                if (config.ConsiderMenuClose)
                {
                    Jump = MenuClose.GetAdvances(ref rng, config.MenuCloseNPCs, config.MenuCloseIsHoldingDirection);
                }

                rng.NextInt();
                rng.NextInt(100);

                // ENCOUNTER RATE, LEAD ABILITY ACTIVATION & ENCOUNTER SLOT
                while (!CanGenerate) // does this stop after 5 iters?
                {
                    Lead = GenerateLeadAbilityActivation(ref rng);
                    BaseEncounterRate = Util.GetHiddenEncounterModifiedRate(step, config.AbilityType);

                    EncounterRate = GenerateEncounterRate(ref rng);
                    
                    if (!(BaseEncounterRate <= EncounterRate)) CanGenerate = true;
                    step++;
                }

                if (config.AbilityType == AbilityType.CuteCharm && (Lead + 1) <= 66)
                {
                    CuteCharm = true;
                }
                if (config.AbilityType == AbilityType.TypePulling && table.AbilityTable.Count > 0 && Lead >= 49)
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
                if (FiltersEnabled && Encounter.Species != config.TargetSpecies)
                {
                    outer.Next();
                    continue;
                }

                // LEVEL
                Level = GenerateLevel(ref rng, Encounter);

                // MARK -- DISCARDED
                _ = GenerateMark(ref rng, config.MarkRolls, config.WeatherActive);

                // SHINY
                IsShiny = GenerateIsShiny(ref rng, config.ShinyRolls, config.TSV);

                // GENDER
                Gender = GenerateGender(ref rng, Encounter.Gender, CuteCharm);

                // NATURE
                Nature = GenerateNature(ref rng, config.AbilityType == AbilityType.Synchronize);
                if (FiltersEnabled && !CheckNature(Nature, config.TargetNature))
                {
                    outer.Next();
                    continue;
                }

                // ABILITY
                Ability = GenerateAbility(ref rng, Encounter.Abilities);

                // HELD ITEM
                Item = GenerateItem(ref rng, Encounter);

                // FIXED SEED
                var go = new Xoroshiro128Plus(GenerateFixedSeed(ref rng), 0x82A2B175229D6A5B);

                // ENCRYPTION CONSTANT
                EC = GenerateEC(ref go);
                if (FiltersEnabled && !CheckEC(EC, config.RareEC))
                {
                    outer.Next();
                    continue;
                }

                // PID
                PID = GeneratePID(ref go, IsShiny, config.TSV);
                ShinyXOR = Util.GetShinyXOR(PID, config.TSV);
                if (FiltersEnabled && !CheckIsShiny(ShinyXOR, config.TargetShiny))
                {
                    outer.Next();
                    continue;
                }

                // IVS
                (PassIVs, IVs) = GenerateIVs(ref go, 0, config);
                if (!PassIVs) // FiltersEnabled check takes place in GenerateIVs
                {
                    outer.Next();
                    continue;
                }

                // HEIGHT
                Height = GenerateHeightWeightScale(ref go);

                // MARK
                Mark = GenerateMark(ref rng, config.MarkRolls, config.WeatherActive);
                if (FiltersEnabled && !CheckMark(Mark, config.TargetMark))
                {
                    outer.Next();
                    continue;
                }

                // Matches, keep!
                frames.Add(new Frame()
                {
                    Advances = $"{i:N0}",

                    Jump = $"+{Jump}",

                    Step = step,

                    Animation = (os.s0 & 1 ^ os.s1 & 1) == 1 ? 'P' : 'S',

                    Species = Encounter.Species!,
                    Shiny = Util.GetShinyType(ShinyXOR),
                    Level = (byte)Level,

                    Gender = Gender,
                    Nature = Nature,
                    Ability = Ability,
                    Item = Item,

                    PID = $"{PID:X8}",
                    EC = $"{EC:X8}",

                    H = IVs[0],
                    A = IVs[1],
                    B = IVs[2],
                    C = IVs[3],
                    D = IVs[4],
                    S = IVs[5],

                    Height = $"{Height}",

                    Mark = Util.GetRibbonName(Mark),

                    Seed0 = $"{os.s0:X16}",
                    Seed1 = $"{os.s1:X16}",
                });
                outer.Next();
            }
            return frames;
        });
    }
}