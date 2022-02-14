using DSharpPlus.Entities;

namespace FancyDiscordBot.Bot;

internal class SpecialService
{
    private readonly Dictionary<string, SpecialAction> _specialWords;

    private readonly RegexService _regexService;
    private readonly DiscordClient _client;
    private readonly string _prefix;

    public SpecialService(DiscordClient client, string prefix)
    {
        _client = client;
        _prefix = prefix;
        _regexService = new(client);

        _specialWords = new()
        {
            ["f"] = OnF,
            ["nice"] = OnNice,
            ["noice"] = OnNice,
            ["69"] = On69,
            ["?"] = OnQuestionMark,
            ["xd"] = OnXD,
        };
    }

    public async Task<bool> WasSpecial(MessageCreateEventArgs e)
    {
        string message = e.Message.Content.ToLower();

        if (_specialWords.ContainsKey(message))
        {
            await _specialWords[message](e); // invoke event
            return true;
        }

        bool isCommand = message.StartsWith(_prefix);

        if (isCommand && IsUpperCase(e.Message.Content))
        {
            await _client.SendMessageAsync(e.Channel, "Please don't scream at me Master :sob:");
            return true;
        }

        if (isCommand)
        {
            return false;
        }

        await _regexService.HandleRegex(e);

        return false;
    }

    private static bool IsUpperCase(string message)
    {
        foreach (char c in message)
        {
            if (char.IsLetter(c) && !char.IsUpper(c))
            {
                return false;
            }
        }

        return true;
    }

    #region OnSpecialWord
    private async Task OnF(MessageCreateEventArgs e)
    {
        await _client.SendMessageAsync(e.Channel, "f");
    }

    private async Task On69(MessageCreateEventArgs e)
    {
        await e.Message.RespondAsync("nice");
    }

    private async Task OnNice(MessageCreateEventArgs e)
    {
        await e.Message.CreateReactionAsync(DiscordEmoji.FromName(_client, ":love_cornel:"));
    }

    private async Task OnQuestionMark(MessageCreateEventArgs e)
    {
        await e.Message.RespondAsync("Even I have questions.");
    }

    private async Task OnXD(MessageCreateEventArgs e)
    {
        await e.Message.CreateReactionAsync(DiscordEmoji.FromName(_client, ":regional_indicator_x:"));
        await e.Message.CreateReactionAsync(DiscordEmoji.FromName(_client, ":regional_indicator_d:"));
    }

    private async Task OnBa(MessageCreateEventArgs e)
    {
        await _client.SendMessageAsync(e.Channel, "Da ba");
    }
    #endregion
}
