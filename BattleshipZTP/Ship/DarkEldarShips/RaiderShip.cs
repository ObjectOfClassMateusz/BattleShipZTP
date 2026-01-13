using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.DarkEldarShips
{
    public class RaiderShip : Advanced40KShip
    {
        private const int RaiderSize = 5;
        public RaiderShip( List<Point> initialPlacement)
            : base(RaiderSize, initialPlacement)
        {
            _body.Add(("⚨", 2));
            _body.Add(("↼╫⇀", 1));
            _body.Add(("▇╫▇", 1));
            _body.Add(("{╫}", 1));
            _body.Add(("⤧=╫=⤧", 0));

            _colors = (ConsoleColor.DarkMagenta,ConsoleColor.Black);
            _name = "Raider";
        }
    }
}
