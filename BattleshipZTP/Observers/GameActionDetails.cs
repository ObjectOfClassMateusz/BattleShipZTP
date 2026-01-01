namespace BattleshipZTP.Observers;

public class GameActionDetails
{
    public int PlayerID { get; set; }
    public string ActionType { get; set; } // np. "Attack" lub "Move"
    public Point Coords { get; set; }
    public HitResult Result { get; set; }
}