using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Library.DiscordBot
{
    public class DiscordCommands : ApplicationCommandModule
    {
        [SlashCommand("Test", "Verifica el estado del bot.")]
        public async Task Test(InteractionContext context)
        {
            await context.CreateResponseAsync("**Funciona** \u2705");
        }
        
        [SlashCommand("Play", "Permite jugar.")] 
        public async Task AddPlayer(InteractionContext context)
        {
            await context.CreateResponseAsync("\u231b **Iniciando partida...** \u23f3");
            var builder = new DiscordWebhookBuilder().WithContent(Lobby.GetInstance().AddWaitingPlayer(context));
            await Lobby.GetInstance().TryToStartGame(context);
            
            await context.EditResponseAsync(builder);
        }
        
        [SlashCommand("Choose", "Permite elegir un pokemon.")] 
        public async Task ChoosePokemon(InteractionContext context,
            [Option("Pokemon", "Pokemon a elegir.")] string pokemonName)
        {
            GameRoom room = Lobby.GetInstance().GetGameRoomById(context.Channel.Id);
            await context.CreateResponseAsync("**Buscando Pokemon...**");
            var builder = new DiscordWebhookBuilder();
            
            if (room != null!)
            {
                if (room.Commands.GameHasStarted())
                {
                    builder.WithContent("\u26d4  **LA BATALLA YA HA COMENZADO**  \u26d4");
                }
                else
                {
                    var (message, imageUrl) = await room.Commands.ChoosePokemon(
                        context.Member.Username, 
                        pokemonName.ToLower());
                
                    builder.WithContent(message);
                
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        var embed = new DiscordEmbedBuilder()
                            .WithImageUrl(imageUrl)
                            .WithColor(DiscordColor.Red); 

                        builder.AddEmbed(embed);
                    }

                    string turn = room.Commands.ShowTurn();
                    if (turn != "") await context.Channel.SendMessageAsync(turn);
                }
               
            }
            else
            {
                builder.WithContent("\u26d4  **DEBES ESTAR EN EL CANAL DE BATALLA.**  \u26d4");
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
            await context.CreateResponseAsync("**Iniciando juego, por favor espera...**");
            
            Lobby lobby = Lobby.GetInstance();
            GameRoom room = lobby.GetGameRoomById(context.Channel.Id);
            var builder = new DiscordWebhookBuilder();
            
            if (room != null!)
            {
                builder.WithContent(room.Commands.StartBattle());
            }
            else
            {
                builder.WithContent("\u26d4  **DEBES ESTAR EN EL CANAL DE BATALLA PARA INICIAR LA PARTIDA.**  \u26d4");
            }
            
            await context.EditResponseAsync(builder);
        }
        
        [SlashCommand("Attack", "Ataca a un pokemon enemigo.")]
        public async Task Attack(InteractionContext context,
            [Option("MoveSlot", "Movimiento a utilizar.")] string moveSlot)
        {
            GameRoom room = Lobby.GetInstance().GetGameRoomById(context.Channel.Id);
            
            if (room == null!)
            {
                await context.CreateResponseAsync("\u26d4  **DEBES ESTAR EN EL CANAL DE BATALLA PARA ATACAR UN POKEMON.**  \u26d4");
                return;
            }
            
            await context.CreateResponseAsync(room.Commands.Attack(int.Parse(moveSlot) - 1, context.User.Username));
        }
        
        [SlashCommand("NextTurn", "Cambia el turno de la partida.")]
        public async Task NextTurn(InteractionContext context)
        {
            GameRoom room = Lobby.GetInstance().GetGameRoomById(context.Channel.Id);
            
            if (room == null!)
            {
                await context.CreateResponseAsync("\u26d4  **DEBES ESTAR EN EL CANAL DE BATALLA.**  \u26d4");
                return;
            }

            
            await context.CreateResponseAsync(room.Commands.NextTurn());
        }

        [SlashCommand("ClearRooms", "Iniciar nueva partida.")]
        public async Task ClearRooms(InteractionContext context)
        {
            await context.CreateResponseAsync("\u26d4  **Eliminando todas los servidores de batalla...**  \u26d4");
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
            await context.CreateResponseAsync("\u23f3  **Buscando Pokemon...** \u23f3");
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
                    builder.WithContent("\u26d4  **DEBE ESPERAR TU TURNO.**  \u26d4");
                }
            }
            else
            {
                builder.WithContent("\u26d4  **DEBES ELEGIR EN EL CANAL DE BATALLA.**  \u26d4");
            }
            await context.EditResponseAsync(builder);
        }
        
        [SlashCommand("Item", "Utiliza un item pasandole un índice y el nombre del pokemon.")]
        public async Task UseItem(InteractionContext context,
            [Option("ItemIndex", "Index del item a usar.")] string itemIndex,
            [Option("PokemonName", "Nombre del pokemon que va a utilizar el item.")] string pokemonName)
        {
            Lobby lobby = Lobby.GetInstance();
            GameRoom room = lobby.GetGameRoomById(context.Channel.Id);
            await context.CreateResponseAsync("\u23f3  **Buscando Pokemon...** \u23f3");
            var builder = new DiscordWebhookBuilder();

            if (room != null!)
            {
                if (!room.Commands.GameHasStarted())
                {
                    builder.WithContent("\u26d4  **EL JUEGO NO HA EMPEZADO**  \u26d4");
                }
                else
                {
                    Player playerInTurn = room.Commands.GetPlayerInTurn();
                    if (context.Member.Username == playerInTurn.Name)
                    {
                        if (room.Commands.GetPlayerInTurn().GetPokemonByName(pokemonName) == null!)
                        {
                            builder.WithContent("\u26d4  **El pokemon no ha sido encontrado**  \u26d4");
                        }
                        else
                        {
                            builder.WithContent(room.Commands.UseItem(playerInTurn, pokemonName, Int32.Parse(itemIndex)-1));
                        }
                    }
                    else
                    {
                        builder.WithContent("Espera tu turno.");
                    }
                }
            }
            else
            {
                builder.WithContent("\u26d4  **DEBES ELEGIR EN EL CANAL DE BATALLA.**  \u26d4");
            }
            await context.EditResponseAsync(builder);
        }
        
        [SlashCommand("ShowPokemons", "Muestra los pokemons aliados y rivales.")]
        public async Task ShowPokemons(InteractionContext context)
        {
            await context.CreateResponseAsync("\u231b **Iniciando juego, por favor espera...** \u23f3");

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
            await context.CreateResponseAsync("\u231b **Realizando comprobaciones para resetear...** \u23f3");

            Lobby lobby = Lobby.GetInstance();
            GameRoom room = lobby.GetGameRoomById(context.Channel.Id);
            var builder = new DiscordWebhookBuilder();
            
            if (room != null!)
            {
                builder.WithContent($"{room.Commands.RestartGame()}");
            }
            else
            {
                builder.WithContent("\u26d4  **DEBES ELEGIR EN EL CANAL DE BATALLA.**  \u26d4");
            }
            
            await context.EditResponseAsync(builder);
        }
        
        [SlashCommand("WaitList", "Muestra los jugadores esperando una partida.")]
        public async Task WaitList(InteractionContext context)
        {
            await context.CreateResponseAsync("\u231b **Buscando jugadores...** \u23f3");
            Lobby lobby = Lobby.GetInstance();

            var builder = new DiscordWebhookBuilder();

            string msgListPlayers = lobby.GetWaitingPlayersName();
            if (msgListPlayers == "")
            {
                builder.WithContent("**NO SE HAN ENCONTRADO JUGADORES ESPERANDO PARTIDA** \ud83e\udd7a");
            }
            else
            {
                builder.WithContent($"**LISTA DE JUGADORES ESPERANDO PARTIDA:**\n{msgListPlayers}");
            }
            
            await context.EditResponseAsync(builder);
        }
        
        [SlashCommand("ShowItems", "Muestra los pokemons aliados y rivales.")]
        public async Task ShowItems(InteractionContext context)
        {
            await context.CreateResponseAsync(GameCommands.ShowItemsDesc());
        }
    }
}
