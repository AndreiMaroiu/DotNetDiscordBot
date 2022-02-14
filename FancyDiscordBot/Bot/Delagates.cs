namespace FancyDiscordBot.Bot;

public delegate Task BotAction(DiscordClient client, MessageCreateEventArgs e, string[] arguments);
public delegate Task SpecialAction(MessageCreateEventArgs e);

