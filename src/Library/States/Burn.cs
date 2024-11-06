namespace Library.States
{
    /// <summary>
    /// Clase que representa el estado de quemadura (Burn) en un Pokémon.
    /// </summary>
    public class Burn : State
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Burn"/> con el nombre del estado "quemadura".
        /// </summary>
        public Burn()
        {
            Name = "quemadura";
        }

        /// <summary>
        /// Aplica el efecto de quemadura al Pokémon actual, reduciendo su HP en un 10% de su HP inicial en cada turno.
        /// </summary>
        /// <param name="currentPokemon">El Pokémon que recibe el daño por quemadura.</param>
        public override void OnTurn(Pokemon currentPokemon)
        {
            currentPokemon.Hp -= (int)Math.Round(0.1 * currentPokemon.InitialHp);
        }
    }
}