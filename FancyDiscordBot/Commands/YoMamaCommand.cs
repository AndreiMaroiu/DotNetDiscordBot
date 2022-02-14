namespace FancyDiscordBot.Commands;

[Command("yomama")]
internal class YoMamaCommand : BaseWebCommand, IDiscordCommand
{
    private const string xpath = @"/html/body/div[1]/div/div[2]/div[1]/p/text()";
    private const string url = "http://www.jokes4us.com/yomamajokes/yomamasofatjokes.html";

    public YoMamaCommand() : base(url, xpath)
    {

    }

    public string Description => "Yo mama so fat";

    public void Init()
    {

    }

    public async Task OnMessage(DiscordClient client, MessageCreateEventArgs e, string[] arguments)
    {
        string joke = null;

        try
        {
            do
            {
                joke = GetRandomJoke();
            } while (string.IsNullOrWhiteSpace(joke));

            await client.SendMessageAsync(e.Channel, joke);
        }
        catch
        {
            Console.WriteLine($"something went wrong when trying to send joke: {joke}");
        }
    }

    private string GetRandomJoke() => GetRandomNode().InnerText;
}
