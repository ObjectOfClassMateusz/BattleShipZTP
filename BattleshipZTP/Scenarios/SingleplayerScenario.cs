using BattleshipZTP.Commands;
using BattleshipZTP.GameAssets;
using BattleshipZTP.Settings;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using BattleshipZTP.Networking;
using BattleshipZTP.Observers;

namespace BattleshipZTP.Scenarios
{
    public class SingleplayerScenario : Scenario
    {
        IGameMode _gameMode;
        List<(int x, int y)> _enemyShipmentCoords = new List<(int x, int y)>();
        Window _windowShipmentList = new Window();

        public SingleplayerScenario(IGameMode gameMode)
        {
            _gameMode = gameMode;
        }

        void WriteNickNameOnConsole(int x , int y , string nickname)
        {
            Env.CursorPos(x, y);
            Console.Write(nickname);
        }

        void DisplayShipmentTable(int x , int y, List<IShip> ships)
        {
            Env.CursorPos(x-1, y);
            Console.WriteLine("Place the ships");
            Env.CursorPos(x, y + 1);
            Console.WriteLine("on the board");
            IWindowBuilder windowBuilder = new WindowBuilder();
            windowBuilder
               .SetPosition(x, y + 2)
               .ColorHighlights(ConsoleColor.Yellow, ConsoleColor.DarkMagenta)
               .ColorBorders(ConsoleColor.DarkBlue, ConsoleColor.DarkRed);

            for (int i = 0; i < ships.Count; i++)
            {
                windowBuilder.AddComponent(new TextOutput(ships[i].Name()));
            }
            _windowShipmentList = windowBuilder.Build();
        }

        void Initialize(BattleBoard.BattleBoardProxy board)
        {
            board.FieldsInitialization();
            board.Display();
        }

        void EnemyPlacementValidate(List<IShip> ships , List<(int x, int y)> coords)
        {
            if(ships.Count != coords.Count)
            {
                throw new Exception("Missing coords or shipment to execute the placement");
            }
        }

        public override void Act()
        {
            base.Act();

            WriteNickNameOnConsole(52, 7,UserSettings.Instance.Nickname);
            BattleBoard board = _gameMode.CreateBoard(52, 8);
            BattleBoard.BattleBoardProxy proxy = new BattleBoard.BattleBoardProxy(board);
            Initialize(proxy);

            WriteNickNameOnConsole(88, 7, "ai_enemy1");
            BattleBoard enemyBoard = _gameMode.CreateBoard(88, 8); 
            BattleBoard.BattleBoardProxy enemyProxy = new BattleBoard.BattleBoardProxy(enemyBoard);
            Initialize(enemyProxy);
            
            List<IShip> ships = _gameMode.ShipmentDelivery();
            (int x, int y) tablePos = (71, 7);
            DisplayShipmentTable(tablePos.x, tablePos.y, ships);
            UIController uI = new UIController();
            uI.AddWindow(_windowShipmentList);
            uI.DrawAndEndSequence();
            
            Drawing.SetColors(ConsoleColor.Black, ConsoleColor.Black);
            foreach (IShip ship in ships) 
            {
                uI.DrawAndEndSequence();
                PlaceCommand command = new PlaceCommand(board, ship , UserSettings.Instance.GetHashCode());
                var coords = proxy.PlaceCommand(command);
                command.Execute(coords);
                _windowShipmentList.Remove(0);
                Drawing.ClearRectangleArea(tablePos.x, tablePos.y + 2, 12, 10);
            }
            Drawing.ClearRectangleArea(tablePos.x-1, tablePos.y , 15, 12);
            Env.SetColor();

            ships = _gameMode.ShipmentDelivery();
            var enemyCoords = _gameMode.GetShipmentPlacementCoords();
            EnemyPlacementValidate(ships, enemyCoords);
            for (int i = 0; i < enemyCoords.Count; i++) 
            {
                var ship = ships[i];
                var cord = enemyCoords[i];
                PlaceCommand command = new PlaceCommand(enemyBoard, ship, "ai_enemy1".GetHashCode());
                var resultCoords = enemyProxy.PlaceShip(ship,cord.x,cord.y);
                command.Execute(resultCoords);
            }


            enemyProxy.Display();
            Console.ReadKey(true);

            // 11
            // 11

            // PlaceCommand enemyCommand


            //AI places the ships
            // 1.) PlaceCommand.
            // 2.) ExecuteNow()


            // enemy_proxy.PlaceShipAI(
            //    ShipFactory.CreateShip(ShipType.Carrier),
            //     0,0);
            // enemy_proxy.Display();



            /*IWindowBuilder windowBuilder = new WindowBuilder();
            windowBuilder.SetPosition(37, 2)
                .SetSize(20)
                .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
                .ColorHighlights(ConsoleColor.White, ConsoleColor.Green)
                .AddComponent(new TextOutput("Game backlogs"))
                .AddComponent(new Button("click me "));

            Window window = windowBuilder.Build();
            var controller = new UIController();
            controller.AddWindow(window);
            controller.DrawAndStart();

            window.Add(new TextOutput("Log:"));
            controller.DrawAndStart();
            window.Add(new TextOutput("Cos sie stało :D"));
            controller.DrawAndStart();
            window.Add(new TextOutput("Text3:"));
            controller.DrawAndStart();
            window.Add(new TextOutput("Text4:"));
            controller.DrawAndStart();
            window.Add(new TextOutput("Text5:"));
            controller.DrawAndStart();*/
            
            IWindowBuilder windowBuilder = new WindowBuilder();
            windowBuilder.SetPosition(2, 2)
                .SetSize(30)
                .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
                .ColorHighlights(ConsoleColor.White, ConsoleColor.Green)
                .AddComponent(new TextOutput("=== Game Backlogs ==="));

            Window backlogWindow = windowBuilder.Build();
            UIController logController = new UIController();
            logController.AddWindow(backlogWindow);
            
            GameLogger logger = new GameLogger(backlogWindow, logController);
            StatisticTracker stats = new StatisticTracker();

            ActionManager.Instance.Attach(logger);
            ActionManager.Instance.Attach(stats);

            SimpleAI ai = new SimpleAI();

            while (true)
            {
                // 1. Tura Gracza
                Point playerTarget = enemyProxy.ChooseAttackPoint();
                AttackCommand playerAttack = new AttackCommand(enemyProxy, playerTarget, UserSettings.Instance.GetHashCode());
                playerAttack.Execute(new List<(int x, int y)>());
                enemyProxy.Display();

                // 2. Tura AI
                Point aiTarget = ai.GetNextMove(board.width, board.height);

                HitResult aiResult = proxy.AttackPoint(aiTarget); 

                ai.ReportResult(aiTarget, aiResult);

                var aiDetails = new GameActionDetails {
                    PlayerID = "ai_enemy1".GetHashCode(),
                    ActionType = "Attack",
                    Coords = aiTarget,
                    Result = aiResult
                };
                ActionManager.Instance.LogAction(aiDetails);

                proxy.Display();
            }
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