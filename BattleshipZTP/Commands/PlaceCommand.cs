using BattleshipZTP.GameAssets;
using BattleshipZTP.Observers;
using System.Text;

namespace BattleshipZTP.Commands;

public class PlaceCommand : ICommand
{
    public BattleBoard Board { get; }//Commandy wysyłajmy bezbośrednio do boarda
    //nie musi byc interfejs bo proxy jest po stronie użytkownika
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
        //Execute command - placing shipment process

        StringBuilder stringBuilder = new StringBuilder();
        foreach (var _body in Ship.GetBody())
            foreach (char character in _body.text)
                stringBuilder.Append(character);

        string shipValue = stringBuilder.ToString();
        int shipIterator = 0;
        foreach ((int x, int y) h in coords)
        {
            int localY = h.y - Board.cornerY - 1;
            int localX = h.x - Board.cornerX - 1;
            Field field = Board.GetField(localX, localY);
            //tutaj znajduje się mnóstwo szczegółów
            //które warto zapisać przez Observera
            field.Character = shipValue[shipIterator];
            field.colors = Ship.GetColors();
            field.ShipReference = Ship;
            Board.DisplayField(localX, localY);
            shipIterator++;
        }

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
    public bool PlaceCondition(int x, int y)
    {
        Field field = Board.GetField(x, y);
        // Sprawdzanie kolizji
        return (field.ShipReference != null || Board.IsNeighborHaveShipRef(field));
    }
}