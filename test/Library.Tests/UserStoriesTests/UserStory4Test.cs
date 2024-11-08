using System.Text.RegularExpressions;

namespace Library.Tests.UserStoriesTests;

public class UserStory4Test
{
    // 4. Como jugador, quiero atacar en mi turno y hacer daño basado en la efectividad de los tipos de Pokémon.
    // Criterios de aceptación:
    // El jugador puede seleccionar un ataque que inflige daño basado en la efectividad de tipos.
    // El sistema aplica automáticamente la ventaja o desventaja del tipo de ataque.


    [Test]
    public async Task VerifyPlayerInTurnAttackTest()
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

        string playerInTurnName = player1Name == commands.GetPlayerInTurn().Name ? player1Name : player2Name;

        string msg = commands.Attack(2, playerInTurnName);
        Assert.IsTrue(!msg.Contains($"No puedes atacar, es el turno de"));
    }
    
    [Test]
    public async Task VerifyNotPlayerInTurnAttackTest()
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

        string playerInTurnName = player1Name == commands.GetPlayerInTurn().Name ? player2Name : player1Name;

        string msg = commands.Attack(2, playerInTurnName);
        Assert.IsTrue(msg.Contains($"No puedes atacar, es el turno de"));
    }
    
    [Test]
    public async Task EffectivityTest()
    {
        string pokemonName = "onix";
        string player1Name = "Jugador1";
        string player2Name = "Jugador2";
        GameCommands commands = new GameCommands();
        commands.AddPlayer(player1Name);
        commands.AddPlayer(player2Name);

        await commands.ChoosePokemon("Jugador1", pokemonName);
        await commands.ChoosePokemon("Jugador2", pokemonName);

        commands.StartBattle();
        
        string msg = commands.Attack(2, commands.GetPlayerInTurn().Name);                  // Ataca y por la relacion de Typos el daño debe ser reducido a la mitad
        
        var regex = new Regex(@"\(\*\*HP:\*\* (\d{1,4})/(\d{1,4})\)"); 
        var match = regex.Match(msg);
        int hp = 0;
        int initialHp = 0;
        if (match.Success)
        {
            hp = int.Parse(match.Groups[1].Value);      // Obtiene los valores de Hp e InitialHp desde el string para saber el daño realizado
            initialHp = int.Parse(match.Groups[2].Value);
        }
        
        int dmg = initialHp - hp;
    
        int minDmg = (int)Math.Round(0.1 * 0.5 * 85 * (((1.2 * 45 * 70) / (25 * 160)) + 2));
        int maxDmg = (int)Math.Round(0.1 * 0.5 * 100 * (((1.2 * 45 * 70) / (25 * 160)) + 2)*1.2);
        
        Assert.IsTrue(dmg >= minDmg && maxDmg >= dmg);
    }
}