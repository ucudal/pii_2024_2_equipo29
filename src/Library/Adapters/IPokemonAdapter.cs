namespace Library.Adapters;

/// <summary>
/// Define la interfaz para el adaptador de la PokeAPI.
/// </summary>
/// <remarks>
/// Esta interfaz proporciona métodos para obtener un pokemon con el formato correcto.
/// </remarks>
public interface IPokemonAdapter
{ 
    /// <summary>
    /// Obtiene un pokemon por su nombre.
    /// </summary>
    /// <param name="pokemonName">El nombre del pokemon a obtener.</param>
    Task<Pokemon> GetPokemonAsync(string pokemonName);
}