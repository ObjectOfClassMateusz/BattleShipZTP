using BattleshipZTP.GameAssets;
using BattleshipZTP.Observers;
using BattleshipZTP.Settings;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using System.Net;
using System.Net.Sockets;
using System.Xml.Linq;

namespace BattleshipZTP.Scenarios
{
    /*public class Player
    {
        public Player() { }
    }*/

    public class MultiplayerScenario : Scenario
    {
        IGameMode _gameMode;
        IScenario _mainScenario;
        Window _windowShipmentList = new Window();
        public MultiplayerScenario(IGameMode gameMode, IScenario mainmenu)
        {
            _gameMode = gameMode;
            _mainScenario = mainmenu;
        }
        void WriteNickNameOnConsole(int x, int y, string nickname)
        {
            Env.CursorPos(x, y);
            Console.Write(nickname);
        }

        void DisplayShipmentTable(int x, int y, List<IShip> ships)
        {
            Env.CursorPos(x - 1, y);
            Console.WriteLine("Place the ships");
            Env.CursorPos(x, y + 1);
            Console.WriteLine("on the board");
            IWindowBuilder windowBuilder = new WindowBuilder();
            windowBuilder
               .SetPosition(x, y + 2)
               .ColorHighlights(ConsoleColor.Yellow, ConsoleColor.DarkMagenta)
               .ColorBorders(ConsoleColor.DarkBlue, ConsoleColor.DarkRed);

            for (int i = 0; i < ships.Count; i++)
            {
                windowBuilder.AddComponent(new TextOutput(ships[i].Name()));
            }
            _windowShipmentList = windowBuilder.Build();
        }

        void Initialize(BattleBoard.BattleBoardProxy board)
        {
            board.FieldsInitialization();
            board.Display();
        }

        bool IsValidHostIPv4(string ipAddress)
        {
            Drawing.DrawRectangleArea(68, 21, 30, 1);
            Env.SetColor(ConsoleColor.DarkRed, ConsoleColor.Black);
            Env.CursorPos(68, 21);
            // Check if valid IPv4 format
            if (!IPAddress.TryParse(ipAddress, out IPAddress ip))
            {
                Console.Write("Address is not valid");
                return false;
            }
            // Ensure it's IPv4
            if (ip.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
            {
                Console.Write("IPv4 is only handled");
                return false;
            }
            byte[] bytes = ip.GetAddressBytes();

            // Reject network (x.x.x.0) and broadcast (x.x.x.255) addresses
            if (bytes[3] == 0 || bytes[3] == 255)
            {
                Console.Write("Host address required");
                return false;
            }
            return true;
        }

        string OtherRole(string role)
        {
            return role == "Server" ? "Client" : "Server";
        }
        async Task RunServer()
        {
            const int port = 5000;
            var listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Env.CursorPos(61, 15);
            Env.SetColor(ConsoleColor.Green, ConsoleColor.Black);
            Console.WriteLine("The server is waiting for a connection");
            using TcpClient client = await listener.AcceptTcpClientAsync();
            Env.CursorPos(61, 16);
            Console.WriteLine("Connected to Client");
            await HandleConnection(client, "Server");
        }
        async Task RunClient()
        {
            const int port = 5000;
            IWindowBuilder windowBuilder = new WindowBuilder();
            Button button = new Button("Ok");
            button.SetMargin(3);
            TextBox box = new TextBox("IP", 15, "0.0.0.0");
            box.SetMargin(3);
            TextOutput output = new TextOutput("Enter the server IP");
            output.SetMargin(3);
            windowBuilder
                .SetPosition(66, 15)
                .SetSize(24)
                .ColorBorders(ConsoleColor.Blue, ConsoleColor.DarkBlue)
                .ColorHighlights(ConsoleColor.Black, ConsoleColor.Cyan)
                .AddComponent(output)
                .AddComponent(box)
                .AddComponent(button);
            Window window = windowBuilder.Build();
            windowBuilder.ResetBuilder();
            UIController controller = new UIController();
            controller.AddWindow(window);
            List<string> option;
            string serverIp;
            Drawing.SetColors(ConsoleColor.Black,ConsoleColor.Black);
            while (true) 
            {
                option = controller.DrawAndStart();
                if (option.Count == 1)
                    continue;
                serverIp = option[0].Split(new[] { "#:" }, StringSplitOptions.None)[1];
                if (IsValidHostIPv4(serverIp))
                {
                    break;
                }
            }
            Env.SetColor(ConsoleColor.Green, ConsoleColor.Black);
            using TcpClient client = new TcpClient();
            Console.WriteLine("[...] Client Connecting");
            Env.Wait(2137);
            await client.ConnectAsync(IPAddress.Parse(serverIp), port);
            Env.CursorPos(68, 21);
            Console.WriteLine("Connected to server");

            await HandleConnection(client, "Client");
        }

        async Task HandleConnection(TcpClient tcp, string role)
        {
            using var stream = tcp.GetStream();
            using var reader = new StreamReader(stream);
            using var writer = new StreamWriter(stream) { AutoFlush = true };

            string myGameModeId = _gameMode.Id().ToString();
            string otherGameModeId = null;
            if (role == "Server")
            {
                await writer.WriteLineAsync(myGameModeId);
                otherGameModeId = await reader.ReadLineAsync();
            }
            else
            {
                otherGameModeId = await reader.ReadLineAsync();
                await writer.WriteLineAsync(myGameModeId);
            }

            if(myGameModeId != otherGameModeId)
            {
                Env.CursorPos(61, 21);
                Env.SetColor(ConsoleColor.DarkRed, ConsoleColor.Black);
                Console.WriteLine("Players choosen different game modes!");
                IWindowBuilder builder = new WindowBuilder();
                Env.SetColor();
                Window window = builder.SetPosition(76, 23)
                    .ColorHighlights(ConsoleColor.DarkRed,ConsoleColor.White)
                    .ColorBorders(ConsoleColor.Black,ConsoleColor.White)
                    .AddComponent(new Button("Return")).Build();
                builder.ResetBuilder();
                UIController controller = new UIController();
                controller.AddWindow(window);
                controller.DrawAndStart();
                _mainScenario.AsyncAct();
                return;
            }
            Console.Clear();

            CoordsToDrawBoard boardCoords = _gameMode.BoardCoords();

            string name1 = UserSettings.Instance.Nickname;
            string name2 = null;
            if (role == "Server")
            {
                await writer.WriteLineAsync(name1);
                name2 = await reader.ReadLineAsync();
            }
            else
            {
                name2 = await reader.ReadLineAsync();
                await writer.WriteLineAsync(name1);
            }
            WriteNickNameOnConsole(boardCoords.XAxis_Player1, boardCoords.YAxis_Player1, name1);
            BattleBoard board = _gameMode.CreateBoard(boardCoords.XAxis_Player1, boardCoords.YAxis_Player1 + 1);
            BattleBoard.BattleBoardProxy proxy = new BattleBoard.BattleBoardProxy(board);
            Initialize(proxy);
            WriteNickNameOnConsole(boardCoords.XAxis_Player2, boardCoords.YAxis_Player2, name2);
            BattleBoard enemyBoard = _gameMode.CreateBoard(boardCoords.XAxis_Player2, boardCoords.YAxis_Player2 + 1);
            BattleBoard.BattleBoardProxy enemyProxy = new BattleBoard.BattleBoardProxy(enemyBoard);
            Initialize(enemyProxy);

            List<IShip> ships = _gameMode.ShipmentDelivery();
            int totalShipsToSink = ships.Count;
            Console.WriteLine(totalShipsToSink);
            //List<IShip> enemyShips  = _gameMode.ShipmentDelivery();
            //int totalShipsToSink = enemyShips.Count;








            Console.ReadKey(true);
            tcp.Close();
        }

        public override async Task AsyncAct()
        {
            ActionManager.Instance.ClearObservers();
            if (UserSettings.Instance.MusicEnabled)
            {
                AudioManager.Instance.Stop("2-02 - Dark Calculation");
                AudioManager.Instance.ChangeVolume(_gameMode.GameThemeAudio(), UserSettings.Instance.MusicVolume);
                AudioManager.Instance.Play(_gameMode.GameThemeAudio(), true);
            }
            await base.AsyncAct();
            IWindowBuilder builder = new WindowBuilder();
            builder
                .SetPosition(65, 9)
                .SetSize(28)
                .ColorBorders(ConsoleColor.Blue, ConsoleColor.DarkBlue)
                .ColorHighlights(ConsoleColor.Black, ConsoleColor.Cyan)
                .AddComponent(new TextOutput("Select network role:"))
                .AddComponent(new Button("Server"))
                .AddComponent(new Button("Client"));
            Window roleWindow = builder.Build();
            builder.ResetBuilder();

            UIController controller = new UIController();
            controller.AddWindow(roleWindow);
            string role = controller.DrawAndStart().LastOrDefault();
            if(role == "Server")
            {
                await RunServer();
            }
            else
            {
                await RunClient();
            }
        }
    }
}
