using BattleshipZTP.Commands;
using BattleshipZTP.GameAssets;
using BattleshipZTP.Networking;
using BattleshipZTP.Observers;
using BattleshipZTP.Settings;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        INetworkingProxy _network;
        Window _windowShipmentList = new Window();
        NetworkingTaskState _taskState = NetworkingTaskState.None;
        public MultiplayerScenario(IGameMode gameMode, IScenario mainmenu)
        {
            _gameMode = gameMode;
            _mainScenario = mainmenu;
        }
        void WriteNickNameOnConsole(int x, int y, string nickname)
        {
            Env.CursorPos(x, y);
            Console.Write(nickname);
            Env.SetColor();
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
            TextBox box = new TextBox("IP", 15, "192.168.1.10");
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
        bool GameModeValidation(string mode1,string mode2)
        {
            if (mode1 != mode2)
            {
                Env.CursorPos(61, 21);
                Env.SetColor(ConsoleColor.DarkRed, ConsoleColor.Black);
                Console.WriteLine("Players choosen different game modes!");
                IWindowBuilder builder = new WindowBuilder();
                Env.SetColor();
                Window window = builder.SetPosition(76, 23)
                    .ColorHighlights(ConsoleColor.DarkRed, ConsoleColor.White)
                    .ColorBorders(ConsoleColor.Black, ConsoleColor.White)
                    .AddComponent(new Button("Return")).Build();
                builder.ResetBuilder();
                UIController controller = new UIController();
                controller.AddWindow(window);
                controller.DrawAndStart();
                return false;
            }
            return true;
        }

        async Task HandleConnection(TcpClient tcp, string role)
        {
            using var stream = tcp.GetStream();
            using var reader = new StreamReader(stream);
            using var writer = new StreamWriter(stream){ AutoFlush = true };
            _network = new NetworkingProxy(tcp,reader, writer,role);

            BattleBoard board;
            BattleBoard.BattleBoardProxy proxy;
            BattleBoard enemyBoard;
            BattleBoard.BattleBoardProxy enemyProxy;

            _ = Task.Run(async () =>
            {
                try
                {
                    while (true)
                    {
                        if(_taskState == NetworkingTaskState.None)
                        {
                            continue;
                        }
                        string line = await reader.ReadLineAsync();
                        if (_taskState == NetworkingTaskState.NameShipment) 
                        {
                            Env.SetColor();
                            if (OtherRole(role) == "Server")
                            {
                                Env.CursorPos(10, 23);
                            }
                            else
                            {
                                Env.CursorPos(60, 23);
                            }
                            if (line == null) { break; }
                            
                            Console.Write($"[{OtherRole(role)}] Otrzymano: {line}");
                        }
                        //if (line == null) { break; }
                        //Console.WriteLine($"\n[{OtherRole(role)}] Otrzymano: {line}");
                        //Console.WriteLine(">");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });


            //read and write gamemode ID
            (string,string) gameModes = await _network.NetworkWriteAndReadStrings(_gameMode.Id().ToString(), null);
            string myGameModeId = gameModes.Item1;
            string otherGameModeId = gameModes.Item2;

            //Game mode validation
            if (!GameModeValidation(myGameModeId, otherGameModeId))
            {
                _mainScenario.AsyncAct();
                return;
            }
            Console.Clear();
            Env.SetColor();

            //read and write players nicknames
            var names = await _network.NetworkWriteAndReadStrings(UserSettings.Instance.Nickname);
            string name1 = names.Item1;
            string name2 = names.Item2;
            CoordsToDrawBoard boardCoords = _gameMode.BoardCoords();

            //Placing Boards
            
            if (role == "Server")
            {
                Env.SetColor(ConsoleColor.Green);
                WriteNickNameOnConsole(boardCoords.XAxis_Player1, boardCoords.YAxis_Player1, name1);
                board = _gameMode.CreateBoard(boardCoords.XAxis_Player1, boardCoords.YAxis_Player1 + 1);
            }
            else
            {
                Env.SetColor(ConsoleColor.Green);
                WriteNickNameOnConsole(boardCoords.XAxis_Player2, boardCoords.YAxis_Player2, name1);
                board = _gameMode.CreateBoard(boardCoords.XAxis_Player2, boardCoords.YAxis_Player2 + 1);
            }
            proxy = new BattleBoard.BattleBoardProxy(board);
            Initialize(proxy);
            if (role == "Server")
            {
                Env.SetColor(ConsoleColor.Red);
                WriteNickNameOnConsole(boardCoords.XAxis_Player2, boardCoords.YAxis_Player2, name2);
                enemyBoard = _gameMode.CreateBoard(boardCoords.XAxis_Player2, boardCoords.YAxis_Player2 + 1);
            }
            else
            {
                Env.SetColor(ConsoleColor.Red);
                WriteNickNameOnConsole(boardCoords.XAxis_Player1, boardCoords.YAxis_Player1, name2);
                enemyBoard = _gameMode.CreateBoard(boardCoords.XAxis_Player1, boardCoords.YAxis_Player1 + 1);
            }
            enemyProxy = new BattleBoard.BattleBoardProxy(enemyBoard);
            Initialize(enemyProxy);

            //Shipment
            List<IShip> ships = _gameMode.ShipmentDelivery();
            List<IShip> enemyShips = _gameMode.ShipmentDelivery();
            int totalShipsToSink = ships.Count;
            if (_gameMode.RemeberArrowHit())
            {
                //BeautifyHelper.ApplyFancyBodies(ships);
                //BeautifyHelper.ApplyFancyBodies(enemyShips);
            }
            (int x, int y) tablePos = _gameMode.RemeberArrowHit() ? (71, 7) : (96, 22);
            DisplayShipmentTable(tablePos.x, tablePos.y, ships);
            UIController uI = new UIController();
            uI.AddWindow(_windowShipmentList);
            uI.DrawAndEndSequence();

            Drawing.SetColors(ConsoleColor.Black, ConsoleColor.Black);
            _taskState = NetworkingTaskState.NameShipment;
            Env.SetColor();
            foreach (IShip ship in ships)
            {
                uI.DrawAndEndSequence();
                PlaceCommand command = new PlaceCommand(board, ship, UserSettings.Instance.GetHashCode());
                var coords = proxy.PutCommand(command);
                command.Execute(coords);
                //uwu
                StringBuilder sb = new StringBuilder();
                sb.Append(ship.Name() + ";");
                foreach(var c in coords)
                {
                    sb.Append(c.x+"_"+c.y+";");
                }
                sb.Append(ship.GetBody().Count.ToString()+";");
                foreach(var b in ship.GetBody())
                {
                    sb.Append(b + ";");
                }

                await writer.WriteLineAsync(sb.ToString());
                //
                Drawing.DrawRectangleArea(tablePos.x, tablePos.y + 2, _windowShipmentList.Width, _windowShipmentList.Height);
                _windowShipmentList.Remove(0);
            }
            Drawing.DrawRectangleArea(tablePos.x - 1, tablePos.y, _windowShipmentList.Width + 6, _windowShipmentList.Height + 3);
            


            //await enemy place ships
            /*List<IShip> enemyShips = _gameMode.ShipmentDelivery(true);
            if (_gameMode.RemeberArrowHit())
            {
                BeautifyHelper.ApplyFancyBodies(enemyShips);
            }
            PlaceShipsRandomly(enemyProxy, enemyShips);*/

            /*Drawing.SetColors(ConsoleColor.Black, ConsoleColor.Black);
            Drawing.DrawRectangleArea(
                boardCoords.XAxis_Player2 + 1,
                boardCoords.YAxis_Player2 + 2,
                proxy.Width, proxy.Height
            );*/

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
