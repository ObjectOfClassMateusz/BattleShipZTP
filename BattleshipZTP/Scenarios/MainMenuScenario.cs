using BattleshipZTP.GameAssets;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Scenarios
{
    public class MainMenuScenario : Scenario
    {
        private List<string> _optionsSet;
        public MainMenuScenario(List<string> bringedOptions) : base() 
        { 
            _optionsSet = bringedOptions;
        }
        public override void Act()
        {
            base.Act();
            Console.OutputEncoding = Encoding.Unicode;

            Drawing.DrawASCII("mainMenuTitle", 20, 1, ConsoleColor.DarkGray);
            Drawing.DrawASCII("mainMenuShip", 41, 15, background: ConsoleColor.DarkGreen);

            IWindowBuilder builder = new WindowBuilder();
            UIDirector director = new UIDirector(builder);

            director.MainMenuInit();
            Window menu1 = builder.Build();
            builder.ResetBuilder();

            UIController controller = new UIController();
            controller.AddWindow(menu1);
            List<string> option = controller.DrawAndStart();
            IScenario scenario;

            switch (option.LastOrDefault())
            {
                case "Singleplayer":
                    scenario = new ChooseGameModeScenario();
                    scenario.Act();
                    break;
                case "Multiplayer":
                    break;
                case "Replay":
                    break;
                case "Options":
                    scenario = new OptionsScenario();
                    scenario.Act();
                    break;
                case "Authors":
                    break;
                case "Exit":
                    break;
            }
        }
    }
}
