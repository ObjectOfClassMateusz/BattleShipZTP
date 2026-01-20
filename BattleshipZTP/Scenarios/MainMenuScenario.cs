using BattleshipZTP.GameAssets;
using BattleshipZTP.Settings;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;

namespace BattleshipZTP.Scenarios
{
    public class MainMenuScenario : Scenario
    {
        public MainMenuScenario() : base() 
        { 
        }
        public override async Task AsyncAct()
        {
            await base.AsyncAct();
            //Decoration for main menu scene
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

            AudioManager.Instance.ChangeVolume("2-02 - Dark Calculation", UserSettings.Instance.MusicVolume);
            if (UserSettings.Instance.MusicEnabled == true)
            {
                AudioManager.Instance.Play("2-02 - Dark Calculation", true);
            }
            string option = controller.DrawAndStart().LastOrDefault();
            await _scenarios[option].AsyncAct();
        }
    }
}