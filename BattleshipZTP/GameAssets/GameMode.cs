using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.GameAssets
{
    public interface IGameMode
    {
        BattleBoard createBoard(int x , int y);
        bool remeberArrowHit();
        Dictionary<string, int> assignResources();

        List<IShip> ShipmentDelivery();
    }

    public class ClassicGameMode : IGameMode
    {
        public BattleBoard createBoard(int x , int y) 
            => new BattleBoard(x,y,12,12);

        public bool remeberArrowHit() 
            => true;

        public Dictionary<string, int> assignResources() 
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
    }

    public class DuelGameMode : IGameMode
    {
        private ShipType _shipType;
        public DuelGameMode(ShipType type)
        { 
            _shipType = type;
        }
        public BattleBoard createBoard(int x , int y)
        {
            return new BattleBoard(x,y,12,12);
        }
        public bool remeberArrowHit()
        {
            return false;
        }
        public Dictionary<string, int> assignResources()
        {
            return null;
        }
        public List<IShip> ShipmentDelivery() => new List<IShip>()
        {
            ShipFactory.CreateShip(ShipType.Carrier)
        };
    }


    public class WarhammerGameMode : IGameMode
    {
        public WarhammerGameMode() { }
        public BattleBoard createBoard(int x , int y)
        {
            return new BattleBoard(x,y,90,16);
        }
        public bool remeberArrowHit()
        {
            return false;
        }
        public Dictionary<string, int> assignResources()
        {
            Dictionary<string,int> resources = new Dictionary<string,int>();
            resources.Add("Energy",200);
            resources.Add("Requisition", 1150);
            resources.Add("Action Points", 2);
            return resources;
        }
        public List<IShip> ShipmentDelivery() => new List<IShip>();
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
            return new DuelGameMode(ShipType.Carrier);
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
