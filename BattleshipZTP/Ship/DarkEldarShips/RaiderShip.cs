using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.DarkEldarShips
{
    public class RaiderShip : Advanced40KShip
    {
        static public int RequisitionCost = 280;
        static public int EnergyCost = 20;
        
        private const int RaiderSize = 15;
        public RaiderShip( List<Point> initialPlacement)
            : base(RaiderSize, initialPlacement)
        {
            _body.Add(("⚨", 2));
            _body.Add(("↼╫⇀", 1));
            _body.Add(("▇╫▇", 1));
            _body.Add(("{╫}", 1));
            _body.Add(("⤧=╫=⤧", 0));
            _colors = (ConsoleColor.DarkMagenta,ConsoleColor.Black);
            _name = "Raider";
            _health = 180;
            _maxHealth = 180;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.Magenta, 4);
            _audioReady.Add("5000602");
            _audioReady.Add("5000603b");
            _audioReady.Add("5000604b");
            _audioAttack.Add("5000594b");
            _audioAttack.Add("5000595");
            _audioMove.Add("5000596");
            _audioMove.Add("5000597");
            _audioMove.Add("5000598");

            _turrets.Add(new ShurikenCannon());
            //_turrets.Add(new RaiderCannon());
        }
    }
}
