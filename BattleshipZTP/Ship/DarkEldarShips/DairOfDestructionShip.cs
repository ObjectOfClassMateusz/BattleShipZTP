using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.DarkEldarShips
{
    public class DairOfDestructionShip : Advanced40KShip
    {
        static public int RequisitionCost = 720;
        static public int EnergyCost = 120;
        
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
            _name = "DairOfDestruction";
            _colors = (ConsoleColor.DarkMagenta, ConsoleColor.Black);
            _health = 770;
            _maxHealth = 770;

            _audioReady.Add("5000636");
            _audioReady.Add("5000637");
            _audioReady.Add("5000638");
            _healthBar = new StatBar(_maxHealth, ConsoleColor.Magenta, 6);

            _turrets.Add(new ShurikenCannon());
        }
    }
}
