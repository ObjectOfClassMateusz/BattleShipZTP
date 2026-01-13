using BattleshipZTP.GameAssets;
using BattleshipZTP.Networking;
using BattleshipZTP.Ship;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using Microsoft.VisualBasic.FileIO;
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
            //Console.WriteLine("Choose game mode");
            Console.WriteLine("ℂ𝕙𝕠𝕠𝕤𝕖 𝔾𝕒𝕞𝕖 𝕄𝕠𝕕𝕖");

            GameModeFactory factory;

            List<string> option = controller.DrawAndStart();
            IScenario scenario;

            switch (option.FirstOrDefault())
            {
                case "Return":
                    _scenarios["Main"].Act();
                    break;
                case "Classic":
                    factory = new ClassicModeFactory();
                    var gameMode = factory.GetGameMode();
                    scenario = new SingleplayerScenario(gameMode, difficulty: ChooseDifficulty(),
                        8, _scenarios["Main"]);
                    scenario.Act();
                    break;
                case "Single ship duel":
                    factory = new DuelModeFactory();
                    var duelMode = factory.GetGameMode();
                    scenario = new SingleplayerScenario(duelMode, difficulty: ChooseDifficulty(),
                        1, _scenarios["Main"]);
                    scenario.Act();
                    break;
                case "40K":
                    var chooseRace = new SelectRaceScenario();
                    chooseRace.Act();
                    Fraction fraction = chooseRace.GetRace();

                    factory = new WarhammerModeFactory(chooseRace.GetRace());
                    var warhammerMode = factory.GetGameMode();
                    scenario = new SingleplayerScenario(warhammerMode, difficulty: ChooseDifficulty(),
                        6, _scenarios["Main"]);
                    scenario.Act();
                    break;
            }
        }
        private AIDifficulty ChooseDifficulty()
        {
            IWindowBuilder builder = new WindowBuilder();
            UIDirector director = new UIDirector(builder);
            director.StandardWindowInit(89, 16, "Easy", "Medium", "Hard");
            Window window = builder.Build();
            UIController controller = new UIController();
            controller.AddWindow(window);
            Env.CursorPos(89, 14);
            Console.WriteLine("Choose your AI difficulty level:");
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
