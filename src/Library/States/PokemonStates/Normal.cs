namespace Library.States;

public class Normal : IPokemonState
{
    public EnumState Name { get; }
    
    public Normal()
    {
        Name = EnumState.Normal;
    }
}