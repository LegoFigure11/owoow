namespace owoow.Core.Interfaces;

public interface IWebhookConfig
{
    bool WebhookEnabled { get; set; }
    string ResultNotificationURL { get; set; }
    string ErrorNotificationURL { get; set; }
    string WebhookMessageContent { get; set; }
}
