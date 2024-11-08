namespace Library.States;

public class Sleep : IPokemonState
{
    public EnumState Name { get; }
    public string Emoji { get; } = "\ud83d\ude34";
    private int minSleepTurns = 1;
    private int maxSleepTurns = 4;
    private int remainingTurns;
    
    public Sleep()
    {
        Name = EnumState.Sleep;
        SetRandomSleepTurns(minSleepTurns + 1, maxSleepTurns + 1);
    }

    public void ApplyEffect(Pokemon currentPokemon)
    {
        if (remainingTurns > 0)
        {
            remainingTurns--;
        }
        else
        {
            currentPokemon.StateMachine.CurrentState = new Normal();
        }
    }

    public bool CanAttack()
    {
        return remainingTurns == 0;
    }

    public int GetRemainingTurnsWithEffect()
    {
        return remainingTurns;
    }
    
    private void SetRandomSleepTurns(int min, int max)
    {
        remainingTurns = new Random().Next(min, max);
    }
}