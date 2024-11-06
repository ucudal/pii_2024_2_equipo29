namespace Library.States
{
    /// <summary>
    /// Clase que representa el estado de sueño (Sleep) en un Pokémon.
    /// </summary>
    public class Sleep : State
    {
        private int remainingTurns; ///Número de turnos restantes en el estado de sueño.
        private int sleepTurns; ///Total de turnos que el Pokémon debe dormir (no utilizado en este fragmento).

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Sleep"/> con el nombre del estado "dormir".
        /// </summary>
        public Sleep()
        {
            Name = "dormir";
        }

        /// <summary>
        /// Establece un número aleatorio de turnos de sueño dentro de un rango especificado.
        /// </summary>
        /// <param name="min">El número mínimo de turnos de sueño.</param>
        /// <param name="max">El número máximo de turnos de sueño.</param>
        private void SetRandomSleepTurns(int min, int max)
        {
            Random rand = new Random();
            remainingTurns = rand.Next(min, max);
        }

        /// <summary>
        /// Maneja el efecto del estado de sueño en cada turno.
        /// </summary>
        /// <param name="currentPokemon">El Pokémon que está en estado de sueño.</param>
        public override void OnTurn(Pokemon currentPokemon)
        {
            if (remainingTurns > 0)
            {
                remainingTurns--;
                // ToogleTurn // 
            }
            else
            {
                currentPokemon.PokemonState = new Normal();
            }
        }
    }
}