using Library.States;

namespace Library.Tests.PokemonStatesTests;

public class PoisonTest
{
    private Poison poison;
    private Pokemon pokemon;
    [SetUp]
    public void Setup()
    {
        poison = new Poison();
        pokemon = new Pokemon
        {
            Hp = 100,
            InitialHp = 100
        };
    }

    [Test]
    public void PoisonNameAreEqualEnumState()
    {
        Assert.AreEqual(poison.Name,EnumState.Poison);
    }
    [Test]
    public void BurnNameAreEqualEnumState()
    {
        int HpObjetivo = pokemon.Hp - (int)Math.Round(pokemon.InitialHp * 0.05);
        poison.ApplyEffect(pokemon);
        Assert.AreEqual(pokemon.Hp, HpObjetivo);
    }
}