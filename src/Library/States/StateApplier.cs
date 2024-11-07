namespace Library.States;

public abstract class StateApplier
{
    public static string ApplyStateEffect(StateMachine stateMachine, EnumState state)
    {
        if (state == EnumState.Normal) return "";
        
        if (stateMachine.CurrentState is not Normal) 
            return $"El pokemon ya tiene el efecto {stateMachine.CurrentState.Name}, el efecto {state} no ha sido aplicado.";

        IPokemonState newState = state switch
        {
            EnumState.Sleep => new Sleep(),
            EnumState.Paralyze => new Paralyze(),
            EnumState.Poison => new Poison(),
            EnumState.Burn => new Burn(),
            _ => new Normal()
        };

        stateMachine.CurrentState = newState;
        return "";
    }
}