using Library.Adapters;
using Library.Services;

namespace Library.Tests.UserStoriesTests;

public class DEFENSE_UserStory12Test
{
    [Test]
    public async Task DefenseTest()
    {
        GameCommands commands = new GameCommands();
        string player1Name = "player1";
        string player2Name = "player2";
        commands.AddPlayer(player1Name);
        commands.AddPlayer(player2Name);

        await commands.ChoosePokemon(player1Name, "charizard");
        await commands.ChoosePokemon(player1Name, "mew");
        await commands.ChoosePokemon(player1Name, "bulvasaur");
        await commands.ChoosePokemon(player1Name, "mewtwo");
        await commands.ChoosePokemon(player1Name, "snorlax");
        await commands.ChoosePokemon(player1Name, "eevee");
        
        await commands.ChoosePokemon(player2Name, "squirtle");
        await commands.ChoosePokemon(player2Name, "pikachu");
        await commands.ChoosePokemon(player2Name, "bulvasaur");
        await commands.ChoosePokemon(player2Name, "mewtwo");
        await commands.ChoosePokemon(player2Name, "snorlax");
        await commands.ChoosePokemon(player2Name, "eevee");

        if (!commands.GameHasStarted()) commands.StartBattle();

        IPokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        string mostEffectivePokemon = "";
        if (commands.GetPlayerInTurn().Name.ToLower() == player1Name.ToLower())
        {
            mostEffectivePokemon = GetMostEffectivePokemonName(commands.GetPlayerInTurn(), await pokemonAdapter.GetPokemonAsync("squirtle"));
            
        }
        else
        {
            mostEffectivePokemon = GetMostEffectivePokemonName(commands.GetPlayerInTurn(), await pokemonAdapter.GetPokemonAsync("charizard"));
        }
        Assert.That(commands.BestPokemonForCombat(commands.GetPlayerInTurn().Name).Contains(mostEffectivePokemon));
    }   
    
    
    [TestCase("pikachu", "mew", "onix", "mewtwo", "snorlax", "charmander")]
    [TestCase("blastoise", "rattata", "pidgeot", "mewtwo", "snorlax", "ekans")]
    [TestCase("raichu", "mew", "arcanine", "machamp", "geodude", "rapidash")]
    [TestCase("electabuzz", "gyarados", "eevee", "dragonite", "flareon", "typhlosion")]
    public async Task ChoosePokemons(params string[] pokemonsNames)
    {
        GameCommands commands = new GameCommands();
        string player1Name = "player1";
        string player2Name = "player2";
        commands.AddPlayer(player1Name);
        commands.AddPlayer(player2Name);

        IPokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        Pokemon firstInCombat = null!;
        
        foreach (string pokemonName in pokemonsNames)
        {
            await commands.ChoosePokemon(player1Name, pokemonName);
            await commands.ChoosePokemon(player2Name, pokemonName);
            if (firstInCombat == null!)
            {
                firstInCombat = await pokemonAdapter.GetPokemonAsync(pokemonName);
            }
        }
        
        if (!commands.GameHasStarted()) commands.StartBattle();


        
        string mostEffectivePokemon = GetMostEffectivePokemonName(commands.GetPlayerInTurn(), firstInCombat);
        Console.WriteLine(commands.BestPokemonForCombat(commands.GetPlayerInTurn().Name));
        Assert.That(commands.BestPokemonForCombat(commands.GetPlayerInTurn().Name).Contains(mostEffectivePokemon));
    }


    private string GetMostEffectivePokemonName(Player player, Pokemon enemy)   // Metodo para saber cual es el mas efectivo de manera rapida
    {
        ICalculate calculator = new Calculate();
        string mostEffectivePokemonName = "";
        int mostMovesEffectives = -1;
        foreach (var pokemon in player.Pokemons)
        {
            int effectiveMoves = 0;
            foreach (var move in pokemon.Moves)
            {
                calculator.CalculateDamage(pokemon, enemy, move, out int dmg, out bool isEffective);
                if (isEffective) effectiveMoves++;
            }

            if (effectiveMoves > mostMovesEffectives)
            {
                mostMovesEffectives = effectiveMoves;
                mostEffectivePokemonName = pokemon.Name;
            }
        }
        return mostEffectivePokemonName.ToUpper();
    }
}