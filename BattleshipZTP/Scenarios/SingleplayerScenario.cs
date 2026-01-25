using BattleshipZTP.Commands;
using BattleshipZTP.GameAssets;
using BattleshipZTP.Networking;
using BattleshipZTP.Observers;
using BattleshipZTP.Settings;
using BattleshipZTP.Ship;
using BattleshipZTP.Ship.Turrets;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using NAudio.Codecs;
using System.Collections.Generic;

namespace BattleshipZTP.Scenarios
{
    public class SingleplayerScenario : Scenario
    {
        IGameMode _gameMode;
        IScenario _mainScenario;
        IAI _ai;
        Window _windowShipmentList = new Window();
        int numberOfShipsPerPlayer = 0;
        private string _aiDifficultyName;

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
            _aiDifficultyName = difficulty.ToString();
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
                        PlaceCommand cmd = new PlaceCommand(enemyProxy.GetBattleBoard(), ship, "AI_ENEMY".GetHashCode());
        
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
            ActionManager.Instance.ClearObservers();
            if (UserSettings.Instance.MusicEnabled)
            {
                AudioManager.Instance.Stop("2-02 - Dark Calculation");
                AudioManager.Instance.ChangeVolume(_gameMode.GameThemeAudio(), UserSettings.Instance.MusicVolume);
                AudioManager.Instance.Play(_gameMode.GameThemeAudio(), true);
            }
            base.Act();
            CoordsToDrawBoard boardCoords = _gameMode.BoardCoords();
            
            string name = $"AI_1 ({_aiDifficultyName})";

            WriteNickNameOnConsole(boardCoords.XAxis_Player1, boardCoords.YAxis_Player1,UserSettings.Instance.Nickname);
            BattleBoard board = _gameMode.CreateBoard(boardCoords.XAxis_Player1, boardCoords.YAxis_Player1 + 1);
            BattleBoard.BattleBoardProxy proxy = new BattleBoard.BattleBoardProxy(board);
            Initialize(proxy);

            WriteNickNameOnConsole(boardCoords.XAxis_Player2, boardCoords.YAxis_Player2, name);
            BattleBoard enemyBoard = _gameMode.CreateBoard(boardCoords.XAxis_Player2, boardCoords.YAxis_Player2 + 1); 
            BattleBoard.BattleBoardProxy enemyProxy = new BattleBoard.BattleBoardProxy(enemyBoard);
            Initialize(enemyProxy);

            List<IShip> ships = _gameMode.ShipmentDelivery();
            numberOfShipsPerPlayer = ships.Count;
            if (_gameMode.RemeberArrowHit())
            {
                BeautifyHelper.ApplyFancyBodies(ships);
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
                var coords = proxy.PutCommand(command);
                command.Execute(coords);
                Drawing.DrawRectangleArea(tablePos.x, tablePos.y + 2, _windowShipmentList.Width , _windowShipmentList.Height);
                _windowShipmentList.Remove(0);
            }
            Drawing.DrawRectangleArea(tablePos.x-1, tablePos.y , _windowShipmentList.Width+6, _windowShipmentList.Height+3);
            Env.SetColor();
            
            List<IShip> enemyShips = _gameMode.ShipmentDelivery(true);
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
                Gameplay40kHandle(proxy,enemyProxy,ships ,enemyShips,logger , stats);
                return;
                ////////////////////////////////////////////////////////////////////////////
           }
            
            int totalShipsToSink = numberOfShipsPerPlayer; 
            int playerSunkCounter = 0;
            int aiSunkCounter = 0;
            bool nextTurn = true;
            bool victory = false;

            while (victory == false)
            {
                // --- TURA GRACZA ---
                while (nextTurn == true)
                {
                    Point playerTarget = enemyProxy.ChooseAttackPoint();

                    AttackCommand playerAttack = new AttackCommand(enemyProxy, playerTarget, UserSettings.Instance.GetHashCode(),UserSettings.Instance.Nickname);
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
                        
                        Env.SetColor();
                        ActionManager.Instance.Detach(stats);
                        ActionManager.Instance.Detach(logger);
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
                    AttackCommand aiAttack = new AttackCommand(proxy, aiTarget, _aiDifficultyName.GetHashCode(),_aiDifficultyName);
    
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
    
                        string winnerName = (playerSunkCounter >= totalShipsToSink) ? UserSettings.Instance.Nickname : _aiDifficultyName;
                        int winnerId = (playerSunkCounter >= totalShipsToSink) ? UserSettings.Instance.GetHashCode() : _aiDifficultyName.GetHashCode();

                        var victoryScen = new VictoryScenario(winnerName, winnerId, stats, replayPlayerProxy, replayEnemyProxy, board.height, board.width);

                        victoryScen.ConnectScenario("Main", _mainScenario);
                        
                        Env.SetColor();
                        ActionManager.Instance.Detach(stats);
                        ActionManager.Instance.Detach(logger);
                        victoryScen.Act();
                        break;
                    }
                }
            }
        }

        void RenewAllTurrets(List<Advanced40KShip> advanced40KShips)
        {
            for (int i = 0; i < advanced40KShips.Count; i++) 
            {
                foreach (var turret in advanced40KShips[i].GetTurrets()) 
                {
                    turret.Renew();
                }
            }
        }
        string GetActionFromAdvancedShip(Advanced40KShip ship)
        {
            Env.CursorPos(96, 24);
            ship.ShowHealthBar();
            ship.AudioPlayReady();
            IWindowBuilder builder = new WindowBuilder();
            builder.SetPosition(96, 25)
                .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
                .ColorHighlights(ConsoleColor.DarkGreen, ConsoleColor.Green);
            int turretsCount = ship.GetTurrets().Count;
            for (int i = 0; i < turretsCount; i++) 
            {
                var turret = ship.GetTurret(i);
                if (turret.IsReady())
                {
                    builder.AddComponent(new MaskedButton(turret.GetName(), i.ToString()));
                }
            }
            builder.AddComponent(new Button("Move"));
            builder.AddComponent(new Button("Return"));
            Window window = builder.Build();
            builder.ResetBuilder();

            UIController controller = new UIController();
            controller.AddWindow(window);
            return controller.DrawAndStart().FirstOrDefault();
        }
        string SelectAdvancedShipOrAction(List<Advanced40KShip> ships)
        {
            IWindowBuilder builder = new WindowBuilder();
            builder.SetPosition(96, 25)
                .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
                .ColorHighlights(ConsoleColor.DarkGreen, ConsoleColor.Green);

            for (int i= 0; i < ships.Count; i++)// 0...n
            {
                if (ships[i].IsSunk())
                {
                    continue;
                }
                builder.AddComponent(new MaskedButton(ships[i].Name(),i.ToString()));
            }
            builder.AddComponent(new Button("Buy Shipment"));
            builder.AddComponent(new Button("End Turn"));
            Window window = builder.Build();
            builder.ResetBuilder();

            UIController controller = new UIController();
            controller.AddWindow(window);
            string option = controller.DrawAndStart().FirstOrDefault();
            Drawing.SetColors(ConsoleColor.Black,ConsoleColor.Black);
            Drawing.DrawRectangleArea(96,22,window.Width+11,window.Height+5);
            return option;
        }

        void Gameplay40kHandle(BattleBoard.BattleBoardProxy proxy , BattleBoard.BattleBoardProxy enemyProxy,
        List<IShip> ships , List<IShip> enemyships , GameLogger logger, StatisticTracker stats)
        {
            List<Advanced40KShip> advanced40KShips = new List<Advanced40KShip> ();
            foreach (var shipItem in ships) 
            {
                advanced40KShips.Add((Advanced40KShip)shipItem);
            }
            List<Advanced40KShip> enemyAdvanced40KShips = new List<Advanced40KShip>();
            foreach (var shipItem in enemyships)
            {
                enemyAdvanced40KShips.Add((Advanced40KShip)shipItem);
            }
            BattleBoardMemento playerMemento = proxy.GetBattleBoard().GetSaveState();
            BattleBoardMemento enemyMemento = enemyProxy.GetBattleBoard().GetSaveState();

            int totalShipsToSink = numberOfShipsPerPlayer;

            int playerSunkCounter = 0;
            int playerShipsToSink = numberOfShipsPerPlayer;
            int enemySunkCounter = 0;
            int enemyShipsToSink = numberOfShipsPerPlayer;

            bool victory = false;
            bool nextTurn = true;
            Dictionary<string, int> playerWallet = _gameMode.AssignResources();
            int actionPointIncrease = playerWallet["Action Points"];

            while (!victory)
            {
                while (nextTurn)
                {
                    //enemyProxy.Display();
                    Env.CursorPos(1, 40);
                    Env.SetColor(ConsoleColor.DarkMagenta, ConsoleColor.Gray);
                    Console.Write($" Action Points {playerWallet["Action Points"]} ");
                    Env.CursorPos(27, 40);
                    Env.SetColor(ConsoleColor.DarkBlue, ConsoleColor.DarkCyan);
                    Console.Write($" Requisition {playerWallet["Requisition"]} ");
                    Env.CursorPos(51, 40);
                    Env.SetColor(ConsoleColor.Green, ConsoleColor.DarkGreen);
                    Console.Write($" Energy {playerWallet["Energy"]} ");
                    Env.SetColor();
                    string selected = SelectAdvancedShipOrAction(advanced40KShips);
                    if(selected == "End Turn")// Gracz dobrowolnie kończy turę
                    {
                        nextTurn = false; 
                        actionPointIncrease++;
                        playerWallet["Action Points"] = actionPointIncrease;
                        Random random = new Random();
                        playerWallet["Energy"] += random.Next(15,89);
                        playerWallet["Requisition"] += random.Next(9, 49);
                        RenewAllTurrets(advanced40KShips);
                        //Koniec tury
                        break;
                    }
                    else if (selected == "Buy Shipment")
                    {
                        if (playerWallet["Action Points"] == 0)
                        {
                            AudioManager.Instance.Play("wrong");
                            continue;
                        }
                        //
                        List<IShip> reinforcement = _gameMode.BuyShip(playerWallet);
                        if (reinforcement != null && reinforcement.Count > 0)
                        {
                            playerWallet["Action Points"] -= 1;
                            foreach (var s in reinforcement)
                            {
                                PlaceCommand cmd = new PlaceCommand(proxy.GetBattleBoard(), s, UserSettings.Instance.GetHashCode());
                                var placeCoords = proxy.PutCommand(cmd);
                                cmd.Execute(placeCoords);
                            }
                            advanced40KShips.Add((Advanced40KShip)reinforcement.FirstOrDefault());
                            playerShipsToSink++;
                        }
                        continue; // Wraca na początek wyboru
                    }
                    int finalIndex = Convert.ToInt32(selected);
                    Advanced40KShip ship = advanced40KShips[finalIndex];
                    string action = GetActionFromAdvancedShip(ship);
                    Drawing.SetColors(ConsoleColor.Black, ConsoleColor.Black);
                    Drawing.DrawRectangleArea(96, 24, 32, 10);
                    if (action == "Move")
                    {
                        Drawing.SetColors(ConsoleColor.Black, ConsoleColor.Black);
                        Drawing.DrawRectangleArea(96, 24, 32, 12);
                        int moveCost = 2;
                        if (playerWallet["Action Points"] >= moveCost)
                        {
                            proxy.Display();

                            MoveCommand moveCmd = new MoveCommand(
                                proxy.GetBattleBoard(),
                                ship,
                                UserSettings.Instance.GetHashCode(),
                                UserSettings.Instance.Nickname
                            );
                            var newCoords = proxy.PutCommand(moveCmd, false);

                            if (newCoords != null && newCoords.Count > 0)
                            {
                                moveCmd.Execute(newCoords);
                                playerWallet["Action Points"] -= moveCost;
                                proxy.Display();
                            }
                            proxy.Display();
                            continue;
                        }
                        else
                        {
                            if (UserSettings.Instance.SfxEnabled)
                            { AudioManager.Instance.Play("wrong"); }
                        }
                        continue;
                    }
                    else if (action == "Return")
                    {
                        continue;
                    }
                    int turretIndex = Convert.ToInt32(action);
                    ITurret turretReference = ship.GetTurret(turretIndex);

                    var actionCost = turretReference.ActionCost();
                    if (actionCost <= playerWallet["Action Points"])
                    {
                        TurretAttackCommand command = new TurretAttackCommand(
                            turretReference,
                            enemyProxy.GetBattleBoard(),
                            UserSettings.Instance.GetHashCode(),
                            UserSettings.Instance.Nickname
                        );
                        var coords = enemyProxy.PutCommand(command, true);
                        ship.AudioPlayAttack();
                        Env.Wait(900);
                        command.Execute(coords);
                        playerWallet["Action Points"] -= actionCost;
                        turretReference.Use();
                        foreach (var c in coords)
                        {
                            var localX = c.x - enemyProxy.GetBattleBoard().cornerX - 1;
                            var localY = c.y - enemyProxy.GetBattleBoard().cornerY - 1;
                            var searachShip = (Advanced40KShip)enemyProxy.GetField(localX, localY).ShipReference;

                            if (searachShip != null && searachShip.IsSunk() && enemyAdvanced40KShips.Contains(searachShip))
                            {
                                enemyAdvanced40KShips.Remove(searachShip);
                                playerWallet["Energy"] += 270;
                                playerWallet["Requisition"] += 100;
                            }
                        }
                        if (enemyAdvanced40KShips.Count == 0)
                        {
                            Env.Wait(1100);
                            victory = true;
                            nextTurn = false;
                            break;
                        }
                        continue;
                    }
                    else
                    {
                        if (UserSettings.Instance.SfxEnabled) 
                        { AudioManager.Instance.Play("wrong"); }
                        continue;
                    }
                }
                if (victory) 
                    break;
                // --- TURA AI ---
                while (nextTurn == false)
                {
                    Env.Wait(900);
                    
                    Point aiTarget = _ai.GetNextMove(proxy.GetBattleBoard().width, proxy.GetBattleBoard().height, proxy.GetBattleBoard());
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
                            enemySunkCounter++;
                        }
                        else if (aiResult == HitResult.Hit)
                        {
                            if (UserSettings.Instance.SfxEnabled == true)
                            {
                                AudioManager.Instance.Play("trafienie");
                            }
                            _ai.AddTargetNeighbors(aiTarget, proxy.GetBattleBoard().width, proxy.GetBattleBoard().height);
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
                    if (playerSunkCounter >= playerShipsToSink || enemySunkCounter >= enemyShipsToSink)
                    {
                        victory = true;

                        BattleBoard lrawPlayer = new BattleBoard(52, 8, proxy.GetBattleBoard().width, proxy.GetBattleBoard().height);
                        BattleBoard lrawEnemy = new BattleBoard(88, 8, enemyProxy.GetBattleBoard().width, enemyProxy.GetBattleBoard().height);

                        lrawPlayer.FieldsInitialization();
                        lrawEnemy.FieldsInitialization();

                        lrawPlayer.Restore(playerMemento);
                        lrawEnemy.Restore(enemyMemento);

                        var lreplayPlayerProxy = new BattleBoard.BattleBoardProxy(lrawPlayer);
                        var lreplayEnemyProxy = new BattleBoard.BattleBoardProxy(lrawEnemy);

                        Env.Wait(900);

                        string lwinnerName = (playerSunkCounter >= totalShipsToSink) ? UserSettings.Instance.Nickname : "AI_ENEMY";
                        int lwinnerId = (playerSunkCounter >= totalShipsToSink) ? UserSettings.Instance.GetHashCode() : "AI_ENEMY".GetHashCode();

                        var lvictoryScen = new VictoryScenario(lwinnerName, lwinnerId, stats, lreplayPlayerProxy, lreplayEnemyProxy, proxy.GetBattleBoard().height, proxy.GetBattleBoard().width);

                        lvictoryScen.ConnectScenario("Main", _mainScenario);
                        
                        Env.SetColor();
                        ActionManager.Instance.Detach(stats);
                        ActionManager.Instance.Detach(logger);
                        lvictoryScen.Act();
                        break;
                    }
                    
                }
            }
            var board = proxy.GetBattleBoard();
            var enemyBoard = enemyProxy.GetBattleBoard();
            BattleBoard rawPlayer = new BattleBoard(52, 8, board.width, board.height);
            BattleBoard rawEnemy = new BattleBoard(88, 8, enemyBoard.width, enemyBoard.height);
            rawPlayer.FieldsInitialization();
            rawEnemy.FieldsInitialization();
            rawPlayer.Restore(playerMemento);
            rawEnemy.Restore(enemyMemento);
            var replayPlayerProxy = new BattleBoard.BattleBoardProxy(rawPlayer);
            var replayEnemyProxy = new BattleBoard.BattleBoardProxy(rawEnemy);
            Env.Wait(1100);
            string winnerName = (enemyAdvanced40KShips.Count == 0) ? UserSettings.Instance.Nickname : "AI_ENEMY";
            int winnerId = (enemyAdvanced40KShips.Count == 0) ? UserSettings.Instance.GetHashCode() : "AI_ENEMY".GetHashCode();
            var victoryScen = new VictoryScenario(winnerName, winnerId, stats, replayPlayerProxy, replayEnemyProxy, board.height, board.width);
            victoryScen.ConnectScenario("Main", _mainScenario);
            Env.SetColor();
            ActionManager.Instance.Detach(stats);
            ActionManager.Instance.Detach(logger);
            victoryScen.Act();

            //koniec xd
        }
    }
}


