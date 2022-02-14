namespace FancyDiscordBot.Commands;

[Command("moralhelp")]
internal class MoralHelpCommand : IDiscordCommand
{
    public string Description => "Get some fancy moral help";

    public void Init()
    {

    }

    public async Task OnMessage(DiscordClient client, MessageCreateEventArgs e, string[] arguments)
    {
        await client.SendMessageAsync(e.Channel, "Everything will get better, just trust me.");
    }
}
