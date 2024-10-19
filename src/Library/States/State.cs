namespace Library.States;

public abstract class State
{
    public string Name;
    public virtual void Attack(Pokemon attacker, Pokemon enemy, int moveSlot)
    {
        ICalculate calculate = new Calculate();
        Move move = attacker.Moves[moveSlot];
        int dmg = calculate.CalculateDamage(attacker, enemy, move);
        enemy.Hp = dmg > enemy.Hp 
            ? 0 
            : enemy.Hp - dmg;

        if (enemy.state.Name == "normal")
        {
            switch (move.Effect.ToLower())    // Applying effects
            {
                case "dormir":
                    enemy.state = new Sleep();
                    break;
                case "paralizar":
                    enemy.state = new Paralyze();
                    break;
                case "veneno":
                    enemy.state = new Poison();
                    break;
                case "quemadura":
                    enemy.state = new Burn();
                    break;
            }
        }
    }
    public virtual void OnTurn(Pokemon currentPokemon)
    {
        
    }
}