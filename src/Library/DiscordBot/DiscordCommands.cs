using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Library.DiscordBot
{
    public class DiscordCommands : ApplicationCommandModule
    {
        private GameCommands gameCommands;
        
        public DiscordCommands()
        {
            gameCommands = new GameCommands();
        }
        
        [SlashCommand("Test", "Verifica el estado del bot.")]
        public async Task Test(InteractionContext context)
        {
            await context.CreateResponseAsync("Funciona");
        }
        
        [SlashCommand("Play", "Permite jugar.")] 
        public async Task AddPlayer(InteractionContext context)
        {
            await context.CreateResponseAsync("Iniciando partida...");
            await Lobby.GetInstance().AddWaitingPlayer(context);
            await Lobby.GetInstance().TryToStartGame(context);
            
            var builder = new DiscordWebhookBuilder().WithContent($"Juego iniciado.");
            await context.EditResponseAsync(builder);
        }
        
        [SlashCommand("ShowCatalogue", "Muestra un link al catálogo de los pokemons.")]
        public async Task ShowCatalogue(InteractionContext context)
        {
            await context.CreateResponseAsync($"Catálogo: {gameCommands.ShowCatalogue()}");
        }
        
        [SlashCommand("StartGame", "Iniciar nueva partida.")]
        public async Task StartGame(InteractionContext context)
        {
            await context.CreateResponseAsync("Iniciando juego, por favor espera...");
            gameCommands = new GameCommands();
            gameCommands.StartGame();
            
            string catalogue = gameCommands.ShowCatalogue();
            var builder = new DiscordWebhookBuilder().WithContent($"Juego iniciado. Catálogo: {catalogue}");
            await context.EditResponseAsync(builder);
        }
    }
}
