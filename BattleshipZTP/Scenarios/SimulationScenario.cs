using BattleshipZTP.Commands;
using BattleshipZTP.GameAssets;
using BattleshipZTP.Networking;
using BattleshipZTP.Scenarios;
using BattleshipZTP.Utilities;
using System.Collections.Generic;
using BattleshipZTP;
using BattleshipZTP.Observers;
using BattleshipZTP.UI;

public class SimulationScenario : Scenario
{
    private IAI _ai1;
    private IAI _ai2;
    private IGameMode _gameMode;
    private int _width, _height;
    IScenario _mainScenario;
    private string _ai1DifficultyName;
    private string _ai2DifficultyName;

    public SimulationScenario(IGameMode gameMode, AIDifficulty difficulty1, AIDifficulty difficulty2,IScenario mainmenu)
    {
        _mainScenario = mainmenu;
        _gameMode = gameMode;
        _ai1 = difficulty1 switch
        {
            AIDifficulty.Easy => new SimpleAI(),
            AIDifficulty.Medium => new MediumAI(),
            AIDifficulty.Hard => new HardAI(),
            _ => new SimpleAI()
        };
        _ai2 = difficulty2 switch
        {
            AIDifficulty.Easy => new SimpleAI(),
            AIDifficulty.Medium => new MediumAI(),
            AIDifficulty.Hard => new HardAI(),
            _ => new SimpleAI()
        };
        _ai1DifficultyName = difficulty1.ToString();
        _ai2DifficultyName = difficulty2.ToString();
    }

    public override void Act()
    {
        Console.Clear();
        ActionManager.Instance.ClearObservers();
        
        string name1 = $"AI_1 ({_ai1DifficultyName})";
        string name2 = $"AI_2 ({_ai2DifficultyName})";
        IWindowBuilder windowBuilder = new WindowBuilder();
        windowBuilder.SetPosition(110, 2)
            .SetSize(30)
            .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
            .ColorHighlights(ConsoleColor.White, ConsoleColor.Green)
            .AddComponent(new TextOutput("=== Simulation Logs ==="));

        Window backlogWindow = windowBuilder.Build();
        UIController logController = new UIController();
        logController.AddWindow(backlogWindow);

        GameLogger logger = new GameLogger(backlogWindow, logController);
        StatisticTracker stats = new StatisticTracker();

        ActionManager.Instance.Attach(logger);
        ActionManager.Instance.Attach(stats);
        
        var coords = _gameMode.BoardCoords();
        BattleBoard board1 = _gameMode.CreateBoard(coords.XAxis_Player1, coords.YAxis_Player1 + 1);
        Env.CursorPos(coords.XAxis_Player1, coords.YAxis_Player1);
        Console.Write($"AI_1 ({_ai1DifficultyName})");
        BattleBoard board2 = _gameMode.CreateBoard(coords.XAxis_Player2, coords.YAxis_Player2 + 1);
        Env.CursorPos(coords.XAxis_Player2, coords.YAxis_Player2);
        Console.Write($"AI_2 ({_ai2DifficultyName})");
        
        board1.FieldsInitialization();
        board2.FieldsInitialization();
        
        var proxy1 = new BattleBoard.BattleBoardProxy(board1);
        var proxy2 = new BattleBoard.BattleBoardProxy(board2);
        
        _width = board1.width;
        _height = board1.height;
        
        List<IShip> ships1 = _gameMode.ShipmentDelivery(true);
        List<IShip> ships2 = _gameMode.ShipmentDelivery(true);
        
        PlaceShipsRandomly(proxy1, _gameMode.ShipmentDelivery(), "AI_1");
        PlaceShipsRandomly(proxy2, _gameMode.ShipmentDelivery(), "AI_2");
        
        proxy1.Display();
        proxy2.Display();

        bool victory = false;
        int totalShips = _gameMode.ShipmentDelivery().Count;
        int sunk1 = 0, sunk2 = 0;

        while (!victory)
        {
            // TURA AI 1
            Env.Wait(900);

            ExecuteAiTurn(_ai1, proxy2, ref sunk2, name1);
            if (sunk2 >= totalShips) { victory = true; break; }

            // TURA AI 2
            Env.Wait(900);

            ExecuteAiTurn(_ai2, proxy1, ref sunk1, name2);
            if (sunk1 >= totalShips) { victory = true; break; }
        }
        BattleBoardMemento board1Memento = board1.GetSaveState();
        BattleBoardMemento board2Memento = board2.GetSaveState();

        BattleBoard raw1 = new BattleBoard(coords.XAxis_Player1, coords.YAxis_Player1 + 1, _width, _height);
        BattleBoard raw2 = new BattleBoard(coords.XAxis_Player2, coords.YAxis_Player2 + 1, _width, _height);
        raw1.FieldsInitialization();
        raw2.FieldsInitialization();
        raw1.Restore(board1Memento);
        raw2.Restore(board2Memento);

        string winnerName = (sunk2 >= totalShips) ? name1 : name2;
        int winnerId = winnerName.GetHashCode(); 
        
        var victoryScen = new VictoryScenario(
            winnerName, 
            winnerId, 
            stats, 
            new BattleBoard.BattleBoardProxy(raw1), 
            new BattleBoard.BattleBoardProxy(raw2), 
            _height, 
            _width
        );

        victoryScen.ConnectScenario("Main", _mainScenario);

        ActionManager.Instance.Detach(logger);
        ActionManager.Instance.Detach(stats);
        Env.SetColor();
        victoryScen.Act();
    }
    private void ExecuteAiTurn(IAI ai, BattleBoard.BattleBoardProxy targetProxy, ref int sunkCounter, string name)
    {
        bool nextTurn = true;
        while (nextTurn)
        {
            Env.Wait(900);
            Point target = ai.GetNextMove(_width, _height, targetProxy.GetBattleBoard());
            AttackCommand attack = new AttackCommand(targetProxy, target, name.GetHashCode(), name);
            attack.Execute(new List<(int, int)>());

            var field = targetProxy.GetField(target.X, target.Y);
            if (field?.ShipReference != null)
            {
                if (field.ShipReference.IsSunk())
                {
                    sunkCounter++;
                    ai.ClearTargets();
                }
                else
                {
                    ai.AddTargetNeighbors(target, _width, _height);
                }
            }
            else
            {
                nextTurn = false;
            }
            targetProxy.Display();
        }
    }
    private void PlaceShipsRandomly(BattleBoard.BattleBoardProxy proxy, List<IShip> ships, string name)
    {
        Random rand = new Random();
        foreach (var ship in ships)
        {
            bool placed = false;
            while (!placed)
            {
                int x = rand.Next(0, _width);
                int y = rand.Next(0, _height);
            
                if (CanPlaceAt(proxy, ship, x, y)) 
                {
                    PlaceCommand cmd = new PlaceCommand(proxy.GetBattleBoard(), ship, name.GetHashCode());
                
                    var coords = proxy.PlaceShip(ship, x, y);
                
                    cmd.Execute(coords); 
                
                    placed = true;
                }
            }
        }
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
}