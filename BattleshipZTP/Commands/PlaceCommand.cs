using BattleshipZTP.GameAssets;
using BattleshipZTP.Observers;
using BattleshipZTP.Settings;
using BattleshipZTP.Ship;
using System.Collections.Generic;
using System.Text;

namespace BattleshipZTP.Commands;

public class PlaceCommand : ICommand
{
    public BattleBoard Board { get; }
    public IShip Ship { get; }
    public int PlayerID { get; }
    
    public PlaceCommand (BattleBoard board, IShip ship, int playerId)
    {
        Board = board;
        Ship = ship;
        PlayerID = playerId;
    }
    
    public void Execute(List<(int x, int y)> coords)
    {
        if(Ship is Advanced40KShip && UserSettings.Instance.SfxEnabled)
        {
            AudioManager.Instance.Play("ships/build");
        }
        StringBuilder stringBuilder = new StringBuilder();
        foreach (var _body in Ship.GetBody())
            foreach (char character in _body.text)
                stringBuilder.Append(character);

        string shipValue = stringBuilder.ToString();
        int shipIterator = 0;
        List <(int x, int y)> boardCoords = new List<(int x, int y)> ();
        foreach ((int x, int y) h in coords)
        {
            int localY = h.y - Board.cornerY - 1;
            int localX = h.x - Board.cornerX - 1;
            boardCoords.Add((localX, localY));
            Field field = Board.GetField(localX, localY);
            field.Character = shipValue[shipIterator];
            field.colors = Ship.GetColors();
            field.ShipReference = Ship;
            Board.DisplayField(localX, localY);
            shipIterator++;
        }
        Ship.Locate(boardCoords);

        var details = new GameActionDetails {
            PlayerID = this.PlayerID,
            ActionType = "Place",
        };
        
        ActionManager.Instance.LogAction(details);
    }

    public List<(string text, int offset)> GetBody()
    {
        return Ship.GetBody();
    }
    public void SetBody(List<(string text, int offset)> body)
    {
        Ship.SetBody(body);
    }
    public bool PlaceCondition(int x, int y)
    {
        Field field = Board.GetField(x, y);
        // Sprawdzanie kolizji
        return (field.ShipReference != null || Board.IsNeighborHaveShipRef(field));
    }
}