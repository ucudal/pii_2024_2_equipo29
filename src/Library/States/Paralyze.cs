namespace Library.States
{
    /// <summary>
    /// Clase que representa el estado de parálisis en un Pokémon.
    /// </summary>
    public class Paralyze : State
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Paralyze"/> con el nombre del estado "paralizar".
        /// </summary>
        public Paralyze()
        {
            Name = "paralizar";
        }

        /// <summary>
        /// Realiza un intento de ataque del Pokémon en estado de parálisis, 
        /// verificando si puede atacar antes de proceder con el ataque.
        /// </summary>
        /// <param name="attacker">El Pokémon que realiza el ataque.</param>
        /// <param name="enemy">El Pokémon objetivo del ataque.</param>
        /// <param name="moveSlot">El índice del movimiento que se usará en el ataque.</param>
        public override void Attack(Pokemon attacker, Pokemon enemy, int moveSlot)
        {
            if (CanAttack())
            {
                base.Attack(attacker, enemy, moveSlot);
            }
        }

        /// <summary>
        /// Determina aleatoriamente si el Pokémon en estado de parálisis puede atacar en este turno.
        /// </summary>
        /// <returns><c>true</c> si el Pokémon puede atacar; de lo contrario, <c>false</c>.</returns>
        private bool CanAttack()
        {
            Random rand = new Random();
            return rand.Next(2) == 0;
        }
    }
}