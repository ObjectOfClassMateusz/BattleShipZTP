using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;

namespace BattleshipZTP.Ship.SpaceShips
{
    public class LandRaiderShip : Advanced40KShip
    {
        static public int RequisitionCost = 1000;
        static public int EnergyCost = 500;

        private const int LandRaiderSize = 0;
        public LandRaiderShip(List<Point> initialPlacement) : base(LandRaiderSize, initialPlacement)
        {
            _name = "Land Raider";
            _colors = (ConsoleColor.DarkRed, ConsoleColor.Black);
            _health = 1000;
            _maxHealth = 1000;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.DarkRed, 6);

            _turrets.Add(new HeavyBolter());
            _turrets.Add(new HeavyBolter());
            _turrets.Add(new Flamethrower());
            _turrets.Add(new PlasmaCannon());
        }
    }
}
