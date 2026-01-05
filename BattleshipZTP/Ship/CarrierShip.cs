namespace BattleshipZTP;

public class CarrierShip : BaseShip
{
    private const int CarrierSize = 5;

    public CarrierShip(List<Point> initialPlacement) 
        : base(CarrierSize, initialPlacement)
    {
        StreamReader file = new StreamReader($"data/ships/classic/carrier/model.txt");
        string reader = file.ReadLine();
        _body.Add((reader, 0));
        _colors = (ConsoleColor.DarkMagenta , ConsoleColor.Black);
        _name = "Carrier";
    }
}