using DSharpPlus.Entities;
using FancyDiscordBot.Utils;

namespace FancyDiscordBot.Commands;

[Command("avatar")]
internal class AvatarCommand : IDiscordCommand
{
    public string Description => "See your fancy avatar or someone else's fancy avatar.";

    public void Init()
    {

    }

    public async Task OnMessage(DiscordClient client, MessageCreateEventArgs e, string[] arguments)
    {
        DiscordEmbedBuilder builder = new()
        {
            Color = BotColors.Main
        };

        if (e.MentionedUsers.Count > 0)
        {
            var mention = e.MentionedUsers[0];
            builder.Title = $"{mention.Username}'s Avatar";
            builder.WithImageUrl(mention.AvatarUrl);
        }
        else
        {
            builder.Title = $"{e.Author.Username}'s Avatar";
            builder.WithImageUrl(e.Author.AvatarUrl);
        }

        await client.SendMessageAsync(e.Channel, builder);
    }
}
