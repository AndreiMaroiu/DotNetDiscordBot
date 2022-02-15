using DSharpPlus.Entities;

namespace FancyDiscordBot.Commands;

[Command("happybirthday")]
internal class HappyBirthdayCommand : IDiscordCommand
{
    private const string ImageUrl = "https://mk0kaleelao979sb1ktf.kinstacdn.com/wp-content/uploads/2020/09/Happy-Birthday-in-the-Top-5-Most-Spoken-Languages.png";

    public string Description => "Say a warn and fancy happy birthday to someone";

    public async Task OnMessage(MessageInfo info)
    {
        if (info.E.MentionedUsers.Count < 1)
        {
            await info.SendPublic("Please mention someone to say happy birthday");
        }

        DiscordEmbedBuilder builder = new()
        {
            Title = $"{info.E.MentionedUsers[0].Username}, {info.E.Author.Username} wishes you a warm and fancy Happy Birthday Brother!",
            Thumbnail = new() { Url =  ImageUrl},
            Color = Utils.BotColors.Yellow
        };

        await info.SendPublic(builder);
    }
}
