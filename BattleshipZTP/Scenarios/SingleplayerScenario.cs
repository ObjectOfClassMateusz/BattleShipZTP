using BattleshipZTP.GameAssets;
using BattleshipZTP.Settings;
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

            Env.CursorPos(2, 1);
            Console.WriteLine(UserSettings.Instance.Nickname);

            BattleBoard board = _gameMode.createBoard(2, 2);
            BattleBoardProxy proxy = new BattleBoardProxy(board);
            proxy.FieldsInitialization();
            proxy.Display();

            Env.CursorPos(20, 1);
            Console.WriteLine("ai_enemy1");
            BattleBoard enemy_board = _gameMode.createBoard(20, 2); 
            BattleBoardProxy enemy_proxy = new BattleBoardProxy(enemy_board);
            enemy_proxy.FieldsInitialization();
            enemy_proxy.Display();

            List<IShip> ships = new List<IShip>()
            {
                ShipFactory.CreateShip(ShipType.Carrier),
                ShipFactory.CreateShip(ShipType.Battleship),
                ShipFactory.CreateShip(ShipType.Destroyer),
                ShipFactory.CreateShip(ShipType.Destroyer),
                ShipFactory.CreateShip(ShipType.Submarine)
            };


            foreach (IShip sh in ships) { 
                proxy.PlaceShip(sh);
            }
            proxy.Display();
        }
    }
}














/*Env.CursorPos(1, 39);
      Env.SetColor(ConsoleColor.DarkMagenta, ConsoleColor.Gray);
      Console.Write(" Action Points ");
      Env.CursorPos(17, 39);
      Env.SetColor(ConsoleColor.DarkBlue, ConsoleColor.DarkCyan);
      Console.Write(" Requisition ");
      Env.CursorPos(31, 39);
      Env.SetColor(ConsoleColor.DarkGreen, ConsoleColor.Green);
      Console.Write(" Energy ");*/