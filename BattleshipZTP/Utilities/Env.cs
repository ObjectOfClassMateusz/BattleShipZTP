using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Utilities
{
    /**Interfejs środowiska i implementacja dla aplikacji konsolowych
     * @brief Ten interfejs definiuje metody interakcji ze środowiskiem, takie jak ustawianie kolorów konsoli, pozycjonowanie kursora i wprowadzanie opóźnień.
     */
    public interface IEnv//Enviroment Interface
    {
        /*Methods can be different for other software enviroment:
         * ConsoleApp
         * Unix Terminal
         * Unity
         * Godot C#
         * etc...
        */
        static abstract void SetColor(
            ConsoleColor Fcolor = ConsoleColor.White,
            ConsoleColor Bcolor = ConsoleColor.Black
        );
        static abstract void CursorPos(int x = 0, int y = 0);
        static abstract void Wait(int milisecs);
    }


    public class Env : IEnv
    {
        public static void SetColor(ConsoleColor Fcolor = ConsoleColor.White, ConsoleColor Bcolor = ConsoleColor.Black)
        {
            Console.ForegroundColor = Fcolor;
            Console.BackgroundColor = Bcolor;
        }
        public static void CursorPos(int x = 0, int y = 0)
        {
            int safeX = Math.Clamp(x, 0, Console.WindowWidth - 1);
            int safeY = Math.Clamp(y, 0, Console.WindowHeight - 1);
            
            Console.SetCursorPosition(x, y);
        }
        public static void Wait(int milisecs)
        {
            System.Threading.Thread.Sleep(milisecs);
        }
    }
}
