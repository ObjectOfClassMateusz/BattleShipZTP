namespace BattleshipZTP;

public class DestroyerShip : BaseShip
{
    private const int DestroyerSize = 2;
    public DestroyerShip(List<Point> initialPlacement) 
        : base(DestroyerSize, initialPlacement)
    {
    }
}