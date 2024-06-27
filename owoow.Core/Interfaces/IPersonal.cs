namespace owoow.Core.Interfaces;

public interface IPersonal
{
    byte EggMoveCount { get; }
    string[]? EggMoves { get; }
    bool HasItems { get; }
    string[]? Items { get; }
    string[]? Types { get; }
    short DevId { get; }
}

public class Personal : IPersonal
{
    public byte EggMoveCount { get; set; }
    public string[]? EggMoves { get; set; }
    public bool HasItems { get; set; }
    public string[]? Items { get; set; }
    public string[]? Types { get; set; }
    public short DevId { get; set; }
}
