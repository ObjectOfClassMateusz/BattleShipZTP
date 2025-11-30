namespace BattleshipZTP;

public struct Point
{
    public int X { get; set; } // Kolumna (np. 1-10 lub A-J)
    public int Y { get; set; } // Wiersz (np. 1-10)

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}