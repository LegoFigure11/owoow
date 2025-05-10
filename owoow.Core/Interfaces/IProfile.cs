namespace owoow.Core.Interfaces;

public interface IProfile
{
    string Name { get; }
    int GameVersion { get; }
    string TID { get; }
    string SID { get; }
    bool HasShinyCharm { get; }
    bool HasMarkCharm { get; }
}

public class Profile : IProfile
{
    public string Name { get; set; } = string.Empty;
    public int GameVersion { get; set; }
    public string TID { get; set; } = string.Empty;
    public string SID { get; set; } = string.Empty;
    public bool HasShinyCharm { get; set; }
    public bool HasMarkCharm { get; set; }
}
