namespace Library
{
    /// <summary>
    /// Interfaz para la gestión de un conjunto de Pokémon.
    /// </summary>
    public interface IPokemonManager
    {
        /// <summary>
        /// Obtiene el Pokémon actualmente seleccionado.
        /// </summary>
        Pokemon CurrentPokemon { get; }

        /// <summary>
        /// Agrega un nuevo Pokémon al conjunto de gestión.
        /// </summary>
        /// <param name="pokemon">El Pokémon a agregar.</param>
        void AddPokemon(Pokemon pokemon);

        /// <summary>
        /// Cambia el Pokémon actualmente seleccionado por otro.
        /// </summary>
        /// <param name="pokemon">El nuevo Pokémon a seleccionar.</param>
        void ChangePokemon(Pokemon pokemon);

        /// <summary>
        /// Obtiene el siguiente Pokémon disponible en el conjunto.
        /// </summary>
        /// <returns>El siguiente Pokémon disponible.</returns>
        Pokemon GetNextPokemon();
    }
}