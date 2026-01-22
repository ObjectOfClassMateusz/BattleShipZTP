using BattleshipZTP.Ship;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;

namespace BattleshipZTP.Scenarios
{
    public class SelectRaceScenario : Scenario
    {
        string _raceToString = "";
        public SelectRaceScenario(){}

        public Fraction GetRace()
        {
            return _raceToString switch
            {
                "Drukhari" => Fraction.Drukhari,
                "BielTan" => Fraction.BielTan,
                "BloodRavens" => Fraction.BloodRavens,
                "SaxonyEmpire" => Fraction.SaxonyEmpire,
                _ => throw new Exception("Non exist race")
            };
        }
        public override void Act()
        {
            base.Act();
            Drawing.DrawASCII("drukhari", 1, 1, ConsoleColor.Magenta);
            Drawing.DrawASCII("bieltan", 19, 24, ConsoleColor.Green);
            Drawing.DrawASCII("saxony", 42, 8, ConsoleColor.Yellow);
            Drawing.DrawASCII("bloodR", 26, 1, ConsoleColor.Red);

            IWindowBuilder builder = new WindowBuilder();
            UIDirector director = new UIDirector(builder);
            director.StandardWindowInit(89, 24, "Drukhari", "BielTan", "BloodRavens", "SaxonyEmpire");
            Window window = builder.Build();
            UIController controller = new UIController();
            controller.AddWindow(window);
            Env.CursorPos(89, 22);
            Console.WriteLine("Select Fraction:");
            List<string> options = controller.DrawAndStart();
            _raceToString = options.FirstOrDefault() ?? throw new Exception("Couldn't drawn out an option");
        }
    }
}
