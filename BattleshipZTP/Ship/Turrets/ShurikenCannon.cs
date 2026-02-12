
using BattleshipZTP.Utilities;

namespace BattleshipZTP.Ship.Turrets
{
    public class ShurikenCannon : ITurret
    {
        bool _ready = true;
        public ShurikenCannon() { }
        public int MinDmg()
        {
            return 10;
        }
        public int MaxDmg()
        {
            return 20;
        }
        public int ActionCost()
        {
            return 2;
        }
        public List<(string text, int offset)> GetAimBody()
        {
            return new List<(string text, int offset)>() 
            {
                ("+",0),
                ("+",0)
            };
        }
        public string GetName()
        {
            return "Shuriken Cannon";
        }
        public string AudioFileName()
        {
            return "shuriken";
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
            Env.Wait(0);
        }
    }
}
