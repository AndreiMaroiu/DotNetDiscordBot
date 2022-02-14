namespace FancyDiscordBot.BaseCommands;

internal interface IDiscordCommand
{
    Task OnMessage(DiscordClient client, MessageCreateEventArgs e, string[] arguments);
    string Description { get; }
    void Init();
}
