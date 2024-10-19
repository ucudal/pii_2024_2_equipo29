namespace Library.States;

public class Poison: State
{
    public override void OnTurn(Pokemon currentPokemon)
    {
        currentPokemon.Hp -= (int)Math.Round(0.05*currentPokemon.InitialHp);
    }
    public Poison()
    {
        Name = "veneno";
    }
}