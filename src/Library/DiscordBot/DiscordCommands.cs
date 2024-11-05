using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Library.DiscordBot
{
    public class DiscordCommands : ApplicationCommandModule
    {
        [SlashCommand("Test", "Verifica el estado del bot.")]
        public async Task Test(InteractionContext context)
        {
            await context.CreateResponseAsync("Funciona");
        }
        
        [SlashCommand("Play", "Permite jugar.")] 
        public async Task AddPlayer(InteractionContext context)
        {
            await context.CreateResponseAsync("Iniciando partida...");
            var builder = new DiscordWebhookBuilder().WithContent(Lobby.GetInstance().AddWaitingPlayer(context));
            await Lobby.GetInstance().TryToStartGame(context);
            
            await context.EditResponseAsync(builder);
        }
        
        [SlashCommand("Choose", "Permite jugar.")] 
        public async Task ChoosePokemon(InteractionContext context,
            [Option("Pokemon", "Pokémon a elegir")] string pokemonName)
        {
            Lobby lobby = Lobby.GetInstance();
            GameRoom room = lobby.GetGameRoomById(context.Channel.Id);
            await context.CreateResponseAsync("Buscando Pokemon...");
            var builder = new DiscordWebhookBuilder();
            
            if (room != null!)
            {
                builder.WithContent(await room.commands.ChoosePokemon(context.Member.Username, pokemonName.ToLower()));
            }
            else
            {
                builder.WithContent("Debe elegir en el canal de batalla");
            }
            await context.EditResponseAsync(builder);
        }
        
        
        [SlashCommand("ShowCatalogue", "Muestra un link al catálogo de los pokemons.")]
        public async Task ShowCatalogue(InteractionContext context)
        {
            await context.CreateResponseAsync($"Catálogo: {GameCommands.ShowCatalogue()}");
        }
        
        [SlashCommand("StartGame", "Iniciar nueva partida.")]
        public async Task StartGame(InteractionContext context)
        {
            await context.CreateResponseAsync("Iniciando juego, por favor espera...");
            
            Lobby lobby = Lobby.GetInstance();
            GameRoom room = lobby.GetGameRoomById(context.Channel.Id);
            var builder = new DiscordWebhookBuilder();
            
            if (room != null!)
            {
                builder.WithContent(room.commands.StartBattle());
            }
            else
            {
                builder.WithContent("Debe estar en el canal de batalla para iniciarla");
            }
            
            await context.EditResponseAsync(builder);
        }

        [SlashCommand("ClearRooms", "Iniciar nueva partida.")]
        public async Task ClearRooms(InteractionContext context)
        {
            await context.CreateResponseAsync("Eliminando todas los servidores de batalla...");
            await Lobby.GetInstance().ClearLobby(context);
            
            var builder = new DiscordWebhookBuilder().WithContent("Servidores de batalla eliminados correctamente");
            await context.EditResponseAsync(builder);
        }
    }
}
