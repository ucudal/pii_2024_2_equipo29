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

        if (enemy.PokemonState.Name == "normal")
        {
            switch (move.Effect.ToLower())    // Applying effects
            {
                case "dormir":
                    enemy.PokemonState = new Sleep();
                    break;
                case "paralizar":
                    enemy.PokemonState = new Paralyze();
                    break;
                case "veneno":
                    enemy.PokemonState = new Poison();
                    break;
                case "quemadura":
                    enemy.PokemonState = new Burn();
                    break;
            }
        }
    }
    public virtual void OnTurn(Pokemon currentPokemon)
    {
        
    }
}