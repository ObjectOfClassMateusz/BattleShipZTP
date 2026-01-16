using BattleshipZTP.GameAssets;
using BattleshipZTP.Settings;
using BattleshipZTP.Ship.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship
{
    public class Advanced40KShip : BaseShip
    {
        protected StatBar _healthBar;
        protected int _health;
        protected int _maxHealth;
        protected List<ITurret> _turrets;

        protected List<string> _audioReady = new List<string>();
        protected List<string> _audioAttack = new List<string>();

        public void ShowHealthBar()
        {
            _healthBar.Show();
        }

        public Advanced40KShip(int size, List<Point> initialPlacement) 
            : base(size, initialPlacement)
        {
            _turrets = new List<ITurret>();
            /*
            bar.Decrease(297);
            bar.Show();*/
        }
        public List<ITurret> GetTurrets()
        {
            return _turrets;
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
            if( _health == 0) {  return true; }
            return false;
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
        }
    }
}
