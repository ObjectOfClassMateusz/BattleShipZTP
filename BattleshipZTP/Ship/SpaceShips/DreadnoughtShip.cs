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
            _body.Add(("⋓___Λ", 0));
            _body.Add(("╔═══╗", 0));
            _body.Add(("║⊑⊜⊒║", 0));
            _body.Add(("╚═══╝", 0));

            _name = "Dreadnought";
            _colors = (ConsoleColor.DarkRed, ConsoleColor.Black);
            _health = 450;
            _maxHealth = 450;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.DarkRed, 3);
            _audioReady.Add("404350");
            _audioReady.Add("404351");
            _audioReady.Add("404353");
            _audioReady.Add("404355");
            _audioMove.Add("404362");
            _audioMove.Add("404363");
            _audioMove.Add("404365");
            _audioAttack.Add("404370");
            _audioAttack.Add("404372");
            _audioAttack.Add("404431");
            _audioAttack.Add("404440");
            _turrets.Add(new HeavyBolter());
            _turrets.Add(new Flamethrower());
        }
    }
}