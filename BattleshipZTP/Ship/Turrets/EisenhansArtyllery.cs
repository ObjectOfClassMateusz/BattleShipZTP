using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.Turrets
{
    public class EisenhansArtyllery : ITurret
    {
        bool _ready = true;
        public EisenhansArtyllery() { }
        public int MinDmg()
        {
            return 33;
        }
        public int MaxDmg()
        {
            return 55;
        }
        public int ActionCost()
        {
            return 5;
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
