namespace Library;
/// <summary>
/// Clase que representa un objeto Revivir, que permite revivir a un Pokémon que ha sido derrotado.
/// </summary>
/// <remarks>
/// Esta clase implementa la interfaz <see cref="IItem"/> y permite revivir a un Pokémon 
/// restaurando su salud a la mitad de su HP inicial.
/// </remarks>
public class Revive: IItem
{
    ///  <summary>
    /// Cantidad de objetos Revivir disponibles.
    /// </summary>
    public int Amount { get; private set; } = 1;
    
    /// <summary>
    /// Utiliza el objeto Revivir en un Pokémon específico.
    /// </summary>
    /// <param name="pokemonName">El nombre del Pokémon que se desea revivir.</param>
    /// <param name="player">El jugador que posee el objeto Revivir.</param>
    /// <returns>Un mensaje que indica el resultado de la acción.</returns>
    public string Use(string pokemonName, Player player)
    {
        Pokemon currentPokemon = player.GetPokemonByName(pokemonName);
        if(currentPokemon == null!) return $"El pokemon **{pokemonName.ToUpper()}** no ha sido encontrado.";
        
        if (!currentPokemon.IsDead()) return $"El pokemon **{currentPokemon.Name.ToUpper()}** ya está vivo.";
        if (Amount == 0) return "No te quedan más pociones para revivir.";
        
        currentPokemon.Hp = currentPokemon.InitialHp / 2;
        Amount -= 1;
        
        return $"El pokemon **{currentPokemon.Name.ToUpper()}** ha revivido con **{currentPokemon.InitialHp}** HP.";
    }
}