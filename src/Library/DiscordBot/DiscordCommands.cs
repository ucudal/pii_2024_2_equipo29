using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Library.DiscordBot
{
    /// <summary>
    /// Clase para la creación de comandos del bot de discord utilizada en <c>DiscordBot</c>.
    /// </summary>
    public class DiscordCommands : ApplicationCommandModule
    {
        /// <summary>
        /// Comando de prueba para verificar si el bot está activo.
        /// </summary>
        /// <param name="context">El contexto del usuario y el lugar donde fue utilizado el comando.</param>
        [SlashCommand("Test", "Verifica el estado del bot.")]
        public async Task Test(InteractionContext context)
        {
            await context.CreateResponseAsync("**Funciona** \u2705");
        }

        /// <summary>
        /// Comando que permite agregar jugadores a la sala de espera utilizando <c>Lobby</c>.
        /// Intenta iniciar una partida cada vez que es ejecutado.
        /// </summary>
        /// <param name="context">
        /// El contexto del usuario y el lugar donde fue utilizado el comando.
        /// </param>
        /// <remarks>
        /// El jugador que ejecuta el comando se agrega a la sala de espera para participar en la partida.
        /// Tras agregar al jugador a la sala de espera, se intenta iniciar la partida si hay suficientes jugadores.
        /// Si el intento de iniciar la partida tiene éxito, se enviará una respuesta por DM de discord y en el canal
        /// donde se ejecutó el comando mencionando el canal de batalla y los nombre de los jugadores emparejados.
        /// El sistema empareja jugadores en salas de batalla privadas, para evitar el desbordamiento de mensajes en un canal público.
        /// Si no hay suficientes jugadores para iniciar la partida, el sistema continuará esperando más jugadores.
        /// </remarks>
        [SlashCommand("Play", "Permite unirte a la sala de espera.")] 
        public async Task AddPlayer(InteractionContext context)
        {
            await context.CreateResponseAsync("\u231b **Iniciando partida...** \u23f3");
            var builder = new DiscordWebhookBuilder().WithContent(Lobby.GetInstance().AddWaitingPlayer(context));
            await Lobby.GetInstance().TryToStartGame(context);
            
            await context.EditResponseAsync(builder);
        }
        
        /// <summary>
        /// Comando para elegir un pokemon en la sala de batalla.
        /// </summary>
        /// <param name="context">
        /// El contexto del usuario y el lugar donde fue utilizado el comando.
        /// </param>
        /// <param name="pokemonName">
        /// Nombre del pokemon a elegir.
        /// <list type="bullet">
        ///   <item>
        ///     El nombre del pokemon debe estar disponible en el catálogo y no debe estar baneado (en el catálogo aparece si fue baneado).
        ///     En caso de no estar disponible o estar baneado, se envía un mensaje diciendo que el pokemon no fue encontrado.
        ///   </item>
        ///   <item>
        ///     Si el nombre del Pokémon no es válido o no está disponible, se enviará un mensaje de error.
        ///   </item>
        /// </list>
        /// </param>
        /// <remarks>
        /// <para>
        ///     Un pokemon es baneado cuando se cumple la siguiente condición:
        ///     <list type="bullet">
        ///         <item>
        ///             Cuando no tiene al menos cuatro movimientos. Esto se debe a que tres movimientos se toman como <c>Move</c> del tipo normal y uno para <c>Move</c> del tipo especial.
        ///         </item>
        ///     </list>
        /// </para>
        /// Requisitos para la ejecución exitosa del comando:
        /// <list type="bullet">
        ///   <item>
        ///     El usuario debe estar en un canal de batalla donde haya una sala activa.
        ///     Si no se encuentra en un canal adecuado, se enviará un mensaje de error indicando que debe estar en un canal de batalla.
        ///   </item>
        ///   <item>
        ///     El juego no debe haber comenzado aún. Si la batalla ya ha iniciado, se enviará un mensaje de error.
        ///   </item>
        ///   <item>
        ///     Si el Pokémon elegido es válido y la batalla no ha comenzado, se confirma la elección del Pokémon y, si aplica,
        ///     se muestra una imagen relacionada con el Pokémon elegido.
        ///   </item>
        ///   <item>
        ///     Se muestra el turno actual de la partida, si está disponible.
        ///   </item>
        /// </list>
        /// </remarks>
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
        
        /// <summary>
        /// Muestra un link al catálogo de pokemons permitidos en el juego.
        /// </summary>
        /// <param name="context">El contexto del usuario y el lugar donde fue utilizado el comando.</param>
        [SlashCommand("ShowCatalogue", "Muestra un link al catálogo de pokemons.")]
        public async Task ShowCatalogue(InteractionContext context)
        {
            await context.CreateResponseAsync(GameCommands.ShowCatalogue());
        }
        
        /// <summary>
        /// Comando para iniciar la partida en una sala de batalla.
        /// </summary>
        /// <param name="context">
        /// El contexto del usuario y el lugar donde fue utilizado el comando.
        /// </param>
        /// <remarks>
        /// El juego iniciará automáticamente si:
        /// <list type="bullet">
        ///     <item>
        ///        Todos los jugadores tienen la cantidad de MaxPokemons establecida en <c>Player</c>. Por defecto, 6.
        ///     </item>
        /// </list>
        /// <para>
        ///     Este comando fue creado con la finalidad de permitir a los jugadores realizar batallas con menos pokemon.
        ///     Por ejemplo, en el caso de que uno de los jugadores quiera jugar con un pokemon
        ///     y otro jugador con tres pokemons podrán hacerlo gracias a este comando.
        /// </para>
        /// <para>
        ///     Requisitos para la ejecución exitosa del comando:
        /// </para>
        /// <list type="bullet">
        ///   <item>
        ///     El usuario debe estar en un canal de batalla.
        ///     De lo contrario, se envía un mensaje de error indicando que el usuario debe estar en un canal de batalla.
        ///   </item>
        ///   <item>
        ///     Los jugadores deben elegir al menos un pokemon cada uno.
        ///     De lo contrario, se enviara un mensaje de error.
        ///   </item>
        ///   <item>
        ///     Si la sala de juego no está activa, se notifica que el usuario debe estar en un canal de batalla para poder iniciar el juego.
        ///   </item>
        /// </list>
        /// </remarks>
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
        
        /// <summary>
        /// Comando para atacar a un pokemon enemigo durante una batalla.
        /// </summary>
        /// <param name="context">
        /// El contexto del usuario y el lugar donde fue utilizado el comando.
        /// </param>
        /// <param name="moveSlot">
        /// Número que indica el movimiento a utilizar. Se debe enviar el valor 1, 2, 3 o 4.
        /// <list type="bullet">
        ///   <item>El valor debe ser un número entero entre 1 y 4, donde cada número corresponde a un movimiento específico.</item>
        ///   <item>Si el número no está dentro de este rango, se envía un mensaje de error.</item>
        /// </list>
        /// </param>
        /// <remarks>
        /// Requisitos para la ejecución exitosa del comando:
        /// <list type="bullet">
        ///   <item>
        ///     El usuario debe estar en un canal de batalla. De lo contrario,
        ///     se envía un mensaje de error indicando que el usuario debe estar en un canal de batalla.
        ///   </item>
        ///   <item>
        ///     El juego debe estar en curso. Si la batalla no está en progreso, se envía un mensaje de error.
        ///   </item>
        ///   <item>
        ///     Si el comando se ejecuta correctamente, se cambia el turno y se muestra un mensaje de confirmación.
        ///   </item>
        /// </list>
        /// </remarks>
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
        
        /// <summary>
        /// Comando para cambiar el turno de la partida.
        /// </summary>
        /// <param name="context">
        /// El contexto del usuario y el lugar donde fue utilizado el comando.
        /// </param>
        /// <remarks>
        /// El juego cambiará de turno automáticamente si ocurre una de las siguientes acciones:
        /// <list type="bullet">
        ///     <item>
        ///        Se ejecuta el comando <c>/Change</c> de forma exitosa.
        ///     </item>
        ///     <item>
        ///        Se ejecuta el comando <c>/Attack</c> de forma exitosa.
        ///     </item>
        ///     <item>
        ///        Se ejecuta el comando <c>/Item</c> de forma exitosa.
        ///     </item>
        /// </list>
        /// <para>
        ///     Este comando fue creado a causa del siguiente problema, cuando un pokemon de un jugador se encuentra afectado por <c>Sleep</c>,
        ///     se quedó sin pociones y a su vez es el único pokemon que tiene disponible el juego no cambiará el turno de forma automática,
        ///     porque no se cumplen ningunas de las 3 condiciones presentadas en la lista de forma exitosa.
        /// </para>
        /// <para>Requisitos para la ejecución exitosa del comando:</para>
        /// <list type="bullet">
        ///   <item>
        ///     El usuario debe estar en un canal de batalla. De lo contrario,
        ///     se envía un mensaje de error indicando que el usuario debe estar en un canal de batalla.
        ///   </item>
        ///   <item>
        ///     El juego debe estar en curso para que el turno pueda ser cambiado.
        ///     Si la partida no está iniciada, no se podrá realizar la acción.
        ///   </item>
        ///   <item>
        ///     Si el comando se ejecuta correctamente, se cambia el turno y se muestra un mensaje de confirmación.
        ///   </item>
        /// </list>
        /// </remarks>
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

        /// <summary>
        /// Comando para eliminar todas las salas de las partidas en curso.
        /// <para>---> ESTO SERÁ AUTOMATIZADO PARA LA PROXIMA ENTREGA.</para>
        /// </summary>
        /// <param name="context">
        /// El contexto del usuario y el lugar donde fue utilizado el comando.
        /// </param>
        /// <remarks>
        /// Una vez ejecutado el comando, se eliminarán todas las salas de batalla activas en <c>Lobby</c>
        /// y se enviará un mensaje confirmando que los canales de batalla fueron eliminados.
        /// <para>En caso de error durante el proceso, se enviará un mensaje notificando al usuario.</para>
        /// </remarks>
        [SlashCommand("ClearRooms", "Elimina las salas de las partidas en curso.")]
        public async Task ClearRooms(InteractionContext context)
        {
            await context.CreateResponseAsync("\u26d4  **Eliminando todas los servidores de batalla...**  \u26d4");
            await Lobby.GetInstance().ClearLobby(context);
            
            var builder = new DiscordWebhookBuilder().WithContent("Servidores de batalla eliminados correctamente.");
            await context.EditResponseAsync(builder);
        }
        
        /// <summary>
        /// Comando para cambiar el pokemon seleccionado durante la batalla.
        /// </summary>
        /// <param name="context">
        /// El contexto del usuario y el lugar donde fue utilizado el comando.
        /// </param>
        /// <param name="pokemonName">
        /// Nombre del pokemon a cambiar:
        /// <list type="bullet">
        ///   <item>
        ///     Debe existir en la lista de pokemons elegidos por el jugador al inicio de la batallao.
        ///     En caso contrario, no se encontrará el pokemon y se enviará un mensaje de error.
        ///   </item>
        /// </list>
        /// </param>
        /// <remarks>
        /// Requisitos para la ejecución exitosa del comando:
        /// <list type="bullet">
        ///   <item>
        ///     La sala de juego debe existir en el canal donde se ejecutó el comando; de lo contrario, se enviará un mensaje indicando que el usuario debe estar en un canal de batalla.
        ///   </item>
        ///   <item>
        ///     Solo el jugador en turno puede ejecutar el comando; si no es su turno, se enviará un mensaje indicando que debe esperar su turno.
        ///   </item>
        ///   <item>
        ///     Si los parámetros son correctos y es el turno del jugador, el pokemon será cambiado exitosamente y se enviará una confirmación.
        ///   </item>
        /// </list>
        /// </remarks>
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
        
        /// <summary>
        /// Comando para usar un item sobre un pokemon en una sala de batalla.
        /// </summary>
        /// <param name="context">
        /// El contexto del usuario y el lugar donde fue utilizado el comando.
        /// </param>
        /// <param name="itemIndex">
        /// El índice del item que se va a utilizar:
        /// <list type="bullet">
        ///   <item>Debe ser un índice válido en la lista de items del jugador en turno.</item>
        ///   <item>El item no debe estar agotado.</item>
        ///   <item>Debe tomar valores de 1, 2 o 3.</item>
        ///   <item>En caso de no encontrar la poción o que se halla agotado, envía un mensaje del error.</item>
        /// </list>
        /// </param>
        /// <param name="pokemonName">
        /// El nombre del pokemon en el cual se utilizará el item:
        /// <list type="bullet">
        ///   <item>Debe ser uno de los pokemons seleccionados por el jugador al inicio de la batalla.</item>
        ///   <item>El pokemon debe estar vivo para usar las pociones de FullHeal y SuperPotion. En el caso de Revive debe estar muerto.</item>
        ///   <item>En caso de no encontrar el pokemon, envía un mensaje del error.</item>
        /// </list>
        /// </param>
        /// <remarks>
        /// Requisitos para la ejecución exitosa del comando:
        /// <list type="bullet">
        ///   <item>La sala de juego debe existir en el canal donde se ejecutó el comando.</item>
        ///   <item>El juego debe estar en curso, de lo contrario, se notifica que no ha empezado.</item>
        ///   <item>Solo el jugador en turno puede ejecutar el comando, si no es su turno, se le indica que espere.</item>
        ///   <item>Si los parámetros son correctos, el item se aplica al pokemon y se emvía un mensaje de confirmación.</item>
        /// </list>
        /// </remarks>
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
        
        /// <summary>
        /// Comando para mostrar los pokemons aliados y rivales en una sala de batalla.
        /// </summary>
        /// <param name="context">
        /// El contexto del usuario y el lugar donde fue utilizado el comando.
        /// </param>
        /// <remarks>
        /// Requisitos para la ejecución exitosa del comando:
        /// <list type="bullet">
        ///   <item>
        ///     La sala de juego debe existir en el canal donde se ejecutó el comando, de lo contrario,
        ///     se envía un mensaje de error indicando que el usuario debe estar en un canal de batalla.
        ///   </item>
        ///   <item>
        ///     El juego debe estar en curso, de lo contrario,
        ///     se envía un mensaje de error indicando que el usuario debe esperar a que empiece la batalla.
        ///   </item>
        ///   <item>
        ///     Si la sala de juego es la correcta y se ha iniciado la batalla, se envía una lista
        ///     de los pokemons aliados y rivales.
        ///   </item>
        /// </list>
        /// </remarks>
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
                builder.WithContent("\u26d4  **DEBES ESTAR EN EL CANAL DE BATALLA.**  \u26d4");
            }

            await context.EditResponseAsync(builder);
        }
        
        /// <summary>
        /// Comando para resetear la partida en una sala de batalla.
        /// </summary>
        /// <param name="context">
        /// El contexto del usuario y el lugar donde fue utilizado el comando.
        /// </param>
        /// <remarks>
        /// Requisitos para la ejecución exitosa del comando:
        /// <list type="bullet">
        ///   <item>
        ///     La sala de juego debe existir en el canal donde se ejecutó el comando, de lo contrario,
        ///     se envía un mensaje de error indicando que el usuario debe estar en un canal de batalla.
        ///   </item>
        ///   <item>
        ///     El juego debe estar haber finalizado para permitir el reinicio, de lo contrario,
        ///     el reinicio no se ejecutará.
        ///   </item>
        ///   <item>
        ///     Si la sala de juego es correcta y las condiciones de reinicio se cumplen,
        ///     el juego se reinicia y se envía un mensaje de confirmación.
        ///   </item>
        /// </list>
        /// </remarks>
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
                builder.WithContent("\u26d4  **DEBES ESTAR EN EL CANAL DE BATALLA.**  \u26d4");
            }
            
            await context.EditResponseAsync(builder);
        }
        
        /// <summary>
        /// Comando para mostrar la lista de jugadores que están esperando una partida.
        /// </summary>
        /// <param name="context">
        /// El contexto del usuario y el lugar donde fue utilizado el comando.
        /// </param>
        /// <remarks>
        /// Requisitos para la ejecución exitosa del comando:
        /// <list type="bullet">
        ///   <item>
        ///     Si existen jugadores en la lista de espera, se envía un mensaje con sus nombres.
        ///   </item>
        ///   <item>
        ///     Si no se encuentran jugadores en espera, se envía un mensaje indicando que no hay jugadores esperando una partida.
        ///   </item>
        /// </list>
        /// </remarks>
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
        
        /// <summary>
        /// Muestra una descripición de la utilidad de cada item.
        /// </summary>
        /// <param name="context">
        /// El contexto del usuario y el lugar donde fue utilizado el comando.
        /// </param>
        [SlashCommand("ShowItems", "Muestra la descripición de los items.")]
        public async Task ShowItems(InteractionContext context)
        {
            await context.CreateResponseAsync(GameCommands.ShowItemsDesc());
        }
        
        /// <summary>
        /// Muestra el mejor pokemon para pelear contra el pokemon enemigo basándose en la efectividad de sus ataques.
        /// </summary>
        /// <param name="context">
        /// El contexto del usuario y el lugar donde fue utilizado el comando.
        /// </param>
        [SlashCommand("GetBestPokemonToFight", "Muestra el mejor pokemon para pelear contra el pokemon enemigo basándose en la efectividad de sus ataques.")]
        public async Task GetBestPokemonToFight(InteractionContext context)
        {
            Lobby lobby = Lobby.GetInstance();
            GameRoom room = lobby.GetGameRoomById(context.Channel.Id);
            var builder = new DiscordWebhookBuilder();

            if (room != null!)
            {
                builder.WithContent(room.Commands.ViewBestPokemonToFight());
            }
            else
            {
                builder.WithContent("\u26d4  **DEBES ESTAR EN EL CANAL DE BATALLA.**  \u26d4");
            }

            await context.EditResponseAsync(builder);
            
            await context.CreateResponseAsync(GameCommands.ShowItemsDesc());
        }
    }
}
