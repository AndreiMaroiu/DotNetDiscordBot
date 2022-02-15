namespace FancyDiscordBot.Commands;

[Command("execute")]
internal class ExecuteCommand : ArgumentCommand
{
    public ExecuteCommand(DiscordClient client) : base(client)
    {
    }

    public override string Description => "Execute some fancy orders, like order 66 or a sandwich";

    [Argument("", "")]
    public async Task OnSimple(MessageInfo e)
    {
        await e.SendPublic("Please say an order to execute");
    }

    [Argument("order", "")]
    public async Task OnOrder(MessageInfo e)
    {
        await e.SendPublic("Please say what order to execute");
    }

    [Argument("66", "order")]
    public async Task On66(MessageInfo e)
    {
        await e.SendPublic("It will be done my lord!");
    }

    [Argument("me", "")]
    public async Task OnMe(MessageInfo e)
    {
        await e.SendPublic("Kinky.");
    }

    [Argument("sandwich", "")]
    public async Task OnSandwich(MessageInfo e)
    {
        await e.SendPublic("Sorry, I run out of bread. :pensive:");
    }
}
