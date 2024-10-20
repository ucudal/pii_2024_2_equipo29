namespace Library;

public interface ICalculate
{ 
    int CalculateDamage(Pokemon attacker, Pokemon defender, Move move);
}