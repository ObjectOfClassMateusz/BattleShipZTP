using BattleshipZTP.GameAssets;
using BattleshipZTP.Observers;

namespace BattleshipZTP.Commands;

public class AttackCommand : ICommand
{
    public IBattleBoard Board { get; }
    public Point Target { get; }
    public int PlayerID { get; }
    
    public AttackCommand (IBattleBoard board, Point target, int playerId)
    {
        Board = board;
        Target = target;
        PlayerID = playerId;
    }
    
    public void Execute()
    {
        HitResult hitResult = Board.AttackPoint(Target);
        
        var details = new GameActionDetails {
            PlayerID = this.PlayerID,
            ActionType = "Attack",
            Coords = Target,
            Result = hitResult
        };
        
        ActionManager.Instance.LogAction(details);
    }
}