namespace Library;

public class Player
{
    private string name;
    public string Name
    {
        get { return name; }
    }
    private List<Pokemon> pokemons = new List<Pokemon>();
    public Pokemon currentPokemon;
    private int maxPokemons = 6;


    public void AddPokemon(Pokemon pokemon)
    {
        if (pokemons.Count < 6)
        {
            pokemons.Add(pokemon);
            currentPokemon ??= pokemon;
        }
    }

    public void ChangePokemon(Pokemon pokemon)
    {
        if (pokemons.Contains(pokemon))
        {
            currentPokemon = pokemon;
        }
    }

    public void Attack(Player enemyPlayer, int moveSlot)
    {
        currentPokemon.Attack(enemyPlayer.currentPokemon, moveSlot);
    }
    public bool HasLost()
    {
        foreach (var pokemon in pokemons)
        {
            if (!pokemon.isDead())
            {
                return false;
            }
        }

        return true;
    }

    public void ClearPokemons()
    {
        pokemons.Clear();
        currentPokemon = null;
    }
}