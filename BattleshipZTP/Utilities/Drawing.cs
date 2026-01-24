/*using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Utilities
{
    /**
    * @brief Helper class to store coordinates for drawing the game board
    
    public class CoordsToDrawBoard
    {
        public readonly int XAxis_Player1;
        public readonly int YAxis_Player1;
        public readonly int XAxis_Player2;
        public readonly int YAxis_Player2;

        public CoordsToDrawBoard(int Pl1x ,int Pl1y ,int Pl2x , int Pl2y)
        {
            XAxis_Player1 = Pl1x; YAxis_Player1 = Pl1y;
            XAxis_Player2 = Pl2x; YAxis_Player2 = Pl2y;
        }
        public override string ToString()
        {
            return $"{XAxis_Player1}|{YAxis_Player1}|{XAxis_Player2}|{YAxis_Player2}";
        }
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
    * @brief Class with static methods for drawing and ASCII images in the console
    *
    
    public class Drawing
    {
        static (ConsoleColor foreground, ConsoleColor background) _colors;
        public static void SetColors(ConsoleColor foreground, ConsoleColor background)
        {
            _colors.foreground = foreground;
            _colors.background = background;
        }
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

        class ASCIIImage
        {
            public List<string> pixels;
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

        static readonly Dictionary<string, ASCIIImage> _images = new Dictionary<string, ASCIIImage>();
        public static void AddASCIIDrawing(string filename)
        {
            _images [filename] = new ASCIIImage(filename);
        }
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
*/

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
    * @brief Helper class to store coordinates for drawing the game board
    * @details This class encapsulates the X and Y coordinates for two players' board positions.
    * It provides serialization and deserialization capabilities for storing and retrieving coordinates while playing multiplayer mode.
    */
    public class CoordsToDrawBoard
    {
        /// @brief X-axis coordinate for Player 1's board
        public readonly int XAxis_Player1;
        
        /// @brief Y-axis coordinate for Player 1's board
        public readonly int YAxis_Player1;
        
        /// @brief X-axis coordinate for Player 2's board
        public readonly int XAxis_Player2;
        
        /// @brief Y-axis coordinate for Player 2's board
        public readonly int YAxis_Player2;

        /**
        * @brief Constructor to initialize board coordinates for both players
        * @param Pl1x X-axis coordinate for Player 1
        * @param Pl1y Y-axis coordinate for Player 1
        * @param Pl2x X-axis coordinate for Player 2
        * @param Pl2y Y-axis coordinate for Player 2
        */
        public CoordsToDrawBoard(int Pl1x ,int Pl1y ,int Pl2x , int Pl2y)
        {
            XAxis_Player1 = Pl1x; YAxis_Player1 = Pl1y;
            XAxis_Player2 = Pl2x; YAxis_Player2 = Pl2y;
        }
        
        /**
        * @brief Serializes coordinates to a pipe-delimited string format
        * @return String representation in format "X1|Y1|X2|Y2"
        */
        public override string ToString()
        {
            return $"{XAxis_Player1}|{YAxis_Player1}|{XAxis_Player2}|{YAxis_Player2}";
        }
        
        /**
        * @brief Deserializes coordinates from a pipe-delimited string format
        * @param data String in format "X1|Y1|X2|Y2"
        * @return CoordsToDrawBoard object with parsed coordinates
        * @throws ArgumentException if data does not contain exactly 4 pipe-separated values
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
    * @brief Utility class for console-based graphics rendering
    * @details Provides static methods for drawing primitive shapes, colored text,
    * and ASCII art images to the console with customizable colors and positioning.
    * Maintains a cache of ASCII images for efficient rendering.
    */
    public class Drawing
    {
        /// @brief Stores current foreground and background colors for drawing operations
        static (ConsoleColor foreground, ConsoleColor background) _colors;
        
        /// @brief Cache dictionary for loaded ASCII art images indexed by filename
        static readonly Dictionary<string, ASCIIImage> _images = new Dictionary<string, ASCIIImage>();

        /**
        * @brief Sets the default foreground and background colors for subsequent drawing operations
        * @param foreground The foreground color to apply
        * @param background The background color to apply
        */
        public static void SetColors(ConsoleColor foreground, ConsoleColor background)
        {
            _colors.foreground = foreground;
            _colors.background = background;
        }

        /**
        * @brief Draws a character horizontally (left to right) at the specified position
        * @param character The character to draw
        * @param count Number of times to repeat the character
        * @param x X-axis starting position
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
        * @param character The character to draw
        * @param count Number of times to repeat the character (creates vertical line)
        * @param x X-axis position
        * @param y Y-axis starting position
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
        * @param x X-axis starting position of the rectangle
        * @param y Y-axis starting position of the rectangle
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
        * @brief Private inner class for loading and storing ASCII art images
        * @details Loads ASCII images from text files in the img directory with fallback error handling
        */
        class ASCIIImage
        {
            /// @brief List storing each line of the ASCII image
            public List<string> pixels;
            
            /**
            * @brief Constructor that loads an ASCII image from file
            * @param filename The filename of the image (without extension)
            * @details Loads from "img/{filename}/{filename}.txt". If load fails, falls back to error.txt
            * and appends the exception message to the pixels list.
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
        * @brief Loads and caches an ASCII art image from the img directory
        * @param filename The filename of the image to load (without extension)
        * @details Creates a new ASCIIImage instance and stores it in the _images cache.
        * Images are cached for efficient rendering on subsequent calls.
        */
        public static void AddASCIIDrawing(string filename)
        {
            _images [filename] = new ASCIIImage(filename);
        }

        /**
        * @brief Renders a cached ASCII art image at the specified position with optional coloring
        * @param key The cache key/filename of the ASCII image to render
        * @param x X-axis position where the image will be drawn
        * @param y Y-axis position where the image will be drawn
        * @param foreground The foreground color for the image (default: White)
        * @param background The background color for the image (default: Black)
        * @details If a mask file (colorDoesntCount.txt) exists for the image, it will be applied.
        * The mask uses '!' characters to indicate areas where spaces should overwrite the image.
        * @throws KeyNotFoundException if the image key is not found in the cache
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