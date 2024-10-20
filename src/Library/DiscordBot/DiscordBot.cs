using DSharpPlus.CommandsNext;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Library.DiscordBot
{
    public class DiscordBot
    {
        private static DiscordClient Client { get; set; }
        private static CommandsNextExtension Commands { get; set; }

        public async Task Iniciate()
        {
            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = "MTI5NzM4OTIxMzkyMjM2MTM5Ng.GGcSvR.gRqbmVD4jB6l-udmm88M9FepICaYp-2la_7DAI",
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(discordConfig);

            Client.Ready += Client_Ready;

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = ["/"],
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<DiscordCommands>();

            await Client.ConnectAsync();
            await Task.Delay(-1);

        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }

    }
}
