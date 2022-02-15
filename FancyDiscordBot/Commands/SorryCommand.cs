using DSharpPlus.Entities;
using FancyDiscordBot.Utils;

namespace FancyDiscordBot.Commands;

[Command("sorry")]
internal class SorryCommand : IDiscordCommand
{
    public string Description => "Say a fancy sorry to someone. A mention is required!";

    public async Task OnMessage(MessageInfo info)
    {
        if (info.E.MentionedUsers.Count == 0)
        {
            await info.SendPublic("You must mention somebody.");
            return;
        }

        DiscordEmbedBuilder builder = new()
        {
            Color = BotColors.Sorry,
            Title = $"{info.E.MentionedUsers[0].Username}, you should know that {info.E.Author.Username} is really sorry!",
            ImageUrl = "https://us.123rf.com/450wm/frenta/frenta2107/frenta210700075/171848805-unhappy-kawaii-bunny-and-duckling-with-red-heart-inscription-i-m-sorry-cute-little-duck-and-rabbit-a.jpg?ver=6"
        };

        await info.SendPublic(builder.Build());
    }
}