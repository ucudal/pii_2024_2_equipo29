using Library.States;

namespace Library;
/// <summary>
/// Representa un movimiento que un Pokémon puede realizar.
/// Esta clase contiene información sobre el nombre, precisión, tipo, poder y efectos de un movimiento.
/// </summary>
public class Move
{
    /// <summary>
    /// Obtiene o establece el nombre del movimiento.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Obtiene o establece la precisión del movimiento (0-100).
    /// </summary>
    public int Accuracy { get; set; }
    
    /// <summary>
    /// Obtiene o establece el tipo del movimiento.
    /// </summary>
    public Type Type { get; set; }
    
    /// <summary>
    /// Obtiene o establece el poder del movimiento.
    /// </summary>
    public int Power { get; set; }
    
    /// <summary>
    /// Obtiene el tiempo de enfriamiento del movimiento especial
    /// </summary>
    public int CoolDownSpecialMove { get; } = 2;
    
    /// <summary>
    /// Obtiene o establece lso turnos restantes en enfriamiento
    /// </summary>
    public int RemainingTurnsInCoolDown = 0;
    
    /// <summary>
    /// Obtiene o establece un valor que indica si el movimiento es especial.
    /// </summary>
    public bool IsSpecialMove { get; set; }
    
    /// <summary>
    /// Obtiene o establece el estado del movimiento.
    /// </summary>
    public EnumState State { get; set; } = EnumState.Normal;
    
    /// <summary>
    /// Muestra la información del movimiento en formato de cadena.
    /// </summary>
    /// <returns>Una cadena que representa la información del movimiento.</returns>
    
    public string ViewMove()
    {
        string msg = $"{(IsSpecialMove ? "Special move" : "Move")}:** {Name.ToUpper()} Power: {Power} / Accuracy: {Accuracy}";
        if (Type != null)
        {
            msg += $" / Type: {Type.Name}";
        }
        msg += $" / Effect: {State}";
        return msg;
    }
}