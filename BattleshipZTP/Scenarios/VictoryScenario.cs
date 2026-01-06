using BattleshipZTP.UI;
using BattleshipZTP.Settings;
using BattleshipZTP.Observers;
using BattleshipZTP.GameAssets;
using BattleshipZTP.Settings;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Scenarios
{
    public class VictoryScenario : Scenario
    {
        private readonly string _winnerName;
        private readonly int _winnerId;
        private readonly StatisticTracker _stats;

        public VictoryScenario(string winnerName, int winnerId, StatisticTracker stats)
        {
            _winnerName = winnerName;
            _winnerId = winnerId;
            _stats = stats;
        }

        public override void Act()
        {
            Console.Clear();
            var winnerStats = _stats.GetStats(_winnerId);

            AudioManager.Instance.ChangeVolume(
                "victory_sound", 
                UserSettings.Instance.MusicVolume
            );
            
            AudioManager.Instance.Stop("2-02 - Dark Calculation");
            AudioManager.Instance.Play("victory_sound");
            
            IWindowBuilder winBuilder = new WindowBuilder();
            winBuilder.SetPosition(Console.WindowWidth / 2 - 15, 5)
                .SetSize(30)
                .ColorBorders(ConsoleColor.Cyan, ConsoleColor.Black)
                .ColorHighlights(ConsoleColor.White, ConsoleColor.Blue)
                .AddComponent(new TextOutput("      BITWA ZAKONCZONA      "))
                .AddComponent(new TextOutput("----------------------------"))
                .AddComponent(new TextOutput($"  ZWYCIEZCA: {_winnerName.ToUpper()}  "))
                .AddComponent(new TextOutput("----------------------------"))
                .AddComponent(new TextOutput($" Celnosc: {winnerStats.Accuracy:F1}%"))
                .AddComponent(new TextOutput($" Trafienia: {winnerStats.Hits}"))
                .AddComponent(new TextOutput($" Pudla: {winnerStats.Misses}"))
                .AddComponent(new TextOutput("----------------------------"))
                .AddComponent(new Button("POWROT DO MENU"));

            Window winWindow = winBuilder.Build();
            UIController winUI = new UIController();
            winUI.AddWindow(winWindow);
            winUI.DrawAndStart();
        }
    }
}