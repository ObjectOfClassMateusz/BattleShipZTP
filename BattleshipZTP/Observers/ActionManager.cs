namespace BattleshipZTP.Observers;

public class ActionManager
{
    private static ActionManager _instance;
    private ActionManager() { }
    public static ActionManager Instance => _instance ??= new ActionManager();

    private List<IActionManager> _observers = new List<IActionManager>();
    
    public void Attach(IActionManager observer)
    {
        _observers.Add(observer);
    }
    public void Detach(IActionManager observer)
    {
        _observers.Remove(observer);
    } 
    
    public void NotifyObservers(GameActionDetails details)
    {
        foreach (var observer in _observers)
        {
            observer.Update(details);
        }
    }
    
    public void ClearObservers()
    {
        _observers.Clear();
    }

    public void LogAction(GameActionDetails details)
    {
        NotifyObservers(details);
    }
}