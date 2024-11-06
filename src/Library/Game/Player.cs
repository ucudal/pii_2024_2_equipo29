namespace Library;

public class Player : IPokemonManager
{
    private List<Pokemon> pokemons = new();
    public const int maxPokemons = 2;
    public int pokemonsCount => pokemons.Count;
  
    private string name;
    public string Name
    {
        get => name;
    }
    public List<IItem> Items = new List<IItem>();
    
    public Pokemon CurrentPokemon { get; private set; }

    public Player(string name)
    {
        this.name = name;
        AddBaseItems();
    }
    
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
        if (enemyPlayer.CurrentPokemon.IsDead()) enemyPlayer.ChangePokemon(enemyPlayer.GetNextPokemon());
    }
    
    public bool HasLost()
    {
        return pokemons.All(pokemon => pokemon.IsDead());
    }

    public Pokemon GetNextPokemon()
    {
        Console.WriteLine("Entro 1");
        if (pokemons.Count == 0) return null!;
        Console.WriteLine("Entro 2");
        
        int indexNextPokemon = GetPokemonIndex(CurrentPokemon) + 1;
        Console.WriteLine("Entro 3");
        
        for (int i = indexNextPokemon; i < pokemons.Count; i++)
        {
            Console.WriteLine($"Entro 4: i = {indexNextPokemon}; i < {pokemons.Count}; i++");
            if (!pokemons[i].IsDead()) Console.WriteLine("Entro 5"); return pokemons[i];
        }
        
        for (int i = 0; i < indexNextPokemon; i++)
        {
            Console.WriteLine("Entro 6");
            if (!pokemons[i].IsDead()) Console.WriteLine("Entro 7"); return pokemons[i];
        }
        Console.WriteLine("Entro 8");
        
        return null!;
    }

    public void UseItem(IItem item)
    {
        Items.Remove(item);
        string message = item.Use(CurrentPokemon);
    }

    private void AddBaseItems()
    {
        Items.Add(new Revive());
        for (int i = 0; i < 4; i++)
        {
            Items.Add(new SuperPotion());
        }
        for (int i = 0; i < 2; i++)
        {
            Items.Add(new FullHeal());
        }
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
        return pokemonsCount == maxPokemons;
    }
}