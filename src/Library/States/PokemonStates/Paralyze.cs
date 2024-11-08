namespace Library.States;

/// <summary>
/// Clase que representa el estado <c>Paralyze</c> de un pokemon.
/// </summary>
public class Paralyze : IPokemonState
{
    /// <summary>
    /// Permite obtener el nombre del estado.
    /// </summary>
    /// <returns>
    ///     <c>EnumState</c> con el nombre del estado.
    /// </returns>
    public EnumState Name { get; }
    
    /// <summary>
    /// Obtiene un string con un emoji relacionado al estado <c>Paralyze</c>.
    /// </summary>
    /// <returns>
    ///     <c>string</c> que incluye un emoji relacionado al estado.
    /// </returns>
    public string Emoji { get; } = "\u26a1";

    /// <summary>
    /// Constructor de la clase <c>Paralyze</c> que inicializa el nombre del estado como <c>EnumState.Paralyze</c>.
    /// </summary>
    public Paralyze()
    {
        Name = EnumState.Paralyze;
    }

    /// <summary>
    /// Método que permite saber si se ha perdido un turno de forma aleatoria con un 50% de probabilidad.
    /// </summary>
    /// <returns>
    ///     <c>bool</c> que indica si se perdió el turno.
    /// </returns>
    public bool HasLostTurn()
    {
        return new Random().Next(2) == 0;
    }
}