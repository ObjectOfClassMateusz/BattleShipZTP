using BattleshipZTP.GameAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.DarkEldarShips
{
    public class RavangerShip : Advanced40KShip
    {
        private const int RavangerSize = 5;
        public RavangerShip(List<Point> initialPlacement)
            : base(RavangerSize, initialPlacement)
        {
            _body.Add(("⇫",2));
            _body.Add(("⥑_♰_⥏", 0));
            _body.Add(("|", 2));
            _body.Add(("◿╳◺", 1));
            _body.Add(("⇙▓⇘", 1));

            _name = "Ravanger";
            _colors = (ConsoleColor.DarkMagenta, ConsoleColor.Black);

            _health = 300;
            _maxHealth = 300;
        }
    }
}