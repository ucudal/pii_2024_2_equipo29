namespace Library;

public class Move
{
    private string name;
    private int accuracy;
    public Type Type { get; }
    public bool IsSpecialMove { get; }
    public int Power { get; }
    public string Effect = "normal";

    public string ViewMove()
    {
        string msg = $"Move: {name.ToUpper()} Power: {Power} / Accuracy: {accuracy}";
        if (Type != null)
        {
            msg += $" / Type: {Type.Name}";
        }
        return msg;
    }
}