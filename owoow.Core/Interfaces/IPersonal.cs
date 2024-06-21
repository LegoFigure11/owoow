namespace owoow.Core.Interfaces;

public interface IPersonal
{
    byte EggMoveCount { get; }
    string[] EggMoves { get; }
    bool HasItems { get; }
    string[] Items { get; }
}

public class Personal : IPersonal
{
    public byte EggMoveCount { get; set; } = 0;
    public string[] EggMoves { get; set; } = [];
    public bool HasItems { get; set; } = false;
    public string[] Items { get; set; } = [];
}
