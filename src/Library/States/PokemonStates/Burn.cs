namespace Library.States;

public class Burn : IPokemonState
{
    public string Name { get; }
    
    public Burn()
    {
        Name = EnumState.Burn.ToString();
    }
    
    public void ApplyEffect(Pokemon pokemon)
    {
        pokemon.Hp -= (int)Math.Round(0.1 * pokemon.InitialHp);
    }
}