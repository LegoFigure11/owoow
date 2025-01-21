using owoow.Core.Enums;
using owoow.Core.Interfaces;
using owoow.Core.RNG.Generators.Misc;
using PKHeX.Core;
using static owoow.Core.RNG.Validators.Validator;

namespace owoow.Core.RNG.Generators.Item;

public static class LotoID
{
    public static Task<List<LotoIDFrame>> Generate(ulong s0, ulong s1, ulong start, ulong end, GeneratorConfig config)
    {
        return Task.Run(() =>
        {
            List<LotoIDFrame> frames = [];

            Xoroshiro128Plus outer = new(s0, s1);

            ulong advances = 0;
            uint Jump = 0;

            for (; advances < start; advances++)
            {
                outer.Next();
            }

            for (ulong i = start; i <= end; i++)
            {
                var os = outer.GetState();
                var rng = new Xoroshiro128Plus(os.s0, os.s1);

                Jump = 0;

                if (config.ConsiderMenuClose)
                {
                    Jump += MenuClose.GetAdvances(ref rng, config.MenuCloseNPCs, config.MenuCloseIsHoldingDirection, config.Weather);
                }

                uint id = 0;
                for (uint j = 10000; j > 0; j /= 10)
                {
                    id += (uint)rng.NextInt(10) * j;
                }

                var s = $"{id:D5}";
                var item = LotoIDTargetType.Any;
                foreach (var ID in config.IDs)
                {
                    if (s[4] == ID[5] && s[3] == ID[4] && s[2] == ID[3] && s[1] == ID[2] && s[0] == ID[1]) item = LotoIDTargetType.MasterBall;
                    else if (s[4] == ID[5] && s[3] == ID[4] && s[2] == ID[3] && s[1] == ID[2]) item = LotoIDTargetType.RareCandy;
                    else if (s[4] == ID[5] && s[3] == ID[4] && s[2] == ID[3]) item = LotoIDTargetType.PPMax;
                    else if (s[4] == ID[5] && s[3] == ID[4]) item = LotoIDTargetType.PPUp;
                    else if (s[4] == ID[5]) item = LotoIDTargetType.MoomooMilk;
                }

                if (!CheckLotoIDResult(item, config.LotoIDTargetType))
                {
                    outer.Next();
                    continue;
                }

                frames.Add(new LotoIDFrame
                {
                    Advances = $"{i:N0}",
                    Jump = $"+{Jump}",
                    Animation = (os.s0 & 1 ^ os.s1 & 1) == 0 ? 'P' : 'S',
                    ID = s,
                    Prize = Util.GetLotoIDPrizeName(item),
                    Seed0 = $"{os.s0:X16}",
                    Seed1 = $"{os.s1:X16}",
                });


                outer.Next();
            }
            return frames;
        });
    }
}
