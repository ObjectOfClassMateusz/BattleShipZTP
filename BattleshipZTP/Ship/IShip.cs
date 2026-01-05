namespace BattleshipZTP;

public interface IShip
{
    string Name();
    List<(string text, int offset)> GetBody();
    void SetBody(List<(string text, int offset)> body);
    (ConsoleColor foreground, ConsoleColor background) GetColors();
    void Locate(List<(int x, int y)> coords);

    int GetSize(); // Poprawiono nazwę
    HitResult TakeHit(Point coords);
    bool IsSunk(); // Poprawiono nazwę
}