using DSharpPlus.Entities;

namespace FancyDiscordBot.Commands;

[Command("kill")]
internal class KillCommand : IDiscordCommand
{
    public string Description => "Kill someone. In a fancy way of course!";

    public void Init()
    {

    }

    public async Task OnMessage(DiscordClient client, MessageCreateEventArgs e, string[] arguments)
    {
        if (arguments.Length > 0 && arguments[0] == "me")
        {
            await client.SendMessageAsync(e.Channel, "Please Master, don't kill yourself, I believe in you! :heart:");
            return;
        }

        if (e.MentionedUsers.Count < 1)
        {
            await client.SendMessageAsync(e.Channel, "Please mention someone to kill.");
            return;
        }

        DiscordUser user = e.MentionedUsers[0];

        await client.SendMessageAsync(e.Channel, $"Fancy kill attempt at {user.Mention} begun {DiscordEmoji.FromName(client, ":SlavSquat:")}!");
    }
}
