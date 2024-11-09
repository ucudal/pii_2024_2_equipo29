namespace Library;

/// <summary>
/// Clase que representa una Super Poción, un ítem que puede curar a un pokemon.
/// </summary>
/// <remarks>
/// Esta clase implementa la interfaz <see cref="IItem"/> y permite curar a un pokemon 
/// restaurando una cantidad específica de puntos de salud (HP).
/// </remarks>
public class SuperPotion: IItem
{
    private const int HealValue = 70;
    public int Amount { get; private set; } = 4;
    
    /// <summary>
    /// Utiliza la Super poción en un pokemon específico.
    /// </summary>
    /// <param name="pokemonName">El nombre del pokemon al que se le aplicará la Super Poción.</param>
    /// <param name="player">El jugador que posee la Super poción.</param>
    /// <returns>Un mensaje que indica el resultado de la acción.</returns>
    public string Use(string pokemonName, Player player)
    {
        Pokemon currentPokemon = player.GetPokemonByName(pokemonName);
        if(currentPokemon == null!) return $"El pokemon **{pokemonName}** no ha sido encontrado.";
        
        if (currentPokemon.IsDead()) return $"El pokemon **{currentPokemon.Name.ToUpper()}** está muerto, no puedes curarlo.";
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

        return $"El pokemon **{currentPokemon.Name.ToUpper()}** ha recuperado **{HealValue}** HP";
    }
}