namespace Library.States
{
    /// <summary>
    /// Clase que representa el estado de envenenamiento (Poison) en un Pokémon.
    /// </summary>
    public class Poison : State
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Poison"/> con el nombre del estado "veneno".
        /// </summary>
        public Poison()
        {
            Name = "veneno";
        }

        /// <summary>
        /// Aplica el efecto de envenenamiento al Pokémon actual, reduciendo su HP en un 5% de su HP inicial en cada turno.
        /// </summary>
        /// <param name="currentPokemon">El Pokémon que recibe el daño por envenenamiento.</param>
        public override void OnTurn(Pokemon currentPokemon)
        {
            currentPokemon.Hp -= (int)Math.Round(0.05 * currentPokemon.InitialHp);
        }
    }
}