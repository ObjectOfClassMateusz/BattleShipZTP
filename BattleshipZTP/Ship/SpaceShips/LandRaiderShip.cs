using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;

namespace BattleshipZTP.Ship.SpaceShips
{
    public class LandRaiderShip : Advanced40KShip
    {
        static public int RequisitionCost = 1000;
        static public int EnergyCost = 500;

        private const int LandRaiderSize = 75;
        public LandRaiderShip(List<Point> initialPlacement) : base(LandRaiderSize, initialPlacement)
        {
            _body.Add((    "⋓", 5));
            _body.Add(( "╔╗[]8[]╔╗", 1));
            _body.Add(( "║║[]8[]║║", 1));
            _body.Add(( "║║[]8[]║║", 1));
            _body.Add(("┣║║[]8[]║║┨", 0));
            _body.Add(( "║║[]8[]║║", 1));
            _body.Add(( "║║[]8[]║║", 1));
            _body.Add(( "║║[]8[]║║", 1));
            _body.Add(( "╚╝[]8[]╚╝", 1));

            _name = "Land Raider";
            _colors = (ConsoleColor.DarkRed, ConsoleColor.Black);
            _health = 1000;
            _maxHealth = 1000;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.DarkRed, 6);

            _audioMove.Add("404700");
            _audioMove.Add("404701");
            _audioMove.Add("404702");
            _audioAttack.Add("404720");
            _audioAttack.Add("404722");
            _audioReady.Add("404680");
            _audioReady.Add("404681");

            _turrets.Add(new HeavyBolter());
            _turrets.Add(new HeavyBolter());
            _turrets.Add(new Flamethrower());
            _turrets.Add(new PlasmaCannon());
        }
    }
}
