using PKHeX.Core;
using static owoow.Core.RNG.MatrixUtil;

namespace owoow.Core.RNG.Generators.Misc;

public static class SeedFinder
{
    private static readonly ulong[,] ObservationsToState = {
        { 0xBC3A7223E4917777, 0x7E20DC0C3A48212E }, { 0x8071979CE140DB91, 0xE1A3D69592B1DD71 }, { 0xF9C525A20F967A70, 0x5550AB49809B64C4 }, { 0x66FF2A6EE7DC2EA8, 0x2AFBF148C576ABF2 },
        { 0x01F122958B0E66A9, 0x47DA97CA1A59B923 }, { 0x8EB2179CEAF56245, 0xDE60DA55F0CBB6FB }, { 0xD464977C8394D832, 0x7BE2CACB5D67DEE0 }, { 0x1DCE64B078AA5496, 0x47255A477B7542C0 },
        { 0x417E448C03CC9F50, 0x7AA5EE9A01F5B582 }, { 0x6DB0243CDDFAF53F, 0xBDBF8990CE2FDA4B }, { 0x5B0973951307037C, 0xDD780D474EEAFF30 }, { 0x86C3163687A28755, 0xBCB441CF0149473F },
        { 0x49255BB4F0A1EAD8, 0x312F118D0D4448D6 }, { 0x18A82620ED590C4E, 0xB2D8AA498209229B }, { 0x9DC91C1DACB2A2F5, 0xD1D95509712467A7 }, { 0x28522FD69990D92F, 0xA1C1A2C479C745DE },
        { 0x88D43931B2E0F661, 0xAB1A031F47EEFB77 }, { 0x1C1C0B26D462C5A8, 0x2439D2D2B7E343E9 }, { 0xD50B3E93BC581853, 0x40B4248CF579D6C5 }, { 0xC583055FF24B5740, 0x603265C60697A03D },
        { 0xD8FE1C6B52C228FD, 0x2AD00F45671AED8A }, { 0xEEA88724AA9A5183, 0xC5905048FBCCD1F9 }, { 0x34D301CFF92F87E6, 0x730D674037030564 }, { 0x31D6AE0EA4AB0BC6, 0xF274180A59EA36A5 },
        { 0x4491741148415F5C, 0xD4448B8BB90BA9C5 }, { 0x03417267F3968A35, 0x9C93810603EDF830 }, { 0xC62C4F7F7B08359C, 0x34298E912135FFCD }, { 0xDD79E8E0DB89205E, 0x5470471EB2286C2A },
        { 0xAE9814486D68557C, 0x9913F906E112811D }, { 0x68CAB40D986C881E, 0x5FC2200574A417D6 }, { 0x2EA36C4035B73A4C, 0xC71CE2D159907D15 }, { 0x99DD5A278741907C, 0x1311D9CF2979776A },
        { 0x171EB22B2DC10459, 0x6E5579907D598A97 }, { 0xC395E77B44547A4A, 0x8265850021FF87DF }, { 0xD36C101D407621E3, 0x1079420C62BC5B92 }, { 0xE5EBF352A562421A, 0x86FE118E35E1880A },
        { 0x6588E4BBE03A5276, 0xD63D46D46AD4F4A3 }, { 0x018232429FCCD863, 0x60CE3BC85FA4640C }, { 0xA893CF43BBE313FA, 0x2874424C063C347D }, { 0xB2B1E82901C7A3F0, 0xD280098AE88401E2 },
        { 0xA399CEE61E4FC560, 0x36C740C6D6029375 }, { 0x43C514A256E2BCFA, 0xB76FAE16218DD0A5 }, { 0x95FBF8BFA8C6EB71, 0xE6ED38155BA4C058 }, { 0x8DB8C33AC088FD71, 0xE57F4B17D966C2E8 },
        { 0xFB07290564B2067D, 0x45CC1F122399FC25 }, { 0x646685A5FACCCA2E, 0xF57A4A44D0E2AAD6 }, { 0x35187C57B727589F, 0x139F6901008C8ACD }, { 0x4F99A57AAEE1EA53, 0x85876F0318A44742 },
        { 0x09EB595682021BF6, 0xD334D54348F776D7 }, { 0x8D872E8BFECD0EED, 0x55DA3B129E8DFE1E }, { 0xBBDF357683B3D5DD, 0x2F2B5A86711D0405 }, { 0xA2B57B4393655887, 0x53273D4C64C5540A },
        { 0xAA5B1FC8D7920D8F, 0x0B679B475BD2623C }, { 0x13315503946C820B, 0x29EFC6127437BED1 }, { 0xAFC6491E82F0CD14, 0xFE7D5AC7D566E57D }, { 0xD527C2A64691C687, 0xEB8CF58DA9622C51 },
        { 0xDD9B598AEACDEFD9, 0x72CAE6C7BB48DE2C }, { 0x6DB8EFA6B07CEDDE, 0x5062A957D3EABAE8 }, { 0xB5F89486F0F8F2E7, 0x0B67C151B27BE7AD }, { 0x15F69FC4B2B297D1, 0x5A7FEED60430FF82 },
        { 0x1768A7222B0AEB5F, 0xA74920DE39D91429 }, { 0x36A2592E0722B92F, 0xC76C918F4A18F029 }, { 0x95DDA38B73D0942D, 0xEF8D640A3DEBF0A1 }, { 0x8CFF0D9BB7F37217, 0xDD10521EB88F1937 },
        { 0x37CAF522165ACDFC, 0x001423C4766B1935 }, { 0x446CE24CB005EB1F, 0x67DFBA8518054A01 }, { 0x532CB96518525531, 0xB9F63D1ACDB5FEBC }, { 0x75E4D92AB4F35F4C, 0xA38976DF513CEEA4 },
        { 0xE52E884BFC8A34AD, 0xDDBDC111074F2942 }, { 0x6FDAA5145EE7EC04, 0xE7E0DC5E480A020C }, { 0x86D094EE6E034C48, 0xB68A6AD1D7E51CF3 }, { 0x5D256C036CB0CAD7, 0x1180EACB2FDDCFD8 },
        { 0xD6F7F3C9C1D386DC, 0x4D93B398DF86D7D3 }, { 0x9F21320551CE6CB1, 0x9BC7AA94157BA774 }, { 0x4451E5E2B14BE0C8, 0xE4FA38D91595DD9C }, { 0xAA310E2211AC2987, 0x629DA0CEF68BF841 },
        { 0xBEC48182475AF4F7, 0xC970DE404C4E19F1 }, { 0xE37205CC697F064F, 0xE1107503E73480BD }, { 0x63BF957BAADFB672, 0xBE9C9F519B0E6383 }, { 0xD152889C7BFB9353, 0x5D8AFB80660F77A9 },
        { 0x77FEFD8A3B28BA11, 0xB6D42818AF1DBB94 }, { 0x6221237CBE249748, 0x6543608FC54B6051 }, { 0x0A8C4C4709F3542E, 0x74A6174582D94F2A }, { 0x505EE6CF0744EC1D, 0xBE5282998A388AFE },
        { 0x1E427B7BBC461D11, 0x10091103976DDF5C }, { 0xF26FEB4FE7647663, 0x2CCE2B9AD93D68B2 }, { 0x5E073FCFE3916C07, 0x88CED48A72F71D5B }, { 0x731AA3B57A5B6349, 0x208D1B5FA61D15ED },
        { 0x3931E14561B3690C, 0x061BA7C5D66E578F }, { 0xBDB432653EEBC9C4, 0x930740D946DB14FA }, { 0x75F3C2AF253E5AD0, 0xBFC51A08F1A660D6 }, { 0x10720001784457B8, 0x57FF3307CDBA7F68 },
        { 0xDF9452DE000032B4, 0x23E32F0A9C669D75 }, { 0x672BCCBF9CCF2AB0, 0x33C8F85435D033C8 }, { 0x2F570064FF9DC8ED, 0x4CE32FD560DAD976 }, { 0x84D193529A64C854, 0x2FD745992803D12A },
        { 0x3672785318DCA571, 0xFF3266DA49A21F5D }, { 0xAF0C35D721162713, 0xBF5B8B021036E27C }, { 0x08C1392B7AF6496F, 0xDA5A201741D5C36E }, { 0x98A21D3F307095AA, 0xE0B61D0B987B12F3 },
        { 0x9D25421CF65AAD1F, 0xC1E69A8B695F1DF7 }, { 0x8199CB3E8004FC7C, 0xDAD333D13E0BC8A8 }, { 0x4C65E2562CD3AD5F, 0xC595BC136A0534C1 }, { 0xA33ACDDD7754ABF8, 0xFA704A54A7A15041 },
        { 0xD529AA0C85C88CFA, 0x7014A08E8A1B0EA8 }, { 0xF0138C5AE8A08E14, 0x4659FA0075F97D49 }, { 0x4241C438560D826D, 0x41EE0FD21C3F4DBD }, { 0x7CF21328CDD45ACC, 0xEC13E94527DB2AB2 },
        { 0xFEC5868C995D1DC5, 0xD7FB5FD90E3D828F }, { 0x1CC73BDAA014273D, 0xD4B6D7CEDEABFDC3 }, { 0x0F918335DC7A9108, 0x7964DCDBC2DF27B6 }, { 0xEB8D9C0B5E58B43E, 0xF781CB19334AC481 },
        { 0xBCBA667433C5935F, 0x752030503C799D07 }, { 0xEE92DDF1B1913509, 0x6F7111198A38348B }, { 0x62BD81E367472E62, 0xAEEC40473C3CA6EA }, { 0x8E76129D8FA0D427, 0x758D5985040EE07C },
        { 0xC0BEEBD155863206, 0xE89E9CD2B62A58CA }, { 0x24C6E007C9EE53F6, 0xB177B2C19EAAA710 }, { 0xF3EE607D0B30B4F1, 0xFF1B35458EBB0967 }, { 0x626D5B07D087CEED, 0x4E18834CEF7F1747 },
        { 0xED0AF7FDAC99CD2E, 0x8A710F4796E90934 }, { 0xB0455499F41BE780, 0x8CF21CD050486FFA }, { 0x9A776671B8C381B0, 0x18F5250E6CA41156 }, { 0x2C2898C998F63CA1, 0x5F4DB84328319589 },
        { 0xCE5163B9C55E9FBF, 0xD710EF158FA4F27D }, { 0x9E18C745BFAA92AC, 0x3265E0D6511215D4 }, { 0x3FBDAABC3EF67831, 0x8D353A129F655737 }, { 0x4FD1E181FF3C6C2B, 0xDB2ACB384355C5FB },
    };

    public static (ulong, ulong) CalculateRetailSeed(string animations)
    {
        ulong state0 = 0;
        ulong state1 = 0;
        for (var i = 0; i < 128; i++)
        {
            if (animations[i] == '1')
            {
                state0 ^= ObservationsToState[i, 0];
                state1 ^= ObservationsToState[i, 1];
            }
        }
        return (state0, state1);
    }

    public static (byte[] sequence, ulong s0, ulong s1) GenerateAnimationSequence(ulong s0, ulong s1, ulong ini, ulong adv)
    {
        var res = new byte[adv];
        var rng = new Xoroshiro128Plus(s0, s1);
        for (ulong i = 0; i < ini; i++) rng.Next();
        for (ulong i = 0; i < adv; i++)
        {
            res[i] = (byte)(rng.Next() & 1);
        }
        return (res, s0, s1);
    }

    public static (int hits, int advances, ulong s0, ulong s1) ReidentifySeed(byte[] sequence, ulong s0, ulong s1, string pattern)
    {
        var hits = 0;
        var pos = 0;

        if (pattern.Length > 5)
        {
            for (var i = 0; i < sequence.Length - pattern.Length; i++)
            {
                if (sequence[i] == pattern[0] - '0')
                {
                    for (var j = 1; j < pattern.Length; j++)
                    {
                        if (sequence[i + j] != pattern[j] - '0') break;
                        if (j == pattern.Length - 1)
                        {
                            hits++;
                            pos = i + j;
                            i += j;
                        }
                    }
                }
            }
        }

        if (hits == 1) (s0, s1) = Util.XoroshiroJump(s0, s1, (ulong)pos);

        return (hits, pos + 1, s0, s1);
    }

    // Adapted from https://github.com/lincoln-lm/swsh-initial-seed/blob/main/main.py
    // I have no idea how this works but Lincoln can probably explain it if needed
    // See also: https://github.com/LegoFigure11/owoow/issues/2
    public static List<(ulong s0, ulong s1)> GetInitialSeedFromRange(int min, int max, byte[] observations)
    {
        // Min/Max validation already handled by caller method

        var len = observations.Length;

        var fullRange = max - min + 1 + 128;

        var jump = Util.XoroshiroJump(0, Util.XOROSHIRO_CONST, (ulong)min);
        var rng = new Xoroshiro128Plus(jump.s0, jump.s1);

        var constObservations = new byte[fullRange];
        for (var i = 0; i < fullRange; i++)
        {
            constObservations[i] = (byte)rng.NextInt(2);
        }

        var allSeedToObservations = new byte[ 64 , fullRange ];
        for (var bit = 0; bit < 64; bit++)
        {
            jump = Util.XoroshiroJump((ulong)1 << bit, 0, (ulong)min);
            var rng2 = new Xoroshiro128Plus(jump.s0, jump.s1);
            for (var i = 0; i < fullRange; i++)
            {
                allSeedToObservations[bit, i] = (byte)rng2.NextInt(2);
            }
        }

        List<(ulong, ulong)> results = [];
        for (var advance = min; advance < max + 1; advance++)
        {
            var offset = advance - min;
            var seedToObservations = new byte[64, len];
            for (var i = 0; i < 64; i++)
            {
                for (var j = 0; j < len; j++)
                {
                    seedToObservations[i, j] = allSeedToObservations[i, offset + j];
                }
            }

            var (observationsToSeed, nullBasis) = GeneralizedInverse(seedToObservations);

            var diff = new byte[len];
            for (var i = 0; i < len; i++)
            {
                diff[i] = (byte)(observations[i] ^ constObservations[offset + i]);
            }

            var cols = observationsToSeed.GetLength(1);
            var principle = new int[cols];
            for (var j = 0; j < cols; j++)
            {
                var sum = 0;
                for (var i = 0; i < diff.Length; i++) sum ^= diff[i] & observationsToSeed[i, j];
                principle[j] = sum;
            }

            var nullBasisRows = nullBasis.GetLength(0);
            var nullBasisCols = nullBasis.GetLength(1);
            for (var key = 0; key < (1 << nullBasisRows); key++)
            {
                var keyBits = IntToBitVector(key, nullBasisRows);

                var candidate = new byte[nullBasisCols];

                for (var j = 0; j < nullBasisCols; j++)
                {
                    var sum = 0;
                    for (var i = 0; i < nullBasisRows; i++)
                    {
                        sum += (byte)(keyBits[i] * nullBasis[i, j]);
                    }

                    candidate[j] = (byte)(principle[j] ^ (sum & 1));
                }

                var initialSeed = BitVectorToInt(candidate);

                jump = Util.XoroshiroJump(initialSeed, Util.XOROSHIRO_CONST, (ulong)advance);
                var rng3 = new Xoroshiro128Plus(jump.s0, jump.s1);
                var matches = true;
                for (var i = 0; i < len; i++)
                {
                    if ((rng3.Next() & 1) == observations[i]) continue;
                    matches = false;
                    break;
                }
                if (matches)
                {
                    results.Add(Util.XoroshiroJump(jump.s0, jump.s1, (ulong)len));
                }
            }
        }

        return results;
    }
}
