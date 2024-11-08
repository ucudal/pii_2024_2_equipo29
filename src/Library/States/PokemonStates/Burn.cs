namespace Library.States;

public class Burn : IPokemonState
{
    public EnumState Name { get; }
    public string Emoji { get; } = "\ud83d\udd25";

    public Burn()
    {
        Name = EnumState.Burn;
    }
    
    public void ApplyEffect(Pokemon pokemon)
    {
        pokemon.Hp -= (int)Math.Round(0.1 * pokemon.InitialHp);
    }
}