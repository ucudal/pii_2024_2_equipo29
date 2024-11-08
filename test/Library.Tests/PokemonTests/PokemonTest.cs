using Library.Adapters;
using Library.Services;
using Library.States;

namespace Library.Tests.PokemonTest;

public class PokemonTest
{
    [Test]
    public void ConstructorTest()
    {
        Pokemon pokemon = new Pokemon();
        Assert.That(pokemon.Moves != null && pokemon.Types != null && pokemon.StateMachine != null);
    }
    
    [TestCase("pikachu", "pikachu")]
    [TestCase("charizard", "pikachu")]
    [TestCase("mew", "charmander")]
    [TestCase("eevee", "pikachu")]
    public async Task AttackTest(string pokemonAttackerName, string pokemonDefenderName)
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        Pokemon attacker = await pokemonAdapter.GetPokemonAsync(pokemonAttackerName);
        Pokemon defender = await pokemonAdapter.GetPokemonAsync(pokemonDefenderName);
        int moveSlot = 1;

        attacker.Moves[moveSlot].Accuracy = 100;
        
        int initialHp = defender.Hp;
        attacker.Attack(defender, moveSlot);
        int finalHp = defender.Hp;
        
        Assert.That(finalHp, Is.Not.EqualTo(initialHp));
    }
    
    [TestCase("pikachu", "pikachu")]
    [TestCase("charizard", "pikachu")]
    [TestCase("mew", "charmander")]
    [TestCase("eevee", "pikachu")]
    public async Task SpecialAttackTest(string pokemonAttackerName, string pokemonDefenderName)
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        Pokemon attacker = await pokemonAdapter.GetPokemonAsync(pokemonAttackerName);
        Pokemon defender = await pokemonAdapter.GetPokemonAsync(pokemonDefenderName);
        int moveSlot = 3;
        
        attacker.Moves[moveSlot].Accuracy = 100;
        
        defender.Hp = defender.InitialHp;
        IPokemonState initialState = defender.StateMachine.CurrentState;
        attacker.Attack(defender, moveSlot);
        IPokemonState finalState =  defender.StateMachine.CurrentState;
        Assert.That(finalState, Is.Not.EqualTo(initialState));
    }
    
    [TestCase("pikachu", "pikachu")]
    [TestCase("charizard", "pikachu")]
    [TestCase("mew", "charmander")]
    [TestCase("eevee", "pikachu")]
    public async Task MissAttackTest(string pokemonAttackerName, string pokemonDefenderName)
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        Pokemon attacker = await pokemonAdapter.GetPokemonAsync(pokemonAttackerName);
        Pokemon defender = await pokemonAdapter.GetPokemonAsync(pokemonDefenderName);
        int moveSlot = 1;
        
        attacker.Moves[moveSlot].Accuracy = 50;
        
        int initialHp;
        int finalHp;
        do
        {
            defender.Hp = defender.InitialHp;
            initialHp = defender.Hp;
            attacker.Attack(defender, moveSlot);
            finalHp = defender.Hp;
        } while (initialHp != finalHp);    // ATACAR HASTA QUE FALLE (DEBIDOA A QUE ES ALEATORIO)
        
        
        Assert.That(finalHp, Is.EqualTo(initialHp));
    }
    
    [TestCase("pikachu", "pikachu")]
    [TestCase("charizard", "pikachu")]
    [TestCase("mew", "charmander")]
    [TestCase("eevee", "pikachu")]
    public async Task UpdateCoolDownSpecialMoveTest(string pokemonAttackerName, string pokemonDefenderName)
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        Pokemon attacker = await pokemonAdapter.GetPokemonAsync(pokemonAttackerName);
        Pokemon defender = await pokemonAdapter.GetPokemonAsync(pokemonDefenderName);
        int moveSlot = 3;
        attacker.Moves[moveSlot].Accuracy = 100;
        
        defender.Hp = defender.InitialHp;
        attacker.Attack(defender, moveSlot);
        int initialCd = attacker.Moves[moveSlot].RemainingTurnsInCoolDown;
        
        attacker.UpdateCoolDownSpecialMove();
        int finalCd = attacker.Moves[moveSlot].RemainingTurnsInCoolDown;
        
        Assert.That(initialCd, Is.Not.EqualTo(finalCd));
    }

    [TestCase("pikachu", "pikachu")]
    [TestCase("charizard", "pikachu")]
    [TestCase("mew", "charmander")]
    [TestCase("eevee", "pikachu")]
    public async Task IsDeadTest(string pokemonAttackerName, string pokemonDefenderName)
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        Pokemon attacker = await pokemonAdapter.GetPokemonAsync(pokemonAttackerName);
        Pokemon defender = await pokemonAdapter.GetPokemonAsync(pokemonDefenderName);
        int moveSlot = 1;
        attacker.Moves[moveSlot].Accuracy = 100;
        
        defender.Hp = 1;
        attacker.Attack(defender, moveSlot);
        Assert.IsTrue(defender.IsDead());
    }
    
    [TestCase("pikachu", "pikachu")]
    [TestCase("charizard", "pikachu")]
    [TestCase("mew", "charmander")]
    [TestCase("eevee", "pikachu")]
    public async Task IsNotDeadTest(string pokemonAttackerName, string pokemonDefenderName)
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        Pokemon attacker = await pokemonAdapter.GetPokemonAsync(pokemonAttackerName);
        Pokemon defender = await pokemonAdapter.GetPokemonAsync(pokemonDefenderName);
        int moveSlot = 1;
        
        defender.Hp = defender.InitialHp;
        attacker.Attack(defender, moveSlot);
        Assert.IsFalse(defender.IsDead());
    }
    
    
    [TestCase("pikachu")]
    [TestCase("charizard")]
    [TestCase("mew")]
    [TestCase("eevee")]
    public async Task ViewPokemonTest(string pokemonName)
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        Pokemon pokemon = await pokemonAdapter.GetPokemonAsync(pokemonName);
        
        Assert.IsNotNull(pokemon.ViewPokemon());
    }

    [TestCase("pikachu")]
    [TestCase("charizard")]
    [TestCase("mew")]
    [TestCase("eevee")]
    public async Task ViewPokemonSimpleTest(string pokemonName)
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        Pokemon pokemon = await pokemonAdapter.GetPokemonAsync(pokemonName);
        
        Assert.IsNotNull(pokemon.ViewPokemonSimple());
    }
    
}