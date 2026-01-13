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

        public Advanced40KShip(int size, List<Point> initialPlacement) 
            : base(size, initialPlacement)
        {
            
        }
    }
}
