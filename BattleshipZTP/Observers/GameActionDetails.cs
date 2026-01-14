namespace BattleshipZTP.Observers;

public class GameActionDetails
{
    public int PlayerID { get; set; }
    
    public string Nickname { get; set; }
    public string ActionType { get; set; }
    public Point Coords { get; set; }
    public HitResult Result { get; set; }
}