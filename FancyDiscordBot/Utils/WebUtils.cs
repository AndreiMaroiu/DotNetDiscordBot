using HtmlAgilityPack;
using System.Text;
using System.Text.Json;

namespace FancyDiscordBot.Utils;

internal class WebUtils
{
    private static JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static HtmlNodeCollection GetHtmlNodes(string url, string xpath)
    {
        using HttpClient client = new();

        string html = client.GetStringAsync(url).GetAwaiter().GetResult();

        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);

        return doc.DocumentNode.SelectNodes(xpath);
    }

    public static async Task<T> GetAsync<T>(string url)
    {
        using HttpClient http = new();
        http.DefaultRequestHeaders.Add("Accept", "application/json");

        HttpResponseMessage response = await http.GetAsync(url);
        response.EnsureSuccessStatusCode();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Request not successful!");
        }

        string message = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(message, Options);
    }

    public static async Task<TResponse> PostAsync<TResponse, TContent>(string url, TContent content)
    {
        using HttpClient http = new();
        http.DefaultRequestHeaders.Add("Accept", "application/json");

        StringContent postContent = new(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await http.PostAsync(url, postContent);
        response.EnsureSuccessStatusCode();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Request not successful!");
        }

        string message = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TResponse>(message, Options);
    }
}
