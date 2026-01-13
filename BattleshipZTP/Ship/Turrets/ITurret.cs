using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.Turrets
{
    public interface ITurret
    {
        List<(string text, int offset)> GetAimBody();
    }
}
