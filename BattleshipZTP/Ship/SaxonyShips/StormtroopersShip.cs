
using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;

namespace BattleshipZTP.Ship.SaxonyShips
{
    public class StormtroopersShip : Advanced40KShip
    {
        static public int RequisitionCost = 165;
        static public int EnergyCost = 15;

        private const int StormtroopersSize = 8;
        public StormtroopersShip(List<Point> initialPlacement)
            : base(StormtroopersSize, initialPlacement)
        {
            _body.Add(("[✠|]", 0));
            _body.Add(("[#%]", 0));
            _name = "Stormstrooper";
            _colors = (ConsoleColor.DarkYellow, ConsoleColor.Black);
            _health = 110;
            _maxHealth = 110;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.DarkYellow, 1);
            _audioReady.Add("8q");
            _audioReady.Add("8w");
            _audioReady.Add("8e");
            _audioReady.Add("8r");
            _audioReady.Add("8t");
            _audioReady.Add("8y");
            _audioMove.Add("m60");
            _audioMove.Add("m61");
            _audioMove.Add("m62");
            _audioMove.Add("m63");
            _audioMove.Add("m64");
            _audioAttack.Add("9011");
            _audioAttack.Add("9012");
            _audioAttack.Add("9013");
            _audioAttack.Add("9014");
            _audioAttack.Add("9015");

            _turrets.Add(new Rifles());
            _turrets.Add(new Rifles());
        }
    }
}
