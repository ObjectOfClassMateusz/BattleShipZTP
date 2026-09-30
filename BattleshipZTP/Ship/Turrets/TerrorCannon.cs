using BattleshipZTP.Utilities;

namespace BattleshipZTP.Ship.Turrets
{
    public class TerrorCannon : ITurret
    {
        bool _ready = true;
        public TerrorCannon() { }
        public int MinDmg()
        {
            return 59;
        }
        public int MaxDmg()
        {
            return 60;
        }
        public int ActionCost()
        {
            return 18;
        }
        public List<(string text, int offset)> GetAimBody()
        {
            return new List<(string text, int offset)>()
            {
                ("⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛",0)
            };
        }
        public void AfterAudioDelay()
        {
            Env.Wait(500);
        }
        public string GetName()
        {
            return "TerrorCannon";
        }
        public string AudioFileName()
        {
            return "dair_of_destrc_laser";
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
