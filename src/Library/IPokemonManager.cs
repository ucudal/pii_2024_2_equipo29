namespace Library;

public interface IPokemonManager
{
    void AddPokemon(Pokemon pokemon);
    void ChangePokemon(Pokemon pokemon);
    Pokemon GetNextPokemon();
}