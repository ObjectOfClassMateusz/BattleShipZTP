using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.Turrets
{
    public class TerrorCannon : ITurret
    {
        bool _ready = true;
        public TerrorCannon() { }
        public int MinDmg()
        {
            return 99;
        }
        public int MaxDmg()
        {
            return 101;
        }
        public int ActionCost()
        {
            return 1;
            //return 15;
        }
        public List<(string text, int offset)> GetAimBody()
        {
            return new List<(string text, int offset)>()
            {
                ("⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛⇛",0)
            };
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
