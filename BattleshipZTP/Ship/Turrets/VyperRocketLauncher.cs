using BattleshipZTP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.Turrets
{
    public class VyperRocketLauncher : ITurret
    {
        bool _ready = true;
        public VyperRocketLauncher() { }
        public int MinDmg()
        {
            return 19;
        }
        public int MaxDmg()
        {
            return 35;
        }
        public int ActionCost()
        {
            return 9;
        }
        public List<(string text, int offset)> GetAimBody()
        {
            return new List<(string text, int offset)>()
            {
                ("#",2),
                ("###",1),
                ("#####",0),
            };
        }
        public string GetName()
        {
            return "Rocket Launcher";
        }
        public string AudioFileName()
        {
            return "vyper_rocket";
        }
        public void AfterAudioDelay()
        {
            Env.Wait(1000);
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
