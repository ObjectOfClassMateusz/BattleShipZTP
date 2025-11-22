using BattleshipZTP.UI;
using System;

namespace BattleshipZTP
{
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
            builder.SetSize(10, 10);
            builder.ColorBorders(ConsoleColor.Yellow, ConsoleColor.Green);
            builder.AddComponent(new Button() { option = "Attack" });
            builder.AddComponent(new Button() { option = "Move" });
            builder.AddComponent(new Button() { option = "etc" });
            Window menu2 = builder.Build();
            builder.ClearDataRef();

            UIController controller = new UIController(ConsoleColor.Black, ConsoleColor.Magenta);
            controller.AddWindow(menu1);
            controller.AddWindow(menu2);
            Console.WriteLine(controller.DrawAndStart());
            




        }
    }
}