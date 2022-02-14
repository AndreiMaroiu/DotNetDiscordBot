using DSharpPlus.Entities;

namespace FancyDiscordBot.Commands;

[Command("happybirthday")]
internal class HappyBirthdayCommand : IDiscordCommand
{
    public string Description => "Say a warn and fancy happy birthday to someone";

    public void Init()
    {

    }

    public async Task OnMessage(DiscordClient client, MessageCreateEventArgs e, string[] arguments)
    {
        if (e.MentionedUsers.Count < 1)
        {
            await client.SendMessageAsync(e.Channel, "Please mention someone to say happy birthday");
            return;
        }

        DiscordEmbedBuilder builder = new()
        {
            Title = $"{e.MentionedUsers[0].Username}, {e.Author.Username} wishes you a warm and fancy Happy Birthday Brother!",
            Thumbnail = new() { Url = "https://mk0kaleelao979sb1ktf.kinstacdn.com/wp-content/uploads/2020/09/Happy-Birthday-in-the-Top-5-Most-Spoken-Languages.png" },
            Color = Utils.BotColors.Yellow
        };

        await client.SendMessageAsync(e.Channel, builder);
    }
}
