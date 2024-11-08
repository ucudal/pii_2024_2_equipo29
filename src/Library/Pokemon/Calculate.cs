namespace Library;

public class Calculate: ICalculate
{
    private int critChancePercentage = 10;
    private int critDamagePercentage = 20;
    
    public bool CalculateDamage(Pokemon attacker, Pokemon defender, Move move, out int damage)
    {
        float bonus = CalculateBonus(attacker.Types, move.Type);
        float effectivity = CalculateEffectivity(defender.Types, move.Type);
        int variation = CalculateVariation(85, 100);
        int power = move.Power;
        int attackPoints = attacker.AttackPoints;
        int defensePoints = attacker.DefensePoints;
        
        if (move.IsSpecialMove)
        {
            attackPoints = attacker.SpecialAttackPoints;
            defensePoints = attacker.SpecialDefensePoints;
        }
        
        double dmg = 0.1 * bonus * effectivity * variation * (((1.2 * attackPoints * power) / (25 * defensePoints)) + 2);
        double preCrit = dmg;
        dmg = ApplyCrit(dmg);
        damage = (int)Math.Round(dmg);
        return dmg > preCrit;
    }

    private double ApplyCrit(double dmg)
    {
        int randomValue = new Random().Next(0, 100);
        dmg += randomValue <= critChancePercentage
            ? dmg * critDamagePercentage / 100
            : 0;
        return dmg;
    }
    
    private float CalculateBonus(List<Type> pokemonTypes, Type moveType)
    {
        return pokemonTypes.Any(pokemonType => pokemonType == moveType) 
            ? 1.5f 
            : 1;
    }
    
    private float CalculateEffectivity(List<Type> enemyTypes, Type moveType)
    {
        float effectivity = 1;
        foreach (Type enemyType in enemyTypes)
        {
            effectivity *= DicTypeEffectivity.Effectivity[moveType.Name][enemyType.Name];
        }
        
        return effectivity;
    }

    private int CalculateVariation(int min, int max)
    {
        return new Random().Next(min, max);
    }
}