namespace Library.States;

/// <summary>
/// Enum que contiene los posibles estados de un pokemon.
/// </summary>
public enum EnumState
{
    /// <summary>
    /// Estado normal, sin efectos especiales.
    /// </summary>
    Normal,
    
    /// <summary>
    /// Estado de sueño, el pokemon no puede atacar por cierta cantidad de rondas.
    /// </summary>
    Sleep,
    
    /// <summary>
    /// Estado de envenenamiento, el pokemon pierde 5% del HP inicial en cada turno.
    /// </summary>
    Poison,
    
    /// <summary>
    /// Estado de parálisis, el pokemon tiene una probabilidad del 50% de perder su turno.
    /// </summary>
    Paralyze,
    
    /// <summary>
    /// Estado de quemadura, el pokemon pierde 10% del HP inicial en cada turno.
    /// </summary>
    Burn
}