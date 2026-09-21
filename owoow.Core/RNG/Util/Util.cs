using System.Numerics;
using owoow.Core.Enums;
using PKHeX.Core;
using static owoow.Core.Encounters;

namespace owoow.Core.RNG;

public static class Util
{
    public static readonly IReadOnlyList<string> Natures = GameInfo.GetStrings("en").Natures;

    public const uint MAX_TRACKED_ADVANCES = 50_000; // 50,000 chosen arbitrarily to prevent an infinite loop

    public const ulong XOROSHIRO_CONST = 0x82A2B175229D6A5B;

    public static uint GetAdvancesPassed(ulong s0, ulong s1, ulong _s0, ulong _s1, ulong limit = MAX_TRACKED_ADVANCES)
    {
        if (s0 == _s0 && s1 == _s1) return 0;
        var rng = new Xoroshiro128Plus(s0, s1);
        uint i = 0;
        do
        {
            i++;
            rng.Next();

            var (cur0, cur1) = rng.GetState();
            if (cur0 == _s0 && cur1 == _s1) break;

        } while (i < limit);

        return i;
    }

    // https://github.com/StarfBerry/PokeRNG/blob/409e2e3ce21d04184faf3b001da2bb002a8282c9/RNG/Xoroshiro.py#L110
    // Characteristic Polynomial: 0x10008828e513b43d5095b8f76579aa001
    private static readonly UInt128[] XoroshiroJumpTable = [
        new(0x0000000000000000, 0x0000000000000002), new(0x0000000000000000, 0x0000000000000004), new(0x0000000000000000, 0x0000000000000010),
        new(0x0000000000000000, 0x0000000000000100), new(0x0000000000000000, 0x0000000000010000), new(0x0000000000000000, 0x0000000100000000),
        new(0x0000000000000001, 0x0000000000000000), new(0x0008828e513b43d5, 0x095b8f76579aa001), new(0x7a8ff5b1c465a931, 0x162ad6ec01b26eae),
        new(0xb18b0d36cd81a8f5, 0xb4fbaa5c54ee8b8f), new(0x23ac5e0ba1cecb29, 0x1207a1706bebb202), new(0xbb18e9c8d463bb1b, 0x2c88ef71166bc53d),
        new(0xe3fbe606ef4e8e09, 0xc3865bb154e9be10), new(0x28faaaebb31ee2db, 0x1a9fc99fa7818274), new(0x30a7c4eef203c7eb, 0x588abd4c2ce2ba80),
        new(0xa425003f3220a91d, 0x9c90debc053e8cef), new(0x81e1dd96586cf985, 0xb82ca99a09a4e71e), new(0x4f7fd3dfbb820bfb, 0x35d69e118698a31d),
        new(0xfee2760ef3a900b3, 0x49613606c466efd3), new(0xf0df0531f434c57d, 0xbd031d011900a9e5), new(0x442576715266740c, 0x235e761b3b378590),
        new(0x1e8bae8f680d2b35, 0x3710a7ae7945df77), new(0xfd7027fe6d2f6764, 0x75d8e7dbceda609c), new(0x28eff231ad438124, 0xde2cba60cd3332b5),
        new(0x1808760d0a0909a1, 0x377e64c4e80a06fa), new(0xb9a362fafedfe9d2, 0x0cf0a2225da7fb95), new(0xf57881ab117349fd, 0x2bab58a3cadfc0a3),
        new(0x849272241425c996, 0x8d51ecdb9ed82455), new(0xf1ccb8898cbc07cd, 0x521b29d0a57326c1), new(0x61179e44214caafa, 0xfbe65017abec72dd),
        new(0xd9aa6b1e93fbb6e4, 0x6c446b9bc95c267b), new(0x86e3772194563f6d, 0x64f80248d23655c6), new(0xd4e95eef9edbdbc6, 0xfad843622b252c78),
        new(0x05667023c584a68a, 0x598742bbfddde630), new(0x401aacf87a5e21ee, 0x3a9d7dce072134a6), new(0xe114b1e65a950e43, 0xf0cc32eaf522f0e0),
        new(0x905dff85834fb8d1, 0xeb2beaa80d3fd8a7), new(0xc449c069734817cb, 0x61f29536e1bb6b99), new(0x1e5bc0fe7032f3df, 0x390cd235d35187da),
        new(0x3f399e6f1ea22dbc, 0x744e5f1168ba3345), new(0xd47a02636f041cca, 0x8cc9aa88a153f5f8), new(0xf83c06b106d3b7ab, 0x08d037056c80b9e0),
        new(0x14223eedae116a83, 0x4ce3c123d196bf7a), new(0x24bfd164204335ae, 0xb1b206870da4e89a), new(0x4a5953c8f4bc2a51, 0x207bb2453717cf67),
        new(0xf6b3f196dc551ccf, 0xa14e342bb11ff7e6), new(0x5b6233b76fa214d7, 0x5422bca5015dd3b7), new(0xf20d7136458bd924, 0xede7341c00c65b85),
        new(0x9b19ba6b3752065a, 0xd769cfc9028deb78), new(0x4f27796502238c48, 0xc7b0e531abe7e4bd), new(0xb7b17dcd25003305, 0x1c6d3ba4bb94182a),
        new(0xaaae579366147d07, 0x3ae9471d0e2d0bcf), new(0x0d56bb288c661ccf, 0x8f9cd3794ca46fbf), new(0x0402342eedff424c, 0xdb2ad4e9c15a9d4e),
        new(0x4e71559e6d0e7f00, 0x79e061af5be21395), new(0x8367af1c9d6c1406, 0x96e7d88c0794e785), new(0x0dbfcd2453d1d33f, 0xccdda809db64b3e7),
        new(0x3309e57f180d4ff6, 0x6c64681c21cd0286), new(0xb439f330ab3b9715, 0xacb8d4c6ba67113e), new(0xc58f079d0205bcf3, 0xbad04ca5d96e2cd3),
        new(0x09417d8c80a37aa7, 0xebfbc2723a906760), new(0x52f51ac639e09712, 0x38ac01316167183d), new(0xf37ead6ea53b96ba, 0x7a134006d4efa484),
        new(0xdc1c01799cb8d734, 0x351561e58f8572d4), new(0x170865df4b3201fc, 0xdf900294d8f554a5), new(0xb2a7b279a8cb1f50, 0x2992ead4972eaed2),
        new(0xe7859c665be57882, 0xc026a7d9e04a7700), new(0x4b4a7aa8c389701c, 0xb4cb6197dea2b1fe), new(0xadb7753d55646eef, 0x0dcfc5b909e7df4d),
        new(0xc80926301806a352, 0x468431669864f789), new(0xc05da051ec96af1d, 0x22b6c1736285fcc8), new(0xf88f6bac8fd30448, 0x74c1daac8729d8bb),
        new(0x752b98d002c408f7, 0x847757c126b23e45), new(0x1aa7bc96dbace110, 0x0f9eaa62d0c9e2a3), new(0xc469b29353a4984b, 0x7475d71b98314377),
        new(0x4b6dd41bce3bb499, 0xbbb7d266d61c85ea), new(0xe023777e70b3a2f8, 0xc419b3742570e16f), new(0x131e94fb35203d80, 0x2a71db3a3ce8b968),
        new(0x9240c95b1e7fa08b, 0x2897bb8961b4dce9), new(0xb879fca0915f893f, 0xf0fc3553d7881d5f), new(0x2adca86fbefe1366, 0xe754db3fbc7536bc),
        new(0x0a40a688d77855ba, 0x0a9e201adfe7baa9), new(0x17771c905e0775a8, 0x1d0d601e49c35837), new(0x2cf775e419a607e0, 0x9b031395aec7b584),
        new(0x93a7cf27dec9b306, 0x79ead2eeddf66699), new(0x93615189fe85b7d5, 0xe1b9805c107679fc), new(0x466421124b50fbfb, 0x2c3925dcd790e3d6),
        new(0x1cda7bd04e3bb94b, 0xdca9b0fa4e95600e), new(0x5ec431d73bbfe49f, 0xefc7905e1cbb5ffb), new(0x31a1f85fd532f302, 0x854414811d534483),
        new(0xed9b991c09177e2f, 0xadb9ba2958f30b6e), new(0x38d9e87dffdfca70, 0x76f8fdf26b0d1cbb), new(0xd8e9e7254052af4d, 0x51f21cddcebdb8c7),
        new(0x62769780d13fbc08, 0xa03f796efb295305), new(0x66e5456c2eaedbff, 0xff2083f6b19e628a), new(0xace8d6ce8e3fba17, 0x8b2be9cd79734bed),
        new(0xdddf9b1090aa7ac1, 0xd2a98b26625eee7b), new(0x00d67dc46ad28695, 0x4fff128094edd94c), new(0xf9540570703e7cf3, 0x726438e9a1d3c6ea),
        new(0x066a9599766619b5, 0x92cc6a0937c9d34e), new(0xa4e540c7ac49aa1b, 0x05730de058e1047f), new(0xc2edfc1ab51c00ad, 0xe408bbecda066551),
        new(0xf11753a4339e78c3, 0xc5477ea8821ce588), new(0xbb42e906efb12540, 0x3c6058e633063180), new(0x4e86f36c495eeedb, 0xbec40e0518086e21),
        new(0xe8345a7c487fefd6, 0x465276434fd98954), new(0x688b762874221434, 0x3adaea5cdfe12e3b), new(0x833801923a05f253, 0xc9dffa95904e99b1),
        new(0x58a00d23a8086646, 0xa10c3fb0b18df787), new(0xec69708d487dbfc4, 0xa4e41f760281c3d0), new(0x47176f17de7ff0e9, 0xb8880fff0e41261c),
        new(0x4f40c533643920ea, 0x58ee3b30f542767e), new(0x83fd48d6b9620584, 0x15f2d25b60c5acd7), new(0x0ce303c7d3aabbc8, 0xe448c83950a687ea),
        new(0x1746715df0dd8fe3, 0xa6ff7863c363cfd4), new(0xc00185964caef8bb, 0x7e9d8517b195d9c9), new(0xb6bde02bd004b144, 0x40ddb4daf3fbdda8),
        new(0xba43c63ec5a9f187, 0x7a794b820672a49b), new(0x2467071b1d261621, 0xc1be31e7536236fb), new(0x5a6fc0435f011daa, 0xf0eec34daea486fb),
        new(0xa5af34331c044d81, 0xf42c01a2a3815db4), new(0xdb43b553cd16ea44, 0xdf7964c343b312de), new(0x432c2bbcd03e65f6, 0x8454182464c29903),
        new(0xcdf56412d1e7ba6e, 0xb76c0ecc6cb5adbb), new(0xac13c8b2ff838036, 0x380b97764c9f7748), new(0x71d208cc2e5c56e9, 0x1868a9f5a4fd4d64),
        new(0xd1d08a01b73de005, 0xe89f5fe075d74a79), new(0xa9495c12936ad0fd, 0x25aa87f3c2704c69)
    ];

    // Chaacteristic Polynomial calculated from Lincoln-LM's python script with modifications
    // https://discord.com/channels/285269328469950464/551605684815265824/1009051571012767824
    // Jump table generated by StarfBerry's python script script
    // https://github.com/StarfBerry/PokeRNG/blob/f74a5b39de21e7c674eb2260fa8b6817f2bfc189/Math/Computation_GF2.py#L106
    // Characteristic Polynomial: 0x1000ab3d4dde3b5215785b914e2822001
    private static readonly UInt128[] XoroshiroJumpTableBack = [
        new(0x0000000000000000, 0x0000000000000002), new(0x0000000000000000, 0x0000000000000004), new(0x0000000000000000, 0x0000000000000010),
        new(0x0000000000000000, 0x0000000000000100), new(0x0000000000000000, 0x0000000000010000), new(0x0000000000000000, 0x0000000100000000),
        new(0x0000000000000001, 0x0000000000000000), new(0x000ab3d4dde3b521, 0x5785b914e2822001), new(0x713c20d6f62f069d, 0x1be6f9ef0f7c6e72),
        new(0xe1e7124be311e263, 0x81d4d1ad4124ad40), new(0x198183f248c62fe4, 0xd15b0aea27ee0b84), new(0x6640bc58c22e6e09, 0x7fdb253cd3096372),
        new(0xaa9fea82b71aa7a7, 0x750e2b697d980c77), new(0x49f5912c1fbbe16c, 0x11f4dd5dea9de81f), new(0xf7ed93095e928283, 0x59163666b6bbf2f9),
        new(0x40f239dd31d0d8e8, 0xd9d96f69a74992a7), new(0xfd8556c65cddf5c8, 0x99e0a82a95da3ec7), new(0x6e71206db7c16667, 0xd123aa298d21df09),
        new(0xffa952dc24a7658f, 0xcc7253fd4b2ef7bf), new(0x897527b00cc1314c, 0xc60cbd520eb5e2a1), new(0x91624cfcc02afdd6, 0x33d679dada618efe),
        new(0xeae5e2e8eca75ab8, 0x6dd5d8d50a429db0), new(0xa68746dd5dbb25c3, 0x8901afc01ec0ff26), new(0x825acf5ba97363b5, 0x09264d3799225beb),
        new(0xc76532622d5b9762, 0x44db2b454b668ab3), new(0xcda5a8792d8afd1f, 0xb31193f714276910), new(0xda849d662974c84a, 0x6552ecf7859d4f74),
        new(0xa94bf29a7e13e7c2, 0xbbc997d2f0259955), new(0x0ae8d73bb1133dfd, 0x344e4336614b4b7b), new(0x90ecdc7830d08754, 0x9382d5fb077e20d5),
        new(0xf6bb2b9c55d6d638, 0x31888c99eff94254), new(0x9926d3ae212dc932, 0x66a482668e80bde4), new(0xb189246299bb5165, 0xb11cecff5f558438),
        new(0xd7167ac343a3b34f, 0x887a70c100930644), new(0x38098b44b078537b, 0x6ccfd506d591494d), new(0xe7090180ae85b56d, 0x8e5056fd972590ed),
        new(0x7d4021e85c233eea, 0x088281764715e7c6), new(0xba8ad190266695c1, 0xbe898f008f69795e), new(0x528e5128cdabc1ec, 0xb2ba2813e26719ec),
        new(0x86cdf41d512a0407, 0x0384719ba606e358), new(0x0e85cfdf62cd4874, 0x1c21aaf104edb8aa), new(0xcda6ec3a032a1052, 0xf36340ba95f8e193),
        new(0xa6f4763279872361, 0xe43663c17c780509), new(0x4d52ad0c69a0139a, 0x7c9bcaf53f3c4ba4), new(0x1034d520f48190ff, 0x113b6a0e658bac62),
        new(0x97307b6aa6522426, 0x93c6c017455b9486), new(0x1d3568c93025774a, 0xd250d4c573dff2e6), new(0x49b6682fb7c90e70, 0xbe498ff6c91a5227),
        new(0x4e2278414a756f47, 0x79e57bfc77e950d2), new(0x607120b3f2e34c60, 0xb3864ba0b1779e24), new(0x626962c3483e93ea, 0x63f0c3822eed2927),
        new(0x919dafdb2d9fb36f, 0xb13b0a779544e2e9), new(0xd537e0c62e819478, 0x59a7586fc8c97f16), new(0xccb0c187041f7897, 0x36f763923b504f22),
        new(0x2f5e5e56bc8f8a80, 0x5e86974000483c40), new(0xe190886e69191e57, 0x2460c19b5931ec74), new(0xdbabb894530f388a, 0x322067408d606672),
        new(0x1019b8416864dab1, 0xa04cd3e3505e447c), new(0xb2d3cfa401466cea, 0xee0f6bd815d24bfb), new(0x41276e3eee3ee06f, 0xe59618ee55e8590c),
        new(0x64e8390434b2d488, 0xed3c639654e29425), new(0x423ccc1beab36d0e, 0x417fe57f81453a6d), new(0x5330beda050b2058, 0x7744e71a2ae1c50e),
        new(0xcc5e8389e96c1ac0, 0xbdd70b441e3b7eb0), new(0xecfd0b33d43a15b1, 0x8246091c31d42e33), new(0xa6a5fa8a3c1456ca, 0xdd6e4d91388deb45),
        new(0x79a8aa8a4c3aa139, 0x87abd0cde37bd226), new(0xaf6147c362f2f245, 0xb4ba28b499a2ffd7), new(0x10ddef105f9477f2, 0x3bc2c13f42860713),
        new(0x8c6a7bb5c1632570, 0x10de846fa5e8b7c0), new(0x89b870496c5b612f, 0x429b0bc79c27749c), new(0x8db688bfe2925505, 0xf56ccc84b4c8ca59),
        new(0x594e0951ef2a0f2a, 0xa427e6e606854870), new(0xf3fe92bc6ea7fe90, 0x15d15b3b1411a830), new(0x768fb2d68c720827, 0x76316bde4e9256e6),
        new(0x299bbbb60539c9e7, 0x239f2d2b6e101117), new(0x4f21b753e309f4c6, 0x48f56d3fc5784df4), new(0x9d7d8b74dae5528f, 0x44ac0e08daa68a41),
        new(0xf4cef2537e24ccd4, 0xc78d7057e3778a83), new(0xb804e2cc686de888, 0xa5f826c65b5328ee), new(0x3b6a39b34abb772b, 0x49b76cf952f43a10),
        new(0x854c71c5ea55d65f, 0x0f72080d3456dd01), new(0x3d963d6da226c160, 0x58d5b693296cd772), new(0x5e033c93bd62d0f1, 0x20b3f0a1ea230b3e),
        new(0x3dbf26d07c8fbf25, 0xfd14cb6c2e41038a), new(0x943f20973ba4329e, 0xaa31fa65ced42934), new(0x4306ae784e078563, 0xdcf8151d1e6937ec),
        new(0xbe1680d07650285b, 0x7be579a6f1298ad1), new(0xdb5cd5c68d76bdce, 0x0144c4d5539867e3), new(0x5ca2ed5959638efe, 0x420e2e3e40ff1685),
        new(0x310dc4cae47f51d3, 0xd281e85b8fa2f1bd), new(0x92cb40f62801ac2d, 0x7c8a273fcbd4a8af), new(0xa0e4ff793686abaf, 0x155c244e81fe8757),
        new(0x149997e06b132553, 0x4a4ece4496fa9a12), new(0x1f146f8b366eb33a, 0x9e29bc15957255df), new(0xdec16227a5ac81ec, 0x86108bf53d0007b2),
        new(0x5b73d06ec669a030, 0xfab0f11e37e8fdfb), new(0x0ed6eb5237d9e864, 0x687a45188faaf78b), new(0xbe4b5edf04a04574, 0x39b4fa92a82e7a67),
        new(0x89dd1149711728ae, 0x36fd5177b0f43977), new(0x8f3747644d231444, 0x44d8cf0112a222f2), new(0x0fa9d69534988421, 0x04a2397bec96387a),
        new(0x4d1bee763dc01f5e, 0x1c8fa7e58671be1b), new(0x36f595d341052b3b, 0xbcbfb54364e80480), new(0xe45b483ae5eb8ff7, 0x6d313839ff279051),
        new(0xe3fd8d69109f7a4b, 0xef05d8cb74236bb4), new(0x9be48cd6f5d1396d, 0xb5cc978113c57c55), new(0x9e3bc98b3409c37b, 0x1d5b532ec147a477),
        new(0x68f20283565086c7, 0xa80f169913882e63), new(0x59800bf03a6ad936, 0x714b81531bade269), new(0x81828a72557061e8, 0xe131fb5b497db064),
        new(0x9124e4750ceefd94, 0xac8bc8355de6bf54), new(0x92a48f9a5b7ad4d3, 0xc963a9bbde5403cf), new(0xd25c2f10df3ed909, 0x81284f70ddc575a5),
        new(0x97e69bbb887c24b2, 0x69d20cf2d45b055b), new(0xee88c2c954e52509, 0xc4dc846adbe0f32d), new(0x26bb5c47155cf71b, 0xea7ed84cea9e61a3),
        new(0xc2af11a5fc5cf7ae, 0x3ed71f239ac79b72), new(0x017c2f656dfde0ae, 0xac6a767bc7afa300), new(0x1288c23000b0c66b, 0x164158d7c5f464e8),
        new(0xc389cf497b1e3e88, 0x3867d89147164975), new(0xedd312f67f7dae52, 0xd65691186b7bafe8), new(0x4515b301d421f3e5, 0x08ef2a3bb3c14cc5),
        new(0xda3bc24f4f9fd56d, 0xeded8d4d07453bae), new(0x21fa37b3f98ed6a3, 0x836bb083b59aa24d), new(0xf64312b33e0699c8, 0x7d9bdf347d504ecd),
        new(0xebca82807c186580, 0x3c69880ab58eb84e), new(0x19b1cc48e10e4e0a, 0xea78a4e4826002b5)
    ];

    // https://github.com/StarfBerry/PokeRNG/blob/409e2e3ce21d04184faf3b001da2bb002a8282c9/RNG/Xoroshiro.py#L69
    private static (ulong s0, ulong s1) XoroshiroJumpPow2(ulong _s0, ulong _s1, byte jump)
    {
        ulong s0 = 0;
        ulong s1 = 0;
        var rng = new Xoroshiro128Plus(_s0, _s1);
        var polynomial = XoroshiroJumpTable[jump];

        while (polynomial > 0)
        {
            if ((polynomial & 1) != 0)
            {
                var (state0, state1) = rng.GetState();
                s0 ^= state0;
                s1 ^= state1;
            }

            rng.Next();
            polynomial >>= 1;
        }

        return (s0, s1);
    }

    private static (ulong s0, ulong s1) XoroshiroJumpPow2Back(ulong _s0, ulong _s1, byte jump)
    {
        ulong s0 = 0;
        ulong s1 = 0;
        var rng = new Xoroshiro128Plus(_s0, _s1);
        var polynomial = XoroshiroJumpTableBack[jump];

        while (polynomial > 0)
        {
            if ((polynomial & 1) != 0)
            {
                var (state0, state1) = rng.GetState();
                s0 ^= state0;
                s1 ^= state1;
            }

            rng.Prev();
            polynomial >>= 1;
        }

        return (s0, s1);
    }

    // https://github.com/StarfBerry/PokeRNG/blob/409e2e3ce21d04184faf3b001da2bb002a8282c9/RNG/Xoroshiro.py#L84
    // Modified from LZC to TZC as suggested by @Lincoln-LM
    // https://discord.com/channels/1375066006279553145/1534407306538782913/1535036408039866580
    public static (ulong s0, ulong s1) XoroshiroJump(ulong _s0, ulong _s1, ulong jump)
    {
        var rng = new Xoroshiro128Plus(_s0, _s1);
        var adv = (byte)(jump & 0x7f);
        for (var i = 0; i < adv; i++) rng.Next();
        var (state0, state1) = rng.GetState();
        jump ^= adv;

        while (jump != 0)
        {
            var i = (byte)BitOperations.TrailingZeroCount(jump);
            (state0, state1) = XoroshiroJumpPow2(state0, state1, i);
            jump ^= 1UL << i;
        }

        return (state0, state1);
    }

    public static (ulong s0, ulong s1) XoroshiroJumpBack(ulong _s0, ulong _s1, ulong jump)
    {
        var rng = new Xoroshiro128Plus(_s0, _s1);
        var adv = (byte)(jump & 0x7f);
        for (var i = 0; i < adv; i++) rng.Prev();
        var (state0, state1) = rng.GetState();
        jump ^= adv;

        while (jump != 0)
        {
            var i = (byte)BitOperations.TrailingZeroCount(jump);
            (state0, state1) = XoroshiroJumpPow2Back(state0, state1, i);
            jump ^= 1UL << i;
        }

        return (state0, state1);
    }

    public static (uint threshold, int rolls) GetBrilliantInfo(int KOs) => KOs switch
    {
        >= 500 => (30, 6),
        >= 300 => (30, 5),
        >= 200 => (30, 4),
        >= 100 => (30, 3),
        >= 50 => (25, 2),
        >= 20 => (20, 1),
        >= 1 => (15, 1),
        _ => (0, 0),
    };

    public static string GetTypePullingLeadAbilityType(string ability) => ability switch
    {
        "Magnet Pull" => "Steel",
        "Lightning Rod" or "Static" => "Electric",
        "Flash Fire" => "Fire",
        "Storm Drain" => "Water",
        "Harvest" => "Grass",
        _ => string.Empty,
    };

    public static uint GetHiddenEncounterModifiedRate(uint step, AbilityType ability)
    {
        var rate = Math.Min((step + 1) * 22, 100);
        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (ability)
        {
            case AbilityType.DecreaseEncounterRate:
                rate >>= 3;
                break;

            case AbilityType.IncreaseEncounterRate:
                rate <<= 1;
                break;
        }
        return Math.Min(rate, 100);
    }

    public static uint GetShinyValue(uint x, uint y) => x ^ y;
    public static uint GetShinyValue(uint x) => (x >> 16) ^ (x & 0xFFFF);

    public static uint GetShinyXOR(uint pid, uint tsv) => GetShinyValue(GetShinyValue(pid), tsv);

    public static string GetShinyType(uint xor) => xor switch
    {
        0 => "Square",
        < 16 => $"Star ({xor})",
        _ => "No",
    };

    public static string GetHeightString(uint height) => height switch
    {
        >= 255 => $"XXXL ({height})",
        >= 231 => $"XXL ({height})",
        >= 196 => $"XL ({height})",
        >= 156 => $"L ({height})",
        >= 100 => $"M ({height})",
        >= 60 => $"S ({height})",
        >= 25 => $"XS ({height})",
        >= 1 => $"XXS ({height})",
        _ => $"XXXS ({height})",
    };

    public static string GetRibbonName(RibbonIndex rib) => rib switch
    {
        RibbonIndex.MAX_COUNT => "None",
        RibbonIndex.MarkLunchtime => "Time",
        RibbonIndex.MarkCloudy => "Weather",
        _ => rib.ToString().Replace("Mark", string.Empty),
    };

    public static WeatherType GetWeatherType(string weather) => weather switch
    {
        "Normal Weather" => WeatherType.NormalWeather,
        "Overcast" => WeatherType.Overcast,
        "Raining" => WeatherType.Raining,
        "Thunderstorm" => WeatherType.Thunderstorm,
        "Intense Sun" => WeatherType.IntenseSun,
        "Snowing" => WeatherType.Snowing,
        "Snowstorm" => WeatherType.Snowstorm,
        "Heavy Fog" => WeatherType.HeavyFog,
        _ => WeatherType.AllWeather,
    };

    public static IVSearchType GetIVSearchType(string labelText) =>
        labelText == "||" ? IVSearchType.Or : IVSearchType.Range;

    public static string GetLotoIDPrizeName(LotoIDTargetType item) => item switch
    {
        LotoIDTargetType.MasterBall => "Master Ball",
        LotoIDTargetType.RareCandy => "Rare Candy",
        LotoIDTargetType.PPMax => "PP Max",
        LotoIDTargetType.PPUp => "PP Up",
        LotoIDTargetType.MoomooMilk => "Moomoo Milk",
        _ => "None",
    };

    public static LotoIDTargetType GetLotoIDTargetType(int selected) => (LotoIDTargetType)selected;
    public static CramomaticTargetType GetCramomaticTargetType(int selected) => (CramomaticTargetType)selected;
    public static CramomaticInputItemType GetCramomaticInputItemType(int selected) => (CramomaticInputItemType)selected;
    public static SuccessType GetSuccessType(int selected) => (SuccessType)selected;

    public static short GetDexRecommendation(string species)
    {
        if (Personal is not null)
        {
            try
            {
                if (Personal[species] is not null) return Personal[species].DevId;
            }
            catch { return 0; } // (None) selected, or user entered garbage
        }
        return 0;
    }

    public static string GetDexRecommendation(ushort species)
    {
        if (Personal is not null)
        {
            foreach (var (k, v) in Personal)
            {
                if (v.DevId == species) return k;
            }
        }
        return "(None)";
    }
}
