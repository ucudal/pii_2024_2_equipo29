namespace Library;

public class Game
{
    private List<Player> players;
    private int maxPlayers;
    private Player PlayerInTurn;
    private int roundCount;
    public bool HasStarted;
    public bool IsFullPlayers
    {
        get { return players.Count == maxPlayers; }
    }

    public void AddPlayer(Player player)
    {
        if (!IsFullPlayers)
        {
            players.Add(player);
        }
    }

    public void ToogleTurn()
    {
        PlayerInTurn = (PlayerInTurn == players[0]) ? players[1] : players[0];
    }

    public void Start()
    {
        if (IsFullPlayers)
        {
            ToogleTurn();
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

        return null;
    }

    public string viewTurn()
    {
        return $"Turno de {PlayerInTurn.Name}\n {PlayerInTurn.currentPokemon.viewPokemon()}\n";
    }
}