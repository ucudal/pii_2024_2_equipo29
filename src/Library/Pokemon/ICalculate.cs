namespace Library;
/// <summary>
/// Define la interfaz para calcular el daño en un combate entre Pokémon.
/// </summary>
/// <remarks>
/// Esta interfaz proporciona un método para calcular el daño que un Pokémon atacante inflige a un Pokémon defensor utilizando un movimiento específico.
/// </remarks>
public interface ICalculate
{ 
    /// <summary>
    /// Calcula el daño infligido por un Pokémon atacante a un Pokémon defensor.
    /// </summary>
    /// <param name="attacker">El Pokémon que realiza el ataque.</param>
    /// <param name="defender">El Pokémon que recibe el ataque.</param>
    /// <param name="move">El movimiento utilizado para el ataque.</param>
    /// <param name="damage">El daño calculado que se infligirá al defensor (salida).</param>
    /// <returns>True si se ha realizado un golpe crítico; de lo contrario, false.</returns>
    bool CalculateDamage(Pokemon attacker, Pokemon defender, Move move, out int damage);
}