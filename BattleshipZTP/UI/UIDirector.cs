using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.UI
{
    public class UIDirector
    {
        private IWindowBuilder _builder;
        public UIDirector(IWindowBuilder builder)
        {
            _builder = builder;
        }
        public void StandardWindowInit(int whereX , int whereY,params string[] options)
        {
            _builder.SetPosition(whereX, whereY)
            .SetSize()
            .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
            .ColorHighlights(ConsoleColor.White, ConsoleColor.Green);
            foreach (string op in options)
            {
                _builder.AddComponent(new Button(op));
            }
        }
        public void MainMenuInit()
        {
            _builder.SetPosition(65, 30)
            .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
            .ColorHighlights(ConsoleColor.White, ConsoleColor.Green);
            Button singleplayer = new Button("Singleplayer");
            singleplayer.SetMargin(7);
            _builder.AddComponent(singleplayer);
            Button multiplayer = new Button("Multiplayer");
            multiplayer.SetMargin(8);
            _builder.AddComponent(multiplayer);
            Button replay = new Button("Replay");
            replay.SetMargin(10);
            _builder.AddComponent(replay);
            Button options = new Button("Options");
            options.SetMargin(10);
            _builder.AddComponent(options);
            Button authors = new Button("Authors");
            authors.SetMargin(10);
            _builder.AddComponent(authors);
            Button exit = new Button("Exit");
            exit.SetMargin(11);
            _builder.AddComponent(exit);
        }
        
        public void AuthorsInit()
        {
            _builder.SetPosition(20, 2)
                .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
                .ColorHighlights(ConsoleColor.White, ConsoleColor.Green);
        }
    }
}
