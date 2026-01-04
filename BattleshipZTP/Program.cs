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
            Console.OutputEncoding = Encoding.Unicode;

            Drawing.AddASCII("mainMenuShip");
            Drawing.AddASCII("mainMenuTitle");
            Drawing.AddASCII("gameModeShip");
            Drawing.AddASCII("optionImg");

            AudioManager.Instance.Add("2-02 - Dark Calculation");
            AudioManager.Instance.Play("2-02 - Dark Calculation");

            IScenario main = new MainMenuScenario();
            IScenario options = new OptionsScenario();
            IScenario exit = new ExitScenario();
            IScenario authors = new AuthorsScenario();
            
            main.ConnectScenario("Options", options);
            main.ConnectScenario("Exit",exit);
            main.ConnectScenario("Authors", authors);
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