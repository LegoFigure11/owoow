namespace owoow.Core.Interfaces;

public interface ISeedResetConfig
{
    int ExtraTimeReturnHome { get; set; }
    int ExtraTimeCloseGame { get; set; }

    int ExtraTimeLoadProfile { get; set; }
    bool AvoidSystemUpdate { get; set; }
    int ExtraTimeLoadGame { get; set; }

    bool LogResultsWhileInProgress { get; set; }
}
