using System.Text;

namespace FancyDiscordBot.Commands;

[Command("image")]
internal class ImageCommand : BaseImageCommand, IDiscordCommand
{
    public ImageCommand()
    {

    }

    public string Description => "Get a fancy image. Requires one parameter to search for. Ex: fancy image cat";

    public async Task OnMessage(MessageInfo info)
    {
        if (info.Arguments.Length < 1)
        {
            await info.SendPublic("No image type provided!");
            return;
        }

        StringBuilder builder = new();

        for (int i = 0; i < info.Arguments.Length - 1; i++)
        {
            builder.Append(info.Arguments[i]).Append(' ');
        }

        builder.Append(info.Arguments[^1]);

        string type = builder.ToString();

        try
        {
            await info.SendPublic(GetRandomImage(type));
        }
        catch
        {
            await info.SendPublic($"Could not find not a single fancy image of type: {type}");
        }
    }
}
