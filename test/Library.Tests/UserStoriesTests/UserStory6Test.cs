namespace Library.Tests.UserStoriesTests;

public class UserStory6Test
{
    // 6. Como jugador, quiero ganar la batalla cuando la vida de todos los Pokémons oponente llegue a cero.
    // Criterios de aceptación:
    // La batalla termina automáticamente cuando todos los Pokémons del oponente alcanza 0 de vida.
    // Se muestra un mensaje indicando el ganador de la batalla.

    [TestCase("kartana", "shedinja", "abra")]
    [TestCase("kartana", "abra","pichu")]
    [TestCase("dragonite", "pichu", "pikachu")]
    [TestCase("dragonite", "shedinja", "pichu")]
    public async Task WinTest(string pokemonName1, string pokemonName2, string pokemonName3)
    {
        string player1Name = "Jugador1";
        string player2Name = "Jugador2";
        GameCommands commands = new GameCommands();
        commands.AddPlayer(player1Name);
        commands.AddPlayer(player2Name);

        await commands.ChoosePokemon(player1Name, pokemonName1);
        await commands.ChoosePokemon(player2Name, pokemonName2);
        await commands.ChoosePokemon(player2Name, pokemonName3);
        
        commands.StartBattle();
        
        string msg = "";
        do
        {
            if (commands.GetPlayerInTurn().Name == player1Name) msg = commands.Attack(1, commands.GetPlayerInTurn().Name);
            else
            {
                commands.NextTurn();
            }
        } while (!msg.Contains($"¡La partida ya ha finalizado"));

        string msgExpected =
            $"¡La partida ya ha finalizado, el jugador \ud83d\udc51 **_{player1Name.ToUpper()}_** \ud83d\udc51 ha ganado!";
        
        Assert.IsTrue(msg.Contains(msgExpected));
    }
    
    
}