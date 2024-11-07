namespace Library;

public class Player : IPokemonManager
{
    private List<Pokemon> pokemons = new();
    public const int MaxPokemons = 2;
    public int pokemonsCount => pokemons.Count;
  
    private string name;
    public string Name
    {
        get => name;
    }
    public List<IItem> Items = new();
    public Pokemon CurrentPokemon { get; private set; }

    public Player(string name)
    {
        this.name = name;
        AddBaseItems();
    }
    
    public void AddPokemon(Pokemon pokemon)
    {
        if (pokemons.Count >= MaxPokemons || pokemons.Contains(pokemon)) return;
        
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

    public string Attack(IPokemonManager enemyPlayer, int moveSlot)
    {
        string msg = CurrentPokemon.Attack(enemyPlayer.CurrentPokemon, moveSlot);
        if (enemyPlayer.CurrentPokemon.IsDead()) enemyPlayer.ChangePokemon(enemyPlayer.GetNextPokemon());
        
        return msg; 
    }
    
    public bool HasLost()
    {
        return pokemons.All(pokemon => pokemon.IsDead());
    }

    public Pokemon GetNextPokemon()
    {
        if (pokemons.Count == 0) return null!;
        
        int indexNextPokemon = GetPokemonIndex(CurrentPokemon) + 1;
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

    public string UseItem(IItem item, string pokemonName)
    {
        return item.Use(pokemonName, this);
    }

    private void AddBaseItems()
    {
        Items.Add(new Revive());
        Items.Add(new SuperPotion());
        Items.Add(new FullHeal());
    }

    public Pokemon GetPokemonByName(string pokemonName)
    {
        foreach (var pokemon in pokemons)
        {
            if (pokemon.Name == pokemonName)
            {
                return pokemon;
            }
        }

        return null!;
    }
    
    public int GetPokemonIndex(Pokemon pokemon)
    {
        return pokemons.IndexOf(pokemon);
    }

    public bool HasAllPokemnos()
    {
        return pokemonsCount == MaxPokemons;
    }

    public bool PlayersHavePokemons()
    {
        return pokemons.Count > 0;
    }
    
    public string ViewAllPokemons()
    {
        string msg = "";
        foreach (var pokemon in pokemons)
        {
            msg += $"• {pokemon.ViewPokemonSimple()}\n";
        }

        return msg;
    }
}