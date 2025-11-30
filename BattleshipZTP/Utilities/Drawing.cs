using System;
using System.Collections.Generic;
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
        public static void DrawRight(char c, int count , int x , int y)
        {
            Env.SetColor(_colors.Item1,_colors.Item2);
            Env.CursorPos(x, y);
            for (int i = 0; i < count; i++)
            {
                Console.Write(c);
            }
            Env.SetColor();
        }
        public static void DrawDown(char c, int count, int x, int y)
        {
            Env.SetColor(_colors.Item1, _colors.Item2);
            for (int i = 0;i < count; i++)
            {
                Env.CursorPos(x, y + i);
                Console.Write(c);
            }
            Env.SetColor();
        }
        
    }
}
