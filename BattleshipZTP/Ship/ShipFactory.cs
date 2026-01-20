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
            ShipType.Sax_ship => new StormtroopersShip(emptyPlacement),
            ShipType.Sax_sdksGrim => new GrimbartShip(emptyPlacement),
            ShipType.Sax_sdksIse => new IsegrimShip(emptyPlacement),

            _ => throw new ArgumentException("Invalid ship type")
        };
    }

    static public IShip CreateShip(string shipType)
    {
        var emptyPlacement = new List<Point>();

        return shipType switch
        {
            "Carrier" => new CarrierShip(emptyPlacement),
            "Battleship" => new BattleshipShip(emptyPlacement),
            "Destroyer" => new DestroyerShip(emptyPlacement),
            "Submarine" => new SubmarineShip(emptyPlacement),

            "ReaverJetBike" => new ReaverJetBikeShip(emptyPlacement),
            "Raider" => new RaiderShip(emptyPlacement),
            "Ravanger" => new RavangerShip(emptyPlacement),
            "DairOfDestruction" => new DairOfDestructionShip(emptyPlacement),

            "Eisanhans" => new EisenhansShip(emptyPlacement),
            "Stormstrooper" => new StormtroopersShip(emptyPlacement),
            "Grimbart" => new GrimbartShip(emptyPlacement),
            "Isegrim" => new IsegrimShip(emptyPlacement),

            _ => throw new ArgumentException("Invalid ship type")
        };
    }
}