using FancyDiscordBot.Models;
using FancyDiscordBot.Utils;

namespace FancyDiscordBot.Commands;

[Command("dadjoke")]
internal class DadJokeCommand : IDiscordCommand
{
    public string Description => "Get a random fancy dad joke!";

    public async Task OnMessage(MessageInfo info)
    {
        try
        {
            DadJoke response = await WebUtils.GetAsync<DadJoke>("https://icanhazdadjoke.com/");

            await info.SendPublic(response.Joke);
        }
        catch
        {
            await info.SendPublic("Sorry, I could not find a joke, try again later or tomorrow");
        }
    }
}