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
     * @brief Auxiliary class for storing coordinates for drawing the game board
     * @details This class contains X and Y coordinates for positions on the board of two players.
     * It provides serialization and deserialization capabilities for storing
     * and retrieving coordinates during multiplayer gameplay.
     */
    public class CoordsToDrawBoard
    {
        public readonly int XAxis_Player1;
        public readonly int YAxis_Player1;
        public readonly int XAxis_Player2;
        public readonly int YAxis_Player2;
        /**
         * @brief Constructor initializing board coordinates for both players
         * @param Pl1x X coordinate of player 1
         * @param Pl1y Y coordinate of player 1
         * @param Pl2x X coordinate of player 2
         * @param Pl2y Y coordinate of player 2 
         */
        public CoordsToDrawBoard(int Pl1x ,int Pl1y ,int Pl2x , int Pl2y)
        {
            XAxis_Player1 = Pl1x; YAxis_Player1 = Pl1y;
            XAxis_Player2 = Pl2x; YAxis_Player2 = Pl2y;
        }
        /**
         * @brief Serializes coordinates into a pipe-delimited string format
         * @return String representation in the format "X1|Y1|X2|Y2" 
         */
        public override string ToString()
        {
            return $"{XAxis_Player1}|{YAxis_Player1}|{XAxis_Player2}|{YAxis_Player2}";
        }
        /**
        * @brief Deserializes coordinates from a pipe-delimited text format
        * @paramdata String in the format "X1|Y1|X2|Y2"
        * @return CoordsToDrawBoard object with compressed coordinates
        * @throws ArgumentException if the data does not contain exactly 4 values separated by a vertical bar
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
    * @brief Utility class for rendering graphics in the console
    * @details Provides static methods for drawing primitive shapes, colored text, and ASCII images in the console with customizable colors and positions.
    * Maintains a cache of ASCII images for efficient rendering.
    */
    public class Drawing
    {
        /// @brief Stores the current foreground and background colors for drawing operations.
        static (ConsoleColor foreground, ConsoleColor background) _colors;

        /// @brief A dictionary cache for loaded ASCII images, indexed by filename.
        static readonly Dictionary<string, ASCIIImage> _images = new Dictionary<string, ASCIIImage>();
        /**
        * @brief Sets the default foreground and background colors for subsequent drawing operations.
        * @param foreground Foreground color to use
        * @param background Background color to use
        */
        public static void SetColors(ConsoleColor foreground, ConsoleColor background)
        {
            _colors.foreground = foreground;
            _colors.background = background;
        }
        /**
        * @brief Draws a character horizontally (left to right) at the specified position
        * @param character char to draw
        * @param count Number of times a character is repeated
        * @param x Starting position on the X-axis
        * @param y Y-axis position
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
          * @brief Draws a character vertically (top to bottom) at the specified position
          * @param character char to draw
          * @param count Number of times a character is repeated (creates a vertical line)
          * @param x X-axis position
          * @param y Starting position on the Y-axis
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
        * @brief Fills a rectangular area with spaces (creates a colored rectangle)
        * @param x Starting position of the rectangle on the X axis
        * @param y Starting position of the rectangle on the Y axis
        * @param w Width of the rectangle in characters
        * @param h Height of the rectangle in characters
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
        * @brief Private inner class for loading and storing ASCII images
        * @details Loads ASCII images from text files in the img directory with fallback support
        */
        class ASCIIImage
        {
            /// @brief A list containing each line of the ASCII image
            public List<string> pixels;
            /**
            * @brief Constructor that loads an ASCII image from a file
            * @param filename The image filename (without extension)
            * @details Loads from "img/{filename}/{filename}.txt". If loading fails, it uses the error.txt file
            * and adds an exception message to the pixel list.
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
        * @brief Loads and caches an ASCII image from the img directory.
        * @param filename The filename of the image to load (without extension).
        * @details Creates a new ASCIIImage instance and stores it in the _images cache.
        * Images are cached for efficient rendering on subsequent calls.
        */
        public static void AddASCIIDrawing(string filename)
        {
            _images [filename] = new ASCIIImage(filename);
        }
        /**
        * @brief Renders a cached ASCII image at the specified position, with optional coloring.
        * @param key Cache key/filename of the ASCII image to render.
        * @param x X-axis position at which the image will be drawn.
        * @param y Y-axis position at which the image will be drawn.
        * @param foreground Foreground color of the image (default: white).
        * @param background Background color of the image (default: black).
        * @details If a mask file (colorDoesntCount.txt) exists for the image, it will be applied.
        * The mask uses '!' characters to indicate areas where spaces should overwrite the image.
        * @throws KeyNotFoundException if the image key is not found in the cache.
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