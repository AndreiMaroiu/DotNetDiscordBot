using DSharpPlus.Entities;

namespace FancyDiscordBot.BaseCommands;

internal readonly struct MessageInfo
{
    public DiscordClient Client { get; init; }
    public MessageCreateEventArgs E { get; init; }
    public string Message { get; init; }
    public string[] Arguments { get; init; }

    public readonly async Task SendPublic(string message)
    {
        await Client.SendMessageAsync(E.Channel, message);
    }

    public readonly async Task SendPublic(DiscordEmbed embed)
    {
        await Client.SendMessageAsync(E.Channel, embed);
    }

    public readonly async Task SendPublic(DiscordEmbedBuilder builder)
    {
        await Client.SendMessageAsync(E.Channel, builder);
    }

    public readonly async Task SendPrivate(string message)
    {
        if (E.Author is DiscordMember member)
        {
            await member.SendMessageAsync(message);
        }
        else
        {
            await Client.SendMessageAsync(E.Channel, message);
        }
    }

    public readonly async Task SendPrivate(DiscordEmbed message)
    {
        if (E.Author is DiscordMember member)
        {
            await member.SendMessageAsync(message);
        }
        else
        {
            await Client.SendMessageAsync(E.Channel, message);
        }
    }

    public readonly async Task SendPrivate(DiscordEmbedBuilder message)
    {
        if (E.Author is DiscordMember member)
        {
            await member.SendMessageAsync(message);
        }
        else
        {
            await Client.SendMessageAsync(E.Channel, message);
        }
    }
}

