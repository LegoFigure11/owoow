namespace owoow.Core.Interfaces;

public interface ITurboConfig
{
    bool LoopTurbo { get; set; }
    List<string> TurboSequence { get; set; }

    bool NTPAfterDateSkipping { get; set; }
    bool NTPWhileDateSkipping { get; set; }
    uint NTPWhileDateSkippingInterval { get; set; }
}
