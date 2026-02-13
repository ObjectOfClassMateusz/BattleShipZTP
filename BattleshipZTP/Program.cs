using BattleshipZTP.GameAssets;
using BattleshipZTP.Scenarios;
using BattleshipZTP.Ship.Turrets;
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

            AudioManager.Instance.Add("ships/artillery");
            AudioManager.Instance.Add("ships/build");
            AudioManager.Instance.Add("ships/bolt");
            AudioManager.Instance.Add("ships/dair_of_destrc_laser");
            AudioManager.Instance.Add("ships/die");
            AudioManager.Instance.Add("ships/lead-shot");
            AudioManager.Instance.Add("ships/fire_prism");
            AudioManager.Instance.Add("ships/machine-gun");
            AudioManager.Instance.Add("ships/ravanger_shot");
            AudioManager.Instance.Add("ships/rilfes");
            AudioManager.Instance.Add("ships/shooting");
            AudioManager.Instance.Add("ships/plasma");
            AudioManager.Instance.Add("ships/fire");
            AudioManager.Instance.Add("ships/shuriken");
            AudioManager.Instance.Add("ships/vyper_rocket");
            
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
            AudioManager.Instance.Add("111", "ships/Saxony/SdKS49Grimbart/move");
            AudioManager.Instance.Add("112", "ships/Saxony/SdKS49Grimbart/move");
            AudioManager.Instance.Add("113", "ships/Saxony/SdKS49Grimbart/move");
            AudioManager.Instance.Add("107", "ships/Saxony/SdKS78Isegrim");
            AudioManager.Instance.Add("108", "ships/Saxony/SdKS78Isegrim");
            AudioManager.Instance.Add("109", "ships/Saxony/SdKS78Isegrim");
            AudioManager.Instance.Add("110", "ships/Saxony/SdKS78Isegrim");
            AudioManager.Instance.Add("599", "ships/Saxony/SdKS78Isegrim/move");
            AudioManager.Instance.Add("600", "ships/Saxony/SdKS78Isegrim/move");
            AudioManager.Instance.Add("601", "ships/Saxony/SdKS78Isegrim/move");
            AudioManager.Instance.Add("602", "ships/Saxony/SdKS78Isegrim/move");
            AudioManager.Instance.Add("603", "ships/Saxony/SdKS78Isegrim/move");
            AudioManager.Instance.Add("0333", "ships/Saxony/SdKS78Isegrim/attack");
            AudioManager.Instance.Add("0334", "ships/Saxony/SdKS78Isegrim/attack");
            AudioManager.Instance.Add("0335", "ships/Saxony/SdKS78Isegrim/attack");
            AudioManager.Instance.Add("0336", "ships/Saxony/SdKS78Isegrim/attack");
            AudioManager.Instance.Add("8q", "ships/Saxony/StormtrooperShip");
            AudioManager.Instance.Add("8w", "ships/Saxony/StormtrooperShip");
            AudioManager.Instance.Add("8e", "ships/Saxony/StormtrooperShip");
            AudioManager.Instance.Add("8r", "ships/Saxony/StormtrooperShip");
            AudioManager.Instance.Add("8t", "ships/Saxony/StormtrooperShip");
            AudioManager.Instance.Add("8y", "ships/Saxony/StormtrooperShip");
            AudioManager.Instance.Add("m60", "ships/Saxony/StormtrooperShip/move");
            AudioManager.Instance.Add("m61", "ships/Saxony/StormtrooperShip/move");
            AudioManager.Instance.Add("m62", "ships/Saxony/StormtrooperShip/move");
            AudioManager.Instance.Add("m63", "ships/Saxony/StormtrooperShip/move");
            AudioManager.Instance.Add("m64", "ships/Saxony/StormtrooperShip/move");
            AudioManager.Instance.Add("9011", "ships/Saxony/StormtrooperShip/attack");
            AudioManager.Instance.Add("9012", "ships/Saxony/StormtrooperShip/attack");
            AudioManager.Instance.Add("9013", "ships/Saxony/StormtrooperShip/attack");
            AudioManager.Instance.Add("9014", "ships/Saxony/StormtrooperShip/attack");
            AudioManager.Instance.Add("9015", "ships/Saxony/StormtrooperShip/attack");

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

            AudioManager.Instance.Add("402080", $"ships/Eldar/FirePrism");
            AudioManager.Instance.Add("402151", $"ships/Eldar/FirePrism");
            AudioManager.Instance.Add("402212", $"ships/Eldar/FirePrism/move");
            AudioManager.Instance.Add("402213", $"ships/Eldar/FirePrism/move");
            AudioManager.Instance.Add("402214", $"ships/Eldar/FirePrism/move");
            AudioManager.Instance.Add("402217", $"ships/Eldar/FirePrism/move");
            AudioManager.Instance.Add("402251", $"ships/Eldar/FirePrism/attack");
            AudioManager.Instance.Add("402254", $"ships/Eldar/FirePrism/attack");
            AudioManager.Instance.Add("402160", $"ships/Eldar/Vyper");
            AudioManager.Instance.Add("402161", $"ships/Eldar/Vyper");
            AudioManager.Instance.Add("402162", $"ships/Eldar/Vyper");
            AudioManager.Instance.Add("402212", $"ships/Eldar/Vyper/move");
            AudioManager.Instance.Add("402213", $"ships/Eldar/Vyper/move");
            AudioManager.Instance.Add("402214", $"ships/Eldar/Vyper/move");
            AudioManager.Instance.Add("402217", $"ships/Eldar/Vyper/move");
            AudioManager.Instance.Add("402251", $"ships/Eldar/Vyper/attack");
            AudioManager.Instance.Add("402252", $"ships/Eldar/Vyper/attack");
            AudioManager.Instance.Add("402255", $"ships/Eldar/Vyper/attack");
            AudioManager.Instance.Add("402140", $"ships/Eldar/Falcon");
            AudioManager.Instance.Add("402141", $"ships/Eldar/Falcon");
            AudioManager.Instance.Add("402212", $"ships/Eldar/Falcon/move");
            AudioManager.Instance.Add("402213", $"ships/Eldar/Falcon/move");
            AudioManager.Instance.Add("402214", $"ships/Eldar/Falcon/move");
            AudioManager.Instance.Add("402217", $"ships/Eldar/Falcon/move");
            AudioManager.Instance.Add("402251", $"ships/Eldar/Falcon/attack");
            AudioManager.Instance.Add("402252", $"ships/Eldar/Falcon/attack");
            AudioManager.Instance.Add("402255", $"ships/Eldar/Falcon/attack");

            AudioManager.Instance.Add("404570", $"ships/SpaceMarines/LandSpeeder");
            AudioManager.Instance.Add("404671", $"ships/SpaceMarines/LandSpeeder");
            AudioManager.Instance.Add("404700", $"ships/SpaceMarines/LandSpeeder/move");
            AudioManager.Instance.Add("404701", $"ships/SpaceMarines/LandSpeeder/move");
            AudioManager.Instance.Add("404702", $"ships/SpaceMarines/LandSpeeder/move");
            AudioManager.Instance.Add("404720", $"ships/SpaceMarines/LandSpeeder/attack");
            AudioManager.Instance.Add("404722", $"ships/SpaceMarines/LandSpeeder/attack");
            AudioManager.Instance.Add("404680", $"ships/SpaceMarines/LandRaider");
            AudioManager.Instance.Add("404681", $"ships/SpaceMarines/LandRaider");
            AudioManager.Instance.Add("404700", $"ships/SpaceMarines/LandRaider/move");
            AudioManager.Instance.Add("404701", $"ships/SpaceMarines/LandRaider/move");
            AudioManager.Instance.Add("404702", $"ships/SpaceMarines/LandRaider/move");
            AudioManager.Instance.Add("404720", $"ships/SpaceMarines/LandRaider/attack");
            AudioManager.Instance.Add("404722", $"ships/SpaceMarines/LandRaider/attack");
            AudioManager.Instance.Add("404350", $"ships/SpaceMarines/Dreadnought");
            AudioManager.Instance.Add("404351", $"ships/SpaceMarines/Dreadnought");
            AudioManager.Instance.Add("404353", $"ships/SpaceMarines/Dreadnought");
            AudioManager.Instance.Add("404355", $"ships/SpaceMarines/Dreadnought");
            AudioManager.Instance.Add("404362", $"ships/SpaceMarines/Dreadnought/move");
            AudioManager.Instance.Add("404363", $"ships/SpaceMarines/Dreadnought/move");
            AudioManager.Instance.Add("404365", $"ships/SpaceMarines/Dreadnought/move");
            AudioManager.Instance.Add("404370", $"ships/SpaceMarines/Dreadnought/attack");
            AudioManager.Instance.Add("404372", $"ships/SpaceMarines/Dreadnought/attack");
            AudioManager.Instance.Add("404431", $"ships/SpaceMarines/Dreadnought/attack");
            AudioManager.Instance.Add("404440", $"ships/SpaceMarines/Dreadnought/attack");

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
        }
    }
}