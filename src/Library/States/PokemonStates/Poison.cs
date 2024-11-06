namespace Library.States;

public class Poison : IPokemonState
{
    public string Name { get; }
    
    public Poison()
    {
        Name = EnumState.Poison.ToString();
    }
    
    public void ApplyEffect(Pokemon pokemon)
    {
        pokemon.Hp -= (int)Math.Round(0.05 * pokemon.InitialHp);
    }
}