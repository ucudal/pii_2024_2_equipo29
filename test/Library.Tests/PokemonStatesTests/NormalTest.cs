using Library.States;

namespace Library.Tests.PokemonStatesTests;

public class NormalTest
{
    private Normal normal;
    
    [SetUp]
    public void Setup()
    {
        normal = new Normal();
    }

    [Test]
    public void Test1()
    {
        Assert.AreEqual(normal.Name, EnumState.Normal);
    }
}