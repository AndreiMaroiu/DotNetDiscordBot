namespace FancyDiscordBot.Commands;

[Command("hello")]
[Command("")]
internal class HelloCommand : IDiscordCommand
{
    public string Description => "Get a greeting from Fancy Bot. You can also tag someone to greet him.";

    public void Init()
    {

    }

    public async Task OnMessage(DiscordClient client, MessageCreateEventArgs e, string[] arguments)
    {
        if (arguments.Length > 0 && e.MentionedUsers.Count > 0)
        {
            foreach (var user in e.MentionedUsers)
            {
                await client.SendMessageAsync(e.Channel, $"Hello {user.Mention}");
            }

            return;
        }

        await client.SendMessageAsync(e.Channel, $"Hello {e.Message.Author.Mention}");
    }
}