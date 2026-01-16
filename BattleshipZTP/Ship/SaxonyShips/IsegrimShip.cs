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
            
        }
    }
}
