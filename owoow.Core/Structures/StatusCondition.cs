using PKHeX.Core;

namespace owoow.Core.Structures;

public class StatusCondition(byte[] data)
{
    public const byte SIZE = 0x28;

    public readonly byte[] Data = data;

    public bool Paralysis => Data[0x00] == 0x01;
    public bool Sleep => Data[0x08] == 0x02 || Data[0x08] == 0x62; // 0x02: Sleep inflicted by opponent, 0x62 Sleep inflicted on self by Rest
    public bool Freeze => Data[0x10] == 0x01;
    public bool Burn => Data[0x18] == 0x01;
    public bool Poison => Data[0x20] == 0x01;

    public StatusType GetStatusType()
    {
        if (Paralysis) return StatusType.Paralysis;
        if (Sleep) return StatusType.Sleep;
        if (Freeze) return StatusType.Freeze;
        if (Burn) return StatusType.Burn;
        if (Poison) return StatusType.Poison;
        return StatusType.None;
    }
}

