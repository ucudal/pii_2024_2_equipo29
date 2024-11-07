namespace Library;

public interface IItem
{
    int Amount { get; }
    string Use(string pokemonName, Player player);
}