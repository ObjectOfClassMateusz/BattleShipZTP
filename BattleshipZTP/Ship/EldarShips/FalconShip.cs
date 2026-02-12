using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;

namespace BattleshipZTP.Ship.EldarShips
{
    public class FalconShip : Advanced40KShip
    {
        static public int RequisitionCost = 200;
        static public int EnergyCost = 20;

        private const int FalconSize = 16;
        public FalconShip(List<Point> initialPlacement) : base(FalconSize, initialPlacement)
        {
            _body.Add(( "∐⦡∐", 1));
            _body.Add(("⋥⧮⧍⧮⋤", 0));
            _body.Add(("⋥⧮⊛⧮⋤", 0));
            _body.Add(( "⋥⧮⋤", 1));

            _colors = (ConsoleColor.DarkGreen, ConsoleColor.Black);
            _name = "Falcon";
            _health = 250;
            _maxHealth = 250;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.Green, 4);

            _audioMove.Add("402212");
            _audioMove.Add("402213");
            _audioMove.Add("402214");
            _audioMove.Add("402217");
            _audioAttack.Add("402251");
            _audioAttack.Add("402255");
            _audioAttack.Add("402252");
            _audioReady.Add("402140");
            _audioReady.Add("402141");

            _turrets.Add(new ShurikenCannon());
        }
    }
}
