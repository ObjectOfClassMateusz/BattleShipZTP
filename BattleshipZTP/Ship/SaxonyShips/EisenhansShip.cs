using BattleshipZTP.GameAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.SaxonyShips
{
    public class EisenhansShip : Advanced40KShip
    {
        private const int EisanhansSize = 9;
        public EisenhansShip(List<Point> initialPlacement)
            : base(EisanhansSize, initialPlacement)
        {
            _body.Add(("[-]", 1));
            _body.Add(("◓⬚◓", 1));
            _body.Add(("⋐▥⋑", 1));

            _name = "Eisanhans";
            _colors = (ConsoleColor.DarkYellow, ConsoleColor.Black);

            _health = 310;
            _maxHealth = 310;
        }
    }
}
