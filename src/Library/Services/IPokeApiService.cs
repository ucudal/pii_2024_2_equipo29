using System.Text.Json;

namespace Library.Services;

/// <summary>
/// Define la interfaz para el servicio de la PokeAPI.
/// </summary>
/// <remarks>
/// Esta interfaz proporciona métodos para obtener pokemon y sus movimientos.
/// </remarks>
public interface IPokeApiService
{
    /// <summary>
    /// Obtiene un pokemon de la API por su nombre.
    /// </summary>
    /// <param name="pokemonName">El nombre del pokemon a obtener.</param>
    Task<JsonDocument> GetPokemonDataAsync(string pokemonName);
    
    /// <summary>
    /// Obtiene un movimiento de la API por su nombre.
    /// </summary>
    /// <param name="moveName">El nombre del movimiento a obtener.</param>
    Task<JsonDocument> GetMoveDataAsync(string moveName);
}