using BattleshipZTP.Settings;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;

namespace BattleshipZTP.Scenarios
{
    public class OptionsScenario : Scenario
    {
        private IWindowBuilder _builder;
        private Window _window;
        private UIController _controller;
        private MainMenuScenario _menuScenario;

        public OptionsScenario() : base() 
        {
        }
        
        public override void ConnectScenario(string key, IScenario scenario)
        {
            base.ConnectScenario(key, scenario);
            _menuScenario = (MainMenuScenario)_scenarios.FirstOrDefault().Value;
        }

        public override void Act()
        {
            base.Act();
    
            _builder = new WindowBuilder();
            _builder.SetPosition(20, 2)
                .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
                .ColorHighlights(ConsoleColor.White, ConsoleColor.Green)
                .AddComponent(new TextOutput("Enter your nickname below:"))
                .AddComponent(new TextBox("Nickname", 12, UserSettings.Instance.Nickname))
                .AddComponent(new IntegerSideBar("Music volume", UserSettings.Instance.MusicVolume))
                .AddComponent(new CheckBox("Turn off Music", !UserSettings.Instance.MusicEnabled))
                .AddComponent(new CheckBox("Turn off SFX", !UserSettings.Instance.SfxEnabled))
                .AddComponent(new Button("Save and Return"));

            _window = _builder.Build();
            _controller = new UIController();
            _controller.AddWindow(_window);

            Drawing.DrawASCII("optionImg", 5, 1, ConsoleColor.DarkCyan, ConsoleColor.DarkBlue);
    
            List<string> option = _controller.DrawAndStart();
    
            UserSettings.Instance.UpdateSettings(option);
        }
    }
}
