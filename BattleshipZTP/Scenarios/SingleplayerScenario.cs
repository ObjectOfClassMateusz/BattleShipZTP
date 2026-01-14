using BattleshipZTP.Commands;
using BattleshipZTP.GameAssets;
using BattleshipZTP.Settings;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using BattleshipZTP.Networking;
using BattleshipZTP.Observers;
using BattleshipZTP.Ship;

namespace BattleshipZTP.Scenarios
{
    public class SingleplayerScenario : Scenario
    {
        IGameMode _gameMode;
        IScenario _mainScenario;
        IAI _ai;
        Window _windowShipmentList = new Window();
        int numberOfShipsPerPlayer = 0;

        public SingleplayerScenario(IGameMode gameMode, AIDifficulty difficulty,IScenario mainmenu)
        {
            _mainScenario = mainmenu;
            _gameMode = gameMode;
            _ai = difficulty switch
            {
                AIDifficulty.Easy => new SimpleAI(),
                AIDifficulty.Medium => new MediumAI(),
                AIDifficulty.Hard => new HardAI(),
                _ => new SimpleAI()
            };
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
        
        private bool CanPlaceAt(BattleBoard.BattleBoardProxy proxy, IShip ship, int x, int y)
        {
            var body = ship.GetBody();
            int currentY = y;
            foreach (var line in body)
            {
                for (int i = 0; i < line.text.Length; i++)
                {
                    int checkX = x + line.offset + i;
                    int checkY = currentY;

                    if (checkX >= proxy.Width || checkY >= proxy.Height) return false;

                    var field = proxy.GetField(checkX, checkY);
                    if (field == null || field.ShipReference != null || proxy.IsNeighborHaveShipRef(field)) 
                        return false;
                }
                currentY++;
            }
            return true;
        }

        private void PlaceShipsRandomly(BattleBoard.BattleBoardProxy enemyProxy, List<IShip> ships)
        {
            Random rand = new Random();
            foreach (var ship in ships)
            {
                bool placed = false;
                while (!placed)
                {
                    int x = rand.Next(0, enemyProxy.Width);
                    int y = rand.Next(0, enemyProxy.Height);
    
                    if (rand.Next(2) == 0 && _gameMode.RemeberArrowHit()) 
                    {
                        ship.SetBody(BattleBoard.RotateBody(ship.GetBody())); 
                    }
            
                    try 
                    {
                        PlaceCommand cmd = new PlaceCommand(enemyProxy.GetBoard(), ship, "AI_ENEMY".GetHashCode());
        
                        if (CanPlaceAt(enemyProxy, ship, x, y)) 
                        {
                            var resultCoords = enemyProxy.PlaceShip(ship, x, y);
                            cmd.Execute(resultCoords);
                            placed = true;
                        }
                    }
                    catch { /* Ponowna próba losowania */ }
                }
            }
        }
        
        public override void Act()
        {
            if (UserSettings.Instance.MusicEnabled)
            {
                AudioManager.Instance.Stop("2-02 - Dark Calculation");
                AudioManager.Instance.ChangeVolume(_gameMode.GameThemeAudio(), UserSettings.Instance.MusicVolume);
                AudioManager.Instance.Play(_gameMode.GameThemeAudio(), true);
            }
            base.Act();
            var boardCoords = _gameMode.BoardCoords();


            WriteNickNameOnConsole(boardCoords.XAxis_Player1, boardCoords.YAxis_Player1,UserSettings.Instance.Nickname);
            BattleBoard board = _gameMode.CreateBoard(boardCoords.XAxis_Player1, boardCoords.YAxis_Player1 + 1);
            BattleBoard.BattleBoardProxy proxy = new BattleBoard.BattleBoardProxy(board);
            Initialize(proxy);

            WriteNickNameOnConsole(boardCoords.XAxis_Player2, boardCoords.YAxis_Player2, "AI_ENEMY");
            BattleBoard enemyBoard = _gameMode.CreateBoard(boardCoords.XAxis_Player2, boardCoords.YAxis_Player2 + 1); 
            BattleBoard.BattleBoardProxy enemyProxy = new BattleBoard.BattleBoardProxy(enemyBoard);
            Initialize(enemyProxy);

            List<IShip> ships = _gameMode.ShipmentDelivery();
            numberOfShipsPerPlayer = ships.Count;
            if (_gameMode.RemeberArrowHit())
            {
                BeautifyHelper.ApplyFancyBodies(ships);
            }
            else
            {
                  Env.CursorPos(1, 40);
                  Env.SetColor(ConsoleColor.DarkMagenta, ConsoleColor.Gray);
                  Console.Write(" Action Points ");
                  Env.CursorPos(27, 40);
                  Env.SetColor(ConsoleColor.DarkBlue, ConsoleColor.DarkCyan);
                  Console.Write(" Requisition ");
                  Env.CursorPos(51, 40);
                  Env.SetColor(ConsoleColor.DarkGreen, ConsoleColor.Green);
                  Console.Write(" Energy ");
                  Env.SetColor();
            }

            (int x, int y) tablePos = _gameMode.RemeberArrowHit() ? (71, 7) : (96, 22);
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
                Drawing.DrawRectangleArea(tablePos.x, tablePos.y + 2, _windowShipmentList.Width , _windowShipmentList.Height);
                _windowShipmentList.Remove(0);
            }
            Drawing.DrawRectangleArea(tablePos.x-1, tablePos.y , _windowShipmentList.Width+4, _windowShipmentList.Height+3);
            Env.SetColor();
            
            List<IShip> enemyShips = _gameMode.ShipmentDelivery();
            if (_gameMode.RemeberArrowHit())
            {
                BeautifyHelper.ApplyFancyBodies(enemyShips);
            }
            PlaceShipsRandomly(enemyProxy, enemyShips);

            Drawing.SetColors(ConsoleColor.Black,ConsoleColor.Black);
            Drawing.DrawRectangleArea(
                boardCoords.XAxis_Player2+1, 
                boardCoords.YAxis_Player2+2,
                proxy.Width,proxy.Height
            );
            enemyProxy.Display();//delete me soon
            BattleBoardMemento playerMemento = board.GetSaveState();
            BattleBoardMemento enemyMemento = enemyBoard.GetSaveState();

            IWindowBuilder windowBuilder = new WindowBuilder();
            if(_gameMode.RemeberArrowHit())
            {
                windowBuilder.SetPosition(2, 2);
            }
            else
            {
                windowBuilder.SetPosition(96, 2);
            }
            windowBuilder.SetSize(30)
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

            if (!_gameMode.RemeberArrowHit())
            {
                Gameplay40kHandle(proxy,enemyProxy,ships ,enemyShips);
                return;
            }
            
            int totalShipsToSink = numberOfShipsPerPlayer; 
            int playerSunkCounter = 0;
            int aiSunkCounter = 0;
            bool nextTurn = true;
            bool victory = false;

            while (!victory)
            {
                // --- TURA GRACZA ---
                while (nextTurn == true)
                {
                    Point playerTarget = enemyProxy.ChooseAttackPoint();
                    AttackCommand playerAttack = new AttackCommand(enemyProxy, playerTarget, UserSettings.Instance.GetHashCode());
                    playerAttack.Execute(new List<(int x, int y)>());
                    
                    var fieldAtTarget = enemyProxy.GetField(playerTarget.X, playerTarget.Y);
                    
                    if (fieldAtTarget.ShipReference != null) 
                    {
                        HitResult playerResult = fieldAtTarget.ShipReference.IsSunk() ? HitResult.HitAndSunk : HitResult.Hit;
                        if (fieldAtTarget.ShipReference.IsSunk()) playerSunkCounter++;
                    }
                    else 
                    {
                        nextTurn = false;
                    }
                    if (playerSunkCounter >= totalShipsToSink || aiSunkCounter >= totalShipsToSink) 
                    {
                        victory = true;
                        
                        BattleBoard rawPlayer = new BattleBoard(52, 8, board.width, board.height);
                        BattleBoard rawEnemy = new BattleBoard(88, 8, enemyBoard.width, enemyBoard.height);
    
                        rawPlayer.FieldsInitialization();
                        rawEnemy.FieldsInitialization();

                        rawPlayer.Restore(playerMemento); 
                        rawEnemy.Restore(enemyMemento);

                        var replayPlayerProxy = new BattleBoard.BattleBoardProxy(rawPlayer);
                        var replayEnemyProxy = new BattleBoard.BattleBoardProxy(rawEnemy);

                        Env.Wait(900);
    
                        string winnerName = (playerSunkCounter >= totalShipsToSink) ? UserSettings.Instance.Nickname : "AI_ENEMY";
                        int winnerId = (playerSunkCounter >= totalShipsToSink) ? UserSettings.Instance.GetHashCode() : "AI_ENEMY".GetHashCode();

                        var victoryScen = new VictoryScenario(winnerName, winnerId, stats, replayPlayerProxy, replayEnemyProxy, board.height, board.width);
    
                        victoryScen.ConnectScenario("Main", _mainScenario);
                        
                        ActionManager.Instance.Detach(stats);
                        ActionManager.Instance.Detach(logger);
                        Env.SetColor();
                        victoryScen.Act();
                        break;
                    }               
                }
                if (victory) break;
                // --- TURA AI ---
                while (nextTurn == false)
                {
                    Env.Wait(900);
    
                    Point aiTarget = _ai.GetNextMove(board.width, board.height, board);
                    AttackCommand aiAttack = new AttackCommand(proxy, aiTarget, "AI_ENEMY".GetHashCode());
    
                    aiAttack.Execute(new List<(int x, int y)>());
    
                    var aiField = proxy.GetField(aiTarget.X, aiTarget.Y);
                    HitResult aiResult = HitResult.Miss;

                    if (aiField != null && aiField.ShipReference != null)
                    {
                        aiResult = aiField.ShipReference.IsSunk() ? HitResult.HitAndSunk : HitResult.Hit;

                        if (aiResult == HitResult.HitAndSunk)
                        {
                            if (UserSettings.Instance.SfxEnabled == true)
                            {
                                AudioManager.Instance.Play("trafiony zatopiony");
                            }
                            _ai.ClearTargets();
                            aiSunkCounter++;
                        }
                        else if (aiResult == HitResult.Hit )
                        {
                            if (UserSettings.Instance.SfxEnabled == true)
                            {
                                AudioManager.Instance.Play("trafienie");
                            }
                            _ai.AddTargetNeighbors(aiTarget, board.width, board.height);
                        }
                        else if (aiResult == HitResult.Miss && UserSettings.Instance.SfxEnabled == true)
                        {
                            AudioManager.Instance.Play("miss");
                        }
                    }
                    else
                    {
                        nextTurn = true; 
                    }

                    proxy.Display();
                    if (playerSunkCounter >= totalShipsToSink || aiSunkCounter >= totalShipsToSink) 
                    {
                        victory = true;
                        
                        BattleBoard rawPlayer = new BattleBoard(52, 8, board.width, board.height);
                        BattleBoard rawEnemy = new BattleBoard(88, 8, enemyBoard.width, enemyBoard.height);
    
                        rawPlayer.FieldsInitialization();
                        rawEnemy.FieldsInitialization();

                        rawPlayer.Restore(playerMemento); 
                        rawEnemy.Restore(enemyMemento);

                        var replayPlayerProxy = new BattleBoard.BattleBoardProxy(rawPlayer);
                        var replayEnemyProxy = new BattleBoard.BattleBoardProxy(rawEnemy);

                        Env.Wait(900);
    
                        string winnerName = (playerSunkCounter >= totalShipsToSink) ? UserSettings.Instance.Nickname : "AI_ENEMY";
                        int winnerId = (playerSunkCounter >= totalShipsToSink) ? UserSettings.Instance.GetHashCode() : "AI_ENEMY".GetHashCode();

                        var victoryScen = new VictoryScenario(winnerName, winnerId, stats, replayPlayerProxy, replayEnemyProxy, board.height, board.width);

                        victoryScen.ConnectScenario("Main", _mainScenario);

                        ActionManager.Instance.Detach(stats);
                        ActionManager.Instance.Detach(logger);
                        Env.SetColor();
                        victoryScen.Act();
                        break;
                    }
                }
            }
        }

        void GetAdvancedShipFromOption(List<Advanced40KShip> ships)
        {
            IWindowBuilder builder = new WindowBuilder();
            builder.SetPosition(96, 25)
                .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
                .ColorHighlights(ConsoleColor.DarkGreen, ConsoleColor.Green);

            for (int i= 0; i < ships.Count; i++)// 0...n
            {
                builder.AddComponent(new MaskedButton(ships[i].Name(),i.ToString()));
            }
            Window window = builder.Build();
            builder.ResetBuilder();

            UIController controller = new UIController();
            controller.AddWindow(window);
            List<string> option = controller.DrawAndStart();

            Console.WriteLine(option.FirstOrDefault());

            Console.ReadKey(true);

            List<string> option2 = controller.DrawAndStart();

            Console.WriteLine(option2.FirstOrDefault());
        }

        void Gameplay40kHandle(IBattleBoard board1 , IBattleBoard enemyboard,
            List<IShip> ships , List<IShip> enemyships)
        {
            bool victory = false;
            bool nextTurn = true;

            List<Advanced40KShip> advanced40KShips = new List<Advanced40KShip> ();
            foreach (var shipItem in ships) 
            {
                advanced40KShips.Add((Advanced40KShip)shipItem);
            }
            GetAdvancedShipFromOption(advanced40KShips);




            while (!victory)
            {
                
            }
        }
    }
    
}


