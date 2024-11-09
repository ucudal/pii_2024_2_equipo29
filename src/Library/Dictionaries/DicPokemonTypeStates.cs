namespace Library.States;

/// <summary>
/// Clase abstracta que contiene un diccionario de estados de los tipos de pokemon.
/// </summary>
public abstract class DicPokemonTypeStates
{
    /// <summary>
    /// Diccionario estático de estados donde cada <i><b>CLAVE</b></i> representa el nombre del tipo de un pokemon 
    /// y cada <i><b>VALOR</b></i> es un <c>EnumState</c>.
    /// Dependiendo del tipo de pokemon se le asigna un estado por defecto.
    /// <para>Se utiliza en <c>PokemonAdapter</c> para asignarle un efecto de estado al movimiento especial.</para>
    /// </summary>
    public static Dictionary<string, EnumState> States { get; } = new()
    {
        { "normal", EnumState.Sleep },
        { "fighting", EnumState.Sleep },
        { "flying", EnumState.Paralyze },
        { "poison", EnumState.Poison },
        { "ground", EnumState.Sleep },
        { "rock", EnumState.Paralyze },
        { "bug", EnumState.Poison },
        { "ghost", EnumState.Paralyze },
        { "steel", EnumState.Sleep },
        { "fire", EnumState.Burn },
        { "water", EnumState.Burn },
        { "grass", EnumState.Poison },
        { "electric", EnumState.Burn },
        { "psychic", EnumState.Paralyze },
        { "ice", EnumState.Sleep },
        { "dragon", EnumState.Burn },
        { "dark", EnumState.Poison },
        { "fairy", EnumState.Paralyze },
    };
}