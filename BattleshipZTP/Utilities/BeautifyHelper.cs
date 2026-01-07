namespace BattleshipZTP.Utilities
{
    public static class BeautifyHelper
    {
        public static void ApplyFancyBodies(List<IShip> ships)
        {
            char shipChar = '●'; // mozna zmienic na #, O, ●, ▓
            foreach (var ship in ships)
            {
                int size = ship.GetSize();
                string bar = new string(shipChar, Math.Max(1, size));
                var body = new List<(string text, int offset)> { (bar, 0) };
                ship.SetBody(body);
            }
        }
    }
}