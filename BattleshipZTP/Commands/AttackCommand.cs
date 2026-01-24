using BattleshipZTP.GameAssets;
using BattleshipZTP.Observers;
using BattleshipZTP.Settings;

namespace BattleshipZTP.Commands;

public class AttackCommand : ICommand
{
    public IBattleBoard Board { get; }
    public Point Target { get; }
    public int PlayerID { get; }
    public IGameMode GameMode { get; }
    public string Nickname { get; }

    public AttackCommand (IBattleBoard board, Point target, int playerId, string nickname = "")
    {
        Board = board;
        Target = target;
        PlayerID = playerId;
        Nickname = nickname;
    }
    
    public void Execute(List<(int x, int y)> coords)
    {
        var field = Board.GetField(Target.X, Target.Y);
        if (field == null) return;
        if (field.ArrowHit)
        {
            if (UserSettings.Instance.SfxEnabled)
            {
                AudioManager.Instance.Play("wrong");
            }
            return;
        }
        HitResult hitResult = Board.AttackPoint(Target);
        field.ArrowHit = true;
        if (hitResult == HitResult.Hit && UserSettings.Instance.SfxEnabled == true)
        {
            AudioManager.Instance.Play("trafienie");
        }
        else if (hitResult == HitResult.HitAndSunk && UserSettings.Instance.SfxEnabled == true )
        {
            AudioManager.Instance.Play("trafiony zatopiony");
        }
        else if (hitResult == HitResult.Miss && UserSettings.Instance.SfxEnabled == true)
        {
            AudioManager.Instance.Play("miss");
        }

        var details = new GameActionDetails {
            PlayerID = this.PlayerID,
            Nickname = this.Nickname,
            ActionType = "Attack",
            Coords = Target,
            Result = hitResult
        };
        ActionManager.Instance.LogAction(details);
    }

    public void PlayHitSound()
    {
        AudioManager.Instance.Play("trafiony");
    }
    public void PlayMissSound()
    {
        AudioManager.Instance.Play("miss");
    }
    
    public List<(string text, int offset)> GetBody()
    {
        return new List<(string text, int offset)>()
        {
            ("+",0)
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