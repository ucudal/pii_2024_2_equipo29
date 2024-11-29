using Library.DiscordBot;

namespace Library;

/// <summary>
/// Clase que representa a un jugador en el juego, que puede manejar un equipo de pokemon.
/// </summary>
/// <remarks>
/// Esta clase implementa la interfaz <see cref="IPokemonManager"/> y permite al jugador 
/// gestionar sus pokemon, realizar ataques y utilizar ítems.
/// </remarks>
public class Player : IPokemonManager
{
    /// <summary>
    /// Atributo que contiene la lista de pokemons que tiene el jugador.
    /// </summary>
    private List<Pokemon> pokemons = new();
    
    /// <summary>
    /// Cantidad máxima de pokemons que puede tener un jugador.
    /// Por defecto, <c>6</c>.
    /// </summary>
    public const int MaxPokemons = 6;
    
    /// <summary>
    /// Obtiene la cantidad de pokemon que el jugador tiene.
    /// </summary>
    public int pokemonsCount => pokemons.Count;
    
    /// <summary>
    /// Obtiene el nombre del jugador.
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// Lista de ítems que el jugador posee.
    /// </summary>
    public List<IItem> Items = new();
    
    /// <summary>
    /// Obtiene el pokemon actual que el jugador está utilizando.
    /// Permite setear otro de forma privada.
    /// </summary>
    public Pokemon CurrentPokemon { get; private set; }

    public bool Surrender = false;

    public Calculate calculate = new();

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="Player"/> con un nombre específico.
    /// </summary>
    /// <param name="name">El nombre del jugador.</param>
    public Player(string name)
    {
        Name = name;
        AddBaseItems();
    }
    
    /// <summary>
    /// Agrega un pokemon al equipo del jugador.
    /// </summary>
    /// <param name="pokemon">El pokemon que se desea agregar.</param>
    public void AddPokemon(Pokemon pokemon)
    {
        if (pokemons.Count >= MaxPokemons || pokemons.Contains(pokemon)) return;
        
        pokemons.Add(pokemon);
        CurrentPokemon ??= pokemon;
    }

    /// <summary>
    /// Cambia el pokemon actual por otro del equipo solo si se encuentra en la lista de <c>Pokemon</c> del jugador.
    /// </summary>
    /// <param name="pokemon">El nuevo pokemon que se desea utilizar.</param>
    public void ChangePokemon(Pokemon pokemon)
    {
        if (pokemons.Contains(pokemon))
        {
            CurrentPokemon = pokemon;
        }
    }
    
    /// <summary>
    /// Limpia la lista de pokemons y el <c>CurrentPokemon</c> del jugador.
    /// </summary>
    public void ClearPokemons()
    {
        pokemons.Clear();
        CurrentPokemon = null!;
    }

    /// <summary>
    /// Realiza un ataque al pokemon del jugador enemigo.
    /// </summary>
    /// <param name="enemyPlayer">El jugador enemigo al que se le atacará.</param>
    /// <param name="moveSlot">El slot del movimiento que se utilizará para atacar.</param>
    /// <returns>Un mensaje que indica el resultado del ataque.</returns>
    public string Attack(IPokemonManager enemyPlayer, int moveSlot)
    {
        string msg = CurrentPokemon.Attack(enemyPlayer.CurrentPokemon, moveSlot);
        if (enemyPlayer.CurrentPokemon.IsDead()) enemyPlayer.ChangePokemon(enemyPlayer.GetNextPokemon());
        
        return msg; 
    }
    
    /// <summary>
    /// Verifica si el jugador ha perdido todos sus pokemon.
    /// </summary>
    /// <returns><c>true</c> si el jugador ha perdido, de lo contrario, <c>false</c>.</returns>
    public bool HasLost()
    {
        return pokemons.All(pokemon => pokemon.IsDead()) || Surrender; //PokemonesMuertos o PlayerSurrender***
    }

    /// <summary>
    /// Obtiene el siguiente pokemon disponible que no esté muerto.
    /// </summary>
    /// <returns>El siguiente pokemon disponible o null si no hay ninguno.</returns>
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

    /// <summary>
    /// Utiliza un ítem en un pokemon específico.
    /// </summary>
    /// <param name="item">El ítem que se desea utilizar.</param>
    /// <param name="pokemonName">El nombre del pokemon al que se le aplicará el ítem.</param>
    /// <returns>Un mensaje que indica el resultado de la acción.</returns>
    public string UseItem(IItem item, string pokemonName)
    {
        return item.Use(pokemonName, this);
    }

    /// <summary>
    /// Agrega ítems básicos al inventario del jugador.
    /// </summary>
    private void AddBaseItems()
    {
        Items.Add(new Revive());
        Items.Add(new SuperPotion());
        Items.Add(new FullHeal());
    }

    
    /// <summary>
    /// Obtiene un pokemon del equipo del jugador por su nombre.
    /// </summary>
    /// <param name="pokemonName">El nombre del pokemon que se desea buscar.</param>
    /// <returns>El pokemon encontrado o null si no existe.</returns>
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
    
    /// <summary>
    /// Obtiene el índice de un pokemon en el equipo del jugador.
    /// </summary>
    /// <param name="pokemon">El pokemon cuyo índice se desea obtener.</param>
    /// <returns>El índice del pokemon en la lista o -1 si no se encuentra.</returns>
    private int GetPokemonIndex(Pokemon pokemon)
    {
        return pokemons.IndexOf(pokemon);
    }

    /// <summary>
    /// Verifica si el jugador tiene el número máximo de pokemon.
    /// </summary>
    /// <returns>True si el jugador tiene todos los pokemon, de lo contrario, false.</returns>
    public bool HasAllPokemons()
    {
        return pokemonsCount == MaxPokemons;
    }

    /// <summary>
    /// Verifica si el jugador tiene algún pokemon en su equipo.
    /// </summary>
    /// <returns>True si el jugador tiene pokemon, de lo contrario, false.</returns>
    public bool PlayersHavePokemons()
    {
        return pokemons.Count > 0;
    }
    
    /// <summary>
    /// Muestra todos los pokemon del equipo del jugador.
    /// </summary>
    /// <returns>Una cadena que representa todos los pokemon del jugador.</returns>
    public string ViewAllPokemons()
    {
        string msg = "";
        foreach (var pokemon in pokemons)
        {
            msg += $"• {pokemon.ViewPokemonSimple()}\n";
        }

        return msg;
    }
    
    /// <summary>
    /// Muestra todos los ítems que el jugador posee.
    /// </summary>
    /// <returns>Una cadena que representa todos los ítems del jugador.</returns>
    public string ViewItems()
    {
        string msg = "**ITEMS:**";
        int cont = 1;
        foreach (var item in Items)
        {
            msg += $"   {cont}) **{item.GetType().Name.ToUpper()}** (Cantidad: **{item.Amount}**) ";
            cont++;
        }

        return msg + "\n";
    }

    /// <summary>
    /// Intancia de forma publica la lista privada con los pokemones del jugador en turno
    /// </summary>
    /// <returns></returns>
    public List<Pokemon> PokemonsList()
    {
        return pokemons;
    }
    
    /// <summary>
    /// Obtengo una lista con los pokemones del jugador en turno
    /// </summary>
    /// <param name="playerInTurn"></param>
    /// <returns></returns>
    public List<Type> PlayerPokemonTypes(Player playerInTurn)
    {
        playerInTurn.PokemonsList();
        foreach (var pokemon in playerInTurn.PokemonsList())
        {
            return pokemon.Types;
        }
        return null!;
    }

    /// <summary>
    /// Obtiene una lista de los tipos del pokemon actual del enemigo
    /// </summary>
    /// <param name="enemyPlayer"></param>
    /// <returns></returns>
    public List<Type> EnemyPokemonTypes(IPokemonManager enemyPlayer)
    {
        return enemyPlayer.CurrentPokemon.Types;
    }
    
    /// <summary>
    /// Obtiene el numero de ataques efectivos del pokemon en turno del jugador
    /// </summary>
    /// <param name="enemyTypes"></param>
    /// <param name="moveType"></param>
    /// <returns></returns>
    public string EffectivityAtacks(List<Type> enemyTypes, Type moveType)
    {
        float effectivity = 1;
        int count = 0;
        foreach (Type enemyType in enemyTypes)
        {
            effectivity *= DicTypeEffectivity.Effectivity[moveType.Name][enemyType.Name];
            if (effectivity >= 2)
            {
                count += 1;
                return $"{count}, es la cantidad de ataques efectivos de tu pokemon";
            }
        }

        return "Solo cuentas con ataques normales";
    }

}