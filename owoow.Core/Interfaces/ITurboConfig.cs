namespace owoow.Core.Interfaces;

public interface ITurboConfig
{
    bool LoopTurbo { get; set; }
    List<string> TurboSequence { get; set; }

    int InputSleepTime { get; set; }

    bool ResetTimeAfterDateSkipping { get; set; }
}
