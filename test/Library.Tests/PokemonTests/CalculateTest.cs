using Library.Adapters;
using Library.Services;

namespace Library.Tests.PokemonTest;

public class CalculateTest
{
    private ICalculate calculate;
    private Pokemon defender;
    private Pokemon attacker;
    
    [SetUp]
    public async Task Setup()
    {
        IPokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        
        attacker = await pokemonAdapter.GetPokemonAsync("pikachu");
        defender = await pokemonAdapter.GetPokemonAsync("pikachu");
        calculate = new Calculate();
    }

    [Test]
    public void CalculateDamageTest()
    {
        int min = 39;  // Calculos hechos manualmente para este caso  0.1 * 1 * 1 * 85 * (((1.2 * 55 * 40) / (25 * 40)) + 2)
        int max = 47;  // Calculos hechos manualmente para este caso  0.1 * 1 * 1 * 100 * (((1.2 * 55 * 40) / (25 * 40)) + 2)
        if (calculate.CalculateDamage(attacker, defender, attacker.Moves[1], out int dmg, out bool isEffective))
        {
            min += (int)Math.Round(min*0.2);
            max += (int)Math.Round(max*0.2);
        }
        Assert.That(dmg >= min && max >= dmg);
    }
    
    [Test]
    public void CalculateDamageCanCritTest()
    {
        bool crit;
        do
        {
            crit = calculate.CalculateDamage(attacker, defender, attacker.Moves[1], out int dmg, out bool isEffective);
        } while (!crit);
        Assert.Pass();
    }
}