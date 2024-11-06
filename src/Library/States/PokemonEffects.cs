namespace Library.States
{
    /// <summary>
    /// Clase abstracta que define los efectos de estado aplicables a los tipos de Pokémon.
    /// </summary>
    public abstract class PokemonEffects
    {
        /// <summary>
        /// Diccionario que asocia cada tipo de Pokémon con un efecto de estado correspondiente.
        /// </summary>
        /// <remarks>
        /// La clave es el nombre del tipo de Pokémon (por ejemplo: "normal", "fire") y el valor es el efecto de estado
        /// asociado de tipo <see cref="EnumEffect"/> (por ejemplo, <c>EnumEffect.Sleep</c>, <c>EnumEffect.Burn</c>).
        /// </remarks>
        public static Dictionary<string, EnumEffect> Effects { get; } = new()
        {
            { "normal", EnumEffect.Sleep },
            { "fighting", EnumEffect.Sleep },
            { "flying", EnumEffect.Paralyze },
            { "poison", EnumEffect.Poison },
            { "ground", EnumEffect.Sleep },
            { "rock", EnumEffect.Paralyze },
            { "bug", EnumEffect.Poison },
            { "ghost", EnumEffect.Paralyze },
            { "steel", EnumEffect.Sleep },
            { "fire", EnumEffect.Burn },
            { "water", EnumEffect.Burn },
            { "grass", EnumEffect.Poison },
            { "electric", EnumEffect.Burn },
            { "psychic", EnumEffect.Paralyze },
            { "ice", EnumEffect.Sleep },
            { "dragon", EnumEffect.Burn },
            { "dark", EnumEffect.Poison },
            { "fairy", EnumEffect.Paralyze },
        };
    }
}