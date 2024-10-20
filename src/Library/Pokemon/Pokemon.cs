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
        string msg = $"{name.ToUpper()}   ({hp}/{initialHp})\n";

        if (Types != null)
        {
            msg += $" / Types: \n";
            foreach (var type in Types)
            {
                msg += $"-{type.Name} \n";
            }
        }
        
        return msg;
    }
}