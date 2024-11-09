using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Library.DiscordBot;

/// <summary>
/// Representa un lobby para gestionar jugadores en espera y salas de juego.
/// Implementa el patrón Singleton. Esto es util para tener una única instancia de
/// la clase <c>Lobby</c> en todo el programa, por lo que nos permite evitar problemas
/// en el emparejamiento de jugadores.
/// </summary>
public class Lobby
{
    /// <summary>
    /// Instancia única de la clase <c>Lobby</c>.
    /// </summary>
    private static Lobby instance;
    
    /// <summary>
    /// Lista de jugadores que están esperando para unirse a un juego.
    /// </summary>
    private List<DiscordMember> waitingPlayers = new();
    
    /// <summary>
    /// Lista de salas de juego activas.
    /// </summary>
    private List<GameRoom> rooms = new();

    /// <summary>
    /// Constructor privado para implementar el patrón Singleton, evita crear instancias fuera de la clase.
    /// </summary>
    private Lobby() { }
    
    /// <summary>
    /// Método estático que obtiene la instancia única de la clase Lobby.
    /// </summary>
    /// <returns>La instancia de <c>Lobby</c>.</returns>
    /// <remarks>Si la instancia de Lobby no fue inicializada, la inicializa y la retorna. Si ya fue inicializada, solo la retorna.</remarks>
    public static Lobby GetInstance()
    {
        if (instance == null!)
        {
            instance = new Lobby();
        }
        return instance;
    }
    
    /// <summary>
    /// Agrega un jugador a la lista de espera si aún no está en ella.
    /// </summary>
    /// <param name="context">El contexto de la interacción de Discord.</param>
    /// <returns>
    /// <c>string</c> Un mensaje indicando si el jugador fue agregado o si ya estaba en la lista.
    /// </returns>
    public string AddWaitingPlayer(InteractionContext context)
    {
        DiscordMember member = context.Member;
        if (!IsWaitingPlayerByName(member.Username) && waitingPlayers.Count < Game.MaxPlayers)
        {
            waitingPlayers.Add(member);
            
            return $"**{member.Username.ToUpper()}** se ha agregado a la lista de espera.";
        }
        return $"**{member.Username.ToUpper()}** ya se encuentra esperando rival.";
    }
    
    /// <summary>
    /// Verifica si un jugador está en la lista de espera a través de su nombre.
    /// </summary>
    /// <param name="name">Nombre del jugador a verificar.</param>
    /// <returns><c>true</c> si el jugador está en la lista de espera. De lo contrario, <c>false</c>.</returns>
    private bool IsWaitingPlayerByName(string name)
    {
        return waitingPlayers.Any(player => player.Username == name);
    }

    /// <summary>
    /// Intenta iniciar una partida si hay suficientes jugadores en la lista de espera.
    /// </summary>
    /// <param name="context">El contexto de la interacción de Discord.</param>
    /// <remarks>
    /// <para>
    /// Para iniciar la partida. Se necesita que la cantidad de jugadores esperando en el lobby sea igual
    /// a la cantidad máxima de jugadores permitida en la clase <c>Game</c>. Por defecto, <c>2</c> jugadores.
    /// </para>
    /// <para>
    /// Si la partida es iniciada se crea una <c>GameRoom</c> y agrega a los jugadores emparejados a la misma.
    /// Además, se vacía la lista <c>waitingPlayers</c> para que otros jugadores puedan buscar partida.
    /// </para>
    /// </remarks>
    public async Task TryToStartGame(InteractionContext context)
    {
        if (waitingPlayers.Count == Game.MaxPlayers)
        {
            GameRoom room = new GameRoom();
            foreach (DiscordMember member in waitingPlayers)
            { 
                room.AddMember(member);
            }
            await room.CreateRoomAsync(context);
            rooms.Add(room);

            waitingPlayers.Clear();
        }
    }

    /// <summary>
    /// Elimina todas las salas de batalla activas.
    /// </summary>
    /// <param name="context">El contexto de la interacción de Discord.</param>
    public async Task ClearLobby(InteractionContext context)
    {
        foreach (var room in rooms)
        {
            await room.DeleteRoom(context);
        }
        rooms.Clear();
    }

    /// <summary>
    /// Obtiene una sala de juego por su ID.
    /// </summary>
    /// <param name="id">Id de la sala de juego.</param>
    /// <returns><c>GameRoom</c> Sala de juego correspondiente al ID proporcionado. Si no lo encuentra <c>null</c>.</returns>
    public GameRoom GetGameRoomById(ulong id)
    {
        return rooms.Find(room => room.Id == id)!;
    }

    /// <summary>
    /// Obtiene los nombres de los jugadores en la lista de espera.
    /// </summary>
    /// <returns><c>string</c> Cadena que contiene los nombres de los jugadores en la lista de espera.</returns>
    public string GetWaitingPlayersName()
    {
        return string.Join("\n", waitingPlayers.Select(player => $"\ud83e\uddcc **{player.Username.ToUpper()}**"));
    }
}