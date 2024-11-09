using Library.States;

namespace Library.Tests.PokemonStatesTests;

public class StateApplierTest
{
    [Test]
    public void ApplyEffect()
    {
        Pokemon pokemon = new Pokemon();
        StateApplier.ApplyStateEffect(pokemon.StateMachine, EnumState.Burn);
        string msgStatus = StateApplier.ApplyStateEffect(pokemon.StateMachine, EnumState.Burn);
        
        Assert.That($"El pokemon ya tiene el efecto **{EnumState.Burn.ToString().ToUpper()}**.", Is.EqualTo(msgStatus));
    }

    [Test]
    public void ChangeEffect()
    {
        Pokemon pokemon = new Pokemon();
        StateApplier.ApplyStateEffect(pokemon.StateMachine, EnumState.Burn);
        Assert.That(pokemon.StateMachine.CurrentState.Name, Is.EqualTo(EnumState.Burn));
        
        StateApplier.ApplyStateEffect(pokemon.StateMachine, EnumState.Poison);
        Assert.That(pokemon.StateMachine.CurrentState.Name, Is.Not.EqualTo(EnumState.Poison));
    }
}