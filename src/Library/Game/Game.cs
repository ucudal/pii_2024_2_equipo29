using DSharpPlus.CommandsNext;

namespace Library;

public class Game
{
    public static int MaxPlayers { get; } = 1;
    private List<Player> players = new List<Player>();
    public Player PlayerInTurn { get; private set; }

    public Player PlayerNotInTurn
    {
        get
        {
            return PlayerInTurn == players[0]
                ? players[1] 
                : players[0];
        }
    }

    private int roundCount;
    public bool HasStarted { get; private set; }
    public bool IsFullPlayers
    {
        get => players.Count == MaxPlayers;
    }

    public void AddPlayer(string playerName)
    {
        if (!IsFullPlayers)
        {
            players.Add(new Player(playerName));
        }
    }

    public void ToogleTurn()
    {
        PlayerInTurn = PlayerInTurn == players[0]
            ? players[1] 
            : players[0];
    }

    public void Start()
    {
        if (IsFullPlayers)
        {
            PlayerInTurn = GetRandomPlayer();
            HasStarted = true;
        }
    }

    public void Reset()  // Nose si era esta la idea (REVISAR)
    {
        HasStarted = false;
        foreach (var player in players)
        {
            player.ClearPokemons();
        }
    }

    public Player GetWinner()
    {
        foreach (var player in players)
        {
            if (player.HasLost())
            {
                return player;
            }
        }

        return null!;
    }

    public string ViewTurn()
    {
        return $"Turno de {PlayerInTurn.Name}\n {PlayerInTurn.CurrentPokemon.ViewPokemon()}\n";
    }

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
    private Player GetRandomPlayer()
    {
        return players[new Random().Next(0, players.Count)];
    }

    public bool AllPlayersReady()
    {
        foreach (var player in players)
        {
            if (!player.HasAllPokemnos())
            {
                return false;
            }
        }

        return true;
    }
}