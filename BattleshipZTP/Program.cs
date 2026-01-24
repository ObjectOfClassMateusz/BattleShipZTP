using BattleshipZTP.GameAssets;
using BattleshipZTP.Scenarios;
using BattleshipZTP.Utilities;
using System.Text;

namespace BattleshipZTP
{
    class Program
    {
        public static async Task Main(string[] args)
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
                Console.WriteLine("Note: Cannot set a preferred window size.");
            }

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
            AudioManager.Instance.Add("ships/ravanger_shot");
            AudioManager.Instance.Add("ships/dair_of_destrc_laser");

            //https://kingart-games.com/games/7-iron-harvest/
            AudioManager.Instance.Add("085", "ships/Saxony/EisenhansShip");
            AudioManager.Instance.Add("086", "ships/Saxony/EisenhansShip");
            AudioManager.Instance.Add("087", "ships/Saxony/EisenhansShip");
            AudioManager.Instance.Add("088", "ships/Saxony/EisenhansShip");
            AudioManager.Instance.Add("089", "ships/Saxony/EisenhansShip");
            AudioManager.Instance.Add("033", "ships/Saxony/EisenhansShip/attack");
            AudioManager.Instance.Add("034", "ships/Saxony/EisenhansShip/attack");
            AudioManager.Instance.Add("035", "ships/Saxony/EisenhansShip/attack");
            AudioManager.Instance.Add("011", "ships/Saxony/EisenhansShip/move");
            AudioManager.Instance.Add("012", "ships/Saxony/EisenhansShip/move");
            AudioManager.Instance.Add("013", "ships/Saxony/EisenhansShip/move");

            AudioManager.Instance.Add("12", "ships/Saxony/SdKS49Grimbart");
            AudioManager.Instance.Add("13", "ships/Saxony/SdKS49Grimbart");
            AudioManager.Instance.Add("14", "ships/Saxony/SdKS49Grimbart");
            AudioManager.Instance.Add("15", "ships/Saxony/SdKS49Grimbart");
            AudioManager.Instance.Add("66", "ships/Saxony/SdKS49Grimbart/attack");
            AudioManager.Instance.Add("67", "ships/Saxony/SdKS49Grimbart/attack");

            //https://sounds.spriters-resource.com/pc_computer/warhammer40000dawnofwar/
            AudioManager.Instance.Add("5000588", $"ships/DarkEldar/ReaverJetBike");
            AudioManager.Instance.Add("5000589b", $"ships/DarkEldar/ReaverJetBike");
            AudioManager.Instance.Add("5000590b", $"ships/DarkEldar/ReaverJetBike");
            AudioManager.Instance.Add("5000591", $"ships/DarkEldar/ReaverJetBike");
            AudioManager.Instance.Add("5000592", $"ships/DarkEldar/ReaverJetBike");
            AudioManager.Instance.Add("5000582b", $"ships/DarkEldar/ReaverJetBike/move");
            AudioManager.Instance.Add("5000583b", $"ships/DarkEldar/ReaverJetBike/move");
            AudioManager.Instance.Add("5000584", $"ships/DarkEldar/ReaverJetBike/move");
            AudioManager.Instance.Add("5000578b", $"ships/DarkEldar/ReaverJetBike/attack");
            AudioManager.Instance.Add("5000579", $"ships/DarkEldar/ReaverJetBike/attack");
            AudioManager.Instance.Add("5000580b", $"ships/DarkEldar/ReaverJetBike/attack");
            AudioManager.Instance.Add("5000619", $"ships/DarkEldar/Ravanger");
            AudioManager.Instance.Add("5000620b", $"ships/DarkEldar/Ravanger");
            AudioManager.Instance.Add("5000624b", $"ships/DarkEldar/Ravanger");
            AudioManager.Instance.Add("5000612", $"ships/DarkEldar/Ravanger/move");
            AudioManager.Instance.Add("5000613b", $"ships/DarkEldar/Ravanger/move");
            AudioManager.Instance.Add("5000609b", $"ships/DarkEldar/Ravanger/attack");
            AudioManager.Instance.Add("5000610b", $"ships/DarkEldar/Ravanger/attack");
            AudioManager.Instance.Add("5000611", $"ships/DarkEldar/Ravanger/attack");
            AudioManager.Instance.Add("5000615b", $"ships/DarkEldar/Ravanger/attack");
            AudioManager.Instance.Add("5000594b", $"ships/DarkEldar/Raider/attack");
            AudioManager.Instance.Add("5000595", $"ships/DarkEldar/Raider/attack");
            AudioManager.Instance.Add("5000598", $"ships/DarkEldar/Raider/move");
            AudioManager.Instance.Add("5000597", $"ships/DarkEldar/Raider/move");
            AudioManager.Instance.Add("5000596", $"ships/DarkEldar/Raider/move");
            AudioManager.Instance.Add("5000602", $"ships/DarkEldar/Raider");
            AudioManager.Instance.Add("5000603b", $"ships/DarkEldar/Raider");
            AudioManager.Instance.Add("5000604b", $"ships/DarkEldar/Raider");
            AudioManager.Instance.Add("5000636", $"ships/DarkEldar/DairOfDestruction");
            AudioManager.Instance.Add("5000637", $"ships/DarkEldar/DairOfDestruction");
            AudioManager.Instance.Add("5000638", $"ships/DarkEldar/DairOfDestruction");
            AudioManager.Instance.Add("5000630", $"ships/DarkEldar/DairOfDestruction/move");
            AudioManager.Instance.Add("5000631", $"ships/DarkEldar/DairOfDestruction/move");
            AudioManager.Instance.Add("5000641", $"ships/DarkEldar/DairOfDestruction/move");
            AudioManager.Instance.Add("5000626", $"ships/DarkEldar/DairOfDestruction/attack");
            AudioManager.Instance.Add("5000627", $"ships/DarkEldar/DairOfDestruction/attack");
            AudioManager.Instance.Add("5000629", $"ships/DarkEldar/DairOfDestruction/attack");

            //Declare Scenarios
            IScenario main = new MainMenuScenario();
            IScenario options = new OptionsScenario();
            IScenario singleplayer = new ChooseGameModeScenario();
            IScenario multiplayer = new ChooseGameModeScenario(true);
            IScenario exit = new ExitScenario();
            IScenario authors = new AuthorsScenario();

            main.ConnectScenario("Options", options);
            main.ConnectScenario("Exit",exit);
            main.ConnectScenario("Authors", authors);
            main.ConnectScenario("Singleplayer", singleplayer);
            singleplayer.ConnectScenario("Main", main);
            main.ConnectScenario("Multiplayer", multiplayer);
            multiplayer.ConnectScenario("Main", main);
            options.ConnectScenario("Main",main);
            authors.ConnectScenario("Main",main);

            await main.AsyncAct();
            /*try
            {
                await main.AsyncAct();
            }
            catch (Exception ex) 
            {
                Env.CursorPos(10, 10);
                Env.SetColor(ConsoleColor.Red, ConsoleColor.White);
                Console.WriteLine(ex);            
            }*/
        }
    }
}