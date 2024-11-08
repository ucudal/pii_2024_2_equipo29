using System.Diagnostics;
using Library.States;

namespace Library.Tests.PokemonStatesTests;

public class StateMachineTest
{
    private StateMachine stateMachine;
    private Pokemon pokemon;
    private IPokemonState pokemonState;
    [SetUp]
    public void Setup()
    {
        stateMachine = new StateMachine(pokemonState);
    }

    [Test]
    public void StateMachineApplyState()
    {
        var pokemonState = pokemon.StateMachine.CurrentState;
        stateMachine.ApplyEffect(pokemon);
        Assert.AreEqual(pokemonState, pokemon.StateMachine.CurrentState);
    }
}///Test falla, corregir