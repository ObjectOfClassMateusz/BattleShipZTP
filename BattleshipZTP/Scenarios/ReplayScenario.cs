using BattleshipZTP.GameAssets;
using BattleshipZTP.Observers;
using BattleshipZTP.Settings;
using BattleshipZTP.Utilities;
using BattleshipZTP.UI;

namespace BattleshipZTP.Scenarios;

public class ReplayScenario : Scenario
{
    private IScenario _mainMenu;
    private List<GameActionDetails> _history;
    private IBattleBoard _playerBoard;
    private IBattleBoard _enemyBoard;
    private int _height;
    private int _width;

    public ReplayScenario(List<GameActionDetails> history, IBattleBoard playerBoard, IBattleBoard enemyBoard, int height, int width, IScenario mainMenu)
    {
        _history = history;
        _playerBoard = playerBoard;
        _enemyBoard = enemyBoard;
        _height = height;
        _width = width;
        _mainMenu = mainMenu;
    }

    public override void Act()
    {
        Console.Clear();
        RevealEnemyShips(_enemyBoard);

        IWindowBuilder windowBuilder = new WindowBuilder();
        
        Console.WriteLine("--- POWTÓRKA BITWY ---");
        windowBuilder.SetPosition(2, 2)
            .SetSize(30)
            .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
            .ColorHighlights(ConsoleColor.White, ConsoleColor.Green)
            .AddComponent(new TextOutput("=== Game Backlogs ==="));
        
        Window backlogWindow = windowBuilder.Build();
        UIController logController = new UIController();
        logController.AddWindow(backlogWindow);

        GameLogger logger = new GameLogger(backlogWindow, logController);
        
        Env.CursorPos(2, 1);
        
        foreach (var action in _history)
        {
            Env.Wait(900);
            HitResult hitResult = action.Result;

            if (action.PlayerID == UserSettings.Instance.GetHashCode())
            {
                _enemyBoard.PlaceMarker(action.Coords, action.Result);
                
                if (hitResult == HitResult.Hit && UserSettings.Instance.SfxEnabled == true)
                {
                    AudioManager.Instance.Play("trafienie");
                }
                else if (hitResult == HitResult.HitAndSunk && UserSettings.Instance.SfxEnabled == true )
                {
                    AudioManager.Instance.Play("trafiony zatopiony");
                }
                else if (hitResult == HitResult.Miss && UserSettings.Instance.SfxEnabled == true)
                {
                    AudioManager.Instance.Play("miss");
                }
            }
            
            else
            {
                _playerBoard.PlaceMarker(action.Coords, action.Result);
                if (hitResult == HitResult.Hit && UserSettings.Instance.SfxEnabled == true)
                {
                    AudioManager.Instance.Play("trafienie");
                }
                else if (hitResult == HitResult.HitAndSunk && UserSettings.Instance.SfxEnabled == true )
                {
                    AudioManager.Instance.Play("trafiony zatopiony");
                }
                else if (hitResult == HitResult.Miss && UserSettings.Instance.SfxEnabled == true)
                {
                    AudioManager.Instance.Play("miss");
                }
            }

            _playerBoard.Display();
            _enemyBoard.Display();

            logger.Update(action);
        }

        IWindowBuilder exitBuilder = new WindowBuilder();

        exitBuilder.SetPosition(2, 15)
            .SetSize(30)
            .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
            .ColorHighlights(ConsoleColor.White, ConsoleColor.Green)
            .AddComponent(new Button("POWROT DO MENU"));
        
        Window exitWindow = exitBuilder.Build();
        UIController exitController = new UIController();
        exitController.AddWindow(exitWindow);
        List<string> results = exitController.DrawAndStart();
        string choice = results.LastOrDefault();
        if (choice == "POWROT DO MENU")
        {
            _mainMenu.Act();
        }
    }
    
    private void RevealEnemyShips(IBattleBoard board)
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                var field = board.GetField(x, y);
                if (field != null && field.ShipReference != null && field.Character == ' ')
                {
                    field.Character = 'X'; // symbol statku
                    field.colors = (ConsoleColor.Red, ConsoleColor.Black);
                }
            }
        }
    }
}