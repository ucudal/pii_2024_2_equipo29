using Library.States;

namespace Library;

public class FullHeal: IItem
{
    public int Amount { get; private set; } = 2;
    
    public string Use(Pokemon currentPokemon)
    {
        if (currentPokemon.IsDead()) return $"El pokemon **{currentPokemon.Name.ToUpper()}** está _muerto_, no puedes curarlo de efectos de ataques especiales.";
        if (currentPokemon.StateMachine.CurrentState.Name == EnumState.Normal.ToString()) return $"El pokemon **{currentPokemon.Name.ToUpper()}** no tiene efectos de ataques especiales.";
        if (Amount == 0) return "No te quedan super pociones para curar.";
        
        currentPokemon.Hp = currentPokemon.InitialHp;
        string negativeEffect = currentPokemon.StateMachine.CurrentState.Name;
        currentPokemon.StateMachine.CurrentState = new Normal();
        Amount -= 1;
        
        return $"El pokemon **{currentPokemon.Name.ToUpper()}** se ha curado del efecto **{negativeEffect.ToUpper()}**.";
    }
}