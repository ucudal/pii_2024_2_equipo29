namespace Library;

public class Calculate: ICalculate
{
    private int critChancePercentage = 10;
    private int critDamagePercentage = 20;
    
    public int CalculateDamage(Pokemon attacker, Pokemon defender, Move move)
    {
        float b = CalculateBonus(attacker.Types, move.Type);
        float e = CalculateEffectivity(defender.Types, move.Type);
        int v = CalculateVariation(85, 100);
        int p = move.Power;
        int a = attacker.AttackPoints;
        int d = attacker.DefensePoints;
        
        if (move.IsSpecialMove)
        {
            a = attacker.SpecialAttackPoints;
            d = attacker.SpecialDefensePoints;
        }
        
        double dmg = 0.1 * b * e * v * (((1.2 * a * p) / (25 * d)) + 2);

        dmg = ApplyCrit(dmg);
        return (int)Math.Round(dmg);
    }

    private double ApplyCrit(double dmg)
    {
        Random rand = new Random();
        dmg += rand.Next(0, 100) <= critChancePercentage ? dmg * critDamagePercentage / 100 : 0;
        return dmg;
    }
    private static float CalculateBonus(List<Type> pokemonTypes, Type moveType)
    {
        return pokemonTypes.Any(pokemonType => pokemonType == moveType) 
            ? 1.5f 
            : 1;
    }
    
    private static float CalculateEffectivity(List<Type> enemyTypes, Type moveType)
    {
        float effectivity = 1;
        foreach (Type enemyType in enemyTypes)
        {
            effectivity *= TypeEffectivity.Effectivity[moveType.Name][enemyType.Name];
        }
        
        return effectivity;
    }

    private static int CalculateVariation(int min, int max)
    {
        return new Random().Next(min, max);
    }
}