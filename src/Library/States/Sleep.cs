namespace Library.States;

public class Sleep: State
{
    private int remainingTurns;
    private int sleepTurns;
    
    public Sleep()
    {
        Name = "dormir";
    }
    
    private void setRandomSleepTurns(int min, int max)
    {
        Random rand = new Random();
        remainingTurns = rand.Next(min, max);
    }
    
    public override void OnTurn(Pokemon currentPokemon)
    {
        if (remainingTurns > 0)
        {
            remainingTurns--;
            // ToogleTurn // 
        }
        else
        {
            currentPokemon.PokemonState = new Normal();
        }
    }
}