namespace Library;

public interface ICalculate
{ 
    bool CalculateDamage(Pokemon attacker, Pokemon defender, Move move, out int damage);
}