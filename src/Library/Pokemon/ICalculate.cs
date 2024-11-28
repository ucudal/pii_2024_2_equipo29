namespace Library;
/// <summary>
/// Define la interfaz para calcular el daño en un combate entre pokemon.
/// </summary>
/// <remarks>
/// Esta interfaz proporciona un método para calcular el daño que un pokemon atacante inflige a un pokemon defensor utilizando un movimiento específico.
/// </remarks>
public interface ICalculate
{ 
    /// <summary>
    /// Calcula el daño infligido por un pokemon atacante a un pokemon defensor.
    /// </summary>
    /// <param name="attacker">El pokemon que realiza el ataque.</param>
    /// <param name="defender">El pokemon que recibe el ataque.</param>
    /// <param name="move">El movimiento utilizado para el ataque.</param>
    /// <param name="damage">El daño calculado que se infligirá al defensor (salida).</param>
    /// <returns>True si se ha realizado un golpe crítico; de lo contrario, false.</returns>
    bool CalculateDamage(Pokemon attacker, Pokemon defender, Move move, out int damage);

    /// <summary>
    /// Calcula la efectividad del ataque de un pokemon atacante a un pokemon defensor.
    /// </summary>
    /// <param name="enemyTypes">Lista con los tipos del pokemon defensor.</param>
    /// <param name="moveType">El movimiento utilizado del pokemon atacante.</param>
    /// <returns>El multiplicador de efectividad del movimiento.</returns>
    float CalculateEffectivity(List<Type> enemyTypes, Type moveType);
}