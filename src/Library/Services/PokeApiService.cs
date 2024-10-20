using System.Text.Json;

namespace Library.Services;

public class PokeApiService
{
    private HttpClient httpClient;

    public PokeApiService()
    {
        httpClient = new HttpClient();
    }

    public async Task<JsonDocument> GetPokemonDataAsync(string pokemonName)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{pokemonName}");
        if (!response.IsSuccessStatusCode) throw new HttpRequestException("No se pudieron obtener datos de la PokeAPI.");
        
        string jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonDocument.Parse(jsonResponse);
    }
    
    public async Task<JsonDocument> GetMoveDataAsync(string moveName)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"https://pokeapi.co/api/v2/move/{moveName}");
        if (!response.IsSuccessStatusCode) throw new HttpRequestException("No se pudieron obtener datos de la PokeAPI.");
        
        string jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonDocument.Parse(jsonResponse);
    }
}