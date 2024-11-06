namespace Library
{
    /// <summary>
    /// Interfaz que representa un ítem en el juego.
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// Obtiene la cantidad disponible de este ítem.
        /// </summary>
        int Amount { get; }

        /// <summary>
        /// Usa el ítem en el Pokémon actual.
        /// </summary>
        /// <param name="currentPokemon">El Pokémon en el cual se usará el ítem.</param>
        /// <returns>Un mensaje indicando el resultado del uso del ítem.</returns>
        string Use(Pokemon currentPokemon);
    }
}