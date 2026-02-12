using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.SaxonyShips
{
    public class IsegrimShip : Advanced40KShip
    {
        static public int RequisitionCost = 600;
        static public int EnergyCost = 400;

        private const int IsegriSize = 3+7+5+7;
        public IsegrimShip(List<Point> initialPlacement)
            : base(IsegriSize, initialPlacement)
        {
            _body.Add(("◇", 3));
            _body.Add(("█", 3));
            _body.Add(("█", 3));
            _body.Add(("◢[=#=]◣", 0));
            _body.Add(("[=#=]", 1));
            _body.Add(("◢[=#=]◣", 0));

            _name = "Isegrim";
            _colors = (ConsoleColor.DarkYellow, ConsoleColor.Black);
            _health = 610;
            _maxHealth = 610;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.DarkYellow, 6);

            _audioReady.Add("107");
            _audioReady.Add("108");
            _audioReady.Add("109");
            _audioReady.Add("110");
            _audioMove.Add("599");
            _audioMove.Add("600");
            _audioMove.Add("601");
            _audioMove.Add("602");
            _audioMove.Add("603");
            _audioAttack.Add("0333");
            _audioAttack.Add("0334");
            _audioAttack.Add("0335");
            _audioAttack.Add("0336");

            _turrets.Add(new LeadCannon());
            _turrets.Add(new Rifles());
        }
    }
}
