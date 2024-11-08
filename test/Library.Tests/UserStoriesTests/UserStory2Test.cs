namespace Library.Tests.UserStoriesTests;

public class UserStory2Test
{
    // 2. Como jugador, quiero ver los ataques disponibles de mis Pokémons para poder elegir cuál usar en cada turno.    
    // Criterios de aceptación:                                                                                        
    // Se muestran los ataques disponibles para el turno actual.                                                                    
    // Los ataques especiales solo pueden seleccionarse cada dos turnos.
    
    
    [TestCase("pikachu")]
    [TestCase("onix")]
    [TestCase("dragonite")]
    [TestCase("mewtwo")]
    public async Task SeeAvailableAttacksEachRoundTest(string pokemonName)
    {
        string player1Name = "Jugador1";
        string player2Name = "Jugador2";
        GameCommands commands = new GameCommands();
        commands.AddPlayer(player1Name);
        commands.AddPlayer(player2Name);

        await commands.ChoosePokemon(player1Name, pokemonName);
        await commands.ChoosePokemon(player2Name, pokemonName);
        commands.StartBattle();

        string msg = commands.ShowTurn();
        Player playerInTurn = commands.GetPlayerInTurn();
        string msgExpected =
            $"Turno de: **{playerInTurn.Name.ToUpper()}**\n{playerInTurn.CurrentPokemon.ViewPokemon()}";

        commands.NextTurn();
        
        msg += commands.ShowTurn();      // Para verificar que cada turno muestre los ataques disponibles
        
        playerInTurn = commands.GetPlayerInTurn();
        string msgExpected2 =
            $"Turno de: **{playerInTurn.Name.ToUpper()}**\n{playerInTurn.CurrentPokemon.ViewPokemon()}";
        
        Assert.That(msg.Contains(msgExpected) && msg.Contains(msgExpected2));
    }



    [TestCase("articuno")]
    [TestCase("snorlax")]
    [TestCase("gyarados")]
    [TestCase("mew")]
    public async Task SpecialAttacksCooldownTest(string pokemonName)
    {
        string player1Name = "Jugador1";
        string player2Name = "Jugador2";
        GameCommands commands = new GameCommands();
        commands.AddPlayer(player1Name);
        commands.AddPlayer(player2Name);

        await commands.ChoosePokemon(player1Name, pokemonName);
        await commands.ChoosePokemon(player2Name, pokemonName);
        commands.StartBattle();
        int moveSlot = 3;    

        string firstplayerName = commands.GetPlayerInTurn().Name;
        commands.Attack(moveSlot, commands.GetPlayerInTurn().Name);    // Aca cambia de turno y setea el Cooldown
        
        commands.NextTurn();    // Cambio el turno manualmente para que vuela al que tiro el special Attack y baja el cooldown
        
        commands.Attack(moveSlot, commands.GetPlayerInTurn().Name);    // Al realizar esto no cambia de turno ya que la habilidad esta en cooldown
        commands.NextTurn();                            // Cambio manualmente
        commands.NextTurn();                            // Cambio manualmente y baja el cooldown y ahora deberia estar disponible el Special Attack

        bool attackFirstPlayer = commands.GetPlayerInTurn().Name == firstplayerName;    // Primera condicion para probar que funciona es verificar que ataque el jugador que estaba con la habilidad en cooldown
        commands.Attack(moveSlot, commands.GetPlayerInTurn().Name);             // Si la condicion anterior se cumple y en esta funcion cambia de turno, quiere decir que el ataque se lanzo
        
        bool turnChanged = commands.GetPlayerInTurn().Name != firstplayerName; 
        
        Assert.IsTrue(attackFirstPlayer && turnChanged);
    }
    
}