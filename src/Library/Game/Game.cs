namespace Library;

/// <summary>
/// Clase principal que administra el flujo del juego, la gestión de jugadores, turnos
/// y las reglas de finalización de la partida. Implementa el patrón de diseño <c>Facade</c> para simplificar 
/// la interacción con las funcionalidades del juego.
/// </summary>
public class Game
{
    /// <summary>
    /// Atributo estático que permite obtener el número máximo de jugadores permitidos en el juego.
    /// Por defecto, <c>2</c>.
    /// </summary>
    public static int MaxPlayers { get; } = 2;
    
    /// <summary>
    /// Lista de jugadores en el juego.
    /// </summary>
    private List<Player> players = new();
    
    /// <summary>
    /// Permite obtener el <c>Player</c> que actualmente tiene el turno o setear uno nuevo de forma privada.
    /// </summary>
    public Player PlayerInTurn { get; private set; }

    /// <summary>
    /// Permite obtener el <c>Player</c> que actualmente <b>NO</b> tiene el turno.
    /// </summary>
    public Player PlayerNotInTurn
    {
        get
        {
            return PlayerInTurn == players[0]
                ? players[1] 
                : players[0];
        }
    }
    
    /// <summary>
    /// Indica si el juego ha comenzado. Permite modifcar su valor de forma privada.
    /// </summary>
    public bool HasStarted { get; private set; }
    
    /// <summary>
    /// Indica si el juego alcanz+o el maximo de jugadores-
    /// </summary>
    public bool IsFullPlayers
    {
        get => players.Count == MaxPlayers;
    }

    /// <summary>
    /// Agrega un jugador al juego. Si se alcanza el máximo de jugadores,
    /// se establece de forma aleatoria el jugador que comienza la batalla.
    /// </summary>
    /// <param name="player">El jugador a agregar.</param>
    public void AddPlayer(Player player)
    {
        if (!IsFullPlayers) players.Add(player);
        if (IsFullPlayers) PlayerInTurn = GetRandomPlayer();
    }

    /// <summary>
    /// Cambia el turno al otro jugador. Posteriormente, actualiza el estado del pokemon actual
    /// del jugador en turno solo si tiene pokemons.
    /// </summary>
    public void ToogleTurn()
    {
        PlayerInTurn = PlayerNotInTurn;

        if (!PlayerInTurn.PlayersHavePokemons()) return;
        PlayerInTurn.CurrentPokemon.UpdateCoolDownSpecialMove();
        PlayerInTurn.CurrentPokemon.StateMachine.ApplyEffect(PlayerInTurn.CurrentPokemon);
    }

    /// <summary>
    /// Establece el atributo <c>HasStarted</c> en <c>true</c> si el número máximo de jugadores ha sido alcanzado.
    /// </summary>
    public void Start()
    {
        if (IsFullPlayers)
        {
            HasStarted = true;
        }
    }
    
    /// <summary>
    /// Verifica si el nombre del jugador coincide con el jugador que está en turno.
    /// </summary>
    /// <param name="playerName">Nombre del jugador.</param>
    /// <returns><c>true</c> si el nombre coincide. De lo contrario, <c>false</c>.</returns>
    public bool IsPlayerNameInTurn(string playerName)
    {
        return PlayerInTurn.Name == playerName;
    }

    /// <summary>
    /// Reinicia el juego, eliminando los <c>Pokemon</c> de todos los
    /// jugadores y estableciendo <c>HasStarted</c> en <c>false</c>.
    /// </summary>
    public void Reset()  // Era la idea si
    {
        HasStarted = false;
        foreach (var player in players)
        {
            player.ClearPokemons();
        }
    }

    /// <summary>
    /// Determina el ganador del juego basado en el estado de los jugadores.
    /// </summary>
    /// <returns>El <c>Player</c> ganador o <c>null</c> si no hay un ganador.</returns>
    public Player GetWinner()
    {
        if (PlayerInTurn.HasLost()) return PlayerNotInTurn;
        if (PlayerNotInTurn.HasLost()) return PlayerInTurn;
        return null!;
    }

    /// <summary>
    /// Obtiene un jugador por su nombre.
    /// </summary>
    /// <param name="playerName">Nombre del jugador.</param>
    /// <returns>El <c>Player</c> que coincide con el nombre, o <c>null</c> si no se encuentra.</returns>
    public Player GetPlayerByName(string playerName)
    {
        foreach (var player in players)
        {
            if (player.Name == playerName)
            {
                return player;
            } 
        }

        return null!;
    }
    
    /// <summary>
    /// Selecciona aleatoriamente un <c>Player</c> de la lista de jugadores.
    /// </summary>
    /// <returns>Un <c>Player</c> seleccionado aleatoriamente.</returns>
    private Player GetRandomPlayer()
    {
        return players[new Random().Next(0, players.Count)];
    }

    /// <summary>
    /// Verifica si todos los jugadores están listos para comenzar el juego.
    /// Un jugador está listo si alcanzó el máximo de pokemons.
    /// </summary>
    /// <returns>
    /// <c>true</c> si todos los jugadores están listos. De lo contrario, <c>false</c>.
    /// </returns>
    public bool AllPlayersReady()
    {
        return players.All(player => player.HasAllPokemons());
    }
    
    /// <summary>
    /// Verifica si todos los jugadores tienen al menos un <c>Pokemon</c> disponibles.
    /// </summary>
    /// <returns><c>true</c> si todos los jugadores tienen al menos un <c>Pokemon</c>. De lo contrario, <c>false</c>.</returns>
    public bool AllPlayersHavePokemons()
    {
        return players.All(player => player.PlayersHavePokemons());
    }
    
    /// <summary>
    /// Muestra una vista de todos los <c>Pokemon</c> de cada jugador.
    /// </summary>
    /// <returns><c>string</c> con información de los <c>Pokemon</c> de cada jugador.</returns>
    public string ViewAllPokemons()
    {
        string msg = "";
        foreach (var player in players)
        {
            msg +=$"{player.Name.ToUpper()}:\n{player.ViewAllPokemons()}\n";
        }

        return msg;
    }

    /// <summary>
    /// Obtiene el mejor pokemon para pelear contra el pokemon enemigo basándose en la efectividad de sus ataques.
    /// </summary>
    /// <returns>El mejor <c>Pokemon</c> para pelear contra el pokemon enemigo. <c>Null</c> si no hay pokemons disponibles.</returns>
    public Pokemon GetBestPokemonToFight()
    {
        return PlayerInTurn.GetBestPokemonToFight(PlayerNotInTurn.CurrentPokemon);
    }
}