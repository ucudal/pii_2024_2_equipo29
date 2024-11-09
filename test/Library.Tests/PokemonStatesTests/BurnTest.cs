using System.Runtime.CompilerServices;
using DSharpPlus.EventArgs;
using Library.States;

namespace Library.Tests.PokemonStatesTests;

public class BurnTest
{ 
    private Pokemon pokemon;
    private Burn burn;
    
    [SetUp]
    public void Setup()
    {
        pokemon = new Pokemon
        {
            Hp = 100,
            InitialHp = 100,
        };
        burn = new Burn();
    }

    [Test]
    public void BurnNameAreEqualEnumState()
    {
        Assert.AreEqual(burn.Name,EnumState.Burn);
    }
    [Test]
    public void BurrAreApplyed()
    {
        int HpObjetivo = pokemon.Hp - (int)Math.Round(pokemon.InitialHp * 0.1);
        burn.ApplyEffect(pokemon);
        Assert.AreEqual(pokemon.Hp, HpObjetivo);
    }
}