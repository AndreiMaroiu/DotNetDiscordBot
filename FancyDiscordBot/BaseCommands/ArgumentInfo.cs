using System.Reflection;

namespace FancyDiscordBot.BaseCommands;

internal struct ArgumentInfo
{
    public MethodInfo Method { get; init; }
    public ArgumentAttribute Attribute { get; init; }
}

