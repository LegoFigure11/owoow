namespace owoow.Core.Interfaces;

public interface IWebhookConfig
{
    bool WebhookEnabled { get; set; }
    string ResultNotificationURL { get; set; }
    string ErrorNotificationURL { get; set; }
    string ResultWebhookMessageContent { get; set; }
    string ErrorWebhookMessageContent { get; set; }
}
