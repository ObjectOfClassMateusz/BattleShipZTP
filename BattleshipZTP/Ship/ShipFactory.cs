using BattleshipZTP.Ship.DarkEldarShips;
using BattleshipZTP.Ship.SaxonyShips;

namespace BattleshipZTP;

public class ShipFactory
{
    static public IShip CreateShip(ShipType shipType)
    {
        var emptyPlacement = new List<Point>();
        
        return shipType switch
        {
            ShipType.Carrier => new CarrierShip(emptyPlacement),
            ShipType.Battleship => new BattleshipShip(emptyPlacement),
            ShipType.Destroyer => new DestroyerShip(emptyPlacement),
            ShipType.Submarine => new SubmarineShip(emptyPlacement),

            ShipType.Dr_JetBike => new ReaverJetBikeShip(emptyPlacement),
            ShipType.Dr_Raider => new RaiderShip(emptyPlacement),
            ShipType.Dr_Ravanger => new RavangerShip(emptyPlacement),
            ShipType.Dr_Dair => new DairOfDestructionShip(emptyPlacement),

            ShipType.Sax_Eisen => new EisenhansShip(emptyPlacement),


            _=> throw new ArgumentException("Invalid ship type")
        };
    }
}