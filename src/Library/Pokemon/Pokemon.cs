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
    
    public string Attack(Pokemon pokemonEnemy, int moveSlot)
    {
        if (new Random().Next(0, 100) >= Moves[moveSlot].Accuracy)
        {
            return "No tienes puntería, le erraste.";
        }
        
        Move move = Moves[moveSlot];
        if (move.IsSpecialMove)
        {
            if (move.RemainingTurnsInCoolDown > 0)
            {
                string terminationLetter = move.RemainingTurnsInCoolDown == 1 ? "" : "s";
                return $"La habilidad se encuentra en cooldown. Espera {move.RemainingTurnsInCoolDown} turno{terminationLetter}.";
            }
            
            move.RemainingTurnsInCoolDown = move.CoolDownSpecialMove;
        }
        
        ICalculate calculate = new Calculate();
        int dmg = calculate.CalculateDamage(this, pokemonEnemy, move);
    
        pokemonEnemy.Hp = dmg > pokemonEnemy.Hp 
            ? 0 
            : pokemonEnemy.Hp - dmg;

        return StateApplier.ApplyStateEffect(pokemonEnemy.StateMachine, move.State);
    }

    public void UpdateCoolDownSpecialMove()
    {
        Move specialMove = Moves[Moves.Count - 1];
        if (specialMove.RemainingTurnsInCoolDown > 0) specialMove.RemainingTurnsInCoolDown -= 1;
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
        string msg = $"**{Name.ToUpper()}**   (**HP:** {Hp}/{InitialHp}) ";
        
        if (Types.Count > 0)
        {
            msg += $" (**TYPES:** {string.Join(", ", Types.Select(t => t.Name.ToUpper()))})\n";
        }

        for (int i = 0; i < Moves.Count; i++)
        {
            msg += $"**{i+1}) {Moves[i].ViewMove()}\n";
        }
        
        bool hasEffect = StateMachine.CurrentState is not Normal;
        int remainingTurnsWithEffect = StateMachine.GetRemainingTurnsWithEffect();
        string terminationLetter = remainingTurnsWithEffect == 1 ? "" : "s";
        string permanentEffect = remainingTurnsWithEffect == -1
            ? "el resto de la batalla"
            : $"{remainingTurnsWithEffect} turno{terminationLetter}";
        
        msg += hasEffect && remainingTurnsWithEffect != 0
            ? $"**El Pokemon está bajo el efecto {StateMachine.CurrentState.Name} por {permanentEffect}.**\n" 
            : "";
        
        return msg;
    }
    
    public string ViewPokemonSimple()
    {
        string msg = $"**{Name.ToUpper()}**   (**HP:** {Hp}/{InitialHp})";

        if (Types.Count > 0)
        {
            msg += $" (**TYPES:** {string.Join(", ", Types.Select(t => t.Name.ToUpper()))})";
        }

        return msg;
    }
}