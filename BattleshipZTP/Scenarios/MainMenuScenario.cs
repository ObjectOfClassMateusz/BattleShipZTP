using BattleshipZTP.GameAssets;
using BattleshipZTP.Settings;
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
        public MainMenuScenario() : base() 
        { 
        }
        public override void Act()
        {
            base.Act();

            Drawing.DrawASCII("mainMenuTitle", 10, 1, ConsoleColor.DarkGray);
            Drawing.DrawASCII("mainMenuShip", 31, 12, background: ConsoleColor.DarkGreen);

            IWindowBuilder builder = new WindowBuilder();
            UIDirector director = new UIDirector(builder);

            director.MainMenuInit();
            Window menu1 = builder.Build();
            builder.ResetBuilder();

            UIController controller = new UIController();
            controller.AddWindow(menu1);
            Env.CursorPos();

            AudioManager.Instance.ChangeVolume(
                "2-02 - Dark Calculation", 
                UserSettings.Instance.MusicVolume
            );

            if (UserSettings.Instance.MusicEnabled == true)
            {
                AudioManager.Instance.Play("2-02 - Dark Calculation", true);
            }
            
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
                    _scenarios[option.LastOrDefault()].Act();
                    break;
                case "Authors":
                    _scenarios[option.LastOrDefault()].Act();
                    break;
                case "Exit":
                    _scenarios[option.LastOrDefault()].Act();
                    break;
            }
            //_scenarios[option.LastOrDefault()].Act();
        }
    }
}
