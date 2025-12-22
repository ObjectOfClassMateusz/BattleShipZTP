using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace BattleshipZTP.GameAssets
{
    public interface IGameMode
    {
        BattleBoard createBoard(int x , int y);
        bool remeberArrowHit();
        Dictionary<string, int> assignResources();
    }

    public class ClassicGameMode : IGameMode
    {
        public ClassicGameMode() { }
        public BattleBoard createBoard(int x , int y) 
        { 
            return new BattleBoard(x,y,12,12);
        }
        public bool remeberArrowHit()
        {
            return true;
        }
        public Dictionary<string, int> assignResources() 
        {
            return null;
        }
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
