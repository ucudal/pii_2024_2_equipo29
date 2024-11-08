using Library.Adapters;
using Library.Services;

namespace Library.Tests.Game;
using Library;
public class GameTest
{
    Game game = new();
    [SetUp]
    public async Task Setup()
    {

    }
    
    
    [Test]
    public void AddPlayerTest()
    {
        bool fullPlayersAtFirst = game.IsFullPlayers;
        for (var i = 1; i < Game.MaxPlayers + 1; i++)
        {
            game.AddPlayer($"player{i}");
        }
        bool fullPlayersAtEnd = game.IsFullPlayers;
        
        Assert.That(fullPlayersAtFirst, Is.Not.EqualTo(fullPlayersAtEnd));
    }
    
    [Test]
    public void ToggleTurnTest()
    {
        for (var i = 1; i < Game.MaxPlayers + 1; i++)
        {
            game.AddPlayer($"player{i}");
        }
        game.Start();
        Player initialPlayerInTurn = game.PlayerInTurn;
        game.ToogleTurn();
        Player finalPlayerInTurn = game.PlayerNotInTurn;
        
        Assert.That(initialPlayerInTurn.Name, Is.EqualTo(finalPlayerInTurn.Name));
    }
}