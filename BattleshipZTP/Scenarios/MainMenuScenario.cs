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

            //Decoration for main menu scene
            Drawing.DrawASCII("mainMenuTitle", 18, 1, ConsoleColor.DarkGray);
            Drawing.DrawASCII("mainMenuShip", 40, 12, background: ConsoleColor.DarkGreen);

            IWindowBuilder builder = new WindowBuilder();
            UIDirector director = new UIDirector(builder);

            director.MainMenuInit();
            Window menu1 = builder.Build();
            builder.ResetBuilder();

            UIController controller = new UIController();
            controller.AddWindow(menu1);
            Env.CursorPos();

            //Playing Dark Calculation
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
            _scenarios[option.LastOrDefault()].Act();
        }
    }
}
