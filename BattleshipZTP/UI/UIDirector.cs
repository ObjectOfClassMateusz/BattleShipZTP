using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.UI
{
    /**
     * @brief Class that shares ready to use UI complete set of components
     */
    public class UIDirector
    {
        private IWindowBuilder _builder;
        public UIDirector(IWindowBuilder builder)
        {
            _builder = builder;
        }
        /**
         * @brief creates an standard window at (X,Y) point with
         * multiple options as standard buttons
         * @param whereX X-position
         * @param whereY Y-position
         * @param options many strings related to make an buttons for window
         */
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
        /**
         * @brief creates an main menu at the start of program
         */
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
        /**
         * @brief setting an position and colors for authors list
         */
        public void AuthorsInit()
        {
            _builder.SetPosition(66, 2)
                .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
                .ColorHighlights(ConsoleColor.White, ConsoleColor.Green);
        }
    }
}
