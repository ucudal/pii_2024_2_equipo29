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
                        
                        if (!game.HasStarted && game.AllPlayersReady())
                        {
                            msg += $"\n\n{StartBattle()}";
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
        if (!game.HasStarted)
        {
            if (game.AllPlayersReady())
            {
                game.Start();
                msg = game.ViewTurn();
            }
            else
            {
                msg = "Espera a que todos los jugadores esten listos.";
            }
        }
        else
        {
            msg = "_La partida ya esta en curso._";
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
        return  $"Turno de: **{game.PlayerInTurn.Name}**\n" +
                game.PlayerInTurn.CurrentPokemon.ViewPokemon() + "\n" +
                $"Recive: **{game.PlayerNotInTurn.Name}**\n" +
                game.PlayerNotInTurn.CurrentPokemon.ViewPokemonSimple();
    }

    public string Attack(int moveSlot, string playerNameAttacker)
    {
        Player winner = game.GetWinner();
        if (winner != null)
        {
            return $"¡La partida ya ha finalizado, el jugador {winner.Name} ha ganado!";
        }
        
        string playerNameInTurn = game.PlayerInTurn.Name;
        if (!game.IsPlayerNameInTurn(playerNameAttacker))
        {
            return $"No puedes atacar, es el turno de {playerNameInTurn}";
        }
        
        bool playerCanAttack = game.PlayerInTurn.CurrentPokemon.StateMachine.CanAttack();
        string stateName = game.PlayerInTurn.CurrentPokemon.StateMachine.CurrentState.Name;
        
        if (!playerCanAttack)
        {
            return $"No puedes atacar, estás bajo el efecto {stateName}";
        }

        IPokemonManager enemyPlayer = game.PlayerNotInTurn;
        string msg = game.PlayerInTurn.Attack(enemyPlayer, moveSlot);
        
        if (!msg.Contains("cooldown")) game.ToogleTurn();
        
        return $"{msg}\n{game.ViewTurn()}";
    }

    public string ChangePokemon(Player playerInTurn, string pokemonName)
    {
        Pokemon pokemon = playerInTurn.GetPokemonByName(pokemonName);
        if (pokemon != null!)
        {
            playerInTurn.ChangePokemon(pokemon);
            game.ToogleTurn();
            return $"**{pokemon.Name.ToUpper()}** ha entrado en batalla \n \n {ShowTurn()}";
        }

        return $"**{pokemonName.ToUpper()}** no se encuentra disponible para cambiar.";
    }

    public string UseItem(Player inTurn, int itemSlot)
    {
        string msg = inTurn.UseItem(inTurn.Items[itemSlot]) + "\n";
        game.ToogleTurn();
        msg += ShowTurn();
        return msg;
    }
    
    public string ViewPokemons()
    {
        if (game.HasStarted)
        {
            return game.ViewAllPokemons();
        }

        return "Espera a que empiece la batalla";
    }

    public string RestartGame()
    {
        if (game.GetWinner() == null) return "La partida no ha finalizado.";
            
        game.Reset();
        return "La partida se ha reseteado. Puedes volver a elegir los pokemons.";
    }
}