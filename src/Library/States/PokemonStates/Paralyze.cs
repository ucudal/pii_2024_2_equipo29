namespace Library.States;

public class Paralyze : IPokemonState
{
    public string Name { get; }
    
    public Paralyze()
    {
        Name = EnumState.Paralyze.ToString();
    }

    public bool CanAttack()
    {
        return new Random().Next(2) == 0;
    }
}