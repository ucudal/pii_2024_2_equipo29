namespace Library.States;

public class Burn : State
{
    public Burn()
    {
        Name = "quemadura";
    }
    
    public override void OnTurn(Pokemon currentPokemon)
    {
        currentPokemon.Hp -= (int)Math.Round(0.1*currentPokemon.InitialHp);
    }
}