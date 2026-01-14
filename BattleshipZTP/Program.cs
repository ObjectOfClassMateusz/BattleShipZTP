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
            try
            {
                if (OperatingSystem.IsWindows())
                {
                    // Ustawiamy szerokość na co najmniej 140, żeby pozycja 123 była bezpieczna
                    Console.SetWindowSize(150, 45);
                    Console.SetBufferSize(150, 45);
                }
            }
            catch
            {
                // Jeśli laptop ma za mały ekran na 150 znaków, program przynajmniej się nie wywali
                Console.WriteLine("Uwaga: Nie można ustawić preferowanego rozmiaru okna.");
            }

            Console.SetWindowSize(130, 45); 
            Env.SetColor();
            Drawing.SetColors(ConsoleColor.White,ConsoleColor.Black);
            Env.Wait(300);
            Env.SetColor();
            Drawing.SetColors(ConsoleColor.White, ConsoleColor.Black);
            Console.Clear();
            Console.OutputEncoding = Encoding.Unicode;

            //Register all ASCII written images
            Drawing.AddASCIIDrawing("mainMenuShip");
            Drawing.AddASCIIDrawing("mainMenuTitle");
            Drawing.AddASCIIDrawing("gameModeShip");
            Drawing.AddASCIIDrawing("optionImg");
            Drawing.AddASCIIDrawing("skull");
            Drawing.AddASCIIDrawing("drukhari");
            Drawing.AddASCIIDrawing("bloodR");
            Drawing.AddASCIIDrawing("saxony");
            Drawing.AddASCIIDrawing("bieltan");

            //Register all sounds
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
            
            //Declare Scenarios
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
        }
    }
}