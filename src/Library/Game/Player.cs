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
    public List<Pokemon> Pokemons { get; } = new();
    
    /// <summary>
    /// Cantidad máxima de pokemons que puede tener un jugador.
    /// Por defecto, <c>6</c>.
    /// </summary>
    public const int MaxPokemons = 6;
    
    /// <summary>
    /// Obtiene la cantidad de pokemon que el jugador tiene.
    /// </summary>
    public int pokemonsCount => Pokemons.Count;
    
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
        if (Pokemons.Count >= MaxPokemons || Pokemons.Contains(pokemon)) return;
        
        Pokemons.Add(pokemon);
        CurrentPokemon ??= pokemon;
    }

    /// <summary>
    /// Cambia el pokemon actual por otro del equipo solo si se encuentra en la lista de <c>Pokemon</c> del jugador.
    /// </summary>
    /// <param name="pokemon">El nuevo pokemon que se desea utilizar.</param>
    public void ChangePokemon(Pokemon pokemon)
    {
        if (Pokemons.Contains(pokemon))
        {
            CurrentPokemon = pokemon;
        }
    }
    
    /// <summary>
    /// Limpia la lista de pokemons y el <c>CurrentPokemon</c> del jugador.
    /// </summary>
    public void ClearPokemons()
    {
        Pokemons.Clear();
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
        return Pokemons.All(pokemon => pokemon.IsDead());
    }

    /// <summary>
    /// Obtiene el siguiente pokemon disponible que no esté muerto.
    /// </summary>
    /// <returns>El siguiente pokemon disponible o null si no hay ninguno.</returns>
    public Pokemon GetNextPokemon()
    {
        if (Pokemons.Count == 0) return null!;
        
        int indexNextPokemon = GetPokemonIndex(CurrentPokemon) + 1;
        for (int i = indexNextPokemon; i < Pokemons.Count; i++)
        {
            if (!Pokemons[i].IsDead()) return Pokemons[i];
        }
        
        for (int i = 0; i < indexNextPokemon; i++)
        {
            if (!Pokemons[i].IsDead()) return Pokemons[i];
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
        foreach (var pokemon in Pokemons)
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
        return Pokemons.IndexOf(pokemon);
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
        return Pokemons.Count > 0;
    }
    
    /// <summary>
    /// Muestra todos los pokemon del equipo del jugador.
    /// </summary>
    /// <returns>Una cadena que representa todos los pokemon del jugador.</returns>
    public string ViewAllPokemons()
    {
        string msg = "";
        foreach (var pokemon in Pokemons)
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
    
    
    
    ///////////////////////////////////////
    /// CODIGO AGREGADO PARA LA DEFENSA ///
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// LA IDEA DE EL CODIGO ES VER QUE POKEMON HARIA MAS DAÑO PARA VER QUIEN TIENE MAS POSIBILIDADES        ///
    /// PARA ESTO CALCULO CUANTO DAÑO REALIZAN TODOS LOS ATAQUES SUMADOS Y LOS COMPARO, ESTO YA REALIZA      ///
    /// LA EFECTIVIDAD DE TIPOS ENTRE EL ATAQUE Y EL ATACADO, SUMADO A QUE TAMBIEN TOMA EN CUENTA EL         ///
    /// DAÑO BASE DEL ATACANTE, YA QUE SI UN POKEMON TIENE MUCHO MAS DAÑO BASE QUE OTRO QUIZAS AUNQUE EL     ///
    /// OTRO SEA MAS "EFECTIVO" EN CONTRA, EL QUE TIENE MAS DAÑO SEGUIRA TENIENDO MAS POSIBILIDADES DE GANAR ///
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    public string BestPokemonForCombat(Pokemon enemy)
    {
        Pokemon bestPokemon = null!;
        Pokemon mostEffective = null!;
        ICalculate dmgCalculator = new Calculate();
        int bestDmg = -1;
        int mostMovesEffective = -1;
        foreach (var pokemon in Pokemons)
        {
            if (!pokemon.IsDead())
            {
                int pokemonDmg = 0;
                int movesEffective = 0;
                foreach (var move in pokemon.Moves)
                {
                    if (dmgCalculator.CalculateDamage(pokemon, enemy, move, out int dmg, out bool isEffective))  // EVALUO SI EL ATAQUE FUE CRITICO PARA GUARDAR SOLO EL DAÑO BASE
                    {
                        pokemonDmg += (int)Math.Round(dmg * 0.830);  // Elimino el daño critico
                    }
                    else
                    {
                        pokemonDmg += dmg;
                    }

                    if (isEffective) movesEffective++;
                }

                if (pokemonDmg > bestDmg)
                {
                    bestDmg = pokemonDmg;
                    bestPokemon = pokemon;
                }

                if (movesEffective > mostMovesEffective)
                {
                    mostMovesEffective = movesEffective;
                    mostEffective = pokemon;
                }
            }
        }

        if (bestPokemon != mostEffective)
        {
            return
                $"{bestPokemon.Name.ToUpper()} es la mejor opcion porque puede realizar mas daño, pero {mostEffective.Name.ToUpper()} tiene {mostMovesEffective} ataque/s efectivos.";
        }
        return $"{bestPokemon.Name.ToUpper()} es la mejor opcion. Tiene {mostMovesEffective} ataque/s efectivos.";
    }
}