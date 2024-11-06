namespace Library.States
{
    /// <summary>
    /// Clase abstracta que representa un estado de un Pokémon.
    /// </summary>
    public abstract class State
    {
        /// <summary>
        /// Obtiene o establece el nombre del estado del Pokémon.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Realiza un ataque desde el Pokémon atacante hacia el Pokémon enemigo utilizando un movimiento específico.
        /// </summary>
        /// <param name="attacker">El Pokémon que realiza el ataque.</param>
        /// <param name="enemy">El Pokémon objetivo del ataque.</param>
        /// <param name="moveSlot">El índice del movimiento que se usará en el ataque.</param>
        public virtual void Attack(Pokemon attacker, Pokemon enemy, int moveSlot)
        {
            ICalculate calculate = new Calculate();
            Move move = attacker.Moves[moveSlot];
            int dmg = calculate.CalculateDamage(attacker, enemy, move);
            
            // Actualiza el HP del enemigo, asegurando que no baje por debajo de 0
            enemy.Hp = dmg > enemy.Hp 
                ? 0 
                : enemy.Hp - dmg;

            // Aplica efectos si el estado del enemigo es Normal
            if (enemy.PokemonState is Normal)
            {
                switch (move.Effect) // Aplicando efectos
                {
                    case EnumEffect.Sleep:
                        enemy.PokemonState = new Sleep();
                        break;
                    case EnumEffect.Paralyze:
                        enemy.PokemonState = new Paralyze();
                        break;
                    case EnumEffect.Poison:
                        enemy.PokemonState = new Poison();
                        break;
                    case EnumEffect.Burn:
                        enemy.PokemonState = new Burn();
                        break;
                }
            }
        }
        
        /// <summary>
        /// Maneja el efecto del estado en cada turno del Pokémon.
        /// </summary>
        /// <param name="currentPokemon">El Pokémon que se encuentra en este estado.</param>
        public virtual void OnTurn(Pokemon currentPokemon)
        {
            // Método virtual que puede ser sobrescrito por clases derivadas para implementar efectos específicos por turno
        }
    }
}
