namespace FancyDiscordBot.Models;

internal class DadJokeBody
{
    public string ID { get; set; }
    public string Setup { get; set; }
    public string Punchline { get; set; }
    public string Type { get; set; }
    public List<object> Likes { get; set; }
    public object Author { get; set; }
    public bool Approved { get; set; }
    public object Date { get; set; }
    public bool NSFW { get; set; }
}

