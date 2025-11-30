namespace BattleshipZTP;

public class CarrierShip : BaseShip
{
    private const int CarrierSize = 5;

    public CarrierShip(List<Point> initialPlacement) 
        : base(CarrierSize, initialPlacement)
    {
    }
}