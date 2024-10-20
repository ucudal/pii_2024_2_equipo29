namespace Library.States;

public abstract class PokemonEffects
{
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