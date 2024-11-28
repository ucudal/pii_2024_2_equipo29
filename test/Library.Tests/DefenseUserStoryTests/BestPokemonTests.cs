namespace Library.Tests.DefenseUserStoryTests;

public class BestPokemonTests
{
    [TestCase("pikachu", "CHARMANDER","mew", "onix", "mewtwo", "snorlax", "charmander")]
    [TestCase("blastoise", "EKANS", "rattata", "pidgeot", "mewtwo", "snorlax", "ekans")]
    [TestCase("raichu", "RAPIDASH", "mew", "arcanine", "machamp", "geodude", "rapidash")]
    [TestCase("electabuzz", "TYPHLOSION", "gyarados", "eevee", "dragonite", "flareon", "typhlosion")]
    public async Task GetBestPokemonToFightTest(string pokemonEnemyName, string bestPokemonToFight, params string[] pokemonsNames)
    {
        GameCommands commands = new GameCommands();
        commands.AddPlayer("Jugador");
        commands.AddPlayer("JugadorEnemigo");
        if (commands.GetPlayerInTurn().Name != "Jugador")
        {
            commands.NextTurn();
        }
        
        foreach (string pokemonName in pokemonsNames)
        {
            await commands.ChoosePokemon("Jugador", pokemonName);
        }
        await commands.ChoosePokemon("JugadorEnemigo", pokemonEnemyName);
        
        string msgBestPokemonToFight = commands.ViewBestPokemonToFight();
        string msgExpected = $"El mejor pokemon para pelear es **{bestPokemonToFight}**.";
        
        Assert.That(msgBestPokemonToFight, Is.EqualTo(msgExpected));
    }
}