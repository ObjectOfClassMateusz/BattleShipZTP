using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Utilities
{
    /**
    * @brief Klasa pomocnicza do przechowywania współrzędnych do rysowania planszy do gry
    * @details Ta klasa zawiera współrzędne X i Y dla pozycji na planszy dwóch graczy.
    * Zapewnia możliwości serializacji i deserializacji w celu przechowywania i pobierania współrzędnych podczas rozgrywki w trybie wieloosobowym.
    */
    public class CoordsToDrawBoard
    {
        public readonly int XAxis_Player1;
        public readonly int YAxis_Player1;
        public readonly int XAxis_Player2;
        public readonly int YAxis_Player2;

        /**
        * @brief Konstruktor inicjalizujący współrzędne planszy dla obu graczy
        * @param Pl1x Współrzędna X gracza 1
        * @param Pl1y Współrzędna Y gracza 1
        * @param Pl2x Współrzędna X gracza 2
        * @param Pl2y Współrzędna Y gracza 2
        */
        public CoordsToDrawBoard(int Pl1x ,int Pl1y ,int Pl2x , int Pl2y)
        {
            XAxis_Player1 = Pl1x; YAxis_Player1 = Pl1y;
            XAxis_Player2 = Pl2x; YAxis_Player2 = Pl2y;
        }
        
        /**
        * @brief Serializuje kordynaty na pipe-delimited format napisu
        * @return Napisowa reprezentacja w formatcie "X1|Y1|X2|Y2"
        */
        public override string ToString()
        {
            return $"{XAxis_Player1}|{YAxis_Player1}|{XAxis_Player2}|{YAxis_Player2}";
        }

        /**
* @brief Deserializuje współrzędne z formatu tekstowego rozdzielonego pionową kreską
* @param data Ciąg w formacie "X1|Y1|X2|Y2"
* @return Obiekt CoordsToDrawBoard z sparsowanymi współrzędnymi
* @throws ArgumentException jeśli dane nie zawierają dokładnie 4 wartości rozdzielonych pionową kreską
*/

        public static CoordsToDrawBoard FromString(string data)
        {
            var parts = data.Split('|');

            if (parts.Length != 4)
                throw new ArgumentException("Invalid coordinate format");

            return new CoordsToDrawBoard(
                int.Parse(parts[0]),
                int.Parse(parts[1]),
                int.Parse(parts[2]),
                int.Parse(parts[3])
            );
        }
    }

    /**
    * @brief Klasa narzędziowa do renderowania grafiki w konsoli
    * @details Udostępnia metody statyczne do rysowania prymitywnych kształtów, kolorowego tekstu
    * oraz obrazów ASCII w konsoli z możliwością dostosowania kolorów i pozycji.
    * Utrzymuje pamięć podręczną obrazów ASCII dla wydajnego renderowania.
*/
    public class Drawing
    {
        /// @brief Przechowuje bieżące kolory pierwszego planu i tła dla operacji rysowania
        static (ConsoleColor foreground, ConsoleColor background) _colors;

        /// @brief Słownik pamięci podręcznej dla wczytanych obrazów ASCII, indeksowany według nazwy pliku
        static readonly Dictionary<string, ASCIIImage> _images = new Dictionary<string, ASCIIImage>();
        /**
        * @brief Ustawia domyślne kolory pierwszego planu i tła dla kolejnych operacji rysowania
        * @param foreground Kolor pierwszego planu do zastosowania
        * @param background Kolor tła do zastosowania
        */
        public static void SetColors(ConsoleColor foreground, ConsoleColor background)
        {
            _colors.foreground = foreground;
            _colors.background = background;
        }

        /**
        * @brief Rysuje znak poziomo (od lewej do prawej) w określonej pozycji
        * @param character Znak do narysowania
        * @param count Liczba powtórzeń znaku
        * @param x Pozycja początkowa na osi X
        * @param y Pozycja na osi Y
        */
        public static void DrawRight(char character, int count , int x , int y)
        {
            Env.SetColor(_colors.foreground, _colors.background);
            Env.CursorPos(x, y);
            for (int i = 0; i < count; i++)
            {
                Console.Write(character);
            }
            Env.SetColor();
        }

        /**
  * @brief Rysuje znak pionowo (od góry do dołu) w określonej pozycji
  * @param character Znak do narysowania
  * @param count Liczba powtórzeń znaku (tworzy pionową linię)
  * @param x Pozycja na osi X
  * @param y Pozycja początkowa na osi Y
  */

        public static void DrawDown(char character, int count, int x, int y)
        {
            Env.SetColor(_colors.foreground, _colors.background);
            for (int i = 0;i < count; i++)
            {
                Env.CursorPos(x, y + i);
                Console.Write(character);
            }
            Env.SetColor();
        }

        /**
  * @brief Wypełnia prostokątny obszar spacjami (tworzy kolorowy prostokąt)
  * @param x Pozycja początkowa prostokąta na osi X
  * @param y Pozycja początkowa prostokąta na osi Y
  * @param w Szerokość prostokąta w znakach
  * @param h Wysokość prostokąta w znakach
  */

        public static void DrawRectangleArea(int x, int y, int w, int h)
        {
            StringBuilder b = new StringBuilder();
            b.Append(' ', w);
            string rect = b.ToString();
            Env.SetColor(_colors.foreground, _colors.background);
            for (int i = 0; i < h; i++)
            {
                Env.CursorPos(x, y + i);
                Console.Write(rect);
            }
            Env.SetColor();
        }

        /**
    * @brief Prywatna klasa wewnętrzna do wczytywania i przechowywania obrazów ASCII
    * @details Wczytuje obrazy ASCII z plików tekstowych w katalogu img z obsługą awaryjnego fallbacku
    */
        class ASCIIImage
        {
            /// @brief Lista zawierająca każdą linie obrazu ASCII
            public List<string> pixels;

            /**
            * @brief Konstruktor, który wczytuje obraz ASCII z pliku
            * @param filename Nazwa pliku obrazu (bez rozszerzenia)
            * @details Wczytuje z "img/{filename}/{filename}.txt". Jeśli wczytywanie się nie powiedzie, używa pliku error.txt
            * i dodaje komunikat wyjątku do listy pikseli.
            */
            public ASCIIImage(string filename)
            {
                pixels = new List<string>();
                try
                {
                    StreamReader reader = new StreamReader("img//"+filename+"//"+filename+".txt");
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        pixels.Add(line);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    StreamReader reader = new StreamReader("img//error.txt");
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        pixels.Add(line);
                    }
                    pixels.Add(ex.Message);
                    reader.Close();
                }
            }
        }

        /**
          * @brief Wczytuje i zapisuje w pamięci podręcznej obraz ASCII z katalogu img
          * @param filename Nazwa pliku obrazu do wczytania (bez rozszerzenia)
          * @details Tworzy nową instancję ASCIIImage i przechowuje ją w pamięci podręcznej _images.
          * Obrazy są buforowane w celu wydajnego renderowania przy kolejnych wywołaniach.
          */
        public static void AddASCIIDrawing(string filename)
        {
            _images [filename] = new ASCIIImage(filename);
        }

        /**
        * @brief Renderuje zbuforowany obraz ASCII w określonej pozycji z opcjonalnym kolorowaniem
        * @param key Klucz pamięci podręcznej / nazwa pliku obrazu ASCII do wyrenderowania
        * @param x Pozycja na osi X, w której zostanie narysowany obraz
        * @param y Pozycja na osi Y, w której zostanie narysowany obraz
        * @param foreground Kolor pierwszego planu obrazu (domyślnie: biały)
        * @param background Kolor tła obrazu (domyślnie: czarny)
        * @details Jeśli dla obrazu istnieje plik maski (colorDoesntCount.txt), zostanie on zastosowany.
        * Maska używa znaków '!' do wskazania obszarów, w których spacje powinny nadpisywać obraz.
        * @throws KeyNotFoundException jeśli klucz obrazu nie zostanie znaleziony w pamięci podręcznej
        */
        public static void DrawASCII(string key,int x,int y,
        ConsoleColor foreground = ConsoleColor.White,
        ConsoleColor background = ConsoleColor.Black)
        {
            //Draw the image normally
            Env.SetColor(foreground, background);
            for (int i = 0; i < _images[key].pixels.Count; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(_images[key].pixels[i]);
            }
            Env.SetColor();

            if (!File.Exists($"img/{key}/colorDoesntCount.txt"))
            {
                //if mask doesnt exist
                return;
            }
            using StreamReader reader = new StreamReader($"img/{key}/colorDoesntCount.txt");
            int row = 0;
            //Apply mask in string runs
            while (!reader.EndOfStream)
            {
                string maskLine = reader.ReadLine();
                int col = 0;
                while (col < maskLine.Length)
                {
                    if (maskLine[col] != '!')
                    {
                        col++;
                        continue;
                    }
                    int start = col;
                    while (col < maskLine.Length && maskLine[col] == '!')
                        col++;

                    int length = col - start;
                    Console.SetCursorPosition(x + start, y + row);
                    Console.Write(new string(' ', length));
                }
                row++;
            }
            Env.SetColor();
        }
    }
}