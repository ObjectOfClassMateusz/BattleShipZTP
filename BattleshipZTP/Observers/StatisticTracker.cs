
namespace BattleshipZTP.Observers;

public class StatisticTracker : IActionManager
{
    private Dictionary<int, PlayerStats> _allPlayerStats = new Dictionary<int, PlayerStats>();
    private List<GameActionDetails> _actionHistory = new List<GameActionDetails>();
    
    public int RequiredHitsToWin { get; set; }
    
    public void Update(GameActionDetails details)
    {
        if (details.ActionType == "Attack")
        {
            _actionHistory.Add(details);
        }

        if (!_allPlayerStats.ContainsKey(details.PlayerID))
        {
            _allPlayerStats[details.PlayerID] = new PlayerStats();
        }
        
        var stats = _allPlayerStats[details.PlayerID];
        
        if (details.ActionType == "Attack")
        {
            stats.TotalShots++;
            
            if (details.Result == HitResult.Hit || details.Result == HitResult.HitAndSunk)
            {
                stats.Hits++;
            }
            else if (details.Result == HitResult.Miss)
            {
                stats.Misses++;
            }
        }
        else if (details.ActionType == "Move")
        {
            stats.TotalMoves++;
        }
        
        System.Diagnostics.Debug.WriteLine($"[STATS] Gracz {details.PlayerID} | Strzały: {stats.TotalShots} | Trafienia: {stats.Hits} | Pudła: {stats.Misses} | Celność: {stats.Accuracy:F1}%");
    }

    public List<GameActionDetails> GetHistory() => _actionHistory;
    
    public PlayerStats GetStats(int playerId) => 
        _allPlayerStats.ContainsKey(playerId) ? _allPlayerStats[playerId] : new PlayerStats();
}