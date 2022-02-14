using DSharpPlus.Entities;
using FancyDiscordBot.Utils;
using System.Text;

namespace FancyDiscordBot.Commands;

[Command("help")]
internal sealed class HelpCommand : ArgumentCommand
{
    private DiscordEmbed _embededHelp;

    public HelpCommand(DiscordClient client) : base(client)
    {

    }

    public override string Description => "See all the fancy commands.";

    public override void Init()
    {
        DiscordEmbedBuilder builder = new()
        {
            Color = BotColors.Main,
            Title = "Commands",
            Timestamp = DateTime.Now,
            Footer = new() { Text = "Hope this was useful" },
            Thumbnail = new() { Url = "https://www.lollydaskal.com/wp-content/uploads/2019/04/Screen-Shot-2019-04-16-at-5.16.54-PM.png" }
        };

        foreach (var (command, triggers) in CommandUtils.Instance.AllCommands)
        {
            builder.AddField(GetCommandName(triggers), command.Description);
        }

        _embededHelp = builder.Build();
    }

    private static string GetCommandName(List<string> triggers)
    {
        if (triggers.Count == 1)
        {
            return "fancy " + triggers[0];
        }

        StringBuilder builder = new();

        for(int i = 0; i < triggers.Count - 1; i++)
        {
            string trigger = triggers[i];
            builder.Append("fancy");

            if (!string.IsNullOrEmpty(trigger))
            {
                builder.Append(' ').Append(trigger);
            }

            builder.Append(" | ");
        }

        builder.Append("fancy");
        string last = triggers[^1];

        if (!string.IsNullOrEmpty(last))
        {
            builder.Append(last);
        }

        return builder.ToString();
    }


    [Argument("", "")]
    public async Task OnSimple(MessageCreateEventArgs e)
    {
        await Client.SendMessageAsync(e.Channel, _embededHelp);
    }

    [Argument("private", "")]
    public async Task OnPrivate(MessageCreateEventArgs e)
    {
        DiscordMember member = e.Author as DiscordMember;

        if (member is not null)
        {
            await member.SendMessageAsync(_embededHelp);
        }
        else
        {
            await Client.SendMessageAsync(e.Channel, _embededHelp);
        }
    }
}