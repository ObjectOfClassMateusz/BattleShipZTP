using BattleshipZTP.GameAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.DarkEldarShips
{
    public class DairOfDestructionShip : Advanced40KShip
    {
        private const int DairOfDestructionSize = 7;
        public DairOfDestructionShip( List<Point> initialPlacement)
            : base(DairOfDestructionSize, initialPlacement)
        {
            _body.Add((   "⟁", 3));
            _body.Add(("⤩=╫╋╫=⤩", 0));
            _body.Add(("╥♰╥", 2));
            _body.Add((  "༺☫༻", 2));
            _body.Add(("༺⇯༻", 2));
            _body.Add(("=╫╋╫=", 1));
            _body.Add(("╱=╫=╫=╲", 0));

            _colors = (ConsoleColor.DarkMagenta, ConsoleColor.Black);
            _name = "DairOfDestruction";

            _health = 770;
            _maxHealth = 770;
        }
    }
}
