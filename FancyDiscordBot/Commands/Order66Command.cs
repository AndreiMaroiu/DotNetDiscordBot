namespace FancyDiscordBot.Commands;

[Command("executeorder66")]
internal class Order66Command : IDiscordCommand
{
    public string Description => "Execute Order 66.";

    public void Init()
    {

    }

    public async Task OnMessage(DiscordClient client, MessageCreateEventArgs e, string[] arguments)
    {
        await client.SendMessageAsync(e.Channel, "It will be done my lord.");
    }
}
