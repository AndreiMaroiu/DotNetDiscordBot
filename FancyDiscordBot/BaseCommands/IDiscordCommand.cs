namespace FancyDiscordBot.BaseCommands;

internal interface IDiscordCommand
{
    Task OnMessage(MessageInfo info);
    string Description { get; }
}
