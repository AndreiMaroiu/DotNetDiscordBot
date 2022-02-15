namespace FancyDiscordBot.Commands;

[Command("moralhelp")]
internal class MoralHelpCommand : IDiscordCommand
{
    public string Description => "Get some fancy moral help";

    public async Task OnMessage(MessageInfo info)
    {
        await info.SendPublic("Everything will get better, just trust me.");
    }
}
