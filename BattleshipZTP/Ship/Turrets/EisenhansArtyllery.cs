using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.Turrets
{
    public class EisenhansArtyllery : ITurret
    {
        public EisenhansArtyllery() { }
        public int MinDmg()
        {
            return 53;
        }
        public int MaxDmg()
        {
            return 61;
        }
        public int ActionCost()
        {
            return 3;
        }
        public List<(string text, int offset)> GetAimBody()
        {
            return new List<(string text, int offset)>()
            {
                ("+++",0),
                ("+++",0),
                ("+++",0)
            };
        }
        public string GetName()
        {
            return "Artyllery";
        }
        public string AudioFileName()
        {
            return "artillery";
        }
    }
}
