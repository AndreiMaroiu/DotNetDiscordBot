using DSharpPlus.Entities;
using FancyDiscordBot.Utils;

namespace FancyDiscordBot.Commands;

[Command("avatar")]
internal class AvatarCommand : IDiscordCommand
{
    public string Description => "See your fancy avatar or someone else's fancy avatar.";

    public async Task OnMessage(MessageInfo info)
    {
        DiscordEmbedBuilder builder = new()
        {
            Color = BotColors.Main
        };

        if (info.E.MentionedUsers.Count > 0)
        {
            var mention = info.E.MentionedUsers[0];
            builder.Title = $"{mention.Username}'s Avatar";
            builder.WithImageUrl(mention.AvatarUrl);
        }
        else
        {
            builder.Title = $"{info.E.Author.Username}'s Avatar";
            builder.WithImageUrl(info.E.Author.AvatarUrl);
        }

        await info.SendPublic(builder);
    }
}
