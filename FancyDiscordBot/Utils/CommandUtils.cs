using FancyDiscordBot.InversionOfControl;
using System.Reflection;

namespace FancyDiscordBot.Utils;

internal class CommandUtils
{
    public static readonly CommandUtils Instance = new();

    private Dictionary<string, IDiscordCommand> CommandsDictionary { get; } = new();
    public List<(IDiscordCommand Command, List<string> Triggers)> AllCommands { get; } = new();

    private CommandUtils()
    {
        
    }

    public Dictionary<string, IDiscordCommand> GetCommands(IoC containner)
    {
        if (CommandsDictionary.Count == 0)
        {
            SearchForCommands(containner);
        }

        return CommandsDictionary;
    }

    private void SearchForCommands(IoC containner)
    {
        Assembly assembly = typeof(CommandUtils).Assembly;

        var types = assembly.GetTypes()
            .Where(type => typeof(IDiscordCommand).IsAssignableFrom(type) && !type.IsAbstract);

        foreach (Type type in types)
        {
            CommandAttribute[] attributes = GetCommandAttribute(type);

            if (attributes is null || attributes.Length == 0)
            {
                continue;
            }

            IDiscordCommand command = containner.Create(type) as IDiscordCommand;
            List<string> triggers = new();

            foreach (CommandAttribute attribute in attributes)
            {
                CommandsDictionary.Add(attribute.CommandName, command);
                triggers.Add(attribute.CommandName);
            }

            AllCommands.Add((command, triggers));
        }
    }

    private static CommandAttribute[] GetCommandAttribute(Type type) 
        => Attribute.GetCustomAttributes(type, typeof(CommandAttribute)) as CommandAttribute[];
}