using BattleshipZTP.Commands;
using BattleshipZTP.GameAssets;
using BattleshipZTP.Settings;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;

namespace BattleshipZTP.Scenarios
{
    public class SingleplayerScenario : Scenario
    {
        IGameMode _gameMode;
        Window _windowShipmentList = new Window();

        public SingleplayerScenario(IGameMode gameMode)
        {
            _gameMode = gameMode;
        }
        public override void Act()
        {
            base.Act();

            Env.CursorPos(2, 1);
            Console.WriteLine(UserSettings.Instance.Nickname);//Write NickName

            BattleBoard board = _gameMode.createBoard(2, 2);//Create a Board
            BattleBoardProxy proxy = new BattleBoardProxy(board);
            proxy.FieldsInitialization();
            proxy.Display();//Displays an Board

            Env.CursorPos(20, 1);
            Console.WriteLine("ai_enemy1");//enemy name
            BattleBoard enemy_board = _gameMode.createBoard(20, 2); 
            BattleBoardProxy enemy_proxy = new BattleBoardProxy(enemy_board);
            enemy_proxy.FieldsInitialization();
            enemy_proxy.Display();

            List<IShip> ships = _gameMode.ShipmentDelivery();

            (int x, int y) tablePos = (37, 2);
            Env.CursorPos(tablePos.x, tablePos.y);
            Console.WriteLine("Place the ships");
            Env.CursorPos(tablePos.x, tablePos.y+1);
            Console.WriteLine("on the board");
            IWindowBuilder windowBuilder = new WindowBuilder();
             windowBuilder
                .SetPosition(tablePos.x, tablePos.y + 2)
                .ColorHighlights(ConsoleColor.Yellow, ConsoleColor.DarkMagenta)
                .ColorBorders(ConsoleColor.DarkBlue, ConsoleColor.DarkRed);

            for(int i =0; i < ships.Count; i++)
            {
                windowBuilder.AddComponent(new TextOutput(ships[i].ToString()));
            }
            _windowShipmentList = windowBuilder.Build();
            UIController uI = new UIController();
            uI.AddWindow(_windowShipmentList);
            uI.DrawAndEndSequence();

            foreach (IShip sh in ships) 
            {
                uI.DrawAndEndSequence();
                PlaceCommand command = new PlaceCommand(board, sh , UserSettings.Instance.GetHashCode());
                var coords = proxy.PlaceCommand(command);
                command.Execute(coords);
                _windowShipmentList.Remove(0);
                Env.ClearRectangleArea(tablePos.x, tablePos.y + 2, 29, 10);
            }
            Env.ClearRectangleArea(tablePos.x, tablePos.y , 30, 12);




            



            // IShip refShip = ships[0];

            //Console.WriteLine(refShip.GetBody()[0].text);
            //Console.WriteLine(refShip.GetSize());
            //Console.WriteLine(refShip.GetColors().foreground);
            //Console.WriteLine(refShip.IsSunk());


            //AI places the ships
            //PlaceCommand


            // enemy_proxy.PlaceShipAI(
            //    ShipFactory.CreateShip(ShipType.Carrier),
            //     0,0);
            // enemy_proxy.Display();



            /*IWindowBuilder windowBuilder = new WindowBuilder();
            windowBuilder.SetPosition(37, 2)
                .SetSize(20)
                .ColorBorders(ConsoleColor.Black, ConsoleColor.DarkGray)
                .ColorHighlights(ConsoleColor.White, ConsoleColor.Green)
                .AddComponent(new TextOutput("Game backlogs"))
                .AddComponent(new Button("click me "));

            Window window = windowBuilder.Build();
            var controller = new UIController();
            controller.AddWindow(window);
            controller.DrawAndStart();

            window.Add(new TextOutput("Log:"));
            controller.DrawAndStart();
            window.Add(new TextOutput("Cos sie stało :D"));
            controller.DrawAndStart();
            window.Add(new TextOutput("Text3:"));
            controller.DrawAndStart();
            window.Add(new TextOutput("Text4:"));
            controller.DrawAndStart();
            window.Add(new TextOutput("Text5:"));
            controller.DrawAndStart();*/


        }
    }
}














/*Env.CursorPos(1, 39);
      Env.SetColor(ConsoleColor.DarkMagenta, ConsoleColor.Gray);
      Console.Write(" Action Points ");
      Env.CursorPos(17, 39);
      Env.SetColor(ConsoleColor.DarkBlue, ConsoleColor.DarkCyan);
      Console.Write(" Requisition ");
      Env.CursorPos(31, 39);
      Env.SetColor(ConsoleColor.DarkGreen, ConsoleColor.Green);
      Console.Write(" Energy ");*/