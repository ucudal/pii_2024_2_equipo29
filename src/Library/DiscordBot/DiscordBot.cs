using DSharpPlus;
using DSharpPlus.SlashCommands;

namespace Library.DiscordBot
{
    public class DiscordBot
    {
        private static DiscordClient Client { get; set; }
        private static SlashCommandsExtension SlashCommands { get; set; }

        public async Task Iniciate()
        {
            var token = Environment.GetEnvironmentVariable("DISCORD_TOKEN") 
                        ?? throw new Exception("No se ha encontrado el token del bot de discord.");

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All, 
                Token = token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(discordConfig);
            
            SlashCommands = Client.UseSlashCommands();
            SlashCommands.RegisterCommands<DiscordCommands>();
            
            Client.Ready += Client_Ready;
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}