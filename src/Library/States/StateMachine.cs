namespace Library.States;

/// <summary>
/// <para>Representa la máquina de estados que gestiona el estado actual de un pokemon.</para>
/// <para>Se utiliza el patrón de diseño <c>State</c>, la maquina de estados maneja el estado actual de cada <c>Pokemon</c>.</para>
/// <para>Dentro de la máquina de estados se encuentra un atributo que contiene el estado actual <c>CurrentState</c> donde guarda una instancia
/// de una clase que implemente la interfaz <c>IPokemonState</c>.</para>
/// <para>Dependiendo del estado que tenga el <c>Pokemon</c> va actuar de forma distinta al llamar a cualquier método de StateMachine.</para>
/// <para>Además aplica el principio OCP de SOLID, ya que en caso de querer agregar un nuevo estado, se debería crear una nueva clase que implemente <c>IPokemonState</c>,
/// agregar el nombre del estado en el <c>EnumState</c> y extender la clase <c>StateApplier</c> donde se agregan los estados a los pokemon. Esto permitiría agregar un efecto con un nuevo comportamiento sin modificar las clases pertinentes, extendiendo a las mismas.
/// Esto permite que el código sea más mantenible y extensible a futuro.</para>
/// </summary>
public class StateMachine
{
    /// <summary>
    /// Obtiene o establece el estado <c>IPokemonState</c> actual del pokemon.
    /// </summary>
    public IPokemonState CurrentState { get; set; }
    
    /// <summary>
    /// Constructor que inicializa una nueva instancia de la clase <c>StateMachine</c> con un estado inicial.
    /// </summary>
    /// <param name="initialState">El estado inicial del pokemon.</param>
    public StateMachine(IPokemonState initialState)
    {
        CurrentState = initialState;
    }
    
    /// <summary>
    /// Aplica el efecto del estado actual al pokemon.
    /// </summary>
    /// <param name="pokemon">El <c>Pokemon</c> al que se le aplicará el efecto del estado.</param>
    public void ApplyEffect(Pokemon pokemon)
    {
        CurrentState.ApplyEffect(pokemon);
    }
    
    /// <summary>
    /// Obtiene el número de turnos restantes en los que el efecto de estado seguirá activo.
    /// </summary>
    /// <returns><c>int</c> que contiene la cantidad de turnos restantes con el efecto activo.</returns>
    public int GetRemainingTurnsWithEffect()
    {
        return CurrentState.GetRemainingTurnsWithEffect();
    }

    /// <summary>
    /// Determina si el pokemon puede atacar dependiendo de su estado actual.
    /// </summary>
    /// <returns>
    ///     <para><c>true</c> si el pokemon puede atacar.</para>
    ///     <para>De lo contrario, <c>false</c>.</para>
    /// </returns>
    public bool CanAttack()
    {
        return CurrentState.CanAttack();
    }
    
    /// <summary>
    /// Indica si el pokemon ha perdido su turno debido al estado actual.
    /// </summary>
    /// <returns>
    ///     <para><c>true</c> si el pokemon ha perdido el turno.</para>
    ///     <para>De lo contrario, <c>false</c>.</para>
    /// </returns>
    public bool HasLostTurn()
    {
        return CurrentState.HasLostTurn();
    }
}