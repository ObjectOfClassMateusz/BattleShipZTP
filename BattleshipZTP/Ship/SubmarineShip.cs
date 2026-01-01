namespace BattleshipZTP;

public class SubmarineShip : BaseShip
{
    private const int BattleshipSize = 1;

    public SubmarineShip(List<Point> initialPlacement)
        : base(BattleshipSize, initialPlacement)
    {
        StreamReader file = new StreamReader($"data/ships/classic/submarine/model.txt");
        string reader = file.ReadLine();
        _body.Add((reader, 0));
    }
}