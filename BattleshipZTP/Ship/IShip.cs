namespace BattleshipZTP;

public interface IShip
{
    string Name();
    List<(string text, int offset)> GetBody();
    void SetBody(List<(string text, int offset)> body);
    (ConsoleColor foreground, ConsoleColor background) GetColors();
    void Locate(List<(int x, int y)> coords);
    int Size { get; }

    int GetSize();
    HitResult TakeHit(Point coords , int damage=0);
    bool IsSunk();
}