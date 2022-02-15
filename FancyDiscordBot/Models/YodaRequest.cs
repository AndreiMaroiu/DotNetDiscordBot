using System.Text.Json.Serialization;

namespace FancyDiscordBot.Models;

internal class YodaRequest
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
}
