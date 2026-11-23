using PKHeX.Core;
using static System.Buffers.Binary.BinaryPrimitives;

namespace owoow.Core.Structures;

public class LiveSpawn(byte[] data, MyStatus8 myStatus, GameStrings strings)
{
    public const byte SIZE = 0x54;

    public readonly byte[] Data = data;
    public string Identifier => GetIdentifier();

    private ushort TID => myStatus.TID16;
    private ushort SID => myStatus.SID16;
    private GameVersion Version => (GameVersion)myStatus.Game;

    public PK8? PK8 => GeneratePK8();

    public bool IsPokemon =>
        PK8 is not null &&
        Species is > 0 and <= (ushort)PKHeX.Core.Species.Zarude &&
        Level <= 100 &&
        GuaranteedIVs <= 6 &&
        AbilityIndex <= 2 &&
        Nature < (byte)PKHeX.Core.Nature.Random;


    public ushort Species => ReadUInt16LittleEndian(Data.AsSpan()[..]);
    private byte Form => Data[0x04];
    public byte Level => Data[0x06];
    private bool IsShiny => Data[0x08] == 1;
    private byte Nature => Data[0x0C];
    private byte Gender => Data[0x10];
    private int AbilityIndex => Data[0x14] - 1;
    private bool HasHeldItem => Data[0x18] == 1;
    private ushort HeldItem => ReadUInt16LittleEndian(Data.AsSpan()[0x1C..]);
    // public ushort EggMove => ReadUInt16LittleEndian(Data.AsSpan()[0x20..]);
    private byte GuaranteedIVs => Data[0x35];
    public byte Mark => Data[0x48];
    public bool HasMark => Mark != 0xFF;
    // public bool IsBrilliant => Data[0x4a] == 1;
    public uint FixedSeed => ReadUInt32LittleEndian(Data.AsSpan()[0x50..]);

    private PK8? GeneratePK8()
    {
        try
        {

            PK8 pk8 = new()
            {
                Species = Species,
                Form = Form,
                CurrentLevel = Level,
                Nature = (Nature)Nature,
                Gender = (byte)(Gender == 1 ? 0 : 1),
                TID16 = TID,
                SID16 = SID,
                Version = Version,
            };

            pk8.SetNature((Nature)Nature);
            pk8.SetAbilityIndex(AbilityIndex);
            if (HasMark) pk8.SetRibbonIndex((RibbonIndex)Mark);

            // Correct pk.Gender for single-gender species since data may not match.
            var gr = PersonalTable.SWSH[pk8.Species].Gender;
            pk8.Gender = gr switch
            {
                PersonalInfo.RatioMagicMale       => 0,
                PersonalInfo.RatioMagicFemale     => 1,
                PersonalInfo.RatioMagicGenderless => 2,
                _                                 => pk8.Gender,
            };
            if (!pk8.IsGenderValid()) pk8.Gender = 2;

            if (HasHeldItem) pk8.HeldItem = HeldItem;

            var rng = new Xoroshiro128Plus(FixedSeed);
            pk8.EncryptionConstant = RNG.Generators.Fixed.GenerateEC(ref rng);
            pk8.PID = RNG.Generators.Fixed.GeneratePID(ref rng, IsShiny, RNG.Util.GetShinyValue(TID, SID));

            var (_, ivs) = RNG.Generators.Fixed.GenerateIVs(ref rng, GuaranteedIVs);
            pk8.IV_HP = ivs[0];
            pk8.IV_ATK = ivs[1];
            pk8.IV_DEF = ivs[2];
            pk8.IV_SPA = ivs[3];
            pk8.IV_SPD = ivs[4];
            pk8.IV_SPE = ivs[5];

            var height = RNG.Generators.Fixed.GenerateHeightWeightScale(ref rng);
            pk8.HeightScalar = (byte)height;

            return pk8;
        }
        catch
        {
            return null;
        }
    }

    private string GetIdentifier()
    {
        try
        {
            var dex = $"{PK8!.Species:D4} | ";
            var species = strings.Species[PK8.Species];
            var form = PK8.Form != 0 ? $"-{PK8.Form}" : string.Empty;
            var gender = PK8.Gender switch
            {
                0 => " (M)",
                1 => " (F)",
                _ => string.Empty,
            };
            return $"{dex}{species}{form}{gender} - {PK8.EncryptionConstant:X8}";
        }
        catch
        {
            return string.Empty;
        }
    }
}

