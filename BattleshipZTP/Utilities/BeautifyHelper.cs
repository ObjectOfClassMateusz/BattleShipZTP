namespace BattleshipZTP.Utilities
{
    /**
* @brief Klasa narzędziowa do stosowania wizualnych modyfikacji statków
* @details Udostępnia metody pomocnicze do ulepszania wizualnej reprezentacji statków
* w konsoli poprzez ustawianie niestandardowych znaków kadłuba i formatowanie.
*/

    public static class BeautifyHelper
    {
        /**
        * @brief Zastosowuje efektowne wizualne kadłuby dla wszystkich statków w kolekcji
        * @param ships Lista statków do spersonalizowania wizualnie
        * @details Ustawia kadłub każdego statku do wyświetlania przy użyciu znaku punktu (●).
        * Kadłub statku jest reprezentowany jako ciąg powtarzanych znaków, o długości
        * odpowiadającej rozmiarowi statku. Znak można zmienić, modyfikując zmienną shipChar.
        * Obsługiwane znaki to: #, O, ●, ▓
        * @note Zastosowanie tylko w trybach Classic i Duel.
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