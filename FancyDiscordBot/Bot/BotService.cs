using DSharpPlus.Entities;
using FancyDiscordBot.InversionOfControl;
using FancyDiscordBot.Utils;
using Microsoft.Extensions.Configuration;

namespace FancyDiscordBot.Bot;

public class BotService
{
    private readonly DiscordClient _client;
    private readonly string _prefix;
    private readonly Dictionary<string, IDiscordCommand> _commands;
    private readonly SpecialService _specialService;
    private readonly IoC _containner;

    public BotService(DiscordClient client, string prefix, IConfiguration config)
    {
        _client = client;
        _prefix = prefix;
        _specialService = new(client, prefix);

        _containner = new();
        _containner.Add(prefix);
        _containner.Add(client);
        _containner.Add(config);

        _commands = CommandUtils.Instance.GetCommands(_containner);

        foreach (var command in _commands)
        {
            command.Value.Init();
        }
    }

    public async Task OnMessage(MessageCreateEventArgs e)
    {
        if (e.Author.IsBot)
        {
            return;
        }

        if (await _specialService.WasSpecial(e))
        {
            Console.WriteLine($"Reacted to message: {e.Message.Content} from user: {e.Author.Username}");

            return;
        }

        string message = e.Message.Content.ToLower();

        if (!message.StartsWith(_prefix))
        {
            return;
        }

        message = message[_prefix.Length..];

        string[] arguments = message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string command = arguments.Length > 0 ? arguments[0] : string.Empty;

        if (!_commands.ContainsKey(command))
        {
            await _client.SendMessageAsync(e.Channel, $"There is no such fancy command. Use 'fancy help' for more details.");
            return;
        }

        try
        {
            await _commands[command].OnMessage(_client, e, (arguments is not null && arguments.Length > 0) ? arguments[1..] : arguments);
            Console.WriteLine($"Reacted to massage: {e.Message.Content} from user: {e.Author.Username}");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }

    public async Task OnNewMember(GuildMemberAddEventArgs e)
    {
        DiscordChannel channel = e.Guild.Channels.Where(x => x.Value.Name == "general").FirstOrDefault().Value;

        if (channel is null)
        {
            channel = e.Guild.Channels.First().Value;
        }

        DiscordEmbedBuilder builder = new()
        {
            Title = $"Everybody, please welcome {e.Member.Username} to our fancy server!",
            Color = BotColors.Main,
            ImageUrl = e.Member.AvatarUrl
        };

        await _client.SendMessageAsync(channel, builder.Build());
    }

    public async Task OnUserLeave(GuildMemberRemoveEventArgs e)
    {
        DiscordChannel channel = e.Guild.Channels.Where(x => x.Value.Name == "general").FirstOrDefault().Value;

        if (channel is null)
        {
            channel = e.Guild.Channels.First().Value;
        }

        DiscordEmbedBuilder builder = new()
        {
            Title = $"It's a sad day, looks like {e.Member.Username} doesn't like our fancy server anymore!",
            Color = BotColors.Sad,
            Thumbnail = new() { Url = e.Member.AvatarUrl },
            ImageUrl = "https://www.theparisreview.org/blog/wp-content/uploads/2018/04/576cb32f05aae_louisette.jpg",
        };

        await _client.SendMessageAsync(channel, builder.Build());
    }
}