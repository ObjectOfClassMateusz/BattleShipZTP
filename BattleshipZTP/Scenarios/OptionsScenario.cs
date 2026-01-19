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

        bool _musicCheckPresses;
        bool _sfxCheckPresses;

        public OptionsScenario() : base() 
        {
            _builder = new WindowBuilder();
            _builder.SetPosition(20, 2)
            .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
            .ColorHighlights(ConsoleColor.White, ConsoleColor.Green)
            .AddComponent(new TextOutput("Enter you nickname below:"))
            .AddComponent(new TextBox("Nickname", 12,UserSettings.Instance.Nickname))
            .AddComponent(new IntegerSideBar("Music volume",UserSettings.Instance.MusicVolume))
            .AddComponent(new CheckBox("Turn off Music"))
            .AddComponent(new CheckBox("Turn off SFX"))
            .AddComponent(new Button("Save and Return"));

            _window = _builder.Build();
            _builder.ResetBuilder();

            _controller = new UIController();
            _controller.AddWindow(_window);
        }
        public override void ConnectScenario(string key, IScenario scenario)
        {
            base.ConnectScenario(key, scenario);
            _menuScenario = (MainMenuScenario)_scenarios.FirstOrDefault().Value;
        }

        public override void Act()
        {
            base.Act();
            _musicCheckPresses = false;
            _sfxCheckPresses = false;
            Drawing.DrawASCII("optionImg", 5,1,ConsoleColor.DarkCyan,ConsoleColor.DarkBlue);
            List<string> option = _controller.DrawAndStart();
            UserSettings.Instance.UpdateSettings(option);
            //
            foreach (var o in option)
            {
                Console.WriteLine(o);
            }
            Console.ReadKey(true);
            //
            _menuScenario.AsyncAct();
        }
    }
}
