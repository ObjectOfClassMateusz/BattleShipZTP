
namespace BattleshipZTP.Observers;

public class StatisticTracker : IActionManager
{
    private Dictionary<int, PlayerStats> _allPlayerStats = new Dictionary<int, PlayerStats>();
    
    public int RequiredHitsToWin { get; set; }
    
    public void Update(GameActionDetails details)
    {
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

    public bool HasPlayerWon(int playerId)
    {
        if (!_allPlayerStats.ContainsKey(playerId)) return false;
        
        return _allPlayerStats[playerId].Hits >= RequiredHitsToWin;
    }
    public PlayerStats GetStats(int playerId) => 
        _allPlayerStats.ContainsKey(playerId) ? _allPlayerStats[playerId] : new PlayerStats();
}