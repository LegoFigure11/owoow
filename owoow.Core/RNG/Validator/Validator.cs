﻿using owoow.Core.Enums;
using PKHeX.Core;

namespace owoow.Core.RNG.Validator
{
    public static class Validator
    {
        public static bool CheckIsAura(bool aura, AuraType target) => target switch
        {
            AuraType.None => !aura,
            AuraType.Brilliant => aura,
            _ => true,
        };

        public static bool CheckNature(string nature, Nature target) => nature == "Sync" || target == Nature.Random || nature == Util.Natures[(int)target];

        public static bool CheckEC(uint ec, bool rare) => rare && ec % 100 == 0;

        public static bool CheckIsShiny(uint xor, ShinyType target) => target switch
        {
            ShinyType.Either => xor < 16,
            ShinyType.Square => xor == 0,
            ShinyType.Star => xor > 0 && xor < 16,
            ShinyType.None => xor >= 16,
            _ => true,
        };
    }
}
