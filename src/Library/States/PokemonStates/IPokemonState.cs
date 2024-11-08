namespace Library.States;

public interface IPokemonState
{
    public EnumState Name { get; }
    public void ApplyEffect(Pokemon currentPokemon) { }
    public int GetRemainingTurnsWithEffect() => -1;
    public bool CanAttack() => true;
    public bool HasLostTurn() => false;
}