using BattleshipZTP.GameAssets;
using BattleshipZTP.Observers;
using BattleshipZTP.Settings;
using BattleshipZTP.Ship.Turrets;

namespace BattleshipZTP.Commands
{
    public class TurretAttackCommand : ICommand
    {
        ITurret _turret;
        public BattleBoard Board { get; }
        public int PlayerID { get; }

        public TurretAttackCommand(ITurret turret,BattleBoard board)
        {
            Board = board;
            _turret = turret;
        }
        public void Execute(List<(int x, int y)> coords)
        {
            if(UserSettings.Instance.SfxEnabled)
                AudioManager.Instance.Play("ships/"+_turret.AudioFileName());

            HitResult overallResult = HitResult.Miss;

            Random random = new Random();
            foreach ((int x, int y) h in coords)
            {
                int localY = h.y - Board.cornerY - 1;
                int localX = h.x - Board.cornerX - 1;
                Point localPoint = new Point(localX, localY);
                Field localField = Board.GetField(localX, localY);
                HitResult hitResult = Board.AttackPoint(localPoint);

                if(hitResult == HitResult.Hit)
                {
                    var damage = random.Next(_turret.MinDmg(), _turret.MaxDmg());
                    overallResult = localField.ShipReference.TakeHit(localPoint, damage);
                }
            }
            var details = new GameActionDetails
            {
                PlayerID = this.PlayerID,
                ActionType = "Attack",
                Coords = new Point(
                    coords[0].x - Board.cornerY - 1, 
                    coords[0].y - Board.cornerX - 1
                ),
                Result = overallResult
            };
            ActionManager.Instance.LogAction(details);
        }
        public List<(string text, int offset)> GetBody() 
        {
            return _turret.GetAimBody();
        }
        public void SetBody(List<(string text, int offset)> body)
        {

        }
        public bool PlaceCondition(int x, int y)
        {
            return false;
        }
    }
}
