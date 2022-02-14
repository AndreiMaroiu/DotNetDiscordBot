using DSharpPlus;
using DSharpPlus.EventArgs;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FancyDiscordBot.Bot
{
    internal sealed class RegexService
    {
        private readonly (Regex, SpecialAction)[] _regexes;
        private readonly DiscordClient _client;

        public RegexService(DiscordClient client)
        {
            _client = client;

            _regexes = new (Regex, SpecialAction)[]
            {
            (new Regex(@"da+ frate+ da+", RegexOptions.Compiled), OnDaFrate),
            (new Regex(@"(\s?69\s|\s69)", RegexOptions.Compiled), On69),
            };
        }

        public async Task HandleRegex(MessageCreateEventArgs e)
        {
            string message = e.Message.Content.ToLower();

            foreach (var (regex, action) in _regexes)
            {
                Match match = regex.Match(message);
                if (match.Success && match.Length == message.Length)
                {
                    await action(e);
                    return;
                }
            }
        }

        private async Task OnDaFrate(MessageCreateEventArgs e)
        {
            await _client.SendMessageAsync(e.Channel, e.Message.Content.ToLower() + "aaaaaaa");
        }

        private async Task On69(MessageCreateEventArgs e)
        {
            await e.Message.RespondAsync("nice");
        }
    }
}
