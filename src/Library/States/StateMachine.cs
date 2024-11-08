namespace Library.States;

public class StateMachine
{
    public IPokemonState CurrentState { get; set; }
    
    public StateMachine(IPokemonState initialState)
    {
        CurrentState = initialState;
    }
    
    public void ApplyEffect(Pokemon pokemon)
    {
        CurrentState.ApplyEffect(pokemon);
    }
    
    public int GetRemainingTurnsWithEffect()
    {
        return CurrentState.GetRemainingTurnsWithEffect();
    }

    public bool CanAttack()
    {
        return CurrentState.CanAttack();
    }
    
    public bool HasLostTurn()
    {
        return CurrentState.HasLostTurn();
    }
}