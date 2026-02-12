using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;

namespace BattleshipZTP.Ship.EldarShips
{
    public class VyperShip : Advanced40KShip
    {
        static public int RequisitionCost = 520;
        static public int EnergyCost = 200;

        private const int VyperSize = 17;
        public VyperShip(List<Point> initialPlacement) : base(VyperSize, initialPlacement)
        {
            _body.Add((    "⧋", 4));
            _body.Add(( "▋⊍▋║", 1));
            _body.Add(( "▋▋⧗╣", 1));
            _body.Add(( "▋▋▋", 1));
            _body.Add(("⧬▔▔▔⧬", 0));

            _colors = (ConsoleColor.DarkGreen, ConsoleColor.Black);
            _name = "Vyper";
            _health = 360;
            _maxHealth = 360;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.Green, 4);

            _audioReady.Add("402160");
            _audioReady.Add("402161");
            _audioReady.Add("402162");
            _audioMove.Add("402212");
            _audioMove.Add("402213");
            _audioMove.Add("402214");
            _audioMove.Add("402217");
            _audioAttack.Add("402251");
            _audioAttack.Add("402255");
            _audioAttack.Add("402252");

            _turrets.Add(new ShurikenCannon());
            _turrets.Add(new VyperRocketLauncher());
        }
    }
}
