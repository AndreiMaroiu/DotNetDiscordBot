using FancyDiscordBot.Bot;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

CancellationTokenSource source = new CancellationTokenSource();

DiscordClient client = new DiscordClient(new DiscordConfiguration()
{
    Token = config["discordtoken"],
    TokenType = TokenType.Bot,
    Intents = DiscordIntents.AllUnprivileged | DiscordIntents.GuildMembers,
});

BotService service = new(client, "fancy", config);

client.MessageCreated += async (sender, e) =>
{
    service.OnMessage(e);
};

client.GuildMemberAdded += async (sender, e) =>
{
    service.OnNewMember(e);
};

client.GuildMemberRemoved += async (sender, e) =>
{
    service.OnUserLeave(e);
};

CancellationToken token = source.Token;

Console.WriteLine("Tring to connect to client!");
await client.ConnectAsync();
Console.WriteLine("Discord connected!");

while (!token.IsCancellationRequested)
{
    await Task.Delay(500);
}

await client.DisconnectAsync();

Console.WriteLine("Discord bot disconnected!");