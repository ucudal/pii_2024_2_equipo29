namespace Library;

public class Revive: IItem
{
    public int Amount { get; private set; } = 1;
    public string Use(Pokemon currentPokemon)
    {
        if (!currentPokemon.IsDead()) return $"El pokemon {currentPokemon.Name} ya está vivo.";
        if (Amount == 0) return "No te quedan más pociones para revivir.";
        
        currentPokemon.Hp = currentPokemon.InitialHp / 2;
        Amount -= 1;
        
        return $"El pokemon {currentPokemon.Name} ha revivido con {currentPokemon.InitialHp} HP.";
    }
}