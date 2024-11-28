namespace Library.Tests.DefenseUserStoryTests;

public class BestPokemonTests
{
    /*
        A continuación se muestran a detalle todos los casos presentados en los TestCase, 
        los datos fueron extraidos utilizando Console.WriteLine en el método GetBestPokemonToFight
        de la clase Player. Actualmente, se encuentran comentados para evitar su ejecución. 
        En caso de querer probar con distintos pokemons, se deberá agregar un nuevo TestCase
        al test GetBestPokemonToFightTest y descomentar los Console.WriteLine.
        
        ---------------------------------- CASO 1 ----------------------------------
        Pokemon aliado: mew - Pokemon enemigo: bulbasaur
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: fire - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento fire: 2
        Efectividad total del pokemon mew: 5


        Pokemon aliado: onix - Pokemon enemigo: bulbasaur
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento normal: 1
        Efectividad total del pokemon onix: 4


        Pokemon aliado: mewtwo - Pokemon enemigo: bulbasaur
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: fire - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento fire: 2
        Tipo del ataque aliado: ice - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento ice: 2
        Efectividad total del pokemon mewtwo: 6


        Pokemon aliado: snorlax - Pokemon enemigo: bulbasaur
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: fire - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento fire: 2
        Tipo del ataque aliado: ice - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento ice: 2
        Efectividad total del pokemon snorlax: 6


        Pokemon aliado: charmander - Pokemon enemigo: bulbasaur
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: fire - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento fire: 2
        Tipo del ataque aliado: electric - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento electric: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: grasspoison
        Efectividad del movimiento normal: 1
        Efectividad total del pokemon charmander: 5

        El mejor pokemon es snorlax
        ----------------------------------------------------------------------------
        
        ---------------------------------- CASO 2 ----------------------------------
        Pokemon aliado: rattata - Pokemon enemigo: blastoise
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Efectividad total del pokemon rattata: 4


        Pokemon aliado: pidgeot - Pokemon enemigo: blastoise
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: flying - Tipos del pokemon enemigo: water
        Efectividad del movimiento flying: 1
        Tipo del ataque aliado: flying - Tipos del pokemon enemigo: water
        Efectividad del movimiento flying: 1
        Tipo del ataque aliado: flying - Tipos del pokemon enemigo: water
        Efectividad del movimiento flying: 1
        Efectividad total del pokemon pidgeot: 4


        Pokemon aliado: mewtwo - Pokemon enemigo: blastoise
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: fire - Tipos del pokemon enemigo: water
        Efectividad del movimiento fire: 0,5
        Tipo del ataque aliado: ice - Tipos del pokemon enemigo: water
        Efectividad del movimiento ice: 0,5
        Efectividad total del pokemon mewtwo: 3


        Pokemon aliado: snorlax - Pokemon enemigo: blastoise
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: fire - Tipos del pokemon enemigo: water
        Efectividad del movimiento fire: 0,5
        Tipo del ataque aliado: ice - Tipos del pokemon enemigo: water
        Efectividad del movimiento ice: 0,5
        Efectividad total del pokemon snorlax: 3


        Pokemon aliado: ekans - Pokemon enemigo: blastoise
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Efectividad total del pokemon ekans: 4

        El mejor pokemon es ekans
        ----------------------------------------------------------------------------
        
        ---------------------------------- CASO 3 ----------------------------------
        Pokemon aliado: mew - Pokemon enemigo: squirtle
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: fire - Tipos del pokemon enemigo: water
        Efectividad del movimiento fire: 0,5
        Efectividad total del pokemon mew: 3,5


        Pokemon aliado: arcanine - Pokemon enemigo: squirtle
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Efectividad total del pokemon arcanine: 4


        Pokemon aliado: machamp - Pokemon enemigo: squirtle
        Tipo del ataque aliado: fighting - Tipos del pokemon enemigo: water
        Efectividad del movimiento fighting: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: fire - Tipos del pokemon enemigo: water
        Efectividad del movimiento fire: 0,5
        Tipo del ataque aliado: ice - Tipos del pokemon enemigo: water
        Efectividad del movimiento ice: 0,5
        Efectividad total del pokemon machamp: 3


        Pokemon aliado: geodude - Pokemon enemigo: squirtle
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: fire - Tipos del pokemon enemigo: water
        Efectividad del movimiento fire: 0,5
        Tipo del ataque aliado: electric - Tipos del pokemon enemigo: water
        Efectividad del movimiento electric: 2
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Efectividad total del pokemon geodude: 4,5


        Pokemon aliado: rapidash - Pokemon enemigo: squirtle
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: fighting - Tipos del pokemon enemigo: water
        Efectividad del movimiento fighting: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: water
        Efectividad del movimiento normal: 1
        Efectividad total del pokemon rapidash: 4


        El mejor pokemon es geodude
        ----------------------------------------------------------------------------
        
        ---------------------------------- CASO 4 ----------------------------------
        Pokemon aliado: gyarados - Pokemon enemigo: charizard
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento normal: 1
        Efectividad total del pokemon gyarados: 4


        Pokemon aliado: eevee - Pokemon enemigo: charizard
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: fighting - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento fighting: 0,5
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento normal: 1
        Efectividad total del pokemon eevee: 3,5


        Pokemon aliado: dragonite - Pokemon enemigo: charizard
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: fire - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento fire: 0,5
        Tipo del ataque aliado: ice - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento ice: 1
        Tipo del ataque aliado: electric - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento electric: 1
        Efectividad total del pokemon dragonite: 3,5


        Pokemon aliado: flareon - Pokemon enemigo: charizard
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: fighting - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento fighting: 0,5
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento normal: 1
        Efectividad total del pokemon flareon: 3,5


        Pokemon aliado: typhlosion - Pokemon enemigo: charizard
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento normal: 1
        Tipo del ataque aliado: fire - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento fire: 0,5
        Tipo del ataque aliado: electric - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento electric: 1
        Tipo del ataque aliado: normal - Tipos del pokemon enemigo: fireflying
        Efectividad del movimiento normal: 1
        Efectividad total del pokemon typhlosion: 3,5

        El mejor pokemon es gyarados
        ----------------------------------------------------------------------------
     */
    
    [TestCase("bulbasaur", "SNORLAX","mew", "onix", "mewtwo", "snorlax", "charmander")]
    [TestCase("blastoise", "EKANS", "rattata", "pidgeot", "mewtwo", "snorlax", "ekans")]
    [TestCase("squirtle", "GEODUDE", "mew", "arcanine", "machamp", "geodude", "rapidash")]
    [TestCase("charizard", "GYARADOS", "gyarados", "eevee", "dragonite", "flareon", "typhlosion")]
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