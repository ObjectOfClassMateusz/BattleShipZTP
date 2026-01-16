
namespace BattleshipZTP.Ship.SaxonyShips
{
    public class StormtroopersShip : Advanced40KShip
    {
        private const int StormtroopersSize = 8;
        public StormtroopersShip(List<Point> initialPlacement)
            : base(StormtroopersSize, initialPlacement)
        {
            _body.Add(("[✠|]", 0));
            _body.Add(("[#%]", 0));

            _name = "Stormstrooper";
            _colors = (ConsoleColor.DarkYellow, ConsoleColor.Black);
            _health = 150;
            _maxHealth = 150;
        }
    }
}
