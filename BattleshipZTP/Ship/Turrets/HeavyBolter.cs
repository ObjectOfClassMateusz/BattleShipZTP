using BattleshipZTP.Utilities;

namespace BattleshipZTP.Ship.Turrets
{
    public class HeavyBolter : ITurret
    {
        bool _ready = true;
        public HeavyBolter() { }
        public int MinDmg()
        {
            return 13;
        }
        public int MaxDmg()
        {
            return 19;
        }
        public int ActionCost()
        {
            return 3;
        }
        public List<(string text, int offset)> GetAimBody()
        {
            return new List<(string text, int offset)>()
            {
                ("===",0)
            };
        }
        public string GetName()
        {
            return "Heavy Bolter";
        }
        public string AudioFileName()
        {
            return "bolt";
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
