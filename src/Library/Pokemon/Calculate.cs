namespace Library;
/// <summary>
/// Clase que implementa la lógica para calcular el daño en un combate entre pokemon.
/// </summary>
/// <remarks>
/// Esta clase utiliza la interfaz <see cref="ICalculate"/> para calcular el daño que un pokemon atacante inflige a un pokemon defensor utilizando un movimiento específico.
/// </remarks>
public class Calculate: ICalculate
{
    private int critChancePercentage = 10;
    private int critDamagePercentage = 20;
    
    /// <summary>
    /// Calcula el daño infligido por un pokemon atacante a un pokemon defensor.
    /// </summary>
    /// <param name="attacker">El pokemon que realiza el ataque.</param>
    /// <param name="defender">El pokemon que recibe el ataque.</param>
    /// <param name="move">El movimiento utilizado para el ataque.</param>
    /// <param name="damage">El daño calculado que se infligirá al defensor (salida).</param>
    /// <returns>True si se ha realizado un golpe crítico; de lo contrario, false.</returns>
    public bool CalculateDamage(Pokemon attacker, Pokemon defender, Move move, out int damage, out bool isEffective)    // isEffective Agregado para la defensa
    {
        float bonus = CalculateBonus(attacker.Types, move.Type);
        float effectivity = CalculateEffectivity(defender.Types, move.Type, out isEffective);
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

    /// <summary>
    /// Aplica el daño crítico al daño calculado.
    /// </summary>
    /// <param name="dmg">El daño calculado antes de aplicar el golpe crítico.</param>
    /// <returns>El daño ajustado después de aplicar el golpe crítico.</returns>
    private double ApplyCrit(double dmg)
    {
        int randomValue = new Random().Next(0, 100);
        dmg += randomValue <= critChancePercentage
            ? dmg * critDamagePercentage / 100
            : 0;
        return dmg;
    }
    
    /// <summary>
    /// Calcula el bono de daño basado en los tipos del pokemon atacante y el tipo del movimiento.
    /// </summary>
    /// <param name="pokemonTypes">Lista de tipos del pokemon atacante.</param>
    /// <param name="moveType">Tipo del movimiento utilizado.</param>
    /// <returns>El bono de daño (1.5 si hay coincidencia de tipo, 1 en caso contrario).</returns>
    private float CalculateBonus(List<Type> pokemonTypes, Type moveType)
    {
        return pokemonTypes.Any(pokemonType => pokemonType == moveType) 
            ? 1.5f 
            : 1;
    }
    
    /// <summary>
    /// Calcula la efectividad del movimiento contra los tipos del pokemon defensor.
    /// </summary>
    /// <param name="enemyTypes">Lista de tipos del pokemon defensor.</param>
    /// <param name="moveType">Tipo del movimiento utilizado.</param>
    /// <returns>El multiplicador de efectividad del movimiento.</returns>
    private float CalculateEffectivity(List<Type> enemyTypes, Type moveType, out bool isEffective)  // isEffective Agregado para la defensa
    {
        isEffective = false;    // isEffective Agregado para la defensa
        
        float effectivity = 1;
        foreach (Type enemyType in enemyTypes)
        {
            effectivity *= DicTypeEffectivity.Effectivity[moveType.Name][enemyType.Name];
            if (DicTypeEffectivity.Effectivity[moveType.Name][enemyType.Name] > 1) isEffective = true;   // isEffective Agregado para la defensa

        }
        
        return effectivity;
    }

    /// <summary>
    /// Calcula la efectividad del movimiento contra los tipos del Pokémon defensor.
    /// </summary>
    /// <param name="enemyTypes">Lista de tipos del Pokémon defensor.</param>
    /// <param name="moveType">Tipo del movimiento utilizado.</param>
    /// <returns>El multiplicador de efectividad del movimiento.</returns>
    private int CalculateVariation(int min, int max)
    {
        return new Random().Next(min, max);
    }
    
    
}