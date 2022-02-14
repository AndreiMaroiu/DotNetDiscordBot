namespace FancyDiscordBot.BaseCommands;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class CommandAttribute : Attribute
{
    public string CommandName { get; }

    public CommandAttribute(string commandName)
    {
        CommandName = commandName;
    }
}
