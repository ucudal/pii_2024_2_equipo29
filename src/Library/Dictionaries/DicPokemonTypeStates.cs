namespace Library.States;

public abstract class DicPokemonTypeStates
{
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