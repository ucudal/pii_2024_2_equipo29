namespace Library;

public interface IItem
{
    int Amount { get; }
    string Use(Pokemon currentPokemon);
}