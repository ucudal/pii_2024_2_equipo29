namespace Library;

public class SuperPotion: IItem
{
    private const int HealValue = 70;
    public int Amount { get; private set; } = 4;
    
    public string Use(Pokemon currentPokemon)
    {
        if (currentPokemon.IsDead()) return $"El pokemon {currentPokemon.Name} está muerto, no puedes curarlo.";
        if (Amount == 0) return "No te quedan super pociones para curar.";
        
        if (currentPokemon.Hp + HealValue <= currentPokemon.InitialHp)
        {
            currentPokemon.Hp += HealValue;
        }
        else
        {
            currentPokemon.Hp = currentPokemon.InitialHp;
        }

        Amount -= 1;

        return $"El pokemon {currentPokemon.Name} ha recuperado {HealValue} HP";
    }
}