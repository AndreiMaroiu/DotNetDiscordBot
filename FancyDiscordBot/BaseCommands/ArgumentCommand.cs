using System.Reflection;

namespace FancyDiscordBot.BaseCommands;

internal abstract class ArgumentCommand : IDiscordCommand
{
    private readonly Dictionary<string, ArgumentInfo> _argumentActions = new();
    
    protected DiscordClient Client { get; init; }

    public ArgumentCommand(DiscordClient client)
    {
        Client = client;
        AddAllMethods();
    }

    private void AddAllMethods()
    {
        MethodInfo[] methods = GetType().GetMethods();

        foreach (MethodInfo method in methods)
        {
            if (!IsValidMethod(method))
            {
                continue;
            }

            ArgumentAttribute attribute = method.GetCustomAttribute<ArgumentAttribute>();

            if (attribute is null)
            {
                continue;
            }

            _argumentActions.Add(attribute.Argument, new ArgumentInfo
            {
                Attribute = attribute,
                Method = method
            });
        }
    }

    private static bool IsValidMethod(MethodInfo method)
    {
        var parameters = method.GetParameters();

        return method.ReturnType == typeof(Task) 
            && parameters.Length == 1 
            && parameters[0].ParameterType == typeof(MessageCreateEventArgs);
    }

    public abstract string Description { get; }

    public virtual void Init()
    {
        
    }

    public virtual async Task OnArgumentNotFound(MessageCreateEventArgs e, string argument)
    {
        await Client.SendMessageAsync(e.Channel, "No such fancy argument exists");
    }

    public virtual async Task OnIncorrectOrder(MessageCreateEventArgs e, ArgumentInfo argument)
    {
        await Client.SendMessageAsync(e.Channel, 
            $"argument order incorect, {argument.Attribute.Argument} should be before " +
            $"{argument.Attribute.Last}");
    }

    public async Task OnMessage(DiscordClient client, MessageCreateEventArgs e, string[] arguments)
    {
        ArgumentInfo info = _argumentActions[""];

        foreach (var argument in arguments)
        {
            if (!_argumentActions.ContainsKey(argument))
            {
                await OnArgumentNotFound(e, argument);
                return;
            }

            ArgumentInfo current = _argumentActions[argument];
            if (current.Attribute.Last != info.Attribute.Argument)
            {
                await OnIncorrectOrder(e, current);
                return;
            }

            info = current;
        }

        info.Method.Invoke(this, new object[] { e });
    }
}

