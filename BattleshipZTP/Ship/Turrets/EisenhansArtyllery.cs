using BattleshipZTP.Utilities;

namespace BattleshipZTP.Ship.Turrets
{
    public class EisenhansArtyllery : ITurret
    {
        bool _ready = true;
        public EisenhansArtyllery() { }
        public int MinDmg()
        {
            return 13;
        }
        public int MaxDmg()
        {
            return 35;
        }
        public int ActionCost()
        {
            return 10;
        }
        public List<(string text, int offset)> GetAimBody()
        {
            return new List<(string text, int offset)>()
            {
                ("+++",0),
                ("+++",0),
                ("+++",0)
            };
        }
        public string GetName()
        {
            return "Artyllery";
        }
        public string AudioFileName()
        {
            return "artillery";
        }

        public void Use()
        {
            _ready = false;
        }
        public void Renew()
        {
            _ready = true;
        }
        public bool IsReady() => _ready;
        public void AfterAudioDelay()
        {
            Env.Wait(100);
        }
    }
}
