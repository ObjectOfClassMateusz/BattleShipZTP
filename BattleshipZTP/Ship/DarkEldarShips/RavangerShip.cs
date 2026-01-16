using BattleshipZTP.GameAssets;
using BattleshipZTP.Ship.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.DarkEldarShips
{
    public class RavangerShip : Advanced40KShip
    {
        static public int RequisitionCost = 375;
        static public int EnergyCost = 30;
        
        private const int RavangerSize = 5;
        public RavangerShip(List<Point> initialPlacement)
            : base(RavangerSize, initialPlacement)
        {
            _body.Add(("⇫",2));
            _body.Add(("⥑_♰_⥏", 0));
            _body.Add(("|", 2));
            _body.Add(("◿╳◺", 1));
            _body.Add(("⇙▓⇘", 1));

            _name = "Ravanger";
            _colors = (ConsoleColor.DarkMagenta, ConsoleColor.Black);

            _health = 300;
            _maxHealth = 300;
            _healthBar = new StatBar(_maxHealth, ConsoleColor.Magenta, 4);

            _audioReady.Add("5000619");
            _audioReady.Add("5000620b");
            _audioReady.Add("5000624b");

            _turrets.Add(new ShurikenCannon());

        }
    }
}