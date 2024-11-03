using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Library.DiscordBot
{
    public class DiscordCommands : BaseCommandModule
    {
        private GameCommands gameCommands;
        public Lobby lobby = new Lobby();
        public DiscordCommands()
        {
            gameCommands = new GameCommands();
        }
        
        [Command("Test")]
        public async Task Test(CommandContext context)
        {
            await context.Channel.SendMessageAsync("Funciona");
        }
        
        [Command("jugar")] 
        public async Task AddPlayer(CommandContext context)
        {
            await lobby.AddWaitingPlayer(context);
            await lobby.TryToStartGame(context);
        }
        
        [Command("ShowCatalogue")]
        public async Task ShowCatalogue(CommandContext context)
        {
            await context.Channel.SendMessageAsync(gameCommands.ShowCatalogue());
        }
        
        [Command("StartGame")]
        public async Task StartGame(CommandContext context)
        {
            await lobby.TryToStartGame(context);
            await context.Channel.SendMessageAsync(gameCommands.ShowCatalogue());
        }
    }
}
