using BattleshipZTP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.Turrets
{
    public class Rifles : ITurret
    {
        bool _ready = true;
        public Rifles() { }
        public int MinDmg()
        {
            return 3;
        }
        public int MaxDmg()
        {
            return 9;
        }
        public int ActionCost()
        {
            return 2;
        }
        public List<(string text, int offset)> GetAimBody()
        {
            return new List<(string text, int offset)>()
            {
                ("^^^^",0),
                ("^^^^",0)
            };
        }
        public string GetName()
        {
            return "Rifles";
        }
        public string AudioFileName()
        {
            return "rilfes";
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
