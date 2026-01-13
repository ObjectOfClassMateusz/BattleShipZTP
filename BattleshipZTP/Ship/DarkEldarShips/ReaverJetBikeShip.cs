using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.DarkEldarShips
{
    public class ReaverJetBikeShip : Advanced40KShip
    {
        private const int ReaverJetBikeSize = 4;
        public ReaverJetBikeShip( List<Point> initialPlacement)
            : base(ReaverJetBikeSize, initialPlacement)
        {
            _body.Add(("٨", 2));
            _body.Add(("༺☫༻", 1));
            _body.Add(("☽=☾", 1));
            _body.Add(("◿═══◺", 0));

            _name = "ReaverJetBike";
            _colors = (ConsoleColor.DarkMagenta, ConsoleColor.Black);
        }
    }
}
