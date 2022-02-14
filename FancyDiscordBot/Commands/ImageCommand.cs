using System.Text;

namespace FancyDiscordBot.Commands;

[Command("image")]
internal class ImageCommand : BaseImageCommand, IDiscordCommand
{
    public ImageCommand()
    {

    }

    public string Description => "Get a fancy image. Requires one parameter to search for. Ex: fancy image cat";

    public void Init()
    {

    }

    public async Task OnMessage(DiscordClient client, MessageCreateEventArgs e, string[] arguments)
    {
        if (arguments.Length < 1)
        {
            await client.SendMessageAsync(e.Channel, "No image type provided!");
            return;
        }

        StringBuilder builder = new();

        for (int i = 0; i < arguments.Length - 1; i++)
        {
            builder.Append(arguments[i]).Append(" ");
        }

        builder.Append(arguments[^1]);

        var type = builder.ToString();
        try
        {
            await client.SendMessageAsync(e.Channel, GetRandomImage(type));
        }
        catch
        {
            await client.SendMessageAsync(e.Channel, $"Could not find not a single fancy image of type: {type}");
        }
    }
}
