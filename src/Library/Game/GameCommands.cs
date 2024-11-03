using DSharpPlus.CommandsNext;

namespace Library;

public class GameCommands
{
    public Game game;

    public GameCommands()
    {
        game = new Game();
    }

    public void StartGame()
    {
        game.Start();
    }

    public void AddPlayer(string playerName)
    {
        game.AddPlayer(playerName);
    }
    public void ChoosePokemon(Player player, string pokemonName)
    {
        if (player.GetPokemonByName(pokemonName) == null)
        {
            if (player.pokemonsCount < 6)
            {
                /*Pokemon pokemon = getPokemonFromApi();
                player.AddPokemon(pokemon);*/
            }
            else
            {
                // MSG sobre que ya tiene 6 pokemons 
            }
        }
        else
        {
            // Msg sobre que ya tiene el Pokemon
        }
    }


    public string ShowCatalogue()
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
        if (pokemon != null)
        {
            playerInTurn.ChangePokemon(pokemon);
        }
    }

    public void UseItem(Player inTurn, int itemSlot)
    {
        inTurn.UseItem(inTurn.Items[itemSlot]);
    }
}