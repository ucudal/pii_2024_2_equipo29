namespace Library.States;

public class Paralyze : State
{
    public Paralyze()
    {
        Name = "paralizar";
    }
    
    public override void Attack(Pokemon attacker, Pokemon enemy, int moveSlot)
    {
        if (CanAttack())
        {
            base.Attack(attacker, enemy, moveSlot);
        }
    }

    private bool CanAttack()
    {
        Random rand = new Random();
        return rand.Next(2) == 0;
    }
}