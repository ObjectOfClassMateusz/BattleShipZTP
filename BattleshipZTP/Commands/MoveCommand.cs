using BattleshipZTP.GameAssets;
using BattleshipZTP.Observers;
using BattleshipZTP.Settings;
using BattleshipZTP.Ship;
using NAudio.Codecs;
using System.Text;

namespace BattleshipZTP.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly BattleBoard _board;
        private readonly Advanced40KShip _ship;
        private readonly int _playerId;
        private readonly string _nickname;

        public MoveCommand(BattleBoard board, Advanced40KShip ship, int playerId, string nickname)
        {
            _board = board;
            _ship = ship;
            _playerId = playerId;
            _nickname = nickname;
        }

        public void Execute(List<(int x, int y)> newCoords)
        {
            _board.RemoveShip(_ship);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var _body in _ship.GetBody())
                foreach (char character in _body.text)
                    stringBuilder.Append(character);

            if (UserSettings.Instance.SfxEnabled)
                _ship.AudioPlayMove();

            string shipValue = stringBuilder.ToString();
            int shipIterator = 0;
            List<(int x, int y)> boardCoords = new List<(int x, int y)>();
            foreach ((int x, int y) h in newCoords)
            {
                int localY = h.y - _board.cornerY - 1;
                int localX = h.x - _board.cornerX - 1;
                boardCoords.Add((localX, localY));
                Field field = _board.GetField(localX, localY);
                field.Character = shipValue[shipIterator];
                field.colors = _ship.GetColors();
                field.ShipReference = _ship;
                _board.DisplayField(localX, localY);
                shipIterator++;
            }

            var details = new GameActionDetails
            {
                PlayerID = _playerId,
                Nickname = _nickname,
                ActionType = "Move",
                Coords = new Point(newCoords[0].x, newCoords[0].y),
                Result = HitResult.Miss // Ruch nie jest strzałem
            };
            ActionManager.Instance.LogAction(details);
        }

        public List<(string text, int offset)> GetBody() => _ship.GetBody();
        public void SetBody(List<(string text, int offset)> body) => _ship.SetBody(body);

        public bool PlaceCondition(int x, int y)
        {
            Field field = _board.GetField(x, y);
            // Sprawdzanie kolizji
            return (field.ShipReference != null || _board.IsNeighborHaveShipRef(field));
        }
    }
}