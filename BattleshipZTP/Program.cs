using BattleshipZTP.GameAssets;
using BattleshipZTP.Scenarios;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace BattleshipZTP
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Console.SetWindowSize(130, 45); 
            //Console.SetBufferSize(130, 100); 
            Env.SetColor();
            Drawing.SetColors(ConsoleColor.White,ConsoleColor.Black);
            Env.Wait(300);
            Env.SetColor();
            Drawing.SetColors(ConsoleColor.White, ConsoleColor.Black);
            Console.Clear();
            Console.OutputEncoding = Encoding.Unicode;

            Drawing.AddASCII("mainMenuShip");
            Drawing.AddASCII("mainMenuTitle");
            Drawing.AddASCII("gameModeShip");
            Drawing.AddASCII("optionImg");
            Drawing.AddASCII("skull");
            Drawing.AddASCII("drukhari");
            Drawing.AddASCII("bloodR");
            Drawing.AddASCII("saxony");
            Drawing.AddASCII("bieltan");

            AudioManager.Instance.Add("2-02 - Dark Calculation");
            AudioManager.Instance.Add("victory_sound");
            AudioManager.Instance.Add("miss");
            AudioManager.Instance.Add("wrong");
            AudioManager.Instance.Add("Pixel War Overlord");
            AudioManager.Instance.Add("przyciski");
            AudioManager.Instance.Add("stawianie");            
            AudioManager.Instance.Add("trafienie");
            AudioManager.Instance.Add("trafiony zatopiony");
            AudioManager.Instance.Add("2-11 - Blood of Man");
            
            IScenario main = new MainMenuScenario();
            IScenario options = new OptionsScenario();
            IScenario chooseGamemode = new ChooseGameModeScenario();
            IScenario exit = new ExitScenario();
            IScenario authors = new AuthorsScenario();
            
            main.ConnectScenario("Options", options);
            main.ConnectScenario("Exit",exit);
            main.ConnectScenario("Authors", authors);
            main.ConnectScenario("Singleplayer",chooseGamemode);

            chooseGamemode.ConnectScenario("Main", main);
            options.ConnectScenario("Main",main);
            authors.ConnectScenario("Main",main);

            main.Act();

            /*StatBar bar = new StatBar(500, ConsoleColor.Red, 3);
            bar.Show();
            Console.WriteLine();
            bar.Decrease(297);
            bar.Show();*/

        }
    }
}