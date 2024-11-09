namespace Library;

/// <summary>
/// Define la interfaz para gestionar pokemon.
/// </summary>
/// <remarks>
/// Esta interfaz proporciona métodos para agregar, cambiar y obtener pokemon.
/// </remarks>
public interface IPokemonManager
{
    /// <summary>
    /// Obtiene el nombre del gestor de pokemon.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Obtiene el pokemon actual que está siendo gestionado.
    /// </summary>
    Pokemon CurrentPokemon { get; }
    
    /// <summary>
    /// Agrega un nuevo pokemon al gestor.
    /// </summary>
    /// <param name="pokemon">El pokemon que se va a agregar.</param>
    void AddPokemon(Pokemon pokemon);
    
    /// <summary>
    /// Cambia el pokemon actual por uno nuevo.
    /// </summary>
    /// <param name="pokemon">El nuevo pokemon que se va a establecer como actual.</param>
    void ChangePokemon(Pokemon pokemon);
    
    
    /// <summary>
    /// Obtiene el siguiente pokemon en la lista de pokemon.
    /// </summary>
    /// <returns>El siguiente pokemon disponible.</returns>
    Pokemon GetNextPokemon();
}