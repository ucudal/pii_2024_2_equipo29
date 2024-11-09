namespace Library;

/// <summary>
/// Interfaz que representa un ítem que puede ser utilizado en un Pokémon.
/// </summary>
/// <remarks>
/// Esta interfaz define las propiedades y métodos que deben implementar 
/// todos los ítems que se pueden usar en el contexto de un juego de Pokémon.
/// </remarks>
public interface IItem
{
    /// <summary>
    /// Obtiene la cantidad de ítems disponibles.
    /// </summary>
    int Amount { get; }
    
    /// <summary>
    /// Utiliza el ítem en un pokemon específico.
    /// </summary>
    /// <param name="pokemonName">El nombre del pokemon al que se le aplicará el ítem.</param>
    /// <param name="player">El jugador que posee el ítem.</param>
    /// <returns>Un mensaje que indica el resultado de la acción.</returns>
    string Use(string pokemonName, Player player);
}