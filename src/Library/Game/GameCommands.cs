using DSharpPlus.CommandsNext;
using Library.Adapters;
using Library.Services;

namespace Library;

public class GameCommands
{
    public Game game;

    public GameCommands()
    {
        game = new Game();
    }
    

    public void AddPlayer(string playerName)
    {
        game.AddPlayer(playerName);
    }
    
    public async Task<string> ChoosePokemon(string playerName, string pokemonName)
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        Player player = game.GetPlayerByName(playerName);
        string msg = "";
        if (player != null!)
        {
                if (player.GetPokemonByName(pokemonName) == null!)
                {
                    if (player.pokemonsCount < Player.maxPokemons)
                    {
                        Pokemon pokemon = await pokemonAdapter.GetPokemonAsync(pokemonName);
                        if (pokemon != null!)
                        {
                            player.AddPokemon(pokemon);
                            msg += $"{pokemonName} ha sido agregado al equipo de {player.Name}  ***({player.pokemonsCount}/6)***";
                        }
                        else
                        {
                            msg += $"{pokemonName} no ha sido encontrado";
                        }
                    }
                    else
                    {
                        msg += $"{player.Name} ya ha alcanzado el maximo de pokemons en su equipo";
                    }
                }
                else
                {
                    msg += $"{pokemonName} ya se encuentra en el equipo de {player.Name}";
                }
        }
        else
        {
            msg += $"{playerName} no ha sido encontrado";
        }
        
        return msg;
    }

    public string StartBattle()
    {
        string msg = "";
        if (!game.HasStarted)
        {
            if (game.AllPlayersReady())
            {
                game.Start();
                msg = $"Juego iniciado. \n" +
                      $"{game.ViewTurn()}";
            }
            else
            {
                msg = "Espera a que todos los jugadores esten listos";
            }
        }
        else
        {
            msg = "La partida ya esta en curso";
        }
    
        return msg;
    }
    
    public static string ShowCatalogue()
    {
        return "https://pokemon-blog-api.netlify.app/";
    }

    public void ShowTurn(Player playerInTurn, Player notInTurn)
    {
        string msg = $"Turno de: {playerInTurn.Name} \n" +
                     playerInTurn.CurrentPokemon.ViewPokemon() + "\n" +
                     playerInTurn.CurrentPokemon.ViewMoves() + "\n\n" +
                     $"Recive: {playerInTurn.Name} \n" +
                     notInTurn.CurrentPokemon.ViewPokemon();
        
        // falta mandar msg
    }

    public void ChangePokemon(Player playerInTurn, string pokemonName)
    {
        Pokemon pokemon = playerInTurn.GetPokemonByName(pokemonName);
        if (pokemon != null!)
        {
            playerInTurn.ChangePokemon(pokemon);
        }
    }

    public void UseItem(Player inTurn, int itemSlot)
    {
        inTurn.UseItem(inTurn.Items[itemSlot]);
    }
}