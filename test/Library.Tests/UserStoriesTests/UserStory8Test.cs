namespace Library.Tests.UserStoriesTests;

public class UserStory8Test
{
    // 8. Como entrenador, quiero poder usar un ítem durante una batalla.
    // Criterios de aceptación:
    // Al usar el ítem se pierde el turno
    
    
    [TestCase("pikachu", "charmander")]
    [TestCase("pikachu", "abra")]
    [TestCase("pikachu", "pichu")]
    [TestCase("pikachu", "snorlax")]
    public async Task UseSuperPotionTest(string pokemonName1, string pokemonName2)
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
        if (commands.GetPlayerInTurn().Name != player1Name) commands.NextTurn();
        commands.Attack(1, commands.GetPlayerInTurn().Name);
        
        int superPotionSlot = 1;
        string itemUser = commands.GetPlayerInTurn().Name;
        string msg = commands.UseItem(commands.GetPlayerInTurn(), pokemonName1, superPotionSlot);
        
        Assert.That(itemUser != commands.GetPlayerInTurn().Name && msg.Contains("ha recuperado"));   // verifico que cambie de turno y aplique la curacion 
    }
    
    [TestCase("onix", "charmander")]
    [TestCase("onix", "abra")]
    [TestCase("onix", "pichu")]
    [TestCase("onix", "snorlax")]
    public async Task UseFullHealTest(string pokemonName1, string pokemonName2)
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
        if (commands.GetPlayerInTurn().Name != player1Name) commands.NextTurn();
        commands.Attack(3, commands.GetPlayerInTurn().Name);

        int fullHealSlot = 2;
        string itemUser = commands.GetPlayerInTurn().Name;
        string msg = commands.UseItem(commands.GetPlayerInTurn(), pokemonName1, fullHealSlot);
        
        Assert.That(itemUser != commands.GetPlayerInTurn().Name && msg.Contains($"El pokemon **{pokemonName1.ToUpper()}** se ha curado del efecto"));   // verifico que cambie de turno y aplique la curacion
        
    }


    [TestCase("kartana", "shedinja", "abra")]
    [TestCase("kartana", "abra", "pichu")]
    [TestCase("dragonite", "pichu", "pikachu")]
    [TestCase("dragonite", "shedinja", "pichu")]
    public async Task ReviveTest(string pokemonName1, string pokemonName2, string pokemonName3)
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

        string msgAux = "";
        do
        {
            if (commands.GetPlayerInTurn().Name == player1Name) msgAux = commands.Attack(1, commands.GetPlayerInTurn().Name);
            else
            {
                commands.NextTurn();
            }
        } while (commands.GetPlayerInTurn().CurrentPokemon.Name != pokemonName3);


        int reviveSlot = 0;
        string itemUser = commands.GetPlayerInTurn().Name;
        string msg = commands.UseItem(commands.GetPlayerInTurn(), pokemonName2, reviveSlot);

        Assert.That(itemUser != commands.GetPlayerInTurn().Name && msg.Contains($"El pokemon **{pokemonName2.ToUpper()}** ha revivido")); // verifico que cambie de turno y reviva
    }
}