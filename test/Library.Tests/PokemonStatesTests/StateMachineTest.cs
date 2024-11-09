using System.Diagnostics;
using Library.States;

namespace Library.Tests.PokemonStatesTests;

public class StateMachineTest
{
    private Pokemon pokemon;
    
    [SetUp]
    public void Setup()
    {
        pokemon = new Pokemon();
    }

    [Test]
    public void StateMachineApplyState()
    {
        IPokemonState pokemonState;
        
        pokemonState = new Normal();
        
        pokemon.StateMachine.CurrentState = pokemonState;
        
        var pokemonEstado = pokemon.StateMachine.CurrentState;
        
        Assert.AreEqual(pokemonEstado.Name, EnumState.Normal);
    }
}