using System.Transactions;
using DSharpPlus.SlashCommands;
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
    public string ImgUrl { get; set; }
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
            return "\ud83c\udfaf  **No tienes puntería, le erraste**  \ud83c\udfaf\n\n";
        }
        
        Move move = Moves[moveSlot];
        if (move.IsSpecialMove)
        {
            if (move.RemainingTurnsInCoolDown > 0)
            {
                string terminationLetter = move.RemainingTurnsInCoolDown == 1 ? "" : "s";
                return $"La habilidad **{move.Name.ToUpper()}** se encuentra en cooldown. Espera **{move.RemainingTurnsInCoolDown} turno{terminationLetter}.**\n\n";
            }
            
            move.RemainingTurnsInCoolDown = move.CoolDownSpecialMove;
        }

        string msg = "";
        ICalculate calculate = new Calculate();
        int dmg;
        if (calculate.CalculateDamage(this, pokemonEnemy, move, out dmg))
        {
            msg += "\ud83d\udca2 **GOLPE CRITICO** ";
        }
    
        pokemonEnemy.Hp = dmg > pokemonEnemy.Hp 
            ? 0 
            : pokemonEnemy.Hp - dmg;

        msg += $"El move **{move.Name.ToUpper()}** ha realizado **{dmg}** de daño.\n";
        if (pokemonEnemy.IsDead())
        {
            pokemonEnemy.StateMachine.CurrentState = new Normal();
            return msg;
        }

        if (move.State == EnumState.Normal) return msg;
        
        msg += StateApplier.ApplyStateEffect(pokemonEnemy.StateMachine, move.State) +"\n";
        return msg;
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

    public string ViewPokemon()
    {
        string msg = $"**{Name.ToUpper()}**   (**HP:** {Hp}/{InitialHp}) ";
        
        if (Types.Count > 0)
        {
            msg += $" (**TYPES:** {string.Join(", ", Types.Select(t => t.Name.ToUpper()))})\n";
        }

        if (StateMachine.CurrentState.Name != EnumState.Sleep)
        {
            for (int i = 0; i < Moves.Count; i++)
            {
                msg += $"**{i+1}) {Moves[i].ViewMove()}\n";
            }
        }
        
        
        bool hasEffect = StateMachine.CurrentState.Name != EnumState.Normal;
        int remainingTurnsWithEffect = StateMachine.GetRemainingTurnsWithEffect();
        string terminationLetter = remainingTurnsWithEffect == 1 ? "" : "s";
        string permanentEffect = remainingTurnsWithEffect == -1
            ? "el resto de la batalla"
            : $"{remainingTurnsWithEffect} turno{terminationLetter}";
        
        msg += hasEffect && remainingTurnsWithEffect != 0
            ? $"**El Pokemon está bajo el efecto {StateMachine.CurrentState.Name.ToString().ToUpper()} por {permanentEffect}.**" 
            : "";
        if (hasEffect)
        {
            switch (StateMachine.CurrentState.Name)
            {
                case EnumState.Burn:
                    msg += $" (-10% HP cada turno)  [**-{InitialHp/10} HP**]\n";
                    break;
                case EnumState.Poison:
                    msg += $" (-5% HP cada turno)  [**-{InitialHp/20} HP**]\n";
                    break;
                default:
                    msg += "\n";
                    break;
            }
        }
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