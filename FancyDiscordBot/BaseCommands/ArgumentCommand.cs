using System.Reflection;

namespace FancyDiscordBot.BaseCommands;

internal abstract class ArgumentCommand : IDiscordCommand, IDiscordInit
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
            && parameters[0].ParameterType == typeof(MessageInfo);
    }

    public abstract string Description { get; }

    public virtual void Init()
    {
        
    }

    public virtual async Task OnArgumentNotFound(MessageInfo info, string argument)
    {
        await info.SendPublic("No such fancy argument exists");
    }

    public virtual async Task OnIncorrectOrder(MessageInfo info, ArgumentInfo argument)
    {
        await info.SendPublic($"argument order incorect, {argument.Attribute.Argument} should be before " +
            $"{argument.Attribute.Last}");
    }

    public Task OnMessage(MessageInfo info)
    {
        ArgumentInfo argInfo = _argumentActions[""];

        foreach (var argument in info.Arguments)
        {
            if (!_argumentActions.ContainsKey(argument))
            {
                return OnArgumentNotFound(info, argument);
            }

            ArgumentInfo current = _argumentActions[argument];
            if (current.Attribute.Last != argInfo.Attribute.Argument)
            {
                return OnIncorrectOrder(info, current);
            }

            argInfo = current;
        }

        return argInfo.Method.Invoke(this, new object[] { info }) as Task;
    }
}

