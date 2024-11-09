using Library.States;

namespace Library.Tests.PokemonStatesTests;

public class StateApplierTest
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void ApplyEffect()
    {
        Pokemon pokemon = new Pokemon();
        StateApplier.ApplyStateEffect(pokemon.StateMachine, EnumState.Burn);
        string mensajeEstado=StateApplier.ApplyStateEffect(pokemon.StateMachine, EnumState.Burn);
        Assert.AreEqual(mensajeEstado,$"El pokemon ya tiene el efecto **{EnumState.Burn.ToString().ToUpper()}**." );
    }

    [Test]
    public void Test2()
    {
        Pokemon pokemon = new Pokemon();
        StateApplier.ApplyStateEffect(pokemon.StateMachine, EnumState.Burn);
        StateApplier.ApplyStateEffect(pokemon.StateMachine, EnumState.Poison);
        Assert.AreNotEqual(pokemon.StateMachine.CurrentState.Name, EnumState.Burn);
    }
}