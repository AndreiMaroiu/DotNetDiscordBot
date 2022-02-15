using DSharpPlus.Entities;

namespace FancyDiscordBot.Commands;

[Command("kill")]
internal class KillCommand : IDiscordCommand
{
    public string Description => "Kill someone. In a fancy way of course!";

    public async Task OnMessage(MessageInfo info)
    {
        if (info.Arguments.Length > 0 && info.Arguments[0] == "me")
        {
            await info.SendPublic("Please Master, don't kill yourself, I believe in you! :heart:");
            return;
        }

        if (info.E.MentionedUsers.Count < 1)
        {
            await info.SendPublic("Please mention someone to kill.");
            return;
        }

        DiscordUser user = info.E.MentionedUsers[0];

        await info.SendPublic($"Fancy kill attempt at {user.Mention} begun {DiscordEmoji.FromName(info.Client, ":SlavSquat:")}!");
    }
}
