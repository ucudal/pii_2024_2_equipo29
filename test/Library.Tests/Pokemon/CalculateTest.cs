using Library.Adapters;

namespace Library.Tests.Pokemon;

public class CalculateTest
{
    [SetUp]
    public void Setup()
    {
        PokemonAdapter pokemonAdapter = new ();
        Pokemon attacker = pokemonAdapter.GetPokemonAsync()
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}