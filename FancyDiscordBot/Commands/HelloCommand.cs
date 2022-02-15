namespace FancyDiscordBot.Commands;

[Command("hello")]
[Command("")]
internal class HelloCommand : IDiscordCommand
{
    public string Description => "Get a greeting from Fancy Bot. You can also tag someone to greet him.";

    public async Task OnMessage(MessageInfo info)
    {
        var e = info.E;

        if (e.MentionedUsers.Count > 0)
        {
            foreach (var user in e.MentionedUsers)
            {
                await info.SendPublic($"Hello {user.Mention}");
            }

            return;
        }

        await info.SendPublic($"Hello {e.Message.Author.Mention}");
    }
}