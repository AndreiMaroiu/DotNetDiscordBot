using HtmlAgilityPack;

namespace FancyDiscordBot.Utils;

internal class WebUtils
{
    public static HtmlNodeCollection GetHtmlNodes(string url, string xpath)
    {
        using HttpClient client = new();

        string html = client.GetStringAsync(url).GetAwaiter().GetResult();

        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);

        return doc.DocumentNode.SelectNodes(xpath);
    }
}
