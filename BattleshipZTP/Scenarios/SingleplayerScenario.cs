using BattleshipZTP.GameAssets;
using BattleshipZTP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Scenarios
{
    public class SingleplayerScenario : Scenario
    {
        private IGameMode _gameMode;
        public SingleplayerScenario(IGameMode gameMode)
        {
            _gameMode = gameMode;
        }
        public override void Act()
        {
            base.Act();

            BattleBoard board = _gameMode.createBoard(1, 20);
            BattleBoardProxy proxy = new BattleBoardProxy(board);
            proxy.FieldsInitialization();
            proxy.Display();

            BattleBoard enemy_board = _gameMode.createBoard(1, 1); 
            BattleBoardProxy enemy_proxy = new BattleBoardProxy(enemy_board);
            enemy_proxy.FieldsInitialization();
            enemy_proxy.Display();

            Env.CursorPos(1, 39);
            Env.SetColor(ConsoleColor.DarkMagenta, ConsoleColor.Gray);
            Console.Write(" Action Points ");
            Env.CursorPos(17, 39);
            Env.SetColor(ConsoleColor.DarkBlue, ConsoleColor.DarkCyan);
            Console.Write(" Requisition ");
            Env.CursorPos(31, 39);
            Env.SetColor(ConsoleColor.DarkGreen, ConsoleColor.Green);
            Console.Write(" Energy ");


            IShip ship = ShipFactory.CreateShip(ShipType.Destroyer);
            
            proxy.PlaceShip(ship);
            proxy.Display();
        }
    }
}
