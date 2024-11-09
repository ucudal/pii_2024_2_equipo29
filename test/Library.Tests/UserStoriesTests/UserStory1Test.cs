namespace Library.Tests.UserStoriesTests;

public class UserStory1Test
{
    // 1. Como jugador, quiero elegir 6 Pokémons del catálogo disponible para comenzar la batalla.
    // Criterios de aceptación:
    // El jugador puede seleccionar 6 Pokémons de una lista o catálogo.
    // Los Pokémons seleccionados se muestran en la pantalla del jugador.

    [TestCase("pikachu", "mew", "onix", "mewtwo", "snorlax", "charmander")]
    [TestCase("blastoise", "rattata", "pidgeot", "mewtwo", "snorlax", "ekans")]
    [TestCase("raichu", "mew", "arcanine", "machamp", "geodude", "rapidash")]
    [TestCase("electabuzz", "gyarados", "eevee", "dragonite", "flareon", "typhlosion")]
    public async Task ChoosePokemons(params string[] pokemonsNames)
    {
        GameCommands commands = new GameCommands();
        commands.AddPlayer("Jugador1");
        string msg = "";
        string msgExpected = "";
        int cont = 0;
        foreach (string pokemonName in pokemonsNames)
        {
            var (message, _) = await commands.ChoosePokemon("Jugador1", pokemonName);
            msg += message +"\n";

            cont++;
            msgExpected += $"**{pokemonName.ToUpper()}** ha sido agregado al equipo de **JUGADOR1**  ***({cont}/6)***\n";
        }
        
        Assert.That(msg, Is.EqualTo(msgExpected));
    }

    [TestCase("pika")]
    [TestCase("sanlorenzo")]
    [TestCase("catamarca")]
    [TestCase("plopcr 23")]
    public async Task ChooseNonExistentPokemon(string pokemonName)
    {
        string playerName = "Jugador1";
        GameCommands commands = new GameCommands();
        commands.AddPlayer(playerName);

        var (msg, _) = await commands.ChoosePokemon(playerName, pokemonName);
        string msgExpected = $"**{pokemonName.ToUpper()}** no ha sido encontrado.";
        
        Assert.That(msg, Is.EqualTo(msgExpected));
    }
    
    [TestCase("pikachu")]
    [TestCase("onix")]
    [TestCase("dragonite")]
    [TestCase("mewtwo")]
    public async Task ChooseRepeatedPokemon(string pokemonName)
    {
        string playerName = "Jugador1";
        GameCommands commands = new GameCommands();
        commands.AddPlayer(playerName);
        string msgExpected = "";
        string msgResult = "";
        var (msg, _) = await commands.ChoosePokemon(playerName, pokemonName);
        msgResult += msg + "\n";
        var (msg2, _) = await commands.ChoosePokemon(playerName, pokemonName);
        msgResult += msg2;
        
        msgExpected += $"**{pokemonName.ToUpper()}** ha sido agregado al equipo de **{playerName.ToUpper()}**  ***(1/{Player.MaxPokemons})***\n" +
                       $"**{pokemonName.ToUpper()}** ya se encuentra en el equipo de **{playerName.ToUpper()}**.";
        
        Assert.That(msgResult, Is.EqualTo(msgExpected));
    }
}