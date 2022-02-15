using FancyDiscordBot.Models;
using FancyDiscordBot.Utils;
using System.Text;

namespace FancyDiscordBot.Commands;

[Command("yoda")]
internal class YodaCommand : IDiscordCommand
{
    private const string Url = "https://api.funtranslations.com/translate/yoda.json";

    public string Description => "Translate a phrase in yoda language.";

    private static string GetMessage(string[] arguments)
    {
        StringBuilder builder = new();

        foreach (string argument in arguments)
        {
            builder.Append(argument).Append(' ');
        }

        return builder.ToString();
    }

    public async Task OnMessage(MessageInfo info)
    {
        if (info.Arguments.Length < 1)
        {
            await info.SendPublic("Please specify a phrase to translate.");
            return;
        }

        try
        {
            string message = GetMessage(info.Arguments);

            YodaResponse yodaResponse = await WebUtils.PostAsync<YodaResponse, YodaRequest>(Url, new() { Text = message });

            await info.SendPublic(yodaResponse.Contents.Translated);
        }
        catch (Exception)
        {
            await info.SendPublic("Could not translate. I have failed you Master Jedi!");
        }
    }
}

