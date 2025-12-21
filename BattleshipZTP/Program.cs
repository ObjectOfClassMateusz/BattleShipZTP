using BattleshipZTP.GameAssets;
using BattleshipZTP.Scenarios;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using System;
using System.Collections.Concurrent;
using System.Text;

namespace BattleshipZTP
{
    class Program
    {
        public static void Main(string[] args)
        {
            /*Drawing.AddASCII("mainMenuShip");
            Drawing.AddASCII("mainMenuTitle");
            Drawing.AddASCII("gameModeShip");
            Drawing.AddASCII("optionImg");
            AudioManager audio = new AudioManager();
            //audio.Play("2-02 - Dark Calculation");
            IScenario main = new MainMenuScenario(
            new List<string>()
            {

            });
            main.Act();*/

            Console.OutputEncoding = Encoding.Unicode;
            IWindowBuilder builder = new WindowBuilder();
            UIDirector director = new UIDirector(builder);
            director.StandardWindowInit(1, 1, "SingePlayer", "MultiPlayer", "Replay", "Options", "Exit");
            Window menu1 = builder.Build();
            builder.ResetBuilder();
            builder.SetSize(19);
            builder.SetPosition(17, 5);
            builder.ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray);
            builder.ColorHighlights(ConsoleColor.Black, ConsoleColor.White);
            builder.AddComponent(new Button("Attack"));
            builder.AddComponent(new TextBox("Name",5));
            builder.AddComponent(new TextBox("Surname", 9));
            builder.AddComponent(new CheckBox("Ok"));
            builder.AddComponent(new CheckBox("etc"));
            builder.AddComponent(new IntegerSideBar("Volume"));
            builder.AddComponent(new IntegerSideBar("Cash"));
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
            



            
           /* BattleBoard board = new BattleBoard(1,20);
            BattleBoardProxy proxy = new BattleBoardProxy(board);
            proxy.FieldsInitialization();
            proxy.Display();
            BattleBoard enemy_board = new BattleBoard(1, 1);
            BattleBoardProxy enemy_proxy = new BattleBoardProxy(enemy_board);
            enemy_proxy.FieldsInitialization();
            enemy_proxy.Display();
            Env.CursorPos(1, 39);
            Env.SetColor(ConsoleColor.DarkMagenta, ConsoleColor.Gray);
            Console.Write(" Action Points ");
            Env.CursorPos(17, 39);
            Env.SetColor(ConsoleColor.DarkBlue, ConsoleColor.DarkCyan);
            Console.Write(" Requisition ");
            Env.CursorPos(31, 39);
            Env.SetColor(ConsoleColor.DarkGreen, ConsoleColor.Green);
            Console.Write(" Energy ");
            board.PlaceShip();
            proxy.Display();
            board.PlaceShip();
            proxy.Display();*/

            /*StatBar bar = new StatBar(500, ConsoleColor.Red, 3);
            bar.Show();
            Console.WriteLine();
            bar.Decrease(297);
            bar.Show();*/

        }
    }
}