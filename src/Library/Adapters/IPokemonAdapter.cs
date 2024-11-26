namespace Library.Adapters;

public interface IPokemonAdapter
{ 
    Task<Pokemon> GetPokemonAsync(string pokemonName);
}