namespace Library.States;

public abstract class StateApplier
{
    public static void ApplyStateEffect(StateMachine stateMachine, EnumState state)
    {
        Console.WriteLine($"\n\n'{stateMachine.CurrentState.Name}' != '{EnumState.Normal.ToString()}'");
        if (stateMachine.CurrentState.Name != EnumState.Normal.ToString()) return;

        IPokemonState newState = state switch
        {
            EnumState.Sleep => new Sleep(),
            EnumState.Paralyze => new Paralyze(),
            EnumState.Poison => new Poison(),
            EnumState.Burn => new Burn(),
            _ => new Normal()
        };

        stateMachine.CurrentState = newState;
        Console.Write($"Se aplico el estado {stateMachine.CurrentState.Name}");
    }
}