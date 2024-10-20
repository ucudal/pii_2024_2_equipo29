namespace Library;

public interface IPokemonManager
{
    Pokemon CurrentPokemon { get; }
    void AddPokemon(Pokemon pokemon);
    void ChangePokemon(Pokemon pokemon);
    Pokemon GetNextPokemon();
}