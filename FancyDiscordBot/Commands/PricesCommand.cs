using DSharpPlus.Entities;
using FancyDiscordBot.Utils;
using System.Text;

namespace FancyDiscordBot.Commands;

// TODO: Prices Command
//[Command("prices")]
internal class PricesCommand : IDiscordCommand
{
    private const string Xpath = "//*[@id=\"pageContainer\"]/div[3]/div[1]/section[1]/div/table/tbody/tr";

    private readonly string _prefix;

    public PricesCommand(string prefix)
    {
        _prefix = prefix;
    }

    public string Description => "Find the prices of a game. A name must be specified!";

    public void Init()
    {

    }

    public async Task OnMessage(DiscordClient client, MessageCreateEventArgs e, string[] arguments)
    {
        if (arguments.Length < 1)
        {
            await client.SendMessageAsync(e.Channel, "Please specify the name of the game!");
            return;
        }

        int removeLength = _prefix.Length + "prices".Length + 2;
        string name = e.Message.Content[removeLength..];

        DiscordEmbedBuilder builder = new()
        {
            Title = "Prices of " + name,
        };

        var nodes = WebUtils.GetHtmlNodes(GetUrl(arguments), Xpath);

        if (nodes is null)
        {
            await client.SendMessageAsync(e.Channel, "No prices found!");
            return;
        }

        foreach (var node in nodes)
        {
            var nameNode = node.SelectSingleNode("/td[1]/a");

            if (nameNode is null)
            {
                continue;
            }

            builder.AddField(nameNode.InnerText, "Ceva");
        }

        await client.SendMessageAsync(e.Channel, builder);
    }

    private static string GetUrl(string[] arguments)
    {
        StringBuilder builder = new();

        foreach (string argument in arguments)
        {
            builder.Append(argument);
        }

        return $"https://isthereanydeal.com/game/{builder.ToString()}/info/";

        //return "https://isthereanydeal.com/game/horizonzerodawn/info/";
    }
}
