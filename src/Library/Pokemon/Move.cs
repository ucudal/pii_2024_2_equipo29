using Library.States;

namespace Library;

public class Move
{
    public string Name { get; set; }
    public int Accuracy { get; set; }
    public Type Type { get; set; }
    public int Power { get; set; }
    public bool IsSpecialMove { get; set; }
    public EnumEffect Effect { get; set; } = EnumEffect.Normal;

    public string ViewMove()
    {
        string msg = $"{(IsSpecialMove ? "Special move" : "Move")}: {Name.ToUpper()} Power: {Power} / Accuracy: {Accuracy}";
        if (Type != null)
        {
            msg += $" / Type: {Type.Name}";
        }
        msg += $" / Effect: {Effect}";
        return msg;
    }
}