using owoow.Core.Interfaces;
using PKHeX.Core;
using PKHeX.Drawing.PokeSprite;
using System.Drawing;
using System.Text.Json;
using System.Text;
using static PKHeX.Core.MoveType;

namespace owoow.Core.Discord;

public class WebhookHandler(IWebhookConfig config)
{
    private readonly HttpClient _client = new();
    private readonly string[]? WebhookURLs = config.WebhookEnabled ? config.ResultNotificationURL.Split(",") : null;
    private readonly string[]? ErrorURLs = config.WebhookEnabled ? config.ErrorNotificationURL.Split(",") : null;

    public async Task SendNotification(OverworldFrame frame, string time, int resets, int results, bool shiny, CancellationToken token)
    {
        if (WebhookURLs is null || !config.WebhookEnabled) return;

        var personal = Encounters.Personal![frame.Species]!;

        var color = GetWebhookColor(personal.Types![0]);

        var pk = new PK8()
        {
            Species = (ushort)personal.DevId,
            Gender = (byte)(frame.Gender == 'F' ? 1 : 0),
        };

        var sprite = SpriteName.GetResourceStringSprite(pk.Species, pk.Form, pk.Gender, pk.FormArgument, EntityContext.Gen8, shiny);
        sprite = sprite[1..];
        sprite = sprite.Replace('_', '-').Insert(0, "_");

        var Webhook = new
        {
            username = "owoow",
            avatar_url = $"https://github.com/kwsch/PKHeX/blob/master/PKHeX.Drawing.PokeSprite/Resources/img/Artwork%20Pokemon%20Sprites/a{sprite}.png?raw=true",
            content = config.WebhookMessageContent,
            embeds = new List<object>
            {
                new
                {
                    title = $"{(shiny ? "Shiny " : string.Empty)}{frame.Species}",
                    color = color.ToArgb() & 0xFFFFFF,
                    description = "",
                    fields = new List<object>
                    {
                        new
                        {
                            name = "Results",
                            value = results,
                            inline = true,
                        },
                        new
                        {
                            name = "Earliest Advance",
                            value = frame.Advances,
                            inline = true,
                        },
                        new
                        {
                            name = "Search Time",
                            value = $"{time} (Resets: {resets})",
                            inline = true,
                        }
                    }
                }
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(Webhook), Encoding.UTF8, "application/json");
        foreach (var url in WebhookURLs)
            await _client.PostAsync(url.Trim(), content, token).ConfigureAwait(false);

    }

    public async Task SendErrorNotification(string error, string caption, CancellationToken token)
    {
        if (ErrorURLs is null || !config.WebhookEnabled)
            return;

        var Webhook = new
        {
            username = "owoow",
            avatar_url = "https://www.serebii.net/itemdex/sprites/sv/ultraball.png",
            content = config.WebhookMessageContent,
            embeds = new List<object>
            {
                new
                {
                    title = caption != "" ? caption : "owoow Error",
                    description = error,
                    color = 0xf7262a,
                },
            },
        };

        var content = new StringContent(JsonSerializer.Serialize(Webhook), Encoding.UTF8, "application/json");
        foreach (var url in ErrorURLs)
            await _client.PostAsync(url.Trim(), content, token).ConfigureAwait(false);
    }

    public async Task SendTestWebhook(CancellationToken token)
    {
        for (int i = 0; i < WebhookURLs?.Length; i++)
        {
            var Webhook = new
            {
                username = "owoow",
                avatar_url = "https://www.serebii.net/itemdex/sprites/sv/ultraball.png",
                content = config.WebhookMessageContent,
                embeds = new List<object>
                {
                    new
                    {
                        title = "owoow Test Result Found Webhook",
                        description = "This is a test result found webhook",
                        color = new Random().Next(0x1000000),
                    },
                },
            };

            var content = new StringContent(JsonSerializer.Serialize(Webhook), Encoding.UTF8, "application/json");

            string? url = WebhookURLs[i];
            await _client.PostAsync(url.Trim(), content, token).ConfigureAwait(false);
        }

        for (int i = 0; i < ErrorURLs?.Length; i++)
        {
            var Webhook = new
            {
                username = "owoow",
                avatar_url = "https://www.serebii.net/itemdex/sprites/sv/ultraball.png",
                content = config.WebhookMessageContent,
                embeds = new List<object>
                {
                    new
                    {
                        title = "owoow Test Error Message Webhook",
                        description = "This is a test error message webhook",
                        color = new Random().Next(0x1000000),
                    },
                },
            };  

            var content = new StringContent(JsonSerializer.Serialize(Webhook), Encoding.UTF8, "application/json");

            string? url = ErrorURLs[i];
            await _client.PostAsync(url.Trim(), content, token).ConfigureAwait(false);
        }
    }

    private static Color GetWebhookColor(string? type)
    {
        if (type is not null && Enum.TryParse(type, out MoveType mt))
        {
            return GetWebhookColor(mt);
        }
        return new Color();
    }

    private static Color GetWebhookColor(MoveType type) => type switch
    {
        Normal => Color.FromArgb(159, 161, 159),
        Fighting => Color.FromArgb(255, 128, 000),
        Flying => Color.FromArgb(129, 185, 239),
        Poison => Color.FromArgb(143, 065, 203),
        Ground => Color.FromArgb(145, 081, 033),
        Rock => Color.FromArgb(175, 169, 129),
        Bug => Color.FromArgb(145, 161, 025),
        Ghost => Color.FromArgb(112, 065, 112),
        Steel => Color.FromArgb(096, 161, 184),
        Fire => Color.FromArgb(230, 040, 041),
        Water => Color.FromArgb(041, 128, 239),
        Grass => Color.FromArgb(063, 161, 041),
        Electric => Color.FromArgb(250, 192, 000),
        Psychic => Color.FromArgb(239, 065, 121),
        Ice => Color.FromArgb(063, 216, 255),
        Dragon => Color.FromArgb(080, 097, 225),
        Dark => Color.FromArgb(080, 065, 063),
        Fairy => Color.FromArgb(239, 113, 239),
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
    };
}
