using Library.States;

namespace Library.Tests.PokemonStatesTests;

public class ParalyzeTest
{
    private Paralyze paralyze;
    
    [SetUp]
    public void Setup()
    {
        paralyze = new Paralyze();
    }

    [Test]
    public void ParalyzeNameAreEqualEnumState()
    {
        Assert.AreEqual(paralyze.Name,EnumState.Paralyze);
    }
    
    [Test]
    public void PokemonCanLostTurnOrAttack()
    {
        do
        {

        } while (paralyze.HasLostTurn());

        do
        {

        } while (!paralyze.HasLostTurn());

        Assert.Pass();
    }
}
