using BattleshipZTP.Utilities;

namespace BattleshipZTP.Ship.Turrets
{
    public class PlasmaCannon : ITurret
    {
        bool _ready = true;
        public PlasmaCannon() { }
        public int MinDmg()
        {
            return 43;
        }
        public int MaxDmg()
        {
            return 59;
        }
        public int ActionCost()
        {
            return 17;
        }
        public List<(string text, int offset)> GetAimBody()
        {
            return new List<(string text, int offset)>()
            {
                ("¤",0)
            };
        }
        public string GetName()
        {
            return "Plasma Cannon";
        }
        public string AudioFileName()
        {
            return "plasma";
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
