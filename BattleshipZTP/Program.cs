using BattleshipZTP.UI;
using System;
using System.Text;

namespace BattleshipZTP
{
    public static class Env
    {
        public static void SetColor(ConsoleColor Fcolor = ConsoleColor.White, ConsoleColor Bcolor = ConsoleColor.Black)
        {
            Console.ForegroundColor = Fcolor;
            Console.BackgroundColor = Bcolor;
        }
        public static void CursorPos(int x=0, int y=0) {
            Console.SetCursorPosition(x, y);
        }
    }


    class Program
    {
        public static void Main(string[] args)
        {
            
            IWindowBuilder builder = new WindowsBuilder();
            UIDirector director = new UIDirector(builder);
            director.StandardWindow(1, 1, "SingePlayer", "MultiPlayer", "Replay", "Options", "Exit");
            Window menu1 = builder.Build();
            builder.ClearDataRef();

            builder.SetPosition(17, 5);
            builder.SetSize(19, 5);
            builder.ColorBorders(ConsoleColor.Yellow, ConsoleColor.Green);
            builder.ColorHighlights(ConsoleColor.Green,ConsoleColor.Red);
            builder.AddComponent(new Button("Attack"));
            builder.AddComponent(new Button("Move")  );
            builder.AddComponent(new Button("etc")   );
            Window menu2 = builder.Build();
            builder.ClearDataRef();

            UIController controller = new UIController();
            controller.AddWindow(menu1);
            controller.AddWindow(menu2);
            Console.WriteLine(controller.DrawAndStart());

        }
    }
}