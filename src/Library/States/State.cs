namespace Library.States;

public abstract class State
{
    public string Name { get; set; }
    
    public virtual void Attack(Pokemon attacker, Pokemon enemy, int moveSlot)
    {
        ICalculate calculate = new Calculate();
        Move move = attacker.Moves[moveSlot];
        int dmg = calculate.CalculateDamage(attacker, enemy, move);
        
        enemy.Hp = dmg > enemy.Hp 
            ? 0 
            : enemy.Hp - dmg;

        if (enemy.PokemonState is Normal)
        {
            switch (move.Effect)    // Applying effects
            {
                case EnumEffect.Sleep:
                    enemy.PokemonState = new Sleep();
                    break;
                case EnumEffect.Paralyze:
                    enemy.PokemonState = new Paralyze();
                    break;
                case EnumEffect.Poison:
                    enemy.PokemonState = new Poison();
                    break;
                case EnumEffect.Burn:
                    enemy.PokemonState = new Burn();
                    break;
            }
        }
    }
    
    public virtual void OnTurn(Pokemon currentPokemon)
    {
        
    }
}