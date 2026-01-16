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
            bool exitMenu = false;

            while (!exitMenu)
            {
                base.Act();
        
                if (UserSettings.Instance.MusicEnabled)
                {
                    AudioManager.Instance.ChangeVolume("2-02 - Dark Calculation", UserSettings.Instance.MusicVolume);
                    AudioManager.Instance.Play("2-02 - Dark Calculation", true);
                }
                else
                {
                    AudioManager.Instance.Stop("2-02 - Dark Calculation");
                }

                Drawing.DrawASCII("mainMenuTitle", 10, 1, ConsoleColor.DarkGray);
                Drawing.DrawASCII("mainMenuShip", 31, 12, background: ConsoleColor.DarkGreen);

                IWindowBuilder builder = new WindowBuilder();
                UIDirector director = new UIDirector(builder);
                director.MainMenuInit();
                Window menu1 = builder.Build();
        
                UIController controller = new UIController();
                controller.AddWindow(menu1);

                List<string> option = controller.DrawAndStart();
                string selected = option.LastOrDefault() ?? "";

                if (_scenarios.ContainsKey(selected))
                {
                    _scenarios[selected].Act(); 
                }
        
                if (selected == "Exit" || selected == "Classic" || selected == "Simulation") 
                {
                    exitMenu = true;
                }
            }
        }
    }
}
