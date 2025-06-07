using PKHeX.Core;
using static System.Buffers.Binary.BinaryPrimitives;

namespace owoow.Core.Structures;

public class OverworldPokemon(byte[] data, MyStatus8 myStatus)
{
    public const byte SIZE = 0x68;

    public readonly byte[] Data = data;

    private ushort TID => myStatus.TID16;
    private ushort SID => myStatus.SID16;

    public float X => ReadSingleLittleEndian(Data.AsSpan()[..]);
    public float Y => ReadSingleLittleEndian(Data.AsSpan()[0x04..]);
    public float Z => ReadSingleLittleEndian(Data.AsSpan()[0x08..]);

    public PK8 PK8 => GeneratePK8();

    private ushort Species => ReadUInt16LittleEndian(Data.AsSpan()[0x30..]);
    private byte Form => Data[0x32];
    private byte Level => Data[0x34];
    private bool IsShiny => Data[0x36] == 1;
    private byte Nature => Data[0x38];
    private byte Gender => Data[0x3A];
    private int AbilityIndex => Data[0x3C] - 1;
    private bool HasHeldItem => Data[0x3E] == 1;
    private byte HeldItem => Data[0x40];
    private byte GuaranteedIVs => Data[0x42];
    private byte Mark => Data[0x46];
    private bool HasMark => Mark != 0xFF;
    public uint Seed => ReadUInt32LittleEndian(Data.AsSpan()[0x48..]);

    private PK8 GeneratePK8()
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
            Version = (GameVersion)44,
        };

        pk8.SetNature((Nature)Nature);
        pk8.SetAbilityIndex(AbilityIndex);
        if (HasMark) pk8.SetRibbonIndex((RibbonIndex)Mark);
        if (!pk8.IsGenderValid()) pk8.Gender = 2;
        if (HasHeldItem) pk8.HeldItem = HeldItem;

        var rng = new Xoroshiro128Plus(Seed);
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
}

