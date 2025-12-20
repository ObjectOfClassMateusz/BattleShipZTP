namespace BattleshipZTP;

public class ShipFactory
{
    static public IShip CreateShip(ShipType shipType)
    {
        var emptyPlacement = new List<Point>(); // Tymczasowe puste rozmieszczenie statku
        
        return shipType switch
        {
            ShipType.Carrier => new CarrierShip(emptyPlacement),
            ShipType.Battleship => new BattleshipShip(emptyPlacement),
            ShipType.Destroyer => new DestroyerShip(emptyPlacement),
            _=> throw new ArgumentException("Invalid ship type"), // _=> dla wszystkich innych przypadków 
        };
    }
}