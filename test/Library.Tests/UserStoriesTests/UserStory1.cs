namespace Library.Tests.UserStoriesTests;

public class UserStory1
{
// 1. Como jugador, quiero elegir 6 Pokémons del catálogo disponible para comenzar la batalla.
// Criterios de aceptación:
// El jugador puede seleccionar 6 Pokémons de una lista o catálogo.
// Los Pokémons seleccionados se muestran en la pantalla del jugador.

    private GameCommands commands = new GameCommands();

    private string msgExpected =
        "**PIKACHU** ha sido agregado al equipo de **JUGADOR1**  ***(1/6)***\n" +
        "**MEW** ha sido agregado al equipo de **JUGADOR1**  ***(2/6)***\n" +
        "**ONIX** ha sido agregado al equipo de **JUGADOR1**  ***(3/6)***\n" + 
        "**MEWTWO** ha sido agregado al equipo de **JUGADOR1**  ***(4/6)***\n" + 
        "**SNORLAX** ha sido agregado al equipo de **JUGADOR1**  ***(5/6)***\n" +
        "**CHARMANDER** ha sido agregado al equipo de **JUGADOR1**  ***(6/6)***\n";
    
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
        
        Assert.That(msgExpected, Is.EqualTo(msg));
    }
}