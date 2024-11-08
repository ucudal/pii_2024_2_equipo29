using Library.States;

namespace Library;
/// <summary>
/// Clase que representa un objeto Full Heal, que permite curar a un Pokémon de efectos de ataques especiales.
/// </summary>
/// <remarks>
/// Esta clase implementa la interfaz <see cref="IItem"/> y permite restaurar el estado de un Pokémon 
/// a su estado normal, eliminando cualquier efecto negativo que pueda tener.
/// </remarks>
public class FullHeal: IItem
{
    /// <summary>
    /// Cantidad de objetos Full Heal disponibles.
    /// </summary>
    public int Amount { get; private set; } = 2;
    
    /// <summary>
    /// Utiliza el objeto Full Heal en un Pokémon específico.
    /// </summary>
    /// <param name="pokemonName">El nombre del Pokémon al que se le aplicará el Full Heal.</param>
    /// <param name="player">El jugador que posee el objeto Full Heal.</param>
    /// <returns>Un mensaje que indica el resultado de la acción.</returns>
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