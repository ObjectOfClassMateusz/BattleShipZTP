using BattleshipZTP.GameAssets;
using BattleshipZTP.Networking;
using BattleshipZTP.Ship;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;

namespace BattleshipZTP.Scenarios
{
    public class ChooseGameModeScenario : Scenario
    {
        bool _multi;
        public ChooseGameModeScenario(bool multi=false) : base()
        {
            _multi = multi;
        }
        public override async Task AsyncAct()
        {
            base.AsyncAct();
            IWindowBuilder builder = new WindowBuilder();
            UIDirector director = new UIDirector(builder);
            if (_multi) {
                director.StandardWindowInit(69, 16, "Classic", "Single ship duel", "40K", "Return");
            }else {
                director.StandardWindowInit(69, 16, "Classic", "Single ship duel", "40K", "Simulation", "Return");
            }
            Window window1 = builder.Build();
            builder.ResetBuilder();
            UIController controller = new UIController();
            controller.AddWindow(window1);
            Drawing.DrawASCII("gameModeShip", 41, 0, ConsoleColor.Black , ConsoleColor.Red);
            Env.CursorPos(70, 14);
            Console.WriteLine("Choose game mode");

            GameModeFactory factory;
            List<string> option = controller.DrawAndStart();
            IScenario scenario;
            switch (option.FirstOrDefault())
            {
                case "Return":
                    _scenarios["Main"].AsyncAct();
                    break;
                case "Classic":
                    factory = new ClassicModeFactory();
                    var gameMode = factory.GetGameMode();
                    if (_multi)
                    {
                        scenario = new MultiplayerScenario(gameMode, _scenarios["Main"]);
                        await scenario.AsyncAct();
                    }
                    else
                    {
                        scenario = new SingleplayerScenario(gameMode, difficulty: ChooseDifficulty(),_scenarios["Main"]);
                        scenario.Act();
                    }
                    break;
                case "Single ship duel":
                    factory = new DuelModeFactory();
                    var duelMode = factory.GetGameMode();
                    if (_multi)
                    {
                        scenario = new MultiplayerScenario(duelMode, _scenarios["Main"]);
                        await scenario.AsyncAct();
                    }
                    else
                    {
                        scenario = new SingleplayerScenario(duelMode, difficulty: ChooseDifficulty(), _scenarios["Main"]);
                        scenario.Act();
                    }
                    break;
                case "40K":
                    var chooseRace = new SelectRaceScenario();
                    chooseRace.Act();
                    Fraction fraction = chooseRace.GetRace();
                    factory = new WarhammerModeFactory(chooseRace.GetRace());
                    var warhammerMode = factory.GetGameMode();
                    if (_multi)
                    {
                        scenario = new MultiplayerScenario(warhammerMode, _scenarios["Main"]);
                        await scenario.AsyncAct();
                    }
                    else 
                    {
                        scenario = new SingleplayerScenario(warhammerMode, difficulty: ChooseDifficulty(), _scenarios["Main"]);
                        scenario.Act();
                    }
                    break;
                case "Simulation":
                    factory = new SimulationModeFactory();
                    var simulationMode = factory.GetGameMode();
                    scenario = new SimulationScenario(simulationMode, difficulty1: ChooseDifficulty(),difficulty2: ChooseDifficulty(),_scenarios["Main"]);
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
