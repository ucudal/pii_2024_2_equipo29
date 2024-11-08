namespace Library.Tests.UserStoriesTests;

public class UserStory7Test
{
    // 7. Como jugador, quiero poder cambiar de Pokémon durante una batalla.
    // Criterios de aceptación:
    // Al cambiar de Pokémon se pierde el turno

    [TestCase("kartana", "shedinja")]
    [TestCase("kartana", "abra")]
    [TestCase("dragonite", "pichu")]
    [TestCase("dragonite", "shedinja")]
    public async Task ChangePokemonTest(string pokemonName1, string pokemonName2)
    {
        string player1Name = "Jugador1";
        string player2Name = "Jugador2";
        GameCommands commands = new GameCommands();
        commands.AddPlayer(player1Name);
        commands.AddPlayer(player2Name);

        await commands.ChoosePokemon(player1Name, pokemonName1);
        await commands.ChoosePokemon(player1Name, pokemonName2);
        await commands.ChoosePokemon(player2Name, pokemonName1);
        await commands.ChoosePokemon(player2Name, pokemonName2);

        commands.StartBattle();

        
        string initialMsg = commands.ShowTurn();         

        string initialPlayerName = commands.GetPlayerInTurn().Name;  // lo inicializo igual para verificar el cambio de turno

        commands.ChangePokemon(commands.GetPlayerInTurn(), pokemonName2);
        if (initialPlayerName == commands.GetPlayerInTurn().Name) Assert.Fail();  // Debe cambiar de turno
        
        commands.ChangePokemon(commands.GetPlayerInTurn(), pokemonName2);
        if (initialPlayerName != commands.GetPlayerInTurn().Name) Assert.Fail();   // Si es distinto despues de 2 cambios de turno fallo el test
        string finalMsg = commands.ShowTurn(); 
        
        Assert.IsTrue(initialMsg != finalMsg);   // Si son distintos, y ya verificamos que cambia de turno, significa que el msg es diferente debido al cambio de pokemon
    }
}