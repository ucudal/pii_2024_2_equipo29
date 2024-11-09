using Library.Adapters;
using Library.Services;

namespace Library.Tests.Game;
using Library;

public class GameTests
{
    [Test]
    public void AddPlayerTest()
    {
        Game game = new();
        bool fullPlayersAtFirst = game.IsFullPlayers;
        for (var i = 1; i < Game.MaxPlayers + 1; i++)
        {
            game.AddPlayer(new Player($"player{i}"));
        }
        bool fullPlayersAtEnd = game.IsFullPlayers;
        
        Assert.That(fullPlayersAtFirst, Is.Not.EqualTo(fullPlayersAtEnd));
    }
    
    [Test]
    public void ToggleTurnTest()
    {
        Game game = new();
        for (var i = 1; i < Game.MaxPlayers + 1; i++)
        {
            game.AddPlayer(new Player($"player{i}"));
        }
        game.Start();
        Player initialPlayerInTurn = game.PlayerInTurn;
        game.ToogleTurn();
        Player finalPlayerInTurn = game.PlayerNotInTurn;
        
        Assert.That(initialPlayerInTurn.Name, Is.EqualTo(finalPlayerInTurn.Name));
    }

    [Test]
    public void StartTest()
    {
        Game game = new();
        for (var i = 1; i < Game.MaxPlayers + 1; i++)
        {
            game.AddPlayer(new Player($"player{i}"));
        }
        game.Start();
        Assert.IsTrue(game.HasStarted);
    }
    
    [Test]
    public void StartWithOnlyOnePlayerTest()
    {
        Game game = new();
        game.Reset();
        game.AddPlayer(new($"player1"));
        
        game.Start();
        Assert.IsFalse(game.HasStarted);
    }
    
    [Test]
    public void IsPlayerInTurnTest()
    {
        Game game = new();
        for (var i = 1; i < Game.MaxPlayers + 1; i++)
        {
            game.AddPlayer(new Player($"player{i}"));
        }
        game.Start();
        
        
        Assert.IsTrue(game.IsPlayerNameInTurn(game.PlayerInTurn.Name));
    }
    
    [Test]
    public void IsNotPlayerInTurnTest()
    {
        Game game = new();
        for (var i = 1; i < Game.MaxPlayers + 1; i++)
        {
            game.AddPlayer(new Player($"player{i}"));
        }
        game.Start();
        
        
        Assert.IsFalse(game.IsPlayerNameInTurn(game.PlayerNotInTurn.Name));
    }
    
    [Test]
    public async Task ResetTest()
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        
        Pokemon pokemon1 = await pokemonAdapter.GetPokemonAsync("pikachu");
        Pokemon pokemon2 = await pokemonAdapter.GetPokemonAsync("pikachu");
        Player player1 = new("player1");
        player1.AddPokemon(pokemon1);
        Player player2 = new("player2");
        player2.AddPokemon(pokemon2);
        
        Game game = new();
        game.AddPlayer(player1);
        game.AddPlayer(player2);
        game.Start();
        
        game.Reset();
        Assert.That(!game.HasStarted && !game.AllPlayersHavePokemons());
    }
    
    [Test]
    public async Task GetWinnerTest()
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());

        Pokemon pokemon1 = await pokemonAdapter.GetPokemonAsync("pikachu");
        Pokemon pokemon2 = await pokemonAdapter.GetPokemonAsync("pikachu");
        pokemon1.Hp = 1;
        pokemon2.Hp = 1;
        Player player1 = new("player1");
        player1.AddPokemon(pokemon1);
        Player player2 = new("player2");
        player2.AddPokemon(pokemon2);
        
        Game game = new();
        game.AddPlayer(player1);
        game.AddPlayer(player2);
        game.Start();

        string winnerName = game.PlayerInTurn.Name;
        game.PlayerInTurn.Attack(game.PlayerNotInTurn, 1);
        Assert.That(game.GetWinner().Name, Is.EqualTo(winnerName));
    }
    
    [Test]
    public async Task ShowTurnTest()
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());

        Pokemon pokemon1 = await pokemonAdapter.GetPokemonAsync("pikachu");
        Pokemon pokemon2 = await pokemonAdapter.GetPokemonAsync("pikachu");
        Player player1 = new("player1");
        player1.AddPokemon(pokemon1);
        Player player2 = new("player2");
        player2.AddPokemon(pokemon2);
        
        Game game = new();
        game.AddPlayer(player1);
        game.AddPlayer(player2);
        game.Start();
        
        Assert.That(game.ViewAllPokemons(), Is.Not.Null);
    }
    
    [Test]
    public async Task AllPlayersReadyTest()  // 7:21 am pido perdon por este codigo
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());

        Pokemon pokemon1 = await pokemonAdapter.GetPokemonAsync("pikachu");
        Pokemon pokemon2 = await pokemonAdapter.GetPokemonAsync("mew");
        Pokemon pokemon3 = await pokemonAdapter.GetPokemonAsync("mewtwo");
        Pokemon pokemon4 = await pokemonAdapter.GetPokemonAsync("onix");
        Pokemon pokemon5 = await pokemonAdapter.GetPokemonAsync("snorlax");
        Pokemon pokemon6 = await pokemonAdapter.GetPokemonAsync("charmander");
        
        Pokemon pokemon7 = await pokemonAdapter.GetPokemonAsync("pikachu");
        Pokemon pokemon8 = await pokemonAdapter.GetPokemonAsync("mew");
        Pokemon pokemon9 = await pokemonAdapter.GetPokemonAsync("mewtwo");
        Pokemon pokemon10 = await pokemonAdapter.GetPokemonAsync("onix");
        Pokemon pokemon11= await pokemonAdapter.GetPokemonAsync("snorlax");
        Pokemon pokemon12 = await pokemonAdapter.GetPokemonAsync("charmander");
        
        Player player1 = new("player1");
        player1.AddPokemon(pokemon1);
        player1.AddPokemon(pokemon2);
        player1.AddPokemon(pokemon3);
        player1.AddPokemon(pokemon4);
        player1.AddPokemon(pokemon5);
        player1.AddPokemon(pokemon6);
        Player player2 = new("player2");
        player2.AddPokemon(pokemon7);
        player2.AddPokemon(pokemon8);
        player2.AddPokemon(pokemon9);
        player2.AddPokemon(pokemon10);
        player2.AddPokemon(pokemon11);
        player2.AddPokemon(pokemon12);
        
        Game game = new();
        game.AddPlayer(player1);
        game.AddPlayer(player2);
        
        Assert.That(game.AllPlayersReady(), Is.True);
    }
    
    [Test]
    public async Task NotAllPlayersReadyTest()
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());

        Pokemon pokemon1 = await pokemonAdapter.GetPokemonAsync("pikachu");
        Pokemon pokemon2 = await pokemonAdapter.GetPokemonAsync("pikachu");
        Player player1 = new("player1");
        player1.AddPokemon(pokemon1);
        Player player2 = new("player2");
        player2.AddPokemon(pokemon2);
        
        Game game = new();
        game.AddPlayer(player1);
        game.AddPlayer(player2);
        
        Assert.That(game.AllPlayersReady(), Is.False);
    }
    
    [Test]
    public async Task AllPlayersHavePokemonsTest()
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());

        Pokemon pokemon1 = await pokemonAdapter.GetPokemonAsync("pikachu");
        Pokemon pokemon2 = await pokemonAdapter.GetPokemonAsync("pikachu");
        Player player1 = new("player1");
        player1.AddPokemon(pokemon1);
        Player player2 = new("player2");
        player2.AddPokemon(pokemon2);
        
        Game game = new();
        game.AddPlayer(player1);
        game.AddPlayer(player2);
        
        Assert.That(game.AllPlayersHavePokemons(), Is.True);
    }
    
    [Test]
    public async Task NotAllPlayersHavePokemonsTest()
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());

        Pokemon pokemon1 = await pokemonAdapter.GetPokemonAsync("pikachu");
        Player player1 = new("player1");
        player1.AddPokemon(pokemon1);
        Player player2 = new("player2");
        
        Game game = new();
        game.AddPlayer(player1);
        game.AddPlayer(player2);
        
        Assert.That(game.AllPlayersHavePokemons(), Is.False);
    }
}