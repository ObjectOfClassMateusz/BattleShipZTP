using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;

namespace BattleshipZTP.Ship.EldarShips
{
    public class FirePrismShip : Advanced40KShip
    {
        static public int RequisitionCost = 770;
        static public int EnergyCost = 300;

        private const int FirePrismSize = 23;
        public FirePrismShip(List<Point> initialPlacement) :base(FirePrismSize, initialPlacement)
        {
            _body.Add(("❖", 5));
            _body.Add(("║", 5));
            _body.Add(("◢===◣║", 0));
            _body.Add(("[===]▟", 0));
            _body.Add(("▜===▛" , 0));
            _body.Add(("◘▽◘", 1));
            _colors = (ConsoleColor.DarkGreen, ConsoleColor.Black);
            _name = "FirePrism";
            _health = 690;
            _maxHealth = 690;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.Green, 4);

            _audioAttack.Add("402251");
            _audioAttack.Add("402254");
            _audioMove.Add("402212");
            _audioMove.Add("402213");
            _audioMove.Add("402214");
            _audioMove.Add("402217");
            _audioReady.Add("402080");
            _audioReady.Add("402151");

            _turrets.Add(new ShurikenCannon());
            _turrets.Add(new FirePrism());
        }

    }
}
