using BattleshipZTP.GameAssets;
using BattleshipZTP.Settings;
using BattleshipZTP.Ship.Turrets;

namespace BattleshipZTP.Ship
{
    public class Advanced40KShip : BaseShip
    {
        protected StatBar _healthBar;
        protected int _health;
        protected int _maxHealth;
        protected readonly List<ITurret> _turrets;

        protected readonly List<string> _audioReady = new List<string>();
        protected readonly List<string> _audioAttack = new List<string>();
        protected readonly List<string> _audioMove = new List<string>();
        
        public Advanced40KShip(int size, List<Point> initialPlacement) : base(size, initialPlacement)
        {
            _turrets = new List<ITurret>();
        }
        public void ShowHealthBar()
        {
            _healthBar.Show();
        }
        public List<ITurret> GetTurrets()
        {
            var turretCount = _turrets.Count;
            return (turretCount > 0 ? _turrets : throw new Exception("Ship doesn't have any turrets"));
        }
        public ITurret GetTurret(int index)
        {
            return _turrets[index] ?? throw new Exception("Invalid index for searching turret");
        }
        public int GetHealth()
        {
            return _health;
        }
        public override bool IsSunk()
        {
            return _health == 0 ? true : false;
        }
        public override HitResult TakeHit(Point coords, int damage = 0)
        {
            if (!placement.Contains(coords))
            {
                return HitResult.Miss;
            }
            _health -= _healthBar.Decrease(damage);
            if (_health < 0)
            {
                _health = 0;
            }
            if (IsSunk())
            {
                return HitResult.HitAndSunk;
            }
            return HitResult.Hit;
        }

        public virtual void AudioPlayReady()
        {
            if (!UserSettings.Instance.SfxEnabled)
                return;
            Random rnd = new Random();
            int r = rnd.Next(_audioReady.Count);
            AudioManager.Instance.Play(_audioReady[r]);
        }
        public virtual void AudioPlayAttack()
        {
            if (!UserSettings.Instance.SfxEnabled)
                return;
            Random rnd = new Random();
            int r = rnd.Next(_audioAttack.Count);
            AudioManager.Instance.Play(_audioAttack[r]);
        }
        public virtual void AudioPlayMove()
        {
            if (!UserSettings.Instance.SfxEnabled)
                return;
            Random rnd = new Random();
            int r = rnd.Next(_audioMove.Count);
            AudioManager.Instance.Play(_audioMove[r]);
        }
    }
}
