using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;

namespace BattleshipZTP.Ship.SpaceShips
{
    public class LandSpeederShip : Advanced40KShip
    {
        static public int RequisitionCost = 200;
        static public int EnergyCost = 50;

        private const int LandSpeederSize = 0;
        public LandSpeederShip(List<Point> initialPlacement) : base(LandSpeederSize, initialPlacement)
        {
            _name = "Land Speeder";
            _colors = (ConsoleColor.DarkRed, ConsoleColor.Black);
            _health = 200;
            _maxHealth = 200;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.DarkRed, 2);

            _turrets.Add(new HeavyBolter());
        }
    }
}
