namespace Library;

public class GameCommands
{
    private Game game = new Game();
    
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


    public void ShowCatalogue()
    {
        string msg = "https://pokemon-blog-api.netlify.app/";
        // Enviar MSG 
    }

    public void ShowTurn(Player inTurn, Player notInTurn)
    {
        string msg = $"Turno de: {inTurn.Name} \n" +
                     inTurn.CurrentPokemon.ViewPokemon() + "\n" +
                     inTurn.CurrentPokemon.ViewMoves() + "\n\n" +
                     $"Recive: {inTurn.Name} \n" +
                     notInTurn.CurrentPokemon.ViewPokemon();
        
        // falta mandar msg
    }

    public void ChangePokemon(Player inTurn, string pokemonName)
    {
        Pokemon pokemon = inTurn.GetPokemonByName(pokemonName);
        if (pokemon != null)
        {
            inTurn.ChangePokemon(pokemon);
        }
    }

    public void UseItem(Player inTurn, int itemSlot)
    {
        inTurn.UseItem(inTurn.Items[itemSlot]);
    }
}