using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;

namespace BattleshipZTP.Ship.SpaceShips
{
    public class LandSpeederShip : Advanced40KShip
    {
        static public int RequisitionCost = 200;
        static public int EnergyCost = 50;

        private const int LandSpeederSize = 18;
        public LandSpeederShip(List<Point> initialPlacement) : base(LandSpeederSize, initialPlacement)
        {
            _body.Add(("⇑⇑", 1));
            _body.Add(("⥮⍃⍄⥯", 0));
            _body.Add(("‡[]‡", 0));
            _body.Add(("‡{}‡", 0));
            _body.Add(("⍢⎺⎺⍢", 0));
            _name = "Land Speeder";
            _colors = (ConsoleColor.DarkRed, ConsoleColor.Black);
            _health = 200;
            _maxHealth = 200;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.DarkRed, 2);

            _audioReady.Add("404570");
            _audioReady.Add("404671");
            _audioMove.Add("404700");
            _audioMove.Add("404701");
            _audioMove.Add("404702");
            _audioAttack.Add("404720");
            _audioAttack.Add("404722");

            _turrets.Add(new HeavyBolter());
        }
    }
}
