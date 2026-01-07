using BattleshipZTP.UI;
using BattleshipZTP.Settings;


namespace BattleshipZTP.Observers;

public class GameLogger : IActionManager
{
    private Window _logWindow;
    private UIController _ui;

    public GameLogger(Window logWindow, UIController ui)
    {
        _logWindow = logWindow;
        _ui = ui;
    }
    public void Update(GameActionDetails details)
    {
        string shooterName = (details.PlayerID == UserSettings.Instance.GetHashCode()) 
            ? UserSettings.Instance.Nickname 
            : "AI_ENEMY";

        string logEntry = $"{shooterName}: {details.ActionType} ({details.Coords.X},{details.Coords.Y}) -> {details.Result}";

        if (_logWindow.ComponentsLenght() > 10) 
        {
            _logWindow.Remove(1);
        }

        _logWindow.Add(new TextOutput(logEntry));
        _ui.DrawAndEndSequence();
    }
}