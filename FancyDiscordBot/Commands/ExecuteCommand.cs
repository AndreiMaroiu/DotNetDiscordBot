namespace FancyDiscordBot.Commands;

[Command("execute")]
internal class ExecuteCommand : ArgumentCommand
{
    public ExecuteCommand(DiscordClient client) : base(client)
    {
    }

    public override string Description => "Execute some fancy orders, like order 66 or a sandwich";

    [Argument("", "")]
    public async Task OnSimple(MessageCreateEventArgs e)
    {
        await Client.SendMessageAsync(e.Channel, "Please say an order to execute");
    }

    [Argument("order", "")]
    public async Task OnOrder(MessageCreateEventArgs e)
    {
        await Client.SendMessageAsync(e.Channel, "Please say what order to execute");
    }

    [Argument("66", "order")]
    public async Task On66(MessageCreateEventArgs e)
    {
        await Client.SendMessageAsync(e.Channel, "It will be done my lord!");
    }

    [Argument("me", "")]
    public async Task OnMe(MessageCreateEventArgs e)
    {
        await Client.SendMessageAsync(e.Channel, "Kinky.");
    }

    [Argument("sandwich", "")]
    public async Task OnSandwich(MessageCreateEventArgs e)
    {
        await Client.SendMessageAsync(e.Channel, "Sorry, I run out of bread. :pensive:");
    }
}
