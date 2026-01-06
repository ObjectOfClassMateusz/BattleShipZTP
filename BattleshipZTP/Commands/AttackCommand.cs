using BattleshipZTP.GameAssets;
using BattleshipZTP.Observers;

namespace BattleshipZTP.Commands;

public class AttackCommand : ICommand
{
    public IBattleBoard Board { get; }
    public Point Target { get; }
    public int PlayerID { get; }
    public IGameMode GameMode { get; }

    public AttackCommand (IBattleBoard board, Point target, int playerId)
    {
        Board = board;
        Target = target;
        PlayerID = playerId;
    }
    
    public void Execute(List<(int x, int y)> coords)
    {
        var field = Board.GetField(Target.X, Target.Y);
        if (field == null) return;
        if (field.Character == 'X' || field.Character == '•') return;

        HitResult hitResult = Board.AttackPoint(Target);

        var details = new GameActionDetails {
            PlayerID = this.PlayerID,
            ActionType = "Attack",
            Coords = Target,
            Result = hitResult
        };
        ActionManager.Instance.LogAction(details);
    }

    public void PlayHitSound()
    {
        AudioManager.Instance.Play("hit");
    }

    public void PlayMissSound()
    {
        AudioManager.Instance.Play("miss");
    }
    
    public List<(string text, int offset)> GetBody()
    {
        return new List<(string text, int offset)>()
        {
            ("X",0)
        };
    }
    public void SetBody(List<(string text, int offset)> body)
    {
        //
    }


    public bool PlaceCondition(int x, int y)
    {
        return true;
    }
}