namespace Library;

public interface IPokemonManager
{
    string Name { get; }
    Pokemon CurrentPokemon { get; }
    void AddPokemon(Pokemon pokemon);
    void ChangePokemon(Pokemon pokemon);
    Pokemon GetNextPokemon();
}