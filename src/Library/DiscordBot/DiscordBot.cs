using DSharpPlus;
using DSharpPlus.SlashCommands;

namespace Library.DiscordBot
{
    public class DiscordBot
    {
        private DiscordClient Client;
        private SlashCommandsExtension SlashCommands;

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
            
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}