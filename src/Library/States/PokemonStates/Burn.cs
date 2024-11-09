namespace Library.States;

/// <summary>
/// Clase que representa el estado <c>Burn</c> de un pokemon.
/// </summary>
public class Burn : IPokemonState
{
    /// <summary>
    /// Permite obtener el nombre del estado.
    /// </summary>
    /// <returns>
    ///     <c>EnumState</c> con el nombre del estado.
    /// </returns>
    public EnumState Name { get; }
    
    /// <summary>
    /// Obtiene un string con un emoji relacionado al estado <c>Burn</c>.
    /// </summary>
    /// <returns>
    ///     <c>string</c> que incluye un emoji relacionado al estado.
    /// </returns>
    public string Emoji { get; } = "\ud83d\udd25";
    
    /// <summary>
    /// Constructor de la clase <c>Burn</c> que inicializa el nombre del estado como <c>EnumState.Burn</c>.
    /// </summary>
    public Burn()
    {
        Name = EnumState.Burn;
    }
    
    /// <summary>
    /// Aplica el efecto de daño <c>Burn</c> al pokemon, reduciendo su HP.
    /// El daño es el 10% del HP inicial del pokemon.
    /// </summary>
    /// <param name="pokemon">El pokemon al que se le aplica el efecto de quemadura.</param>
    public void ApplyEffect(Pokemon pokemon)
    {
        pokemon.Hp -= (int)Math.Round(0.1 * pokemon.InitialHp);
    }
}