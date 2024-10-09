namespace Library;

public class Move
{
    private string name;
    private int power;
    public int Power
    {
        get { return power; }
    }
    private int accuracy;
    public Type type { get; }
    public bool isSpecialMove { get; }


    public string ViewMove()
    {
        string msg = $"Move: {name.ToUpper()} Power: {power} / Accuracy: {accuracy}";
        if (type != null)
        {
            msg += $" / Type: {type.Name}";
        }
        return msg;
    }
}