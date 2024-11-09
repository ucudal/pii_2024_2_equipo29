using System.Text.Json;
using Library.Services;
using Library.States;

namespace Library.Adapters;

/// <summary>
/// Clase para adaptar datos obtenidos de la PokeAPI y convertirlos en instancias de <c>Pokemon</c>.
/// Se utiliza el patrón <b>Adapter</b>.
/// </summary>
public class PokemonAdapter
{
    /// <summary>
    /// Atributo utilizado para el servicio de la PokeAPI.
    /// </summary>
    private PokeApiService pokeApiService;

    /// <summary>
    /// Constructor para inicializar el servicio de la PokeAPI.
    /// </summary>
    /// <param name="pokeApiService">Servicio de la PokeAPI utilizado para obtener datos de los pokemon.</param>
    public PokemonAdapter(PokeApiService pokeApiService)
    {
        this.pokeApiService = pokeApiService;
    }

    /// <summary>
    /// Obtiene los datos de un pokemon por su nombre utilizando <c>PokeApiService</c> y los adapta a un objeto <c>Pokemon</c>.
    /// </summary>
    /// <param name="pokemonName">Nombre del Pokémon a buscar en la API.</param>
    /// <returns>
    /// Una instancia de <c>Pokemon</c> con los datos obtenidos,
    /// <c>null</c> en caso de error o si el pokemon no tiene al menos cuatro <c>Move</c>.
    /// </returns>
    /// <exception cref="HttpRequestException">Se lanza si hay problemas al hacer la solicitud HTTP a la PokeAPI.</exception>
    /// <remarks>
    /// <para>
    ///   Este método usa propiedades JSON para extraer información sobre los datos necesarios para crear un objeto <c>Pokemon</c>.
    /// </para>
    /// <para>
    ///   Los <c>Move</c> son agregados al pokemon solo si tienen un power y accuary mayor a cero.
    /// </para>
    /// </remarks>
    public async Task<Pokemon> GetPokemonAsync(string pokemonName)
    {
        JsonDocument apiResponse;
        try
        {
            apiResponse = await pokeApiService.GetPokemonDataAsync(pokemonName);
        }
        catch(HttpRequestException)
        {
            return null!;
        }
        
        var pokemonJson = apiResponse.RootElement;

        Pokemon pokemon = new Pokemon
        {
            Name = pokemonJson
                .GetProperty("name")
                .GetString() ?? string.Empty,

            InitialHp = pokemonJson
                .GetProperty("stats")[0]
                .GetProperty("base_stat")
                .GetInt32() * 7,

            Hp = pokemonJson
                .GetProperty("stats")[0]
                .GetProperty("base_stat")
                .GetInt32() * 7,

            AttackPoints = pokemonJson
                .GetProperty("stats")[1]
                .GetProperty("base_stat")
                .GetInt32(),

            DefensePoints = pokemonJson
                .GetProperty("stats")[2]
                .GetProperty("base_stat")
                .GetInt32(),

            SpecialAttackPoints = pokemonJson
                .GetProperty("stats")[3]
                .GetProperty("base_stat")
                .GetInt32(),

            SpecialDefensePoints = pokemonJson
                .GetProperty("stats")[4]
                .GetProperty("base_stat")
                .GetInt32(),

            Types = pokemonJson
                .GetProperty("types")
                .EnumerateArray()
                .Select(type => new Type
                {
                    Name = type
                        .GetProperty("type")
                        .GetProperty("name")
                        .GetString() ?? string.Empty
                })
                .ToList(),

            Moves = new List<Move>(),
            
            ImgUrl = pokemonJson
                .GetProperty("sprites")
                .GetProperty("other")
                .GetProperty("official-artwork")
                .GetProperty("front_default")
                .GetString() ?? string.Empty
        };

        var moves = pokemonJson
            .GetProperty("moves")
            .EnumerateArray();

        foreach (var moveEntry in moves)
        {
            string moveName = moveEntry
                .GetProperty("move")
                .GetProperty("name")
                .GetString() ?? string.Empty;

            var moveData = await pokeApiService.GetMoveDataAsync(moveName);
            var moveJson = moveData.RootElement;

            int power;
            int accuary;
            try
            {
                power = moveJson
                    .GetProperty("power")
                    .GetInt32();
                accuary = moveJson
                    .GetProperty("accuracy")
                    .GetInt32();
            }
            catch
            {
                power = 0;
                accuary = 0;
            }

            if (power <= 0 || accuary <= 0) continue;
            
            Move move = new Move
            {
                Name = moveName,
                Accuracy = accuary,
                Type = new Type
                {
                    Name = moveJson
                        .GetProperty("type")
                        .GetProperty("name")
                        .GetString() ?? string.Empty
                },
                State = pokemon.Moves.Count == 3
                    ? DicPokemonTypeStates.States[moveJson
                        .GetProperty("type")
                        .GetProperty("name")
                        .GetString() ?? "normal"]
                    : EnumState.Normal,
                Power = power,
                IsSpecialMove = pokemon.Moves.Count == 3
            };
            
            pokemon.Moves.Add(move);
            if (pokemon.Moves.Count == 4) break;
        }
        
        if (pokemon.Moves.Count < 4) return null!;

        return pokemon;
    }
}