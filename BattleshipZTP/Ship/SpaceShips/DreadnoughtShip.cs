using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;

namespace BattleshipZTP.Ship.SpaceShips
{
    public class DreadnoughtShip : Advanced40KShip
    {
        static public int RequisitionCost = 380;
        static public int EnergyCost = 100;

        private const int DreadnoughtSize = 0;
        public DreadnoughtShip(List<Point> initialPlacement) : base(DreadnoughtSize, initialPlacement)
        {
            _name = "Dreadnought";
            _colors = (ConsoleColor.DarkRed, ConsoleColor.Black);
            _health = 450;
            _maxHealth = 450;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.DarkRed, 3);

            _turrets.Add(new HeavyBolter());
            _turrets.Add(new Flamethrower());
        }
    }
}