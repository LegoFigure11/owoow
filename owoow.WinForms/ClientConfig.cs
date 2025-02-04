using SysBot.Base;

namespace owoow.WinForms;

public class ClientConfig
{
    // Connection
    public string IP { get; set; } = "192.168.0.0";
    public int UsbPort { get; set; }
    public SwitchProtocol Protocol { get; set; } = SwitchProtocol.WiFi;
    public int Game { get; set; } = 0;

    // Fields
    public int TID { get; set; } = 1337;
    public int SID { get; set; } = 1390;
    public bool HasShinyCharm { get; set; } = false;
    public bool HasMarkCharm { get; set; } = false;
    public bool FocusWindow { get; set; } = false;
    public bool PlayTone { get; set; } = false;
}
