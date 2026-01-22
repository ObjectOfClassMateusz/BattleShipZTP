using BattleshipZTP.GameAssets;
using BattleshipZTP.Observers;
using BattleshipZTP.Settings;
using BattleshipZTP.Ship;

namespace BattleshipZTP.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly IBattleBoard _board;
        private readonly Advanced40KShip _ship;
        private readonly int _playerId;
        private readonly string _nickname;

        public MoveCommand(IBattleBoard board, Advanced40KShip ship, int playerId, string nickname)
        {
            _board = board;
            _ship = ship;
            _playerId = playerId;
            _nickname = nickname;
        }

        public void Execute(List<(int x, int y)> newCoords)
        {
            _board.RemoveShip(_ship);

            if (UserSettings.Instance.SfxEnabled)
                _ship.AudioPlayMove();
            
            foreach (var coord in newCoords)
            {
                var field = _board.GetField(coord.x, coord.y);
                if (field != null)
                {
                    field.ShipReference = _ship;
                }
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
            var field = _board.GetField(x, y);

            if (field == null) return true;

            if (field.ShipReference != null && field.ShipReference != _ship)
            {
                return true;
            }

            return false;
        }
    }
}