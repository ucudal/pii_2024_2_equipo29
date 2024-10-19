namespace Library;

public class Player: IPokemonManager
{
    private List<Pokemon> pokemons = new();
    private int maxPokemons = 6;
    
    private string name;
    public string Name
    {
        get => name;
    }
    
    public Pokemon CurrentPokemon { get; private set; }
    
    public void AddPokemon(Pokemon pokemon)
    {
        if (pokemons.Count >= maxPokemons || pokemons.Contains(pokemon)) return;
        
        pokemons.Add(pokemon);
        CurrentPokemon ??= pokemon;
    }

    public void ChangePokemon(Pokemon pokemon)
    {
        if (pokemons.Contains(pokemon))
        {
            CurrentPokemon = pokemon;
        }
    }
    
    public void ClearPokemons()
    {
        pokemons.Clear();
        CurrentPokemon = null!;
    }

    public void Attack(IPokemonManager enemyPlayer, int moveSlot)
    {
        CurrentPokemon.Attack(enemyPlayer.CurrentPokemon, moveSlot);
    }
    
    public bool HasLost()
    {
        return pokemons.All(pokemon => pokemon.IsDead());
    }

    public Pokemon GetNextPokemon()
    {
        if (pokemons.Count == 0) return null!;
        
        int indexNextPokemon = pokemons.IndexOf(CurrentPokemon) + 1;
        
        for (int i = indexNextPokemon; i < pokemons.Count; i++)
        {
            if (!pokemons[i].IsDead()) return pokemons[i];
        }
        
        for (int i = 0; i < indexNextPokemon; i++)
        {
            if (!pokemons[i].IsDead()) return pokemons[i];
        }
        
        return null!;
    }

    public void UseItem(IItem item)
    {
        string message = item.Use(CurrentPokemon);
    }
}