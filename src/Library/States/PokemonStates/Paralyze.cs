namespace Library.States;

public class Paralyze : IPokemonState
{
    public EnumState Name { get; }
    
    public Paralyze()
    {
        Name = EnumState.Paralyze;
    }

    public bool HasLostTurn()
    {
        return new Random().Next(2) == 0;
    }
}