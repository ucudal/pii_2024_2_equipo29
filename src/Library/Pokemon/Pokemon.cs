using System.Transactions;
using Library.States;

namespace Library;

public class Pokemon
{
    private string name;
    public string Name => name;
    
    private int initialHp;
    public int InitialHp => initialHp;
    
    private int hp;
    public int Hp
    {
        get => hp;
        set => hp = value;
    }
    
    private int attack;
    public int AttackPoints => attack;
    
    private int specialAttack;
    public int SpecialAttackPoints => specialAttack;
    
    private int defense;
    public int DefensePoints => defense;
    
    private int specialDefense;
    public int SpecialDefensePoints => specialDefense;

    public List<Move> Moves { get; }
    public List<Type> Types { get; }
    
    public State PokemonState { get; set; }
    
    public void Attack(Pokemon enemy, int moveSlot)
    {
        Random rand = new Random();
        if (rand.Next(0, 100) < Moves[moveSlot].Accuracy)
        {
            PokemonState.Attack(this, enemy, moveSlot);
        }
    }

    public bool IsDead()
    {
        return hp == 0;
    }
    
    public string ViewMoves()
    {
        string movesMsg = "";
        foreach (var move in Moves)
        {
            movesMsg += move.ViewMove() + "\n";
        }
        return movesMsg;
    }

    public string ViewPokemon()
    {
        string msg = $"{name.ToUpper()}   ({hp}/{initialHp})\n";  // Nose si la idea es que se muestren todas las stats aca
        msg += $"Attack: {attack}\n" +
               $"Special Attack: {specialAttack}\n" +                                          
               $"Defense: {defense}\n" +
               $"Special Defense: {specialDefense}\n";
        
        if (Types != null)
        {
            msg += $" / Types: \n";
            foreach (var type in Types)
            {
                msg += $"-{type.Name} \n";
            }
        }
        msg += ViewMoves();                     // Nose si la idea es que se muestren todos los moves aca
        
        return msg;
    }
}