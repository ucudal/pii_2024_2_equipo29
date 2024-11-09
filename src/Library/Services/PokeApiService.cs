using System.Text.Json;

namespace Library.Services;
/// <summary>
/// Proporciona servicios para interactuar con la PokeAPI, permitiendo la obtención de datos de Pokémon y movimientos.
/// </summary>
public class PokeApiService
{
    private HttpClient httpClient;
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="PokeApiService"/>.
    /// </summary>

    public PokeApiService()
    {
        httpClient = new HttpClient();
    }
    /// <summary>
    /// Obtiene los datos de un Pokémon específico a partir de su nombre.
    /// </summary>
    /// <param name="pokemonName">El nombre del Pokémon del cual se desean obtener los datos.</param>
    /// <returns>
    /// Una tarea que representa la operación asincrónica. El valor de retorno contiene un <see cref="JsonDocument"/> con los datos del Pokémon.
    /// </returns>
    /// <exception cref="HttpRequestException">
    /// Se lanza si no se pueden obtener datos de la PokeAPI.
    /// </exception>


    public async Task<JsonDocument> GetPokemonDataAsync(string pokemonName)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{pokemonName}");
        if (!response.IsSuccessStatusCode) throw new HttpRequestException("No se pudieron obtener datos de la PokeAPI.");
        
        string jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonDocument.Parse(jsonResponse);
    }
    /// <summary>
    /// Obtiene los datos de un movimiento específico a partir de su nombre.
    /// </summary>
    /// <param name="moveName">El nombre del movimiento del cual se desean obtener los datos.</param>
    /// <returns>
    /// Una tarea que representa la operación asincrónica. El valor de retorno contiene un <see cref="JsonDocument"/> con los datos del movimiento.
    /// </returns>
    /// <exception cref="HttpRequestException">
    /// Se lanza si no se pueden obtener datos de la PokeAPI.
    /// </exception>

    public async Task<JsonDocument> GetMoveDataAsync(string moveName)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"https://pokeapi.co/api/v2/move/{moveName}");
        if (!response.IsSuccessStatusCode) throw new HttpRequestException("No se pudieron obtener datos de la PokeAPI.");
        
        string jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonDocument.Parse(jsonResponse);
    }
}