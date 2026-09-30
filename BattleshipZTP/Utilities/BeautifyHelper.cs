using BattleshipZTP.Ship;

namespace BattleshipZTP.Utilities
{
    /**
     * @brief Utility class for applying visual modifications to ships
     * @details Provides helper methods for enhancing the visual 
     * representation of ships in the console by setting custom hull 
     * characters and formatting. 
     */
    public static class BeautifyHelper
    {
        /**
         * @brief Applies stylish visual hulls to all ships in the collection
         * @param ships List of ships to visually customize
         * @details Sets the hull of each ship to display using the bullet point character (●).
         * The ship's hull is represented as a string of repeated 
         * characters, with a length corresponding to the ship's size. 
         * The character can be changed by modifying the shipChar variable.
         * Supported characters: #, O, ●, ▓
         * @note Apply only in Classic and Duel modes. 
         */
        public static void ApplyFancyBodies(List<IShip> ships)
        {
            char shipChar = '●'; // can be replaced to @, #, O, ●, ▓
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