namespace Library
{
    /// <summary>
    /// Interfaz para calcular el daño de un ataque entre dos Pokémon.
    /// </summary>
    public interface ICalculate
    {
        /// <summary>
        /// Calcula el daño infligido por un Pokémon atacante a un Pokémon defensor usando un movimiento específico.
        /// </summary>
        /// <param name="attacker">El Pokémon que realiza el ataque.</param>
        /// <param name="defender">El Pokémon que recibe el ataque.</param>
        /// <param name="move">El movimiento utilizado en el ataque.</param>
        /// <returns>La cantidad de daño calculada como un entero.</returns>
        int CalculateDamage(Pokemon attacker, Pokemon defender, Move move);
    }
}