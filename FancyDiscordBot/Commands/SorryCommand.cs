using DSharpPlus.Entities;
using FancyDiscordBot.Utils;

namespace FancyDiscordBot.Commands;

[Command("sorry")]
internal class SorryCommand : IDiscordCommand
{
    public string Description => "Say a fancy sorry to someone. A mention is required!";

    public void Init()
    {

    }

    public async Task OnMessage(DiscordClient client, MessageCreateEventArgs e, string[] arguments)
    {
        if (e.MentionedUsers.Count == 0)
        {
            await client.SendMessageAsync(e.Channel, "You must mention somebody.");
            return;
        }

        DiscordEmbedBuilder builder = new()
        {
            Color = BotColors.Sorry,
            Title = $"{e.MentionedUsers[0].Username}, you should know that {e.Author.Username} is really sorry!",
            ImageUrl = "https://us.123rf.com/450wm/frenta/frenta2107/frenta210700075/171848805-unhappy-kawaii-bunny-and-duckling-with-red-heart-inscription-i-m-sorry-cute-little-duck-and-rabbit-a.jpg?ver=6"
        };

        await client.SendMessageAsync(e.Channel, builder.Build());
    }
}