namespace BattleshipZTP;

public class BattleshipShip : BaseShip
{
    private const int BattleshipSize = 4;

    public BattleshipShip(List<Point> initialPlacement) 
        : base(BattleshipSize, initialPlacement)
    {
        StreamReader file = new StreamReader($"data/ships/classic/battleship/model.txt");
        string reader = file.ReadLine();
        _body.Add((reader, 0));
        _name = "Battleship";
        _colors = (ConsoleColor.Blue,ConsoleColor.DarkGray);
    }
}