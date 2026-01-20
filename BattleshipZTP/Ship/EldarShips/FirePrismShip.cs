using BattleshipZTP.GameAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.EldarShips
{
    public class FirePrismShip : Advanced40KShip
    {
        private const int FirePrismSize = 6;
        public FirePrismShip(List<Point> initialPlacement) :base(FirePrismSize, initialPlacement)
        {
            _body.Add(("❖", 5));
            _body.Add(("║", 5));
            _body.Add(("◢===◣║", 1));
            _body.Add(("[===]▟", 0));
            _body.Add(("▜===▛" , 0));
            _body.Add(("◘▽◘", 1));

            _colors = (ConsoleColor.DarkGreen, ConsoleColor.Black);
            _name = "FirePrism";
            _health = 580;
            _maxHealth = 580;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.Green, 4);

            //_turrets.Add(new FirePrism());
        }

    }
}
