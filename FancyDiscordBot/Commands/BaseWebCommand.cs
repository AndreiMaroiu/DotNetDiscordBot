using FancyDiscordBot.Utils;
using HtmlAgilityPack;

namespace FancyDiscordBot.Commands;

internal abstract class BaseWebCommand
{
    protected HtmlNodeCollection _nodes = null;
    private readonly Random _random = new();

    public BaseWebCommand()
    {

    }

    public BaseWebCommand(string url, string xpath)
    {
        _nodes = WebUtils.GetHtmlNodes(url, xpath);
    }

    protected HtmlNode GetRandomNode(string url, string xpath)
    {
        HtmlNodeCollection nodes = WebUtils.GetHtmlNodes(url, xpath);

        if (nodes is not null)
        {
            return nodes[_random.Next(nodes.Count)];
        }

        return null;
    }

    protected HtmlNode GetRandomNode()
    {
        return _nodes[_random.Next(_nodes.Count)];
    }

    protected HtmlNode GetRandomeNode(HtmlNodeCollection nodes)
    {
        if (nodes is null || nodes.Count == 0)
        {
            return null;
        }

        return nodes[_random.Next(nodes.Count)];
    }
}