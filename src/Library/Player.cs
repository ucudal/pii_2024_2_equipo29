namespace Library;

public class Player
{
    private List<Pokemon> pokemons = new();
    private int maxPokemons = 6;
    
    private string name;
    public string Name
    {
        get => name;
    }
    
    private Pokemon currentPokemon;
    public Pokemon CurrentPokemon
    {
        get => currentPokemon;
    }
    
    public void AddPokemon(Pokemon pokemon)
    {
        if (pokemons.Count >= maxPokemons || pokemons.Contains(pokemon)) return;
        
        pokemons.Add(pokemon);
        currentPokemon ??= pokemon;
    }

    public void ChangePokemon(Pokemon pokemon)
    {
        if (pokemons.Contains(pokemon))
        {
            currentPokemon = pokemon;
        }
    }
    
    public void ClearPokemons()
    {
        pokemons.Clear();
        currentPokemon = null!;
    }

    public void Attack(Player enemyPlayer, int moveSlot)
    {
        currentPokemon.Attack(enemyPlayer.CurrentPokemon, moveSlot);
    }
    
    public bool HasLost()
    {
        return pokemons.All(pokemon => pokemon.IsDead());
    }
}