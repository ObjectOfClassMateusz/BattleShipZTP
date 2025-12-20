using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Utilities
{
    public class Drawing
    {
        private static (ConsoleColor, ConsoleColor) _colors;
        public static void SetColors(ConsoleColor foreground, ConsoleColor background)
        {
            _colors.Item1 = foreground;
            _colors.Item2 = background;
        }
        public static void DrawRight(char character, int count , int x , int y)
        {
            Env.SetColor(_colors.Item1,_colors.Item2);
            Env.CursorPos(x, y);
            for (int i = 0; i < count; i++)
            {
                Console.Write(character);
            }
            Env.SetColor();
        }
        public static void DrawDown(char character, int count, int x, int y)
        {
            Env.SetColor(_colors.Item1, _colors.Item2);
            for (int i = 0;i < count; i++)
            {
                Env.CursorPos(x, y + i);
                Console.Write(character);
            }
            Env.SetColor();
        }

        private class ASCIIImage
        {
            public List<string> pixels;
            public ASCIIImage(string filename)
            {
                pixels = new List<string>();
                try
                {
                    StreamReader reader = new StreamReader("img//" + filename);
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        pixels.Add(line);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    StreamReader reader = new StreamReader("img//_.txt");
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        pixels.Add(line);
                    }
                    reader.Close();
                }
            }
        }
        static private readonly Dictionary<string, ASCIIImage> _images = new Dictionary<string, ASCIIImage>();
        public static void AddASCII(string filename)
        {
            _images [filename] = new ASCIIImage(filename);
        }
        public static void DrawASCII(string key ,
            int x , int y ,
            ConsoleColor foreground=ConsoleColor.White , ConsoleColor background=ConsoleColor.Black)
        {
            int i = 0;
            Env.SetColor(foreground,background);
            foreach (string k in _images[key].pixels)
            {
                Console.SetCursorPosition(x, y + i);
                i++;
                Console.Write(k);
            }
        }
    }
}
