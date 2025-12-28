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
            _scenarios.Add("Options", new OptionsScenario());
        }
        public override void Act()
        {
            base.Act();
            
            Drawing.DrawASCII("mainMenuTitle", 20, 1, ConsoleColor.DarkGray);
            Drawing.DrawASCII("mainMenuShip", 41, 15, background: ConsoleColor.DarkGreen);

            IWindowBuilder builder = new WindowBuilder();
            UIDirector director = new UIDirector(builder);

            director.MainMenuInit();
            Window menu1 = builder.Build();
            builder.ResetBuilder();

            UIController controller = new UIController();
            controller.AddWindow(menu1);
            Env.CursorPos();

            string? volume = _optionsSet.FirstOrDefault(s => s.Contains("Music volume"));
            string value = volume?.Split(new[] { "#:" }, StringSplitOptions.None)[1] ?? "0";
            int v = Convert.ToInt32(value);
            AudioManager.Instance.ChangeVolume("2-02 - Dark Calculation", v);

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
                    scenario = new AuthorsScenario();
                    scenario.Act();
                    break;
                case "Exit":
                    break;
            }
        }
    }
}
