using Library.Adapters;
using Library.Services;

namespace Library;

/// <summary>
/// Proporciona comandos para gestionar el juego, los jugadores, y las acciones de los <c>Pokemon</c>.
/// </summary>
public class GameCommands
{
    /// <summary>
    /// Atributo que contiene la instancia de <c>Game</c>.
    /// </summary>
    private Game game = new();
    

    
    /// <summary>
    /// Obtiene el <c>Player</c> en turno.
    /// </summary>
    public Player GetPlayerInTurn()
    {
        return game.PlayerInTurn;
    }

    /// <summary>
    /// Agrega un nuevo <c>Player</c> al juego si no fue agregado anteriormente.
    /// </summary>
    /// <param name="playerName">Nombre del jugador a agregar.</param>
    public void AddPlayer(string playerName)
    {
        if (game.GetPlayerByName(playerName) == null!)
        {
            game.AddPlayer(new Player(playerName));
        }
    }

    /// <summary>
    /// Permite que un jugador elija un <c>Pokemon</c> y lo añade a su equipo.
    /// </summary>
    /// <param name="playerName">Nombre del jugador.</param>
    /// <param name="pokemonName">Nombre del pokemon.</param>
    /// <returns>Un mensaje mostrando el resultado de cambiar el <c>Pokemon</c> y una URL con de la imagen del <c>Pokemon</c>.</returns>
    public async Task<(string message, string imgUrl)> ChoosePokemon(string playerName, string pokemonName)
    {
        IPokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        Player player = game.GetPlayerByName(playerName);
        string msg = "";
        string imgUrl = "";
        
        if (player != null!)
        {
            if (player.GetPokemonByName(pokemonName.ToLower()) == null!)
            {
                if (player.pokemonsCount < Player.MaxPokemons)
                {
                    Pokemon pokemon = await pokemonAdapter.GetPokemonAsync(pokemonName);
                    if (pokemon != null!)
                    {
                        player.AddPokemon(pokemon);
                        msg += $"**{pokemon.Name.ToUpper()}** ha sido agregado al equipo de **{player.Name.ToUpper()}**  ***({player.pokemonsCount}/{Player.MaxPokemons})***";
                        imgUrl = pokemon.ImgUrl;
                        
                        if (!GameHasStarted() && game.AllPlayersReady())
                        {
                            game.Start();
                        }
                    }
                    else
                    {
                        msg += $"**{pokemonName.ToUpper()}** no ha sido encontrado.";
                    }
                }
                else
                {
                    msg += $"**{player.Name.ToUpper()}** ya ha alcanzado el maximo de pokemons en su equipo.";
                }
            }
            else
            {
                msg += $"**{pokemonName.ToUpper()}** ya se encuentra en el equipo de **{player.Name.ToUpper()}**.";
            }
        }
        else
        {
            msg += $"**{playerName}** no ha sido encontrado.";
        }

        return (msg, imgUrl);
    }

    /// <summary>
    /// Inicia la batalla si todos los jugadores cumplen los requisitos.
    /// </summary>
    /// <returns>Un mensaje indicando el resultado de iniciar la batalla.</returns>
    public string StartBattle()
    {
        string msg;
        
        if (!GameHasStarted())
        {
            if (!game.AllPlayersHavePokemons()) return "\u26d4 TODOS LOS JUGADORES DEBEN TENER AL MENOS 1 POKEMON PARA INICIAR LA PARTIDA \u26d4";
                
            game.Start();
            msg = ShowTurn();
        }
        else
        {
            msg = "\u26d4 **LA PARTIDA YA ESTÁ EN CURSO** \u26d4";
        }

        return msg;
    }
    
    /// <summary>
    /// Método estático que muestra un enlace al catálogo de pokemons.
    /// </summary>
    public static string ShowCatalogue()
    {
        return "\ud83c\udf10 **CATÁLOGO DE POKEMONS** \u2192 https://pokemon-blog-api.netlify.app/";
    }

    /// <summary>
    /// Cambia el turno al siguiente jugador.
    /// </summary>
    /// <returns>Un mensaje mostrando el turno del jugador actual.</returns>
    public string NextTurn()
    {
        game.ToogleTurn();
        return ShowTurn();
    }

    /// <summary>
    /// Muestra el turno actual y el estado de los <c>Pokemon</c> solo si, el juego ha empezado.
    /// </summary>
    public string ShowTurn()
    {
        if (!GameHasStarted()) return "";
        
        return  $"Turno de: **{GetPlayerInTurn().Name.ToUpper()}**\n" +
                GetPlayerInTurn().CurrentPokemon.ViewPokemon() + $"{GetPlayerInTurn().ViewItems()}\n" +
                $"Recive: **{game.PlayerNotInTurn.Name.ToUpper()}**\n" +
                game.PlayerNotInTurn.CurrentPokemon.ViewPokemonSimple();
    }

    /// <summary>
    /// Permite que el jugador en turno ataque al oponente.
    /// Si el jugador enemigo fue atacado correctamente cambia de turno.
    /// </summary>
    /// <param name="moveSlot">Slot del movimiento de ataque.</param>
    /// <param name="playerNameAttacker">Nombre del jugador que ataca.</param>
    /// <returns>Un mensaje con el resultado del ataque.</returns>
    public string Attack(int moveSlot, string playerNameAttacker)
    {
        IPokemonManager winner = game.GetWinner();
        if (winner != null!)
        {
            return $"¡La partida ya ha finalizado, el jugador \ud83d\udc51 **_{winner.Name.ToUpper()}_** \ud83d\udc51 ha ganado!\nUtiliza el comando **`/restart`** para volver a jugar.";
        }
        
        string playerNameInTurn = GetPlayerInTurn().Name;
        if (!game.IsPlayerNameInTurn(playerNameAttacker))
        {
            return $"No puedes atacar, es el turno de **{playerNameInTurn.ToUpper()}**.";
        }

        var pokemonStateMachine = GetPlayerInTurn().CurrentPokemon.StateMachine;
        bool playerCanAttack = pokemonStateMachine.CanAttack();
        string stateName = pokemonStateMachine.CurrentState.Name.ToString();
        string msg;
        
        if (playerCanAttack)
        {
            if (pokemonStateMachine.HasLostTurn())
            {
                game.ToogleTurn();
                return $"El ataque no se ha aplicado. Has perdido el turno a causa del efecto **{stateName.ToUpper()}**.\n{ShowTurn()}";
            }
            
            IPokemonManager enemyPlayer = game.PlayerNotInTurn;
            msg = GetPlayerInTurn().Attack(enemyPlayer, moveSlot);
            if (!msg.Contains("cooldown")) game.ToogleTurn();
        }
        else
        {
            msg = $"No puedes atacar, estás bajo el efecto **{stateName.ToUpper()}**.\n";
        }
        
        return $"{msg}\n{ShowTurn()}";
    }
    
    /// <summary>
    /// Cambia el <c>Pokemon</c> activo del jugador en turno por otro de su lista de pokemons.
    /// Si el pokemon fue cambiado correctamente cambia de turno.
    /// </summary>
    /// <param name="playerInTurn">Jugador en turno.</param>
    /// <param name="pokemonName">Nombre del Pokémon a cambiar.</param>
    public string ChangePokemon(Player playerInTurn, string pokemonName)
    {
        Pokemon pokemon = playerInTurn.GetPokemonByName(pokemonName);
        if (pokemon != null!)
        {
            if(playerInTurn.CurrentPokemon.Name == pokemonName) 
                return $"**{pokemonName.ToUpper()}** ya se encuentra en batalla.";
            
            playerInTurn.ChangePokemon(pokemon);
            
            game.ToogleTurn();
            return $"**{pokemon.Name.ToUpper()}** ha entrado en batalla.\n\n{ShowTurn()}";
        }

        return $"**{pokemonName.ToUpper()}** no se encuentra disponible para cambiar.";
    }

    /// <summary>
    /// Usa un ítem del jugador en turno sobre un <c>Pokemon</c> específico de su lista de pokemons.
    /// Si el item fue usado correctamente cambia de turno.
    /// </summary>
    /// <param name="playerInTurn">Jugador en turno.</param>
    /// <param name="pokemonName">Nombre del <c>pokemon</c> que recibe el ítem.</param>
    /// <param name="itemSlot">Slot del ítem a usar.</param>
    /// <returns><c>string</c> con el resultado de usar el item.</returns>
    public string UseItem(Player playerInTurn, string pokemonName, int itemSlot)
    {
        int startingItemCount = playerInTurn.Items[itemSlot].Amount;
        string msg = playerInTurn.UseItem(playerInTurn.Items[itemSlot], pokemonName) + "\n\n";
        int finallyItemCount = playerInTurn.Items[itemSlot].Amount;
        
        if (startingItemCount > finallyItemCount) game.ToogleTurn();
        
        msg += ShowTurn();
        return msg;
    }
    
    /// <summary>
    /// Muestra cada <c>Pokemon</c> de los jugadores en el juego, solo si ha empezado la batalla.
    /// </summary>
    /// <returns><c>string</c> con el resultado de mostrar los pokemons.</returns>
    public string ViewPokemons()
    {
        if (GameHasStarted())
        {
            return game.ViewAllPokemons();
        }

        return "**Espera a que empiece la batalla.**";
    }

    /// <summary>
    /// Reinicia el juego para empezar una nueva partida.
    /// </summary>
    /// <returns><c>string</c> con el resultado de reiniciar el juego.</returns>
    public string RestartGame()
    {
        if (game.GetWinner() == null!) return "**La partida no ha finalizado.**";
            
        game.Reset();
        return $"La partida se ha reseteado. Puedes volver a elegir pokemons. \n{ShowCatalogue()}";
    }

    /// <summary>
    /// Verifica si el juego ha comenzado.
    /// </summary>
    /// <returns><c>true</c> si el juego a comenzado. De lo contrario, <c>false</c></returns>
    public bool GameHasStarted()
    {
        return game.HasStarted;
    }
    
    /// <summary>
    /// Método estático que muestra la descripción de los ítems disponibles en el juego.
    /// </summary>
    /// /// <returns><c>string</c> con el resultado de reiniciar el juego.</returns>
    public static string ShowItemsDesc()
    {
        return "**ITEMS** \n" +
               "1) **REVIVIR**: Revive a un Pokémon con el **50%** de su HP total.\n" +
               "2) **SUPER POCION**: Cada una recupera 70 puntos de HP.\n" +
               "3) **CURA TOTAL**: Cura a un Pokémon de efectos de ataques especiales, dormido, paralizado, envenenado, o quemado.\n";
    }

    public Player GetWinner()
    {
        return game.GetWinner();
    }

    public Player GetPlayerNotInTurn()
    {
        return game.PlayerNotInTurn;
    }
    
}