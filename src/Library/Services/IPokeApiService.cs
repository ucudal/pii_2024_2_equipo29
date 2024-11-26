using System.Text.Json;

namespace Library.Services;

public interface IPokeApiService
{
    Task<JsonDocument> GetPokemonDataAsync(string pokemonName);
    Task<JsonDocument> GetMoveDataAsync(string moveName);
}