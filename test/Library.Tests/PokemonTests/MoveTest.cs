using Library.Adapters;
using Library.Services;

namespace Library.Tests.PokemonTest;

public class MoveTest
{
    private Move move;
    private Move specialMove;
    
    [SetUp]
    public async Task Setup()
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        
        Pokemon pokemon = await pokemonAdapter.GetPokemonAsync("pikachu");
        move = pokemon.Moves[1];
        specialMove = pokemon.Moves[3];
    }
    
    [Test]
    public void MoveConstructionTest()
    {
        Assert.IsNotNull(move);
    }
    
    [Test]
    public void ShowTurnTest()
    {
        Assert.IsNotNull(move.ViewMove());
    }
    
    [Test]
    public void IsNotSpecialMoveTest()
    {
        Assert.IsFalse(move.IsSpecialMove);
    }
    
    [Test]
    public void IsSpecialMoveTest()
    {
        Assert.IsTrue(specialMove.IsSpecialMove);
    }
}