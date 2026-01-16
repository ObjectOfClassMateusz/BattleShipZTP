using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.Turrets
{
    public class ShurikenCannon : ITurret
    {
        public ShurikenCannon() { }
        public int MinDmg()
        {
            return 18;
        }
        public int MaxDmg()
        {
            return 35;
        }
        public int ActionCost()
        {
            return 1;
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
    }
}
