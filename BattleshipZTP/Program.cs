using BattleshipZTP.GameAssets;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using System;
using System.Collections.Concurrent;
using System.Text;

namespace BattleshipZTP
{
    class Program
    {
        public static void MainMenu()
        {
            IWindowBuilder builder = new WindowBuilder();
            UIDirector director = new UIDirector(builder);

            director.MainMenuInit();
            Window menu1 = builder.Build();
            builder.ResetBuilder();

            UIController controller = new UIController();
            controller.AddWindow(menu1);
            List<string> option = controller.DrawAndStart();
        }

        public static void Main(string[] args)
        {
            Env.Wait(1000);
            Env.SetColor();
            //MainMenu();




            IWindowBuilder builder = new WindowBuilder();
            UIDirector director = new UIDirector(builder);
            director.StandardWindowInit(1, 1, "SingePlayer", "MultiPlayer", "Replay", "Options", "Exit");
            Window menu1 = builder.Build();
            builder.ResetBuilder();

            builder.SetPosition(17, 5);
            builder.SetSize(19, 5);
            builder.ColorBorders(ConsoleColor.Yellow, ConsoleColor.Green);
            builder.ColorHighlights(ConsoleColor.Green, ConsoleColor.Red);
            builder.AddComponent(new Button("Attack"));
            builder.AddComponent(new CheckBox("Ok"));
            builder.AddComponent(new CheckBox("etc"));
            Window menu2 = builder.Build();
            builder.ResetBuilder();

            UIController controller = new UIController();
            controller.AddWindow(menu1);
            controller.AddWindow(menu2);
            var list = controller.DrawAndStart();
            Console.Clear();

            foreach (string option in list) {
                Console.WriteLine(option);
            }

            /*BattleBoard board = new BattleBoard(1,1);
            board.Display();
            BattleBoard enemy_board = new BattleBoard(1, 20);
            enemy_board.Display();
            Env.CursorPos(1,39);
            Env.SetColor(ConsoleColor.DarkMagenta, ConsoleColor.Gray);
            Console.Write(" Action Points ");
            Env.CursorPos(17, 39);
            Env.SetColor(ConsoleColor.DarkBlue, ConsoleColor.DarkCyan);
            Console.Write(" Requisition ");
            Env.CursorPos(31, 39);
            Env.SetColor(ConsoleColor.Green,ConsoleColor.DarkGreen);
            Console.Write(" Energy ");*/

            //Console.WriteLine(menu1.Width);

        }
    }
}