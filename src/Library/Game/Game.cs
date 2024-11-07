namespace Library;

public class Game
{
    public static int MaxPlayers { get; } = 2;
    private List<Player> players = new();
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
    public bool HasStarted { get; private set; }
    public bool IsFullPlayers
    {
        get => players.Count == MaxPlayers;
    }

    public void AddPlayer(string playerName)
    {
        if (!IsFullPlayers) players.Add(new Player(playerName));
        if (IsFullPlayers) PlayerInTurn = GetRandomPlayer();
    }

    public void ToogleTurn()
    {
        PlayerInTurn = PlayerNotInTurn;

        PlayerInTurn.CurrentPokemon.UpdateCoolDownSpecialMove();
        PlayerInTurn.CurrentPokemon.StateMachine.ApplyEffect(PlayerInTurn.CurrentPokemon);
    }

    public void Start()
    {
        if (IsFullPlayers)
        {
            HasStarted = true;
        }
    }
    
    public bool IsPlayerNameInTurn(string playerName)
    {
        return PlayerInTurn.Name == playerName;
    }

    public void Reset()  // Era la idea si
    {
        HasStarted = false;
        foreach (var player in players)
        {
            player.ClearPokemons();
        }
    }

    public Player GetWinner()
    {
        if (PlayerInTurn.HasLost()) return PlayerNotInTurn;
        if (PlayerNotInTurn.HasLost()) return PlayerInTurn;
        return null!;
    }

    public string ViewTurn()
    {
        return $"Turno de **{PlayerInTurn.Name.ToUpper()}**\n{PlayerInTurn.CurrentPokemon.ViewPokemon()}\n";
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
        return players.All(player => player.HasAllPokemnos());
    }
    
    public bool AllPlayersHavePokemons()
    {
        return players.All(player => player.PlayersHavePokemons());
    }
    
    public string ViewAllPokemons()
    {
        string msg = "";
        foreach (var player in players)
        {
            msg +=$"{player.Name.ToUpper()}:\n{player.ViewAllPokemons()}\n";
        }

        return msg;
    }
}