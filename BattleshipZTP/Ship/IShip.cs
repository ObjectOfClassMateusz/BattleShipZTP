namespace BattleshipZTP;

public interface IShip
{
    List<(string text, int offset)> GetBody();
    (ConsoleColor foreground, ConsoleColor background) GetColors();

    int GetSize(); // Poprawiono nazwę
    HitResult TakeHit(Point coords);
    bool IsSunk(); // Poprawiono nazwę
}