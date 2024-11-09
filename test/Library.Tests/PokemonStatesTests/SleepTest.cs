using Library.States;

namespace Library.Tests.PokemonStatesTests;

public class SleepTest
{
    private Sleep sleep;
    private Pokemon pokemon;
    
    [SetUp]
    public void Setup()
    {
        sleep = new Sleep();
    }

    [Test]
    public void SleepNameAreEqualEnumState()
    {
        Assert.AreEqual(sleep.Name,EnumState.Sleep);
    }
    [Test]
    public void SleepNameAreEquEnumState()
    {
        int remainingTurns = sleep.GetRemainingTurnsWithEffect();
        sleep.ApplyEffect(pokemon);
        if (remainingTurns==0)
        {
            Assert.Pass();
        }
    }
}