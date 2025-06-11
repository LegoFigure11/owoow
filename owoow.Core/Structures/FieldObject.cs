using PKHeX.Core;
using static System.Buffers.Binary.BinaryPrimitives;

namespace owoow.Core.Structures;

// See also: https://github.com/LegoFigure11/owoow/issues/10
public class FieldObject(byte[] data, MyStatus8 myStatus)
{
    public const byte SIZE = 0xC0;

    public readonly byte[] Data = data;

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

    // private ulong Hash => ReadUInt64LittleEndian(Data.AsSpan()[..]);

    public float X => ReadSingleLittleEndian(Data.AsSpan()[0x10..]);
    public float Z => ReadSingleLittleEndian(Data.AsSpan()[0x14..]);
    public float Y => ReadSingleLittleEndian(Data.AsSpan()[0x18..]);

    public ushort Species => ReadUInt16LittleEndian(Data.AsSpan()[0x40..]);
    private byte Form => Data[0x42];
    private byte Level => Data[0x44];
    private bool IsShiny => Data[0x46] == 1;
    private byte Nature => Data[0x48];
    private byte Gender => Data[0x4A];
    private int AbilityIndex => Data[0x4C] - 1;
    private bool HasHeldItem => Data[0x4E] == 1;
    private byte HeldItem => Data[0x50];
    private byte GuaranteedIVs => Data[0x52];
    public bool IsFollowingPokemon => Data[0x54] == 0;
    private byte Mark => Data[0x56];
    private bool HasMark => Mark != 0xFF;
    public uint FixedSeed => ReadUInt32LittleEndian(Data.AsSpan()[0x58..]);
    // private uint BrilliantIndex => ReadUInt32LittleEndian(Data.AsSpan()[0x60..]);

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
}

