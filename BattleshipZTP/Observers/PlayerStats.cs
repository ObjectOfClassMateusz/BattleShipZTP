namespace BattleshipZTP.Observers;

public class PlayerStats
{
    public int TotalShots { get; set; } = 0;
    public int Hits { get; set; } = 0;
    public int Misses { get; set; } = 0;
    public int TotalMoves { get; set; } = 0;
    public double Accuracy => TotalShots > 0 ? (double)Hits / TotalShots * 100 : 0;
} 
