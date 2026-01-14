using BattleshipZTP.GameAssets;
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

        public Advanced40KShip(int size, List<Point> initialPlacement) 
            : base(size, initialPlacement)
        {
            _turrets = new List<ITurret>();
            _healthBar = new StatBar(_maxHealth, ConsoleColor.DarkRed, 6);
            /*StatBar bar = new StatBar(500, ConsoleColor.Red, 3);
            bar.Show();
            Console.WriteLine();
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

        public virtual void AudioPlayReady()
        {

        }
        public virtual void AudioPlayAttack()
        {

        }
        public virtual void AudioPlayMove()
        {

        }
    }
}
