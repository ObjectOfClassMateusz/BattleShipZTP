namespace BattleshipZTP;

public interface IShip
{
    List<(string, int)> GetBody();

    int GetSize(); // Poprawiono nazwę
    HitResult TakeHit(Point coords);
    bool IsSunk(); // Poprawiono nazwę
}