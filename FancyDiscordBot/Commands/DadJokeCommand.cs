using FancyDiscordBot.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace FancyDiscordBot.Commands;

[Command("dadjoke")]
internal class DadJokeCommand : IDiscordCommand
{
    private const string FilePath = "./Files/dadjokes.txt";
    private readonly IConfiguration _config;

    public DadJokeCommand(IConfiguration configuration)
    {
        _config = configuration;
    }

    public string Description => "Get a random fancy dad joke!";

    public void Init()
    {

    }

    public async Task OnMessage(DiscordClient client, MessageCreateEventArgs e, string[] arguments)
    {
        using HttpClient http = new();
        http.DefaultRequestHeaders.Add("Accept", "application/json");
        http.DefaultRequestHeaders.Add("x-rapidapi-host", "dad-jokes.p.rapidapi.com");
        http.DefaultRequestHeaders.Add("x-rapidapi-key", _config["dadjokekey"]);

        try
        {
            HttpResponseMessage response = await http.GetAsync("https://dad-jokes.p.rapidapi.com/random/joke");
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Request not successful");
            }

            string message = await response.Content.ReadAsStringAsync();

            DadJoke dict = JsonSerializer.Deserialize<DadJoke>(message, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            DadJokeBody body = dict.Body[0];
            string content = $"{body.Setup} {body.Punchline}";

            await client.SendMessageAsync(e.Channel, content);
        }
        catch
        {
            await client.SendMessageAsync(e.Channel, "Sorry, I could not find a joke, try again later or tomorrow");
        }
    }
}