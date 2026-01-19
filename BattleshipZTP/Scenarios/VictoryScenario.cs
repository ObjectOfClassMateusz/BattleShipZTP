using BattleshipZTP.GameAssets;
using BattleshipZTP.Observers;
using BattleshipZTP.Settings;
using BattleshipZTP.Settings;
using BattleshipZTP.UI;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Scenarios
{
    public class VictoryScenario : Scenario
    {
        private readonly string _winnerName;
        private readonly int _winnerId;
        private readonly StatisticTracker _stats;
        private readonly IBattleBoard _playerReplayBoard;
        private readonly IBattleBoard _enemyReplayBoard;
        private readonly int _height;
        private readonly int _width;

        public VictoryScenario(string winnerName, int winnerId, StatisticTracker stats, IBattleBoard pBoard, IBattleBoard eBoard, int height, int width)
        {
            _winnerName = winnerName;
            _winnerId = winnerId;
            _stats = stats;
            _playerReplayBoard = pBoard;
            _enemyReplayBoard = eBoard;
            _height = height;
            _width = width;
        }

        public VictoryScenario()
        {
        }

        public override void Act()
        {
            Env.Wait(100);
            base.Act();
            
            AudioManager.Instance.ChangeVolume(
                "victory_sound", 
                UserSettings.Instance.MusicVolume
            );
            
            var winnerStats = _stats.GetStats(_winnerId);

            if (UserSettings.Instance.MusicEnabled == true)
            {
                AudioManager.Instance.Stop("Pixel War Overlord");
            }

            if (UserSettings.Instance.SfxEnabled == true)
            {
                AudioManager.Instance.Play("victory_sound");

            }
            
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
                .AddComponent(new Button("POWTORKA BITWY"))
                .AddComponent(new Button("POWROT DO MENU"));
            
            Window winWindow = winBuilder.Build();
            UIController winUI = new UIController();
            winUI.AddWindow(winWindow);
            //winUI.DrawAndStart();
            
            List<string> results = winUI.DrawAndStart();
            string choice = results.LastOrDefault();

            if (choice == "POWTORKA BITWY")
            {
                new ReplayScenario(_stats.GetHistory(), _playerReplayBoard, _enemyReplayBoard, _height, _width, _scenarios["Main"]).Act();
                this.Act();
            }
            else if (choice == "POWROT DO MENU")
            {
                _scenarios["Main"].Act();
            }
        }
    }
}