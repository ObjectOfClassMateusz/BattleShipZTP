using BattleshipZTP.GameAssets;

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

            AudioManager.Instance.Add("086", "ships/Saxony/EisenhansShip");
            _audioReady.Add("086");
            AudioManager.Instance.Add("087", "ships/Saxony/EisenhansShip");
            _audioReady.Add("087");
            AudioManager.Instance.Add("088", "ships/Saxony/EisenhansShip");
            _audioReady.Add("088");
        }

        public override void AudioPlayReady()
        {
            Random rnd = new Random();
            int r = rnd.Next(_audioReady.Count);
            AudioManager.Instance.Play(_audioReady[r]);

        }
        public override void AudioPlayAttack()
        {

        }
        public override void AudioPlayMove()
        {

        }
    }
}
