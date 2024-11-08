using Library.Adapters;
using Library.Services;
using Library.States;

namespace Library.Tests.PokemonTest;

public class PokemonTest
{
    
    private Pokemon defender;
    private Pokemon attacker;
    [SetUp]
    public async Task Setup()
    {
        PokemonAdapter pokemonAdapter = new PokemonAdapter(new PokeApiService());
        
        attacker = await pokemonAdapter.GetPokemonAsync("pikachu");
        defender = await pokemonAdapter.GetPokemonAsync("pikachu");
    }
    
    
    [Test]
    public void ConstructorTest()
    {
        Pokemon pokemon = new Pokemon();
        Assert.That(pokemon.Moves != null && pokemon.Types != null && pokemon.StateMachine != null);
    }

    private void AttackTillHit(int moveSlot)  // METODO CREADO DEBIDO A QUE *EL ATAQUE PUEDE FALLAR* 
    {
        int initialHp;
        int finalHp;
        do
        {
            initialHp = defender.Hp;
            attacker.Attack(defender, moveSlot);
            finalHp = defender.Hp;
        } while (initialHp > finalHp);
    }
    
    [Test]
    public void AttackTest()
    {
        int initialHp = defender.Hp;
        AttackTillHit(2);
        int finalHp = defender.Hp;
        
        Assert.That(finalHp, Is.Not.EqualTo(initialHp));
    }
    
    [Test]
    public void SpecialAttackTest()
    {
        defender.Hp = defender.InitialHp;
        IPokemonState initialState = defender.StateMachine.CurrentState;
        AttackTillHit(3);
        IPokemonState finalState =  defender.StateMachine.CurrentState;
        Assert.That(finalState, Is.Not.EqualTo(initialState));
    }
    
    [Test]
    public void MissAttackTest()
    {
        int initialHp;
        int finalHp;
        do
        {
            defender.Hp = defender.InitialHp;
            initialHp = defender.Hp;
            attacker.Attack(defender, 0);
            finalHp = defender.Hp;
        } while (initialHp != finalHp);    // ATACAR HASTA QUE FALLE (DEBIDOA A QUE ES ALEATORIO)
        
        
        Assert.That(finalHp, Is.EqualTo(initialHp));
    }
    
    [Test]
    public void UpdateCoolDownSpecialMoveTest()
    {
        defender.Hp = defender.InitialHp;
        AttackTillHit(3);
        int initialCd = attacker.Moves[3].RemainingTurnsInCoolDown;
        
        attacker.UpdateCoolDownSpecialMove();
        int finalCd = attacker.Moves[3].RemainingTurnsInCoolDown;
        
        Assert.That(initialCd, Is.Not.EqualTo(finalCd));
    }

    [Test]
    public void IsDeadTest()
    {
        defender.Hp = 1;
        AttackTillHit(2);
        Assert.IsTrue(defender.IsDead());
    }
    [Test]
    public void IsNotDeadTest()
    {
        defender.Hp = defender.InitialHp;
        attacker.Attack(defender, 1);
        Assert.IsFalse(defender.IsDead());
    }
    
    
    [Test]
    public void ViewPokemonTest()
    {
        Assert.IsNotNull(attacker.ViewPokemon());
    }

    [Test]
    public void ViewPokemonSimpleTest()
    {
        Assert.IsNotNull(attacker.ViewPokemonSimple());
    }
    
}