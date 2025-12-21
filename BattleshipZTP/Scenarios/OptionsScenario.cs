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
                .SetSize(24)
            .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
            .ColorHighlights(ConsoleColor.White, ConsoleColor.Green)
            .AddComponent(new CheckBox("Turn on/off music"))
            .AddComponent(new CheckBox("Turn on/off SFX"))
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
