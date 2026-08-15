using owoow.Core.Enums;
using owoow.Core.Interfaces;
using PKHeX.Core;
using static owoow.Core.RNG.Validators.Validator;

namespace owoow.Core.RNG.Generators.Misc;

public class Capture
{
    // https://gist.github.com/Lusamine/a6d25bac975358193875700321dd0089
    // https://gist.github.com/Lusamine/e80a0ea4663bd002875bf7608fe25b30
    // https://x.com/Sibuna_Switch/status/1549932659220119552
    public static Task<List<CaptureFrame>> Generate(ulong s0, ulong s1, ulong start, ulong end, GeneratorConfig config)
    {
        return Task.Run(() =>
        {

            List<CaptureFrame> frames = [];
            var outer = new Xoroshiro128Plus(s0, s1);

            bool FiltersEnabled = config.FiltersEnabled;
            bool Caught = false;
            bool IsCrit = false;
            var cvc = GetCVC(config.BeatRaihan, config.Level, config.PartyLevel);
            var BallModifier = GetBallModifier(config);

            var v12 = 3 * config.MaxHP;
            var v13 = v12 - 2 * config.CurrHP;
            var a = (int)(float)(0.5f + (v13 << 12));

            long c = config.CatchRate * a;

            var ball = config.Ball;
            if (ball == BallType.HeavyBall)
            {
                var sumhb = GetHeavyBallMod(config) + config.CatchRate;
                sumhb = Math.Max(sumhb, 1);
                c = sumhb * a;
            }

            if (ball != BallType.BeastBall && config.IsUltraBeast) BallModifier = 0x19A;

            var d = (uint)((ulong)(BallModifier * c + 2048) >> 12) / v12;

            if (config.Level <= 20)
            {
                d = (int)((20 - config.Level) * d) / 10;
            }

            var status = GetStatusMod(config.Status);
            if (config.Status != StatusType.None)
            {
                d = (uint)(status * d + 2048) >> 12;
            }

            var ModifiedCatchRate = Math.Min(((ulong)(d * cvc + 2048) >> 12), 0xFF000);
            if (ModifiedCatchRate == 0xFF000) Caught = true;

            var CritChance = GetCritChance(ModifiedCatchRate, config.HasCatchingCharm, config.ZukanCount);
            var ShakeValue = (double)GetShakeValue(ModifiedCatchRate);

            for (ulong i = start; i <= end; i++)
            {
                IsCrit = false;
                var os = outer.GetState();
                var rng = new Xoroshiro128Plus(os.s0, os.s1);

                var crit = (int)rng.NextInt(0x100);
                if (crit < CritChance) IsCrit = true;
                if (!CheckIsAura(IsCrit, config.TargetCrit))
                {
                    outer.Next();
                    continue;
                }

                var rollCount = IsCrit ? 1 : 4;

                byte roll = 0;
                for (; roll < rollCount; roll++)
                {
                    if (!Caught)
                    {
                        var shake = (int)rng.NextInt(0x10000);
                        if (!(shake < ShakeValue)) break;
                    }
                }

                if (roll < config.TargetMinRolls || roll > config.TargetMaxRolls)
                {
                    outer.Next();
                    continue;
                }

                var success = Caught || (roll == rollCount);

                if (!CheckIsAura(success, config.TargetSuccess))
                {
                    outer.Next();
                    continue;
                }

                var f = new CaptureFrame()
                {
                    Advances = $"{i:N0}",
                    Success = success,
                    Shakes = roll,
                    Critical = IsCrit,
                    Seed0 = $"{os.s0:X16}",
                    Seed1 = $"{os.s1:X16}",
                };
                frames.Add(f);
                outer.Next();
            }
            return frames;
        });
    }

    private static int GetCVC(bool BeatRaihan, byte wLevel, byte pLevel) => !BeatRaihan && (pLevel < wLevel) ? 0x19A : 0x1000;
    private static int GetBallModifier(GeneratorConfig c) => c.Ball switch
    {
        BallType.BeastBall => c.IsUltraBeast ? 0x5000 : 0x19A,
        BallType.DiveBall => c.Surfing || c.Fishing ? 0x3800 : 0x1000,
        BallType.DreamBall => c.Status == StatusType.Sleep ? 0x4000 : 0x1000,
        BallType.DuskBall => c.Dusk ? 0x3000 : 0x1000,
        BallType.FastBall => c.BaseSpeed >= 100 ? 0x4000 : 0x1000,
        BallType.GreatBall => 0x1800,
        BallType.LevelBall => GetLevelBallRate(c),
        BallType.LoveBall => GetLoveBallRate(c),
        BallType.LureBall => c.Fishing ? 0x4000 : 0x1000,
        BallType.MoonBall => c.MoonStoneEvo ? 0x4000 : 0x1000,
        BallType.NestBall => GetNestBallRate(c),
        BallType.NetBall => c.IsBugOrWaterType ? 0x3800 : 0x1000,
        BallType.QuickBall => c.FirstTurn ? 0x5000 : 0x1000,
        BallType.RepeatBall => c.Registered ? 0x3800 : 0x1000,
        BallType.TimerBall => GetTimerBallRate(c),
        BallType.UltraBall => 0x2000,
        _ => 0x1000
    };

    private static int GetLevelBallRate(GeneratorConfig c)
    {
        var wLevel = c.Level;
        var pLevel = c.PartyLevel;

        if (pLevel >> 2 >= wLevel) return 0x8000;
        if (pLevel >> 1 >= wLevel) return 0x4000;
        if (wLevel >= pLevel) return 0x1000;
        return 0x2000;
    }

    private static int GetLoveBallRate(GeneratorConfig c)
    {
        if (c.Species != c.PartySpecies || c.Gender == c.PartyGender) return 0x1000;
        return 0x8000;
    }

    private static int GetNestBallRate(GeneratorConfig c)
    {
        var level = c.Level;
        if (level >= 30) return 0x1000;
        ushort delta = (ushort)(41 - level);
        if (41 - level >= 40) delta = 40;
        float v21 = 0.5f;
        if (delta == 0) v21 = -0.5f;
        return (int)(float)(v21 + (delta << 12)) / 10;
    }

    private static int GetTimerBallRate(GeneratorConfig c) => Math.Min(1229 * c.Turns + 0x1000, 0x4000);

    private static int GetHeavyBallMod(GeneratorConfig c) => c.Weight switch
    {
        >= 3000 => 30,
        >= 2000 => 20,
        >= 1000 => 0,
        _ => -20,
    };

    private static int GetStatusMod(StatusType s) => s switch
    {
        StatusType.Sleep or StatusType.Freeze => 0x2800,
        StatusType.Paralysis or StatusType.Burn or StatusType.Poison => 0x1800,
        _ => 0x1000,
    };

    public static int GetCritCatchRateFromZukanCount(int zukanCount) => zukanCount switch
    {
        > 600 => 0x2800,
        > 450 => 0x2000,
        > 300 => 0x1800,
        > 150 => 0x1000,
        >  30 => 0x0800,
        _ => 0,
    };

    public static int GetCritChance(ulong mcr, bool hasCharm, int zukanCount)
    {
        var zukanMod = GetCritCatchRateFromZukanCount(zukanCount);
        if (zukanMod == 0) return -1;
        var charmMod = hasCharm ? 1 : 0;
        ulong v15 = (ulong)((long)0x2AAAAAAB * (int)(((uint)(zukanMod << charmMod) * mcr + 2048) >> 12));
        return (int)((v15 >> 32) + (v15 >> 63)) >> 12;
    }

    public static int GetShakeValue(ulong mcr)
    {
        int v23;
        int v35;

        var v18 = (double)(mcr & 0xFFF) / 4096 + (mcr >> 12);

        if (v18 == 0.0)
        {
            v23 = 0;
        }
        else
        {
            var v19 = 255.0 / v18;
            var v20 = v19 * 4096.0;
            v23 = (int)Math.Round(v20);
        }
        var v25 = (double)(v23 & 0xFFF) / 4096 + (v23 >> 12);

        var v26 = Math.Pow(v25, 0.1875);
        var v27 = v26 * 4096.0;
        var v29 = (int)Math.Round(v27);
        var v31 = (double)(v29 & 0xFFF) / 4096 + (v29 >> 12);

        if (v31 == 0.0)
        {
            v35 = 0;
        }
        else
        {
            var v32 = 65536.0 / v31;
            var v33 = v32 * 4096.0;
            var v36 = (int)Math.Round(v33);
            v35 = v36 >> 12;
        }
        return v35;
    }
}
