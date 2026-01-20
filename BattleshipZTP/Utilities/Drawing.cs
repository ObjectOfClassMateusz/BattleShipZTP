using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Utilities
{
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
