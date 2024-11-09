namespace Library;

/// <summary>
/// Define la interfaz para gestionar Pokémon.
/// </summary>
/// <remarks>
/// Esta interfaz proporciona métodos para agregar, cambiar y obtener Pokémon.
/// </remarks>
public interface IPokemonManager
{
    /// <summary>
    /// Obtiene el nombre del gestor de Pokémon.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Obtiene el Pokémon actual que está siendo gestionado.
    /// </summary>
    Pokemon CurrentPokemon { get; }
    
    /// <summary>
    /// Agrega un nuevo Pokémon al gestor.
    /// </summary>
    /// <param name="pokemon">El Pokémon que se va a agregar.</param>
    void AddPokemon(Pokemon pokemon);
    
    /// <summary>
    /// Cambia el Pokémon actual por uno nuevo.
    /// </summary>
    /// <param name="pokemon">El nuevo Pokémon que se va a establecer como actual.</param>
    void ChangePokemon(Pokemon pokemon);
    
    
    /// <summary>
    /// Obtiene el siguiente Pokémon en la lista de Pokémon.
    /// </summary>
    /// <returns>El siguiente Pokémon disponible.</returns>
    Pokemon GetNextPokemon();
}