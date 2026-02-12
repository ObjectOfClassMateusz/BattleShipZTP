using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;

namespace BattleshipZTP.Ship.DarkEldarShips
{
    public class DairOfDestructionShip : Advanced40KShip
    {
        static public int RequisitionCost = 920;
        static public int EnergyCost = 210;
        
        private const int DairOfDestructionSize = 29;
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
            _healthBar = new StatBar(_maxHealth, ConsoleColor.Magenta, 6);
            _audioReady.Add("5000636");
            _audioReady.Add("5000637");
            _audioReady.Add("5000638");
            _audioMove.Add("5000630");
            _audioMove.Add("5000631");
            _audioMove.Add("5000640");
            _audioAttack.Add("5000626");
            _audioAttack.Add("5000627");
            _audioAttack.Add("5000629");

            _turrets.Add(new ShurikenCannon());
            _turrets.Add(new TerrorCannon());
        }
    }
}
