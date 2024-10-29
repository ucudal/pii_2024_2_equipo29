using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Library.DiscordBot
{
    public class DiscordCommands : BaseCommandModule
    {
        private GameCommands gameCommands;
        public DiscordCommands()
        {
            gameCommands = new GameCommands();
        }
        
        [Command("Test")]
        public async Task Test(CommandContext context)
        {
            await context.Channel.SendMessageAsync("Funciona");
        }
        
        [Command("ShowCatalogue")]
        public async Task ShowCatalogue(CommandContext context)
        {
            await context.Channel.SendMessageAsync(gameCommands.ShowCatalogue());
        }
        
        [Command("ShowCatalogue")]
        public async Task StartGame(CommandContext context)
        {
            gameCommands.StartGame();
            await context.Channel.SendMessageAsync(gameCommands.ShowCatalogue());
        }
    }
}
