using BattleshipZTP.Utilities;

namespace BattleshipZTP.Ship.Turrets
{
    public class MachineCannon : ITurret
    {
        bool _ready = true;
        public MachineCannon() { }
        public int MinDmg()
        {
            return 4;
        }
        public int MaxDmg()
        {
            return 28;
        }
        public int ActionCost()
        {
            return 4;
        }
        public List<(string text, int offset)> GetAimBody()
        {
            return new List<(string text, int offset)>()
            {
                ( "###",1),
                ("#####",0)
            };
        }
        public string GetName()
        {
            return "Machine Gun";
        }
        public void AfterAudioDelay()
        {
            Env.Wait(200);
        }
        public string AudioFileName()
        {
            return "machine-gun";
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
