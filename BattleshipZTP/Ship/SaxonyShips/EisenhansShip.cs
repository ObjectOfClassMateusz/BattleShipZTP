using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;

namespace BattleshipZTP.Ship.SaxonyShips
{
    public class EisenhansShip : Advanced40KShip
    {
        private const int EisanhansSize = 9;
        public EisenhansShip(List<Point> initialPlacement)
            : base(EisanhansSize, initialPlacement)
        {
            _body.Add(("[-]", 0));
            _body.Add(("◓⬚◓", 0));
            _body.Add(("⋐▥⋑", 0));

            _name = "Eisanhans";
            _colors = (ConsoleColor.DarkYellow, ConsoleColor.Black);
            _health = 310;
            _maxHealth = 310;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.DarkYellow, 3);

            _audioReady.Add("085");
            _audioReady.Add("086");
            _audioReady.Add("087");
            _audioReady.Add("088");
            _audioReady.Add("089");

            _audioAttack.Add("033");
            _audioAttack.Add("034");
            _audioAttack.Add("035");
            _turrets.Add(new EisenhansArtyllery());
        }
    }
}
