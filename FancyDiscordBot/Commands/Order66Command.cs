namespace FancyDiscordBot.Commands;

[Command("executeorder66")]
internal class Order66Command : IDiscordCommand
{
    public string Description => "Execute Order 66.";

    public async Task OnMessage(MessageInfo info)
    {
        await info.SendPublic("It will be done my lord.");
    }
}
