using BattleshipZTP.Utilities;

namespace BattleshipZTP.Ship.Turrets
{
    public class FirePrism : ITurret
    {
        bool _ready = true;
        public FirePrism() { }
        public int MinDmg()
        {
            return 35;
        }
        public int MaxDmg()
        {
            return 50;
        }
        public int ActionCost()
        {
            return 15;
        }
        public List<(string text, int offset)> GetAimBody()
        {
            return new List<(string text, int offset)>()
            {
                ("◢▲◣",0),
                ("◀≸▶",0),
                ("◥▼◤",0),
            };
        }
        public string GetName()
        {
            return "Fire Prism";
        }
        public string AudioFileName()
        {
            return "fire_prism";
        }
        public void AfterAudioDelay()
        {
            Env.Wait(1500);
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
    }
}
