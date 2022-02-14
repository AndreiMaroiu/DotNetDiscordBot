using HtmlAgilityPack;

namespace FancyDiscordBot.Commands;

internal class BaseImageCommand : BaseWebCommand
{
    private const string xpath = "/html/body/div[3]/table/tr";
    private const string NoImageErrorMessage = "Could not find image";

    public BaseImageCommand()
    {

    }

    public BaseImageCommand(string imageType) : base(GetUrl(imageType), xpath)
    {

    }

    private static string GetUrl(string imageType)
        => $"https://www.google.com/search?q={imageType}&tbm=isch&ved=2ahUKEwjJ47XdmvD1AhVVOsAKHZSKDsoQ2-cCegQIABAA&oq=waifu&gs_lcp=CgNpbWcQA1C6B1icDGCoDmgAcAB4AIABiAGIAfMFkgEDMC42mAEAoAEBqgELZ3dzLXdpei1pbWfAAQE&sclient=img&ei=nm8CYsmcI9X0gAaUlbrQDA&bih=937&biw=1920";


    protected string GetRandomImage()
    {
        HtmlNode node = GetRandomNode();
        HtmlAttributeCollection attributes = node.SelectSingleNode("td[1]/div/div/div/div/table/tr[1]/td/a/div/img").Attributes;
        return attributes.First(x => x.Name == "src").Value;
    }

    protected string GetRandomImage(string imageType)
    {
        HtmlNode row = GetRandomNode(GetUrl(imageType), xpath);

        if (row is null)
        {
            throw new Exception(NoImageErrorMessage);
        }

        // relative xpath should not start with "/"
        HtmlNode image = GetRandomeNode(row.SelectNodes("td"));

        if (image is null)
        {
            throw new Exception(NoImageErrorMessage);
        }

        HtmlAttributeCollection attributes = image.SelectSingleNode("div/div/div/div/table/tr[1]/td/a/div/img").Attributes;
        return attributes.First(x => x.Name == "src").Value;
    }
}
