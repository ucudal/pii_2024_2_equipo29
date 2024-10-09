namespace Library;

public class Calculate
{
    public static int calculateDamage(Pokemon attacker, Pokemon defender, Move move)
    {
        float B = calculateBonus(attacker.Types, move.type);
        float E = calculateEffectivity(defender.Types, move.type);
        int V = calculateVariation(85, 100);
        int P = move.Power;
        int A = attacker.AttackPoints;
        int D = attacker.DefensePoints;
        if (move.isSpecialMove)
        {
            A = attacker.SpecialAttackPoints;
            D = attacker.SpecialDefensePoints;
        }
        
        double dmg = 0.1*B*E*V*((1.2*A*P/(25*D))+2);  
        
        
        return (int)Math.Round(dmg);
    }

    private static float calculateBonus(List<Type> pokemonTypes, Type moveType) // A terminar
    {
        float bonus = 0;
        return bonus;
    }
    
    private static float calculateEffectivity(List<Type> enemyTypes, Type moveType)  // A terminar
    {
        float effectivity = 0;
        return effectivity;
    }

    private static int calculateVariation(int min, int max)
    {
        Random var = new Random();
        return var.Next(min, max);
    }
}