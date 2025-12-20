using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.GameObjects
{
    internal class Ship : IShip
    {
        private List<(string, int, ConsoleColor, ConsoleColor)> _body;
        private (int, int)[] _cordinates;
        private bool[] _hits;

        public String Name { get; set; }
        public Ship(String name)
        {
            Name = name;
        }
    }
}
