using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;
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

            _audioReady.Add("12");
            _audioReady.Add("13");
            _audioReady.Add("14");
            _audioReady.Add("15");

            _audioAttack.Add("66");
            _audioAttack.Add("67");

            //_turrets.Add(new MachineGuns());
            _turrets.Add(new ShurikenCannon());
        }
    }
}
