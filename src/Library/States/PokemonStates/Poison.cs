namespace Library.States;

public class Poison : IPokemonState
{
    public EnumState Name { get; }
    public string Emoji { get; } = "\u2620\ufe0f";

    public Poison()
    {
        Name = EnumState.Poison;
    }
    
    public void ApplyEffect(Pokemon pokemon)
    {
        pokemon.Hp -= (int)Math.Round(0.05 * pokemon.InitialHp);
    }
}