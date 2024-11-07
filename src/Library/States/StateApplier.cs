using DSharpPlus.SlashCommands;

namespace Library.States;

public abstract class StateApplier
{
    public static string ApplyStateEffect(StateMachine stateMachine, EnumState state)
    {
        if (state == EnumState.Normal) return "";
        
        if (stateMachine.CurrentState.Name.Equals(state.ToString())) 
            return $"El pokemon ya tiene el efecto **{stateMachine.CurrentState.Name.ToUpper()}**.";
        
        if (stateMachine.CurrentState is not Normal) 
            return $"El pokemon ya tiene el efecto **{stateMachine.CurrentState.Name.ToUpper()}**, el efecto **{state.ToString().ToUpper()}** no ha sido aplicado.";

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