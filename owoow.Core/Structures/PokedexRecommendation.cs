using static owoow.Core.Encounters;
using static System.Buffers.Binary.BinaryPrimitives;

namespace owoow.Core.Structures;

public class PokedexRecommendation(byte[] data)
{
    public const byte SIZE = 0xA0;

    public readonly byte[] Data = data;

    public ushort Species1 => ReadUInt16LittleEndian(Data.AsSpan()[..]);
    public ushort Species2 => ReadUInt16LittleEndian(Data.AsSpan()[0x20..]);
    public ushort Species3 => ReadUInt16LittleEndian(Data.AsSpan()[0x40..]);
    public ushort Species4 => ReadUInt16LittleEndian(Data.AsSpan()[0x60..]);

    private ulong _Location => ReadUInt64LittleEndian(Data.AsSpan()[0x80..]);
    public string Location => Zones[_Location];
    public ulong Seed => ReadUInt64LittleEndian(Data.AsSpan()[0x90..]);
}

