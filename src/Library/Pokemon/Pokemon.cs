using System.Transactions;
using Library.States;

namespace Library;

public class Pokemon
{
    public string Name { get; set; }
    public int InitialHp { get; set; }
    public int Hp { get; set; }
    public int AttackPoints { get; set; }
    public int SpecialAttackPoints { get; set; }
    public int DefensePoints { get; set; }
    public int SpecialDefensePoints { get; set; }
    public List<Move> Moves { get; set; }
    public List<Type> Types { get; set; }
    public StateMachine StateMachine { get; }

    public Pokemon()
    {
        Moves = new List<Move>();
        Types = new List<Type>();
        StateMachine = new StateMachine(new Normal());
    }
    
    public void Attack(Pokemon pokemonEnemy, int moveSlot)
    {
        if (new Random().Next(0, 100) >= Moves[moveSlot].Accuracy)
        {
            Console.WriteLine("No tienes puntería, le erraste.");
            return;
        };
        
        Move move = Moves[moveSlot];
        ICalculate calculate = new Calculate();
        int dmg = calculate.CalculateDamage(this, pokemonEnemy, move);
    
        pokemonEnemy.Hp = dmg > pokemonEnemy.Hp 
            ? 0 
            : pokemonEnemy.Hp - dmg;

        StateApplier.ApplyStateEffect(pokemonEnemy.StateMachine, move.State);
    }

    public bool IsDead()
    {
        return Hp == 0;
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
        string msg = $"**{Name.ToUpper()}**   ({Hp}/{InitialHp})";

        if (Types.Count > 0)
        {
            msg += $" (Types: ";
            foreach (var type in Types)
            {
                msg += $"-{type.Name} ";
            }

            msg += ")\n";
        }

        foreach (var move in Moves)
        {
            msg += move.ViewMove() + "\n";
        }
        
        return msg;
    }
}