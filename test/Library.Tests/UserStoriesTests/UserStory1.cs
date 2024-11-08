namespace Library.Tests.UserStoriesTests;

public class UserStory1
{
// 1. Como jugador, quiero elegir 6 Pokémons del catálogo disponible para comenzar la batalla.
// Criterios de aceptación:
// El jugador puede seleccionar 6 Pokémons de una lista o catálogo.
// Los Pokémons seleccionados se muestran en la pantalla del jugador.

    private GameCommands commands = new GameCommands();
    
    [SetUp]
    public void Setup()
    {
        commands.AddPlayer("Jugador1");
        commands.AddPlayer("Jugador2");
    }

    [Test]
    public async Task ChoosePokemons()
    {
        string msg = "";
        var (message, _) = await commands.ChoosePokemon("Jugador1", "pikachu");
        msg += message +"\n";
        
        var (message2, _) = await commands.ChoosePokemon("Jugador1", "mew");
        msg += message2 +"\n";
        
        var (message3, _) = await commands.ChoosePokemon("Jugador1", "onix");
        msg += message3 +"\n";
        
        var (message4, _) = await commands.ChoosePokemon("Jugador1", "mewtwo");
        msg += message4 +"\n";
        
        var (message5, _) = await commands.ChoosePokemon("Jugador1", "snorlax");
        msg += message5 +"\n";
        
        var (message6, _) = await commands.ChoosePokemon("Jugador1", "charmander");
        msg += message6 +"\n";
        
        File.WriteAllText("../../../UserStoriesTests/userstory1Result.txt", msg);
        
        FileAssert.AreEqual("../../../UserStoriesTests/userstory1Expected.txt", "../../../UserStoriesTests/userstory1Result.txt");
    }
}