using BattleshipZTP.Utilities;

namespace BattleshipZTP.Ship.Turrets
{
    public class Flamethrower : ITurret
    {
        bool _ready = true;
        public Flamethrower() { }
        public int MinDmg()
        {
            return 9;
        }
        public int MaxDmg()
        {
            return 12;
        }
        public int ActionCost()
        {
            return 6;
        }
        public List<(string text, int offset)> GetAimBody()
        {
            return new List<(string text, int offset)>()
            {
                ("≋≋≋≋≋≋≋≋≋≋≋≋",2),
                ("≋≋≋≋≋≋≋≋≋≋≋≋≋≋",1),
                ("≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋≋",0),
            };
        }
        public string GetName()
        {
            return "Flamethrower";
        }
        public string AudioFileName()
        {
            return "fire";
        }
        public void AfterAudioDelay()
        {
            Env.Wait(200);
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
