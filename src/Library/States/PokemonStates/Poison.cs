namespace Library.States;

public class Poison : IPokemonState
{
    public EnumState Name { get; }
    
    public Poison()
    {
        Name = EnumState.Poison;
    }
    
    public void ApplyEffect(Pokemon pokemon)
    {
        pokemon.Hp -= (int)Math.Round(0.05 * pokemon.InitialHp);
    }
}