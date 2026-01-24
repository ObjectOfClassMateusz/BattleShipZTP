namespace BattleshipZTP.Utilities
{
    /**
    * @brief Utility class for applying visual customizations to ships
    * @details Provides helper methods to enhance the visual representation of ships
    * in the console by setting custom body characters and formatting.
    */
    public static class BeautifyHelper
    {
        /**
        * @brief Applies fancy visual bodies to all ships in a collection
        * @param ships List of ships to customize with visual bodies
        * @details Sets each ship's body to display using a bullet character (●).
        * The ship body is represented as a string of repeated characters, with length
        * matching the ship's size. The character can be changed by modifying the shipChar variable.
        * Supported characters include: #, O, ●, ▓
        * @note Applied only on Classic and duel modes.
        */
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