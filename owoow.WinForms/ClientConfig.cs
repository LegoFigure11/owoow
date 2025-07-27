using owoow.Core.Interfaces;
using SysBot.Base;

namespace owoow.WinForms;

public class ClientConfig : ISeedResetConfig, ITurboConfig, IWebhookConfig
{
    // Connection
    public string IP { get; set; } = "192.168.0.0";
    public int UsbPort { get; set; } = 0;
    public SwitchProtocol Protocol { get; set; } = SwitchProtocol.WiFi;
    public int Game { get; set; } = 0;

    // Fields
    public int TID { get; set; } = 1337;
    public int SID { get; set; } = 1390;
    public bool HasShinyCharm { get; set; } = false;
    public bool HasMarkCharm { get; set; } = false;
    public bool FocusWindow { get; set; } = false;
    public bool PlayTone { get; set; } = false;

    public int MaxSearchTasksNthPowerOfTwo { get; set; } = 2;

    // Seed Reset
    public int ExtraTimeReturnHome { get; set; } = 0;
    public int ExtraTimeCloseGame { get; set; } = 0;

    public int ExtraTimeLoadProfile { get; set; } = 0;
    public bool AvoidSystemUpdate { get; set; } = false;
    public int ExtraTimeLoadGame { get; set; } = 0;

    // Turbo
    public bool LoopTurbo { get; set; } = false;
    public List<string> TurboSequence { get; set; } = [];

    public int InputSleepTime { get; set; } = 200;

    public bool ResetTimeAfterDateSkipping { get; set; } = true;

    // Webhook
    public bool WebhookEnabled { get; set; } = false;
    public string ResultNotificationURL { get; set; } = string.Empty;
    public string ErrorNotificationURL { get; set; } = string.Empty;
    public string ResultWebhookMessageContent { get; set; } = string.Empty;
    public string ErrorWebhookMessageContent { get; set; } = string.Empty;

    // Profiles
    public List<Profile> Profiles { get; set; } = [];
}
