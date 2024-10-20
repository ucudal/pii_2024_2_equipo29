namespace Library;

public class FullHeal: IItem
{
    public int Amount { get; private set; } = 2;
    
    public string Use(Pokemon currentPokemon)
    {
        if (currentPokemon.IsDead()) return $"El pokemon {currentPokemon.Name} está muerto, no puedes curarlo de efectos de ataques especiales.";
        if (currentPokemon.PokemonState.Name == "normal") return $"El pokemon {currentPokemon.Name} no tiene efectos de ataques especiales."; 
        if (Amount == 0) return "No te quedan super pociones para curar.";
        
        currentPokemon.Hp = currentPokemon.InitialHp;
        Amount -= 1;

        return $"El pokemon {currentPokemon.Name} se ha curado del efecto {currentPokemon.PokemonState.Name}.";
    }
}