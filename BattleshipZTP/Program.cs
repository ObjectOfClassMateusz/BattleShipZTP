using BattleshipZTP.GameAssets;
using BattleshipZTP.Scenarios;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

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
                    Console.SetWindowSize(152, 45);
                    Console.SetBufferSize(152, 45);
                }
            }
            catch
            {
                Console.WriteLine("Uwaga: Nie można ustawić preferowanego rozmiaru okna.");
            }

            //Console.SetWindowSize(130, 45); 
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

            AudioManager.Instance.Add("ships/shuriken");
            AudioManager.Instance.Add("ships/build");
            AudioManager.Instance.Add("ships/artillery");

            AudioManager.Instance.Add("085", "ships/Saxony/EisenhansShip");
            AudioManager.Instance.Add("086", "ships/Saxony/EisenhansShip");
            AudioManager.Instance.Add("087", "ships/Saxony/EisenhansShip");
            AudioManager.Instance.Add("088", "ships/Saxony/EisenhansShip");
            AudioManager.Instance.Add("089", "ships/Saxony/EisenhansShip");
            AudioManager.Instance.Add("033", "ships/Saxony/EisenhansShip/attack");
            AudioManager.Instance.Add("034", "ships/Saxony/EisenhansShip/attack");
            AudioManager.Instance.Add("035", "ships/Saxony/EisenhansShip/attack");

            AudioManager.Instance.Add("5000588", $"ships/DarkEldar/ReaverJetBike");
            AudioManager.Instance.Add("5000589b", $"ships/DarkEldar/ReaverJetBike");
            AudioManager.Instance.Add("5000590b", $"ships/DarkEldar/ReaverJetBike");
            AudioManager.Instance.Add("5000591", $"ships/DarkEldar/ReaverJetBike");
            AudioManager.Instance.Add("5000592", $"ships/DarkEldar/ReaverJetBike");
            AudioManager.Instance.Add("5000619", $"ships/DarkEldar/Ravanger");
            AudioManager.Instance.Add("5000620b", $"ships/DarkEldar/Ravanger");
            AudioManager.Instance.Add("5000624b", $"ships/DarkEldar/Ravanger");
            AudioManager.Instance.Add("5000602", $"ships/DarkEldar/Raider");
            AudioManager.Instance.Add("5000603b", $"ships/DarkEldar/Raider");
            AudioManager.Instance.Add("5000604b", $"ships/DarkEldar/Raider");
            AudioManager.Instance.Add("5000636", $"ships/DarkEldar/DairOfDestruction");
            AudioManager.Instance.Add("5000637", $"ships/DarkEldar/DairOfDestruction");
            AudioManager.Instance.Add("5000638", $"ships/DarkEldar/DairOfDestruction");

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