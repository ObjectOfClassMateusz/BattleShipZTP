using System;

namespace BattleshipZTP.Ship.Turrets
{
    public class RavangerCannon : ITurret
    {
        bool _ready = true;
        public RavangerCannon() { }
        public int MinDmg()
        {
            return 50;
        }
        public int MaxDmg()
        {
            return 61;
        }
        public int ActionCost()
        {
            return 7;
        }
        public List<(string text, int offset)> GetAimBody()
        {
            return new List<(string text, int offset)>()
            {
                ("+",1),
                ("+++",0)
            };
        }
        public string GetName()
        {
            return "Ravanger Cannon";
        }
        public string AudioFileName()
        {
            return "ravanger_shot";
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
