using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.UI
{
    public class MainMenuBuilder //: IWindowBuilder
    {
        public string Torch { get; set; }
        public MainMenuBuilder()
        {

            Torch = @"         /|
      /\/ |/\
      \  ^   | /\  /\
(\/\  / ^   /\/  )/^ )
 \  \/^ /\       ^  /
  )^       ^ \     (
 (   ^   ^      ^\  )
  \___\/____/______/
  [________________]
   |              |
   |     //\\     |
   |    <<()>>    |
   |     \\//     |
    \____________/
        |    |
        |    |
        |    |
        |    |
        |    |
        |    |
        |    |
            ";
        }
    }
}




/*
UIBuilder ui = new UIBuilder(ConsoleColor.White, ConsoleColor.Black);
Window window = new Window(2, 3,
    "Start", "Options", "Exit");
Window window2 = new Window(12, 12,
    "Start2", "Options", "Exit");
*/

//MainMenu main = new MainMenu();
//Console.WriteLine(main.Torch);

//Console.WriteLine(window.ButtonContent()[0]);



//ui.AddWindow(window);
//ui.AddWindow(window2);
//ui.DrawAndStart();
