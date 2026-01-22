using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.DarkEldarShips
{
    public class ReaverJetBikeShip : Advanced40KShip
    {
        static public int RequisitionCost = 150;
        static public int EnergyCost = 10;

        private const int ReaverJetBikeSize = 12;
        public ReaverJetBikeShip( List<Point> initialPlacement)
            : base(ReaverJetBikeSize, initialPlacement)
        {
            _body.Add(("٨", 2));
            _body.Add(("༺☫༻", 1));
            _body.Add(("☽=☾", 1));
            _body.Add(("◿═══◺", 0));
            _name = "ReaverJetBike";
            _colors = (ConsoleColor.DarkMagenta, ConsoleColor.Black);
            _health = 90;
            _maxHealth = 90;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.Magenta, 2);
            _audioReady.Add("5000588");
            _audioReady.Add("5000589b");
            _audioReady.Add("5000590b");
            _audioReady.Add("5000591");
            _audioReady.Add("5000592");
            _audioAttack.Add("5000578b");
            _audioAttack.Add("5000579");
            _audioAttack.Add("5000580b");
            _audioMove.Add("5000582b");
            _audioMove.Add("5000583b");
            _audioMove.Add("5000584");
            _turrets.Add(new ShurikenCannon());
        }
    }
}
