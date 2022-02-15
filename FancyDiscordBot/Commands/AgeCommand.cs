using FancyDiscordBot.Models;
using FancyDiscordBot.Utils;

namespace FancyDiscordBot.Commands;

[Command("age")]
internal class AgeCommand : IDiscordCommand
{
    public string Description => "Predicts the age of someone based on their name. " +
        "You can also mention someone, or specigy a name.";

    public async Task OnMessage(MessageInfo info)
    {
        string name = info.Message;

        try
        {
            var response = await WebUtils.GetAsync<AgeData>($"https://api.agify.io/?name={name}");

            await info.SendPublic($"Based on the name: {response.Name}, you seem to be {response.Age} years old.");
        }
        catch
        {
            await info.SendPublic("Sorry, I could not predict you age :(");
        }
    }
}
