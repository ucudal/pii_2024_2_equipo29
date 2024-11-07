using System.Reflection.Metadata.Ecma335;
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
            [Option("Pokemon", "Pokémon a elegir.")] string pokemonName)
        {
            GameRoom room = Lobby.GetInstance().GetGameRoomById(context.Channel.Id);
            await context.CreateResponseAsync("Buscando Pokemon...");
            var builder = new DiscordWebhookBuilder();
            
            if (room != null!)
            {
                builder.WithContent(await room.Commands.ChoosePokemon(context.Member.Username, pokemonName.ToLower()));
            }
            else
            {
                builder.WithContent("Debe elegir en el canal de batalla.");
            }
            await context.EditResponseAsync(builder);
        }
        
        
        [SlashCommand("ShowCatalogue", "Muestra un link al catálogo de los pokemons.")]
        public async Task ShowCatalogue(InteractionContext context)
        {
            await context.CreateResponseAsync(GameCommands.ShowCatalogue());
        }
        
        [SlashCommand("Start", "Iniciar nueva partida.")]
        public async Task StartGame(InteractionContext context)
        {
            await context.CreateResponseAsync("Iniciando juego, por favor espera...");
            
            Lobby lobby = Lobby.GetInstance();
            GameRoom room = lobby.GetGameRoomById(context.Channel.Id);
            var builder = new DiscordWebhookBuilder();
            
            if (room != null!)
            {
                builder.WithContent(room.Commands.StartBattle());
            }
            else
            {
                builder.WithContent("Debe estar en el canal de batalla para iniciarla.");
            }
            
            await context.EditResponseAsync(builder);
        }
        
        [SlashCommand("Attack", "Ataca a un pokemon enemigo.")]
        public async Task Attack(InteractionContext context,
            [Option("MoveSlot", "Movimiento a utilizar.")] string moveSlot)
        {
            GameRoom room = Lobby.GetInstance().GetGameRoomById(context.Channel.Id);
            
            if (room == null)
            {
                await context.CreateResponseAsync("Debe estar en el canal de batalla para atacar un pokemon.");
                return;
            }
            
            await context.CreateResponseAsync(room.Commands.Attack(int.Parse(moveSlot) - 1, context.User.Username));
        }
        
        [SlashCommand("NextTurn", "Cambia el turno de la partida.")]
        public async Task NextTurn(InteractionContext context)
        {
            GameRoom room = Lobby.GetInstance().GetGameRoomById(context.Channel.Id);
            
            if (room == null)
            {
                await context.CreateResponseAsync("Debe estar en el canal de batalla para atacar un pokemon.");
                return;
            }

            
            await context.CreateResponseAsync(room.Commands.NextTurn());
        }

        [SlashCommand("ClearRooms", "Iniciar nueva partida.")]
        public async Task ClearRooms(InteractionContext context)
        {
            await context.CreateResponseAsync("Eliminando todas los servidores de batalla...");
            await Lobby.GetInstance().ClearLobby(context);
            
            var builder = new DiscordWebhookBuilder().WithContent("Servidores de batalla eliminados correctamente.");
            await context.EditResponseAsync(builder);
        }
        
        [SlashCommand("Change", "Cambia de pokemon.")]
        public async Task ChangePokemon(InteractionContext context,
            [Option("Pokemon", "Pokemon a cambiar")] string pokemonName)
        {
            Lobby lobby = Lobby.GetInstance();
            GameRoom room = lobby.GetGameRoomById(context.Channel.Id);
            await context.CreateResponseAsync("Buscando Pokemon...");
            var builder = new DiscordWebhookBuilder();

            if (room != null!)
            {
                Player playerInTurn = room.Commands.GetPlayerInTurn();
                if (context.Member.Username == playerInTurn.Name)
                {
                    builder.WithContent(room.Commands.ChangePokemon(playerInTurn, pokemonName.ToLower()));
                }
                else
                {
                    builder.WithContent("Espera a tu turno.");
                }

            }
            else
            {
                builder.WithContent("Debe elegir en el canal de batalla.");
            }
            await context.EditResponseAsync(builder);
        }
        
        [SlashCommand("Item", "Utiliza un item pasandole un índice.")]
        public async Task UseItem(InteractionContext context,
            [Option("ItemIndex", "Index del item a usar.")] string itemIndex)
        {
            Lobby lobby = Lobby.GetInstance();
            GameRoom room = lobby.GetGameRoomById(context.Channel.Id);
            await context.CreateResponseAsync("Buscando Pokemon...");
            var builder = new DiscordWebhookBuilder();

            if (room != null!)
            {
                Player playerInTurn = room.Commands.GetPlayerInTurn();
                if (context.Member.Username == playerInTurn.Name)
                {
                    builder.WithContent(room.Commands.UseItem(playerInTurn, Int32.Parse(itemIndex)));
                }
                else
                {
                    builder.WithContent("Espera a tu turno.");
                }
            }
            else
            {
                builder.WithContent("Debe elegir en el canal de batalla.");
            }
            await context.EditResponseAsync(builder);
        }
        
        [SlashCommand("ShowPokemons", "Muestra los pokemons aliados y rivales.")]
        public async Task ShowPokemons(InteractionContext context)
        {
            await context.CreateResponseAsync("Iniciando juego, por favor espera...");

            Lobby lobby = Lobby.GetInstance();
            GameRoom room = lobby.GetGameRoomById(context.Channel.Id);
            var builder = new DiscordWebhookBuilder();

            if (room != null!)
            {
                builder.WithContent(room.Commands.ViewPokemons());
            }
            else
            {
                builder.WithContent("Debe estar en un canal de batalla.");
            }

            await context.EditResponseAsync(builder);
        }
        
        [SlashCommand("Restart", "Resetea la partida.")]
        public async Task Restart(InteractionContext context)
        {
            await context.CreateResponseAsync("Realizando comprobaciones para resetear...");

            Lobby lobby = Lobby.GetInstance();
            GameRoom room = lobby.GetGameRoomById(context.Channel.Id);
            var builder = new DiscordWebhookBuilder();
            
            if (room != null!)
            {
                builder.WithContent($"{room.Commands.RestartGame()}\n{GameCommands.ShowCatalogue()}");
            }
            else
            {
                builder.WithContent("Debe estar en el canal de batalla para iniciarla.");
            }
            
            await context.EditResponseAsync(builder);
        }
    }
}
