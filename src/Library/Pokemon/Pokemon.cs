using System.Transactions;
using DSharpPlus.SlashCommands;
using Library.States;

namespace Library;

/// <summary>
/// Representa un Pokémon en el juego.
/// </summary>
/// <remarks>
/// Esta clase contiene información sobre el Pokémon, incluyendo sus estadísticas, movimientos y estado.
/// </remarks>

public class Pokemon
{
    
    /// <summary>
    /// Obtiene o establece el nombre del Pokémon.
    /// </summary>
    public string Name { get; set; }
    
    
    /// <summary>
    /// Obtiene o establece los puntos de vida iniciales del Pokémon.
    /// </summary>
    public int InitialHp { get; set; }
    
    
    /// <summary>
    /// Obtiene o establece los puntos de vida actuales del Pokémon.
    /// </summary>
    public int Hp { get; set; }
    
    
    /// <summary>
    /// Obtiene o establece los puntos de ataque del Pokémon.
    /// </summary>
    public int AttackPoints { get; set; }
    
    
    /// <summary>
    /// Obtiene o establece los puntos de ataque especial del Pokémon.
    /// </summary>
    public int SpecialAttackPoints { get; set; }
    
    
    /// <summary>
    /// Obtiene o establece los puntos de defensa del Pokémon.
    /// </summary>
    public int DefensePoints { get; set; }
    
    
    /// <summary>
    /// Obtiene o establece los puntos de defensa especial del Pokémon.
    /// </summary>
    public int SpecialDefensePoints { get; set; }
    
    
    /// <summary>
    /// Obtiene o establece la lista de movimientos del Pokémon.
    /// </summary>
    public List<Move> Moves { get; set; }
    
    
    /// <summary>
    /// Obtiene o establece la lista de tipos del Pokémon.
    /// </summary>
    public List<Type> Types { get; set; }
    
    
    /// <summary>
    /// Obtiene o establece la URL de la imagen del Pokémon.
    /// </summary>
    public string ImgUrl { get; set; }
    
    
    /// <summary>
    /// Obtiene la máquina de estados del Pokémon.
    /// </summary>
    public StateMachine StateMachine { get; }
    
    
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="Pokemon"/>.
    /// </summary>
    public Pokemon()
    {
        Moves = new List<Move>();
        Types = new List<Type>();
        StateMachine = new StateMachine(new Normal());
    }
    
    
    /// <summary>
    /// Realiza un ataque a otro Pokémon.
    /// </summary>
    /// <param name="pokemonEnemy">El Pokémon enemigo al que se le realizará el ataque.</param>
    /// <param name="moveSlot">El índice del movimiento a utilizar.</param>
    /// <returns>Un mensaje que describe el resultado del ataque.</returns>
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
    
    
    /// <summary>
    /// Actualiza el tiempo de enfriamiento del movimiento especial.
    /// </summary>
    public void UpdateCoolDownSpecialMove()
    {
        Move specialMove = Moves[Moves.Count - 1];
        if (specialMove.RemainingTurnsInCoolDown > 0) specialMove.RemainingTurnsInCoolDown -= 1;
    }
    
    
    /// <summary>
    /// Determina si el Pokémon está muerto.
    /// </summary>
    /// <returns>True si el Pokémon está muerto; de lo contrario, false.</returns>
    public bool IsDead()
    {
        return Hp == 0;
    }
    
    
    /// <summary>
    /// Muestra la información del Pokémon.
    /// </summary>
    /// <returns>Una cadena que representa la información del Pokémon.</returns>
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
            ? $"{StateMachine.CurrentState.Emoji}  **El Pokemon está bajo el efecto {StateMachine.CurrentState.Name.ToString().ToUpper()} por {permanentEffect}.**  {StateMachine.CurrentState.Emoji}" 
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
    
    
    /// <summary>
    /// Muestra información simple del Pokémon.
    /// </summary>
    /// <returns>Una cadena que representa la información simple del Pokémon.</returns>
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