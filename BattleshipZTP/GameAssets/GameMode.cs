using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.GameAssets
{
    public interface IGameMode
    {
        BattleBoard CreateBoard(int x , int y);
        bool RemeberArrowHit();
        Dictionary<string, int> AssignResources();

        List<IShip> ShipmentDelivery();
        List<(int x , int y)> GetShipmentPlacementCoords();
        List<int> GetShipSizes();
    }

    public class ClassicGameMode : IGameMode
    {
        readonly List<(int x, int y)> _coords = new List<(int x, int y)>();
        public ClassicGameMode()
        {
        }
        public BattleBoard CreateBoard(int x , int y) 
            => new BattleBoard(x,y,12,12);

        public bool RemeberArrowHit() 
            => true;

        public Dictionary<string, int> AssignResources() 
            => new Dictionary<string, int>();
        
        public List<IShip> ShipmentDelivery() => new List<IShip>()
        {
            ShipFactory.CreateShip(ShipType.Carrier),
            ShipFactory.CreateShip(ShipType.Battleship),
            ShipFactory.CreateShip(ShipType.Battleship),
            ShipFactory.CreateShip(ShipType.Destroyer),
            ShipFactory.CreateShip(ShipType.Destroyer),
            ShipFactory.CreateShip(ShipType.Destroyer),
            ShipFactory.CreateShip(ShipType.Submarine),
            ShipFactory.CreateShip(ShipType.Submarine)
        };
        public List<(int x, int y)> GetShipmentPlacementCoords()
        {
            return _coords;
        }

        public List<int> GetShipSizes()
        {
            return new List<int> { 5, 4, 3, 3, 1 };
        }
    }

    public class DuelGameMode : IGameMode
    {
        private ShipType _shipType;
        readonly (int x, int y) _coords;
        public List<(int x, int y)> GetShipmentPlacementCoords()
        {
            List<(int x,int y)> result = new List<(int x, int y)>() { 
                _coords
            };
            if(result.Count != 1)
            {
                throw new Exception("More shipment are not allowed for duels");
            }
            return result;
        }
        public DuelGameMode(ShipType type)
        { 
            _shipType = type;
        }
        public BattleBoard CreateBoard(int x , int y)
        {
            return new BattleBoard(x,y,5,5);
        }
        public bool RemeberArrowHit()
        {
            return false;
        }
        public Dictionary<string, int> AssignResources()
        {
            return null;
        }
        public List<IShip> ShipmentDelivery() => new List<IShip>()
        {
            ShipFactory.CreateShip(ShipType.Battleship)
            
        };
        public List<int> GetShipSizes()
        {
            return new List<int> { 3};
        }
    }


    public class WarhammerGameMode : IGameMode
    {
        string _path = "singleWarhammer_coords.txt";
        readonly List<(int x, int y)> _coords = new List<(int x, int y)>();
        public List<(int x, int y)> GetShipmentPlacementCoords()
        {
            return _coords;
        }
        public WarhammerGameMode() 
        {
            StreamReader reader = new StreamReader($"data/enemy_ai/{_path}");
            string line;
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                string[] bits = line.Split(' ');
                int x = int.Parse(bits[0]);
                int y = int.Parse(bits[1]);
                _coords.Add((x, y));
            }
        }
        public BattleBoard CreateBoard(int x , int y)
        {
            return new BattleBoard(x,y,90,16);
        }
        public bool RemeberArrowHit()
        {
            return false;
        }
        public Dictionary<string, int> AssignResources()
        {
            Dictionary<string,int> resources = new Dictionary<string,int>();
            resources.Add("Energy",200);
            resources.Add("Requisition", 1150);
            resources.Add("Action Points", 2);
            return resources;
        }
        public List<IShip> ShipmentDelivery() => new List<IShip>();
        
        public List<int> GetShipSizes()
        {
            return new List<int> { 3};
        }
    }


    public abstract class GameModeFactory
    {
        public abstract IGameMode GetGameMode();
    }

    public class ClassicModeFactory : GameModeFactory 
    {
        public override IGameMode GetGameMode()
        { 
            //set standart gamemode
            return new ClassicGameMode();
        }
    }
    public class DuelModeFactory : GameModeFactory
    {
        public override IGameMode GetGameMode()
        {
            //One ship 1v1
            return new DuelGameMode(ShipType.Battleship);
        }
    }
    public class WarhammerModeFactory : GameModeFactory
    {
        public override IGameMode GetGameMode()
        {
            //battleship40k
            return new WarhammerGameMode();
        }
    }
}
