namespace Library.States;

public class Normal : IPokemonState
{
    public string Name { get; }
    
    public Normal()
    {
        Name = EnumState.Normal.ToString();
    }
}