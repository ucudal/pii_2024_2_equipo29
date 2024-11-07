namespace Library;

public class Revive: IItem
{
    public int Amount { get; private set; } = 1;
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