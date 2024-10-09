namespace Library;

public class Pokemon
{
    private string name;
    private int initialHp;
    private int hp;
    private int attack;
    public int AttackPoints { get { return attack; } }
    private int special_attack;
    public int SpecialAttackPoints { get { return special_attack; } }
    private int defense;
    public int DefensePoints { get { return defense; } }
    private int special_defense;
    public int SpecialDefensePoints { get { return special_defense; } }

    public List<Move> Moves;
    public List<Type> Types;

    public void Attack(Pokemon enemy, int moveSlot)
    {
        int dmg = Calculate.calculateDamage(this, enemy, Moves[moveSlot]);
        enemy.hp = (dmg > enemy.hp) ? 0 : enemy.hp - dmg;
    }

    public bool isDead()
    {
        return hp == 0;
    }
    
    public string viewMoves()
    {
        string movesMsg = "";
        foreach (var move in Moves)
        {
            movesMsg += move.ViewMove() + "\n";
        }
        return movesMsg;
    }

    public string viewPokemon()
    {
        string msg = $"{name.ToUpper()}   ({hp}/{initialHp})\n";  // Nose si la idea es que se muestren todas las stats aca
        msg += $"Attack: {attack}\n" +
               $"Special Attack: {special_attack}\n" +                                          
               $"Defense: {defense}\n" +
               $"Special Defense: {special_defense}\n";
        if (Types != null)
        {
            msg += $" / Types: \n";
            foreach (var type in Types)
            {
                msg += $"-{type.Name} \n";
            }
        }
        msg += viewMoves();                     // Nose si la idea es que se muestren todos los moves aca
        
    return msg;
    }
}