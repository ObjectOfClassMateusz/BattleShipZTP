using BattleshipZTP.GameAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.SaxonyShips
{
    public class GrimbartShip : Advanced40KShip
    {
        private const int GrimbartSize = 8;
        public GrimbartShip(List<Point> initialPlacement)
            : base(GrimbartSize, initialPlacement)
        {
            _body.Add(("∐", 3));
            _body.Add(("║", 3));
            _body.Add(("║", 3));
            _body.Add(("▛▇▜║", 0));
            _body.Add(("▋✠_║", 0));
            _body.Add(("▙▅▟║", 0));

            _name = "Grimbart";
            _colors = (ConsoleColor.DarkYellow, ConsoleColor.Black);
            _health = 500;
            _maxHealth = 500;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.DarkYellow, 5);

            //_turrets.Add(new MachineGuns());
        }
    }
}
