using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.GameAssets
{
    public interface IGameMode
    {
        List<string> insertShips(string ships);
        BattleBoard createBoard();
        bool remeberArrowHit();
        (int, int) assignResources();
    }

    public class ClassicGameMode : IGameMode
    {
        public ClassicGameMode() { }
        public List<string> insertShips(string ships)
        {
            return new List<string>();
        }
        public BattleBoard createBoard() 
        { 
            return new BattleBoard();
        }
        public bool remeberArrowHit() 
        {
            return true;
        }
        public (int, int) assignResources() 
        {
            return (0, 0);
        }
    }

    public class DuelGameMode : IGameMode
    {
        public DuelGameMode() { }
        public List<string> insertShips(string ships)
        {
            return new List<string>();
        }
        public BattleBoard createBoard()
        {
            return new BattleBoard();
        }
        public bool remeberArrowHit()
        {
            return true;
        }
        public (int, int) assignResources()
        {
            return (0, 0);
        }
    }


    public class WarhammerGameMode : IGameMode
    {
        public WarhammerGameMode() { }
        public List<string> insertShips(string ships)
        {
            return new List<string>();
        }
        public BattleBoard createBoard()
        {
            return new BattleBoard();
        }
        public bool remeberArrowHit()
        {
            return true;
        }
        public (int, int) assignResources()
        {
            return (0, 0);
        }
    }


    public abstract class GameModeFactory
    {
        protected abstract IGameMode GetGameMode();
    }

    public class ClassicFactory : GameModeFactory 
    {
        protected override IGameMode GetGameMode()
        { 
            //set standart gamemode
            return new ClassicGameMode();
        }
    }
    public class DuelFactory : GameModeFactory
    {
        protected override IGameMode GetGameMode()
        {
            //One ship 1v1
            return new DuelGameMode();
        }
    }
    public class WarhammerFactory : GameModeFactory
    {
        protected override IGameMode GetGameMode()
        {
            //battleship40k
            return new WarhammerGameMode();
        }
    }
}
