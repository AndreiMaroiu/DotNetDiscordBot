using DSharpPlus.Entities;

namespace FancyDiscordBot.Commands;

[Command("dance")]
internal class DanceCommand : IDiscordCommand
{
    public string Description => "Make a fancy dance.";

    public async Task OnMessage(MessageInfo info)
    {
        await info.SendPublic(DiscordEmoji.FromName(info.Client, ":dancin:"));
    }
}
