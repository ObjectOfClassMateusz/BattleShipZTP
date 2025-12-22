using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Scenarios
{
    public class OptionsScenario : Scenario
    {
        public OptionsScenario() : base() 
        {
            
        }
        public override void Act()
        {
            base.Act();
            //
            Drawing.DrawASCII("optionImg", 5,1);

            IWindowBuilder builder = new WindowBuilder();
            builder.SetPosition(20, 2)
            .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
            .ColorHighlights(ConsoleColor.White, ConsoleColor.Green)
            .AddComponent(new TextOutput("Enter you nickname below:"))
            .AddComponent(new TextBox("Nickname",20))
            .AddComponent(new IntegerSideBar("Music volume"))
            .AddComponent(new CheckBox("Turn off SFX"))
            .AddComponent(new Button("Save and Return"));
            
            Window optWindow = builder.Build();
            builder.ResetBuilder();
            UIController controller = new UIController();
            controller.AddWindow(optWindow);

            List<string> option = controller.DrawAndStart();
            IScenario scenario = new MainMenuScenario(option);
            scenario.Act();
        }
    }
}
