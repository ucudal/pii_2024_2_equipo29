using DSharpPlus.SlashCommands;

namespace Library.States;

/// <summary>
/// Clase abstracta que aplica efectos de estado a un pokemon a través de la máquina de estados.
/// </summary>
public abstract class StateApplier
{
    /// <summary>
    /// Método estático que aplica un efecto de estado a la máquina de estados de un pokemon según el estado especificado.
    /// </summary>
    /// <param name="stateMachine">La máquina de estados que gestiona el estado actual del pokemon.</param>
    /// <param name="state">El estado que se desea aplicar al pokemon.</param>
    /// <returns>
    /// <c>string</c> Un mensaje indicando si el estado fue aplicado o si ya existe un estado conflictivo.
    /// </returns>
    public static string ApplyStateEffect(StateMachine stateMachine, EnumState state)
    {
        if (state == EnumState.Normal) return "";
        string stateName = stateMachine.CurrentState.Name.ToString();
        
        if (stateName.Equals(state.ToString())) 
            return $"El pokemon ya tiene el efecto **{stateName.ToUpper()}**.";
        
        if (stateMachine.CurrentState is not Normal) 
            return $"El pokemon ya tiene el efecto **{stateName.ToUpper()}**, el efecto **{state.ToString().ToUpper()}** no ha sido aplicado.";

        IPokemonState newState = state switch
        {
            EnumState.Sleep => new Sleep(),
            EnumState.Paralyze => new Paralyze(),
            EnumState.Poison => new Poison(),
            EnumState.Burn => new Burn(),
            _ => new Normal()
        };

        stateMachine.CurrentState = newState;
        return $"Se ha aplicado el efecto **{state.ToString().ToUpper()}**.";
    }
}