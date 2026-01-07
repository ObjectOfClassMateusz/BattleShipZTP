using BattleshipZTP.GameAssets;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipZTP.Networking;

namespace BattleshipZTP.Scenarios
{
    public class ChooseGameModeScenario : Scenario
    {
        public ChooseGameModeScenario() : base(){ }
        public override void Act()
        {
            base.Act();
            IWindowBuilder builder = new WindowBuilder();
            UIDirector director = new UIDirector(builder);
            director.StandardWindowInit(69, 16, "Classic", "Single ship duel", "40K","Return");
            Window window1 = builder.Build();
            builder.ResetBuilder();
            Window window2 = builder.Build();
            builder.ResetBuilder();
            UIController controller = new UIController();
            controller.AddWindow(window1);
            controller.AddWindow(window2);
            Drawing.DrawASCII("gameModeShip", 41, 0, ConsoleColor.Black , ConsoleColor.Red);
            Env.CursorPos(70, 14);
            Console.WriteLine("Choose game mode");
            //Console.WriteLine("ℂ𝕙𝕠𝕠𝕤𝕖 𝔾𝕒𝕞𝕖 𝕄𝕠𝕕𝕖");

            GameModeFactory factory;

            List<string> options = controller.DrawAndStart();
            IScenario scenario;

            switch (options.FirstOrDefault())
            {
                case "Return":
                    scenario = new MainMenuScenario();
                    scenario.Act();
                    break;
                case "Classic":
                    factory = new ClassicModeFactory();
                    var gameMode = factory.GetGameMode();
                    scenario = new SingleplayerScenario(gameMode, difficulty: ChooseDifficulty(), 8);
                    scenario.Act();
                    break;
                case "Single ship duel":
                    factory = new DuelModeFactory();
                    var duelMode = factory.GetGameMode();
                    scenario = new SingleplayerScenario(duelMode, difficulty: ChooseDifficulty(), 1);
                    scenario.Act();
                    break;
            }
        }
        private AIDifficulty ChooseDifficulty()
        {
            IWindowBuilder builder = new WindowBuilder();
            UIDirector director = new UIDirector(builder);
            director.StandardWindowInit(90, 18, "Easy", "Medium", "Hard");
            Window window = builder.Build();
            UIController controller = new UIController();
            controller.AddWindow(window);
            Env.CursorPos(90, 17);
            Console.WriteLine("Wybierz poziom trudności AI:");
            List<string> options = controller.DrawAndStart();
            return options.FirstOrDefault() switch
            {
                "Easy" => AIDifficulty.Easy,
                "Medium" => AIDifficulty.Medium,
                "Hard" => AIDifficulty.Hard,
                _ => AIDifficulty.Easy
            };
        }
    }
}
