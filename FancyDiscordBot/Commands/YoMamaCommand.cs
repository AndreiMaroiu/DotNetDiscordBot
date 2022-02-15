namespace FancyDiscordBot.Commands;

[Command("yomama")]
internal class YoMamaCommand : BaseWebCommand, IDiscordCommand
{
    private const string xpath = @"/html/body/div[1]/div/div[2]/div[1]/p/text()";
    private const string url = "http://www.jokes4us.com/yomamajokes/yomamasofatjokes.html";

    public YoMamaCommand() : base(url, xpath)
    {
        //_nodes = _nodes.Where(x => !string.IsNullOrEmpty(x.InnerText));
    }

    public string Description => "Yo mama so fat";

    public async Task OnMessage(MessageInfo info)
    {
        string joke;
        do
        {
            joke = GetRandomJoke();
        } while (string.IsNullOrWhiteSpace(joke));

        await info.SendPublic(joke);
    }

    private string GetRandomJoke() => GetRandomNode().InnerText;
}
