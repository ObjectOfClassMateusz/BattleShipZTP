namespace BattleshipZTP.Observers;

public class StatisticTracker : IActionManager
{
    private Dictionary<int, PlayerStats> _allPlayerStats = new Dictionary<int, PlayerStats>();
    public int TotalShots { get; private set; } = 0;
    public int Hits { get; private set; } = 0;
    public int Misses { get; private set; } = 0;

    public void Update(GameActionDetails details)
    {
        // Sprawdzamy, czy w słowniku mamy już statystyki dla tego gracza
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
                Misses++;
            }
        }
        else if (details.ActionType == "Move")
        {
            stats.TotalMoves++;
        }
        
        // Diagnostyka: wyświetlanie statystyk w konsoli debugowania
        System.Diagnostics.Debug.WriteLine($"[STATS] Gracz {details.PlayerID} | Strzały: {stats.TotalShots} | Trafienia: {stats.Hits} | Celność: {stats.Accuracy:F1}%");
    }

    // Metoda pomocnicza do pobierania statystyk konkretnego gracza
    public PlayerStats GetStats(int playerId) => _allPlayerStats.ContainsKey(playerId) ? _allPlayerStats[playerId] : new PlayerStats();
}

// Prosta klasa pomocnicza do przechowywania liczb
public class PlayerStats
{
    public int TotalShots { get; set; } = 0;
    public int Hits { get; set; } = 0;
    public int Misses { get; set; } = 0;
    public int TotalMoves { get; set; } = 0;
    public double Accuracy => TotalShots > 0 ? (double)Hits / TotalShots * 100 : 0;
}