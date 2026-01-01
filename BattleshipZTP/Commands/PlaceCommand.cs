using BattleshipZTP.GameAssets;
using BattleshipZTP.Observers;

namespace BattleshipZTP.Commands;

public class PlaceCommand : ICommand
{
    public IBattleBoard Board { get; }
    public IShip Ship { get; }
    public int PlayerID { get; }
    
    public PlaceCommand (IBattleBoard board, IShip ship, int playerId)
    {
        Board = board;
        Ship = ship;
        PlayerID = playerId;
    }
    
    public void Execute()
    {
        Board.PlaceShip(Ship);
        
        var details = new GameActionDetails {
            PlayerID = this.PlayerID,
            ActionType = "Place",
        };
        
        ActionManager.Instance.LogAction(details);
    }
}