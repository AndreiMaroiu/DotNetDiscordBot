using DSharpPlus.Entities;

namespace FancyDiscordBot.Commands;

[Command("dance")]
internal class DanceCommand : IDiscordCommand
{
    public string Description => "Make a fancy dance.";

    public void Init()
    {

    }

    public async Task OnMessage(DiscordClient client, MessageCreateEventArgs e, string[] arguments)
    {
        await client.SendMessageAsync(e.Channel, DiscordEmoji.FromName(client, ":dancin:"));
    }
}
