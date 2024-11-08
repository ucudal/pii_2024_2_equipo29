using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using DSharpPlus;

namespace Library.Tests.UserStoriesTests;

public class UserStory3Test
{
    // 3. Como jugador, quiero ver la cantidad de vida (HP) de mis Pokémons y de los Pokémons oponentes para saber cuánta salud tienen.
    // Criterios de aceptación:
    // La vida de los Pokémons propios y del oponente se actualizan tras cada ataque.
    // La vida se muestra en formato numérico (ej. 20/50).

    [TestCase("pikachu", "mew")]
    [TestCase("blastoise", "rattata")]
    [TestCase("raichu", "mew")]
    [TestCase("electabuzz", "gyarados")]
    public async Task SeeAllPokemonsHpTest(params string[] pokemonsNames)
    {
        string player1Name = "Jugador1";
        string player2Name = "Jugador2";
        GameCommands commands = new GameCommands();
        commands.AddPlayer(player1Name);
        commands.AddPlayer(player2Name);
        
        
        

        foreach (string pokemonName in pokemonsNames)
        {
            await commands.ChoosePokemon("Jugador1", pokemonName);
            await commands.ChoosePokemon("Jugador2", pokemonName);
        }

        commands.StartBattle();
        
        string msg = commands.ViewPokemons();

        Player player = commands.GetPlayerInTurn();
        string msgExpected = $"{player.Name.ToUpper()}:\n{player.ViewAllPokemons()}\n";
        
        Assert.IsTrue(msg.Contains(msgExpected) && msg.Length > msgExpected.Length);    // Si es mas largo quiere decir que ademas esta mostrando lo mismo para el otro jugador;
    }


    [TestCase("pikachu")]
    [TestCase("onix")]
    [TestCase("dragonite")]
    [TestCase("mewtwo")]
    public async Task HpUpdateEachRoundTest(string pokemonName)
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
        
        do
        {
            msg = commands.Attack(1, commands.GetPlayerInTurn().Name);                  // (Al atacar sucede un cambio de turno automaticamente)
        } while (msg.Contains("erraste"));

        int hp = 0;
        int initialHp = 0;
        
        var regex = new Regex(@"\(\*\*HP:\*\* (\d{1,3})/(\d{1,3})\)");          // El formato mostrado seria (**HP:** {Hp}/{InitialHp})  si el test pasa quiere decir que se encuentra en ese formato, porque es como busco los valores
        var match = regex.Match(msg);
        
        if (match.Success)
        {
            hp = int.Parse(match.Groups[1].Value);      // Obtiene los valores de Hp e InitialHp desde el string 
            initialHp = int.Parse(match.Groups[2].Value);
        }
        
        Assert.IsFalse(hp == initialHp);            // Si son diferentes quiere decir que se actualizo   
        
    }
}