namespace FancyDiscordBot.BaseCommands;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
internal class ArgumentAttribute : Attribute
{
    public string Argument { get; }
    public string Last { get; }

    public ArgumentAttribute(string argument, string last)
    {
        Argument = argument;
        Last = last;
    }
}
