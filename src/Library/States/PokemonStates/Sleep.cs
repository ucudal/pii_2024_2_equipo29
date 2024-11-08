namespace Library.States;

/// <summary>
/// Clase que representa el estado <c>Sleep</c> de un pokemon.
/// </summary>
public class Sleep : IPokemonState
{
    /// <summary>
    /// Permite obtener el nombre del estado.
    /// </summary>
    /// <returns>
    ///     <c>EnumState</c> con el nombre del estado.
    /// </returns>
    public EnumState Name { get; }
    
    /// <summary>
    /// Obtiene un string con un emoji relacionado al estado <c>Paralyze</c>.
    /// </summary>
    /// <returns>
    ///     <c>string</c> que incluye un emoji relacionado al estado.
    /// </returns>
    public string Emoji { get; } = "\ud83d\ude34";
    
    /// <summary>
    /// Atributo que contiene la mínima cantidad de turnos que puede estar dormido un pokemon.
    /// Valor 1 por defecto.
    /// </summary>
    private int minSleepTurns = 1;
    
    /// <summary>
    /// Atributo que contiene la máxima cantidad de turnos que puede estar dormido un pokemon.
    /// Valor 4 por defecto.
    /// </summary>
    private int maxSleepTurns = 4;
    
    /// <summary>
    /// Atributo que contiene los turnos restantes que va a estar dormido el pokemon.
    /// </summary>
    private int remainingTurns;
    
    /// <summary>
    /// Constructor de la clase <c>Sleep</c> que inicializa el nombre del estado como <c>EnumState.Sleep</c>.
    /// Además, establece de forma aleatoria la cantidad de turno que estará dormido el pokemon.
    /// </summary>
    public Sleep()
    {
        Name = EnumState.Sleep;
        SetRandomSleepTurns(minSleepTurns + 1, maxSleepTurns + 1);
    }

    /// <summary>
    /// Decrementa la cantidad de turnos restantes que el pokemon estará dormido.
    /// </summary>
    /// <param name="currentPokemon">
    /// El pokemon al que se le decrementará la cantidad de turnos restantes que estará dormido.
    /// </param>
    /// <remarks>
    /// Solo se decrementa la cantidad de turnos restantes si estos son mayor a cero, en caso contrario el pokemon volverá al estado <c>Normal</c>.
    /// </remarks>
    public void ApplyEffect(Pokemon currentPokemon)
    {
        if (remainingTurns > 0)
        {
            remainingTurns--;
        }
        else
        {
            currentPokemon.StateMachine.CurrentState = new Normal();
        }
    }

    /// <summary>
    /// Método que sirve para verificar si el pokemon puede atacar.
    /// </summary>
    /// <returns>
    ///     <para><c>true</c> si los turnos restantes que está dormido son cero.</para>
    ///     <para><c>false</c> si los turnos restantes que está dormido son distintos de cero.</para>
    /// </returns>
    public bool CanAttack()
    {
        return remainingTurns == 0;
    }

    /// <summary>
    /// Método que devuelve los turnos restantes que estará dormido el pokemon.
    /// </summary>
    public int GetRemainingTurnsWithEffect()
    {
        return remainingTurns;
    }
    
    /// <summary>
    /// Método que establece de forma aleatoria los turnos que estará dormido el pokemon.
    /// </summary>
    /// <param name="min">
    /// Valor minimo de turnos que puede estar dormido el pokemon.
    /// </param>
    /// <param name="max">
    /// Valor máximo de turnos que puede estar dormido el pokemon.
    /// </param>
    private void SetRandomSleepTurns(int min, int max)
    {
        remainingTurns = new Random().Next(min, max);
    }
}