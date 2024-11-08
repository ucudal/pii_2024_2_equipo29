using Library.States;

namespace Library;

public class FullHeal: IItem
{
    public int Amount { get; private set; } = 2;
    
    public string Use(string pokemonName, Player player)
    {
        Pokemon currentPokemon = player.GetPokemonByName(pokemonName);
        if(currentPokemon == null!) return $"El pokemon **{pokemonName}** no ha sido encontrado.";
        
        if (currentPokemon.IsDead()) 
            return $"El pokemon **{currentPokemon.Name.ToUpper()}** está _muerto_, no puedes curarlo de efectos de ataques especiales.";
        if (currentPokemon.StateMachine.CurrentState.Name.ToString() == EnumState.Normal.ToString()) 
            return $"El pokemon **{currentPokemon.Name.ToUpper()}** no tiene efectos de ataques especiales.";
        
        if (Amount == 0) return "No te quedan super pociones para curar.";
        
        string negativeEffect = currentPokemon.StateMachine.CurrentState.Name.ToString();
        currentPokemon.StateMachine.CurrentState = new Normal();
        Amount -= 1;
        
        return $"El pokemon **{currentPokemon.Name.ToUpper()}** se ha curado del efecto **{negativeEffect.ToUpper()}**.";
    }
}