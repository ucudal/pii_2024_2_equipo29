using System.Reflection.Metadata.Ecma335;
using DSharpPlus.CommandsNext;
using Library.Adapters;
using Library.Services;

namespace Library;

public class GameCommands
{
    private Game game;

    public GameCommands()
    {
        game = new Game();
    }

    public Player GetPlayerInTurn()
    {
        return game.PlayerInTurn;
    }

    public void AddPlayer(string playerName)
    {
        game.AddPlayer(playerName);
    }

    public async Task<(string message, string imgUrl)> ChoosePokemon(string playerName, string pokemonName)
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        Player player = game.GetPlayerByName(playerName);
        string msg = "";
        string imgUrl = "";
        
        if (player != null!)
        {
            if (player.GetPokemonByName(pokemonName) == null!)
            {
                if (player.pokemonsCount < Player.MaxPokemons)
                {
                    Pokemon pokemon = await pokemonAdapter.GetPokemonAsync(pokemonName);
                    if (pokemon != null!)
                    {
                        player.AddPokemon(pokemon);
                        msg += $"**{pokemonName.ToUpper()}** ha sido agregado al equipo de **{player.Name.ToUpper()}**  ***({player.pokemonsCount}/{Player.MaxPokemons})***";
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

    public static string ShowCatalogue()
    {
        return "\ud83c\udf10 **CATÁLOGO DE POKEMONS** \u2192 https://pokemon-blog-api.netlify.app/";
    }

    public string NextTurn()
    {
        game.ToogleTurn();
        return game.ViewTurn();
    }

    public string ShowTurn()
    {
        if (!GameHasStarted()) return "";
        
        return  $"Turno de: **{game.PlayerInTurn.Name.ToUpper()}**\n" +
                game.PlayerInTurn.CurrentPokemon.ViewPokemon() + $"{game.PlayerInTurn.ViewItems()}\n" +
                $"Recive: **{game.PlayerNotInTurn.Name.ToUpper()}**\n" +
                game.PlayerNotInTurn.CurrentPokemon.ViewPokemonSimple();
    }

    public string Attack(int moveSlot, string playerNameAttacker)
    {
        Player winner = game.GetWinner();
        if (winner != null!)
        {
            return $"¡La partida ya ha finalizado, el jugador \ud83d\udc51 **_{winner.Name.ToUpper()}_** \ud83d\udc51 ha ganado!\nUtiliza el comando **`/restart`** para volver a jugar.";
        }
        
        string playerNameInTurn = game.PlayerInTurn.Name;
        if (!game.IsPlayerNameInTurn(playerNameAttacker))
        {
            return $"No puedes atacar, es el turno de **{playerNameInTurn.ToUpper()}**.";
        }
        
        bool playerCanAttack = game.PlayerInTurn.CurrentPokemon.StateMachine.CanAttack();
        string stateName = game.PlayerInTurn.CurrentPokemon.StateMachine.CurrentState.Name;
        string msg;
        if (!playerCanAttack)
        {
            msg = $"**No puedes atacar**, estás bajo el efecto **{stateName.ToUpper()}**.\n";
        }
        else
        {
            IPokemonManager enemyPlayer = game.PlayerNotInTurn;
            msg = game.PlayerInTurn.Attack(enemyPlayer, moveSlot);
        }
        
        
        if (!msg.Contains("cooldown")) game.ToogleTurn();
        
        return $"{msg}\n{ShowTurn()}";
    }

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

    public string UseItem(Player playerInTurn, string pokemonName, int itemSlot)
    {
        int startingItemCount = playerInTurn.Items[itemSlot].Amount;
        string msg = playerInTurn.UseItem(playerInTurn.Items[itemSlot], pokemonName) + "\n\n";
        int finallyItemCount = playerInTurn.Items[itemSlot].Amount;
        
        if (startingItemCount > finallyItemCount) game.ToogleTurn();
        
        msg += ShowTurn();
        return msg;
    }
    
    public string ViewPokemons()
    {
        if (GameHasStarted())
        {
            return game.ViewAllPokemons();
        }

        return "**Espera a que empiece la batalla.**";
    }

    public string RestartGame()
    {
        if (game.GetWinner() == null!) return "**La partida no ha finalizado.**";
            
        game.Reset();
        return $"La partida se ha reseteado. Puedes volver a elegir los pokemons. \n{GameCommands.ShowCatalogue()}";
    }

    public bool GameHasStarted()
    {
        return game.HasStarted;
    }

    public static string ShowItemsDesc()
    {
        return "**ITEMS** \n" +
               "1) **REVIVIR**: Revive a un Pokémon con el **50%** de su HP total.\n" +
               "2) **SUPER POCION**: Cada una recupera 70 puntos de HP.\n" +
               "3) **CURA TOTAL**: Cura a un Pokémon de efectos de ataques especiales, dormido, paralizado, envenenado, o quemado.\n";
    }
}