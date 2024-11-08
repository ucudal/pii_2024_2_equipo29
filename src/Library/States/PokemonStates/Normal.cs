namespace Library.States;

/// <summary>
/// Clase que representa el estado <c>Normal</c> de un pokemon.
/// </summary>
public class Normal : IPokemonState
{
    /// <summary>
    /// Permite obtener el nombre del estado.
    /// </summary>
    /// <returns>
    ///     <c>EnumState</c> con el nombre del estado.
    /// </returns>
    public EnumState Name { get; }
    
    /// <summary>
    /// Obtiene un string con un emoji relacionado al estado <c>Normal</c>.
    /// </summary>
    /// <returns>
    ///     <c>string</c> que incluye un emoji relacionado al estado.
    /// </returns>
    public string Emoji { get; } = "\ud83d\ude34";
    
    /// <summary>
    /// Constructor de la clase <c>Normal</c> que inicializa el nombre del estado como <c>EnumState.Normal</c>.
    /// </summary>
    public Normal()
    {
        Name = EnumState.Normal;
    }
}