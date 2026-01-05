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
        // Prosty sygnał dźwiękowy Systemu lub NAudio
        Console.Beep(440, 100); 

        HitResult hitResult = Board.AttackPoint(Target); //
    
        // Logowanie akcji powiadamia Obserwatorów (GameLogger dopisze to do Backlogu)
        var details = new GameActionDetails {
            PlayerID = this.PlayerID,
            ActionType = "Attack",
            Coords = Target,
            Result = hitResult
        };
        ActionManager.Instance.LogAction(details);
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
        //GameMode
        //if already hit at "x" and "y"
        return true;
    }
}