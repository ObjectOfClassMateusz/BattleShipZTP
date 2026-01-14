using BattleshipZTP.GameAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.DarkEldarShips
{
    public class ReaverJetBikeShip : Advanced40KShip
    {
        static public int RequisitionCost = 150;
        static public int EnergyCost = 10;

        private const int ReaverJetBikeSize = 12;
        public ReaverJetBikeShip( List<Point> initialPlacement)
            : base(ReaverJetBikeSize, initialPlacement)
        {
            _body.Add(("٨", 2));
            _body.Add(("༺☫༻", 1));
            _body.Add(("☽=☾", 1));
            _body.Add(("◿═══◺", 0));

            _name = "ReaverJetBike";
            _colors = (ConsoleColor.DarkMagenta, ConsoleColor.Black);

            _health = 90;
            _maxHealth = 90;
        }
    }
}
