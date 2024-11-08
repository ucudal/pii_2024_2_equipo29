namespace Library.States;

/// <summary>
/// Define la interfaz para los estados de un Pokémon, especificando
/// los métodos y propiedades comunes para aplicar efectos de estado.
/// Algunos métodos tiene valores por defecto.
/// </summary>
public interface IPokemonState
{
    /// <summary>
    /// Obtiene el nombre del estado de pokemon.
    /// </summary>
    /// <returns>
    ///     <c>EnumState</c> con el nombre del estado.
    /// </returns>
    public EnumState Name { get; }
    
    /// <summary>
    /// Obtiene el emoji que representa el estado del pokemon.
    /// </summary>
    /// /<returns>
    ///     <c>string</c> que contiene un emoji que representa el estado del pokemon.
    /// </returns>
    public string Emoji { get; }
    
    /// <summary>
    /// Aplica el efecto dependiendo de la clase que se haya aplicado la interfaz.
    /// Por defecto, no aplica ningun efecto.
    /// </summary>
    /// <param name="currentPokemon"><c>Pokemon</c> al que se le aplica el efecto.</param>
    public void ApplyEffect(Pokemon currentPokemon) { }
    
    /// <summary>
    /// Método que devuelve los turnos restantes que estará bajo el efecto el pokemon.
    /// </summary>
    ///  <returns>
    ///     <c>int</c> turnos restantes que estará bajo el efecto.
    ///     Por defecto, <c>-1</c>. Si el valor es <c>-1</c> representa que el estado es permanente.
    /// </returns>
    public int GetRemainingTurnsWithEffect() => -1;
    
    /// <summary>
    /// Método que sirve para verificar si el pokemon puede atacar.
    /// </summary>
    /// <returns>
    ///     <para><c>true</c> si puede atacar.</para>
    ///     <para><c>false</c> si no puede atacar.</para>
    ///     <para> Por defecto, <c>true</c>.</para>
    /// </returns>
    public bool CanAttack() => true;
    
    /// <summary>
    /// Método que sirve para verificar si el pokemon puede atacar.
    /// </summary>
    /// <returns>
    ///     <para><c>true</c> si ha perdido el turno.</para>
    ///     <para><c>false</c> si no ha peridod el turno.</para>
    ///     <para> Por defecto, <c>false</c>.</para>
    /// </returns>
    public bool HasLostTurn() => false;
}