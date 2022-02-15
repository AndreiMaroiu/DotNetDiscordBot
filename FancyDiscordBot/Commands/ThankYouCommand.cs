using DSharpPlus.Entities;
using FancyDiscordBot.Utils;

namespace FancyDiscordBot.Commands;

[Command("thankyou")]
internal class ThankYouCommand : IDiscordCommand
{
    public string Description => "Say a fancy thank you to someone special";

    public async Task OnMessage(MessageInfo info)
    {
        if (info.E.MentionedUsers.Count < 1)
        {
            await info.SendPublic("Please mention someone to thank.");
            return;
        }

        DiscordEmbedBuilder builder = new()
        {
            Title = "Donald J. Trump",
            Description = "@realDonaldTrump",
            Color = BotColors.Blue,
            Thumbnail = new() { Url = "https://pbs.twimg.com/profile_images/736392853992001537/eF4LJLkn_400x400.jpg" },
            Footer = new() { Text = "With love, Donald Trump <3" }
        };

        builder.AddField("\u200B", $"Thank you {info.E.MentionedUsers[0].Username}, very cool!", true);

        await info.SendPublic(builder.Build());
    }
}
