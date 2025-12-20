namespace BattleshipZTP;

public class BattleshipShip : BaseShip
{
    private const int BattleshipSize = 4;

    public BattleshipShip(List<Point> initialPlacement) 
        : base(BattleshipSize, initialPlacement)
    {
    }
}