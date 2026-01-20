using BattleshipZTP.GameAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.SaxonyShips
{
    public class IsegrimShip : Advanced40KShip
    {
        private const int IsegriSize = 8;
        public IsegrimShip(List<Point> initialPlacement)
            : base(IsegriSize, initialPlacement)
        {
            _body.Add(("◇", 3));
            _body.Add(("█", 3));
            _body.Add(("█", 3));
            _body.Add(("[=#=]", 1));
            _body.Add(("[=#=]", 1));
            _body.Add(("◢[=#=]◣", 0));

            _name = "Isegrim";
            _colors = (ConsoleColor.DarkYellow, ConsoleColor.Black);
            _health = 610;
            _maxHealth = 610;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.DarkYellow, 6);

            //_turrets.Add(new MachineGuns());
        }
    }
}
