namespace Library.States;

/// <summary>
/// Clase que representa el estado <c>Poison</c> de un pokemon.
/// </summary>
public class Poison : IPokemonState
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
    public string Emoji { get; } = "\u2620\ufe0f";

    /// <summary>
    /// Constructor de la clase <c>Poison</c> que inicializa el nombre del estado como <c>EnumState.Poison</c>.
    /// </summary>
    public Poison()
    {
        Name = EnumState.Poison;
    }
    
    /// <summary>
    /// Aplica el efecto de daño <c>Poison</c> al pokemon, reduciendo su HP.
    /// El daño es el 5% del HP inicial del pokemon.
    /// </summary>
    /// <param name="pokemon">El pokemon al que se le aplica el efecto de veneno.</param>
    public void ApplyEffect(Pokemon pokemon)
    {
        pokemon.Hp -= (int)Math.Round(0.05 * pokemon.InitialHp);
    }
}