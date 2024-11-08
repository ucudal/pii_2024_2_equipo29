namespace Library.States;

public class Normal : IPokemonState
{
    public EnumState Name { get; }
    public string Emoji { get; } = "\ud83d\ude34";

    public Normal()
    {
        Name = EnumState.Normal;
    }
}