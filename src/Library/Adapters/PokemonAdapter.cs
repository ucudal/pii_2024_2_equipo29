using System.Text.Json;
using Library.Services;
using Library.States;

namespace Library.Adapters;

public class PokemonAdapter
{
    private PokeApiService pokeApiService;

    public PokemonAdapter(PokeApiService pokeApiService)
    {
        this.pokeApiService = pokeApiService;
    }

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
                .GetInt32(),
            
            Hp = pokemonJson
                .GetProperty("stats")[0]
                .GetProperty("base_stat")
                .GetInt32(),
            
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
            
            PokemonState = new Normal()
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

            int power = moveJson
                .GetProperty("power")
                .GetInt32();
            
            if (power > 0)
            {
                Move move = new Move
                {
                    Name = moveName, 
                    Accuracy = moveJson
                        .GetProperty("accuracy")
                        .GetInt32(),
                    Type = new Type
                    {
                        Name = moveJson
                            .GetProperty("type")
                            .GetProperty("name")
                            .GetString() ?? string.Empty
                    },
                    Effect = pokemon.Moves.Count == 4 
                        ? PokemonEffects.Effects[moveJson
                            .GetProperty("type")
                            .GetProperty("name")
                            .GetString() ?? "normal"]
                        : EnumEffect.Normal,
                    Power = power
                };

                pokemon.Moves.Add(move);
                if (pokemon.Moves.Count == 4) break;
            }
        }

        return pokemon;
    }
}