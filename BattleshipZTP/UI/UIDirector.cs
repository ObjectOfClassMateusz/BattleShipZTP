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
        public void StandardWindow(int whereX , int WhereY,params string[] options)
        {
            foreach (string op in options)
            {
                _builder.SetPosition(whereX, WhereY);
                _builder.SetSize();
                _builder.ColorBorders(ConsoleColor.White, ConsoleColor.DarkBlue);
                _builder.ColorHighlights(ConsoleColor.White,ConsoleColor.Green);
                _builder.AddComponent(new Button(op));
            }
        }
        public void BuildMainMenu(int whereX, int WhereY, params string[] options)
        {

        }
    }
}
