using BattleshipZTP.GameAssets;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Scenarios
{
    public class ChooseGameModeScenario : Scenario
    {
        public ChooseGameModeScenario() : base(){ }
        public override void Act()
        {
            base.Act();
            GameModeFactory factory;

            IWindowBuilder builder = new WindowBuilder();
            UIDirector director = new UIDirector(builder);
            director.StandardWindowInit(69, 18, "Classic", "Single ship duel", "40K");
            Window window1 = builder.Build();
            builder.ResetBuilder();

            director.StandardWindowInit(73, 24, "Return");
            Window window2 = builder.Build();
            builder.ResetBuilder();

            UIController controller = new UIController();
            controller.AddWindow(window1);
            controller.AddWindow(window2);


            Drawing.DrawASCII("gameModeShip", 41, 0, ConsoleColor.Black , ConsoleColor.Red);
            Env.CursorPos(70, 15);
            Console.WriteLine("ℂ𝕙𝕠𝕠𝕤𝕖 𝔾𝕒𝕞𝕖 𝕄𝕠𝕕𝕖");
            

            List<string> options = controller.DrawAndStart();
            IScenario scenario;

            switch (options.FirstOrDefault())
            {
                case "Return":
                    scenario = new MainMenuScenario(new List<string>() { });
                    scenario.Act();
                    break;
                case "Classic":

                    break;
            }
        }
    }
}
