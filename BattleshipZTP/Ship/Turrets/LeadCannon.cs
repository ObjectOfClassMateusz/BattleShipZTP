using BattleshipZTP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.Turrets
{
    public class LeadCannon : ITurret
    {
        bool _ready = true;
        public LeadCannon(){}
        public int MinDmg()
        {
            return 45;
        }
        public int MaxDmg()
        {
            return 65;
        }
        public int ActionCost()
        {
            return 11;
        }
        public List<(string text, int offset)> GetAimBody()
        {
            return new List<(string text, int offset)>()
            {
                ("##",0),
                ("##",0)
            };
        }
        public string GetName()
        {
            return "Lead Cannon";
        }
        public string AudioFileName()
        {
            return "lead-shot";
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
