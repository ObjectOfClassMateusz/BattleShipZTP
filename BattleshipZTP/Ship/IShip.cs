namespace BattleshipZTP;

public interface IShip
{ 
    int GetSize(); // Poprawiono nazwę
    HitResult TakeHit(Point coords);
    bool IsSunk(); // Poprawiono nazwę
}