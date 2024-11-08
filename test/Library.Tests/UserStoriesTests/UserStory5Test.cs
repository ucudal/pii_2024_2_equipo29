namespace Library.Tests.UserStoriesTests;

public class UserStory5Test
{
    // 5. Como jugador, quiero saber de quién es el turno para estar seguro de cuándo atacar o esperar.
    // Criterios de aceptación:
    // En la pantalla se muestra claramente un indicador que señala de quién es el turno actual.
    
    
    [Test]
    public async Task ShowTurnTest()                    // Igualmente esta Historia se prueba en historias anteriores
    {
        string pokemonName = "pikachu";
        string player1Name = "Jugador1";
        string player2Name = "Jugador2";
        GameCommands commands = new GameCommands();
        commands.AddPlayer(player1Name);
        commands.AddPlayer(player2Name);

        await commands.ChoosePokemon("Jugador1", pokemonName);
        await commands.ChoosePokemon("Jugador2", pokemonName);

        commands.StartBattle();
        
        string msg = commands.ShowTurn();

        string msgEsperado = $"Turno de: **{commands.GetPlayerInTurn().Name.ToUpper()}**\n" +
                             commands.GetPlayerInTurn().CurrentPokemon.ViewPokemon() +
                             $"{commands.GetPlayerInTurn().ViewItems()}\n";

        Assert.IsTrue(msg.Contains(msgEsperado));
    }
    
    [Test]
    public async Task ShowTurnWithChangingTurnTest()
    {
        string pokemonName = "pikachu";
        string player1Name = "Jugador1";
        string player2Name = "Jugador2";
        GameCommands commands = new GameCommands();
        commands.AddPlayer(player1Name);
        commands.AddPlayer(player2Name);

        await commands.ChoosePokemon("Jugador1", pokemonName);
        await commands.ChoosePokemon("Jugador2", pokemonName);

        commands.StartBattle();
        commands.NextTurn();
        string msg = commands.ShowTurn();

        string msgEsperado = $"Turno de: **{commands.GetPlayerInTurn().Name.ToUpper()}**\n" +
                             commands.GetPlayerInTurn().CurrentPokemon.ViewPokemon() +
                             $"{commands.GetPlayerInTurn().ViewItems()}\n";

        Assert.IsTrue(msg.Contains(msgEsperado));
    }
}