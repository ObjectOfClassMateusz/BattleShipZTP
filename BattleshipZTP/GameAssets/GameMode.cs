using BattleshipZTP.Ship;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;

namespace BattleshipZTP.GameAssets
{
    public interface IGameMode
    {
        BattleBoard CreateBoard(int x , int y);
        bool RemeberArrowHit();
        Dictionary<string, int> AssignResources();

        CoordsToDrawBoard BoardCoords();
        string GameThemeAudio();
        List<IShip> ShipmentDelivery();
        List<(int x , int y)> GetShipmentPlacementCoords();
        List<int> GetShipSizes();
    }

    public class ClassicGameMode : IGameMode
    {
        readonly List<(int x, int y)> _coords = new List<(int x, int y)>();

        public ClassicGameMode(){}

        public CoordsToDrawBoard BoardCoords() => new CoordsToDrawBoard(52, 7, 88, 7);

        public string GameThemeAudio() => "Pixel War Overlord";

        public BattleBoard CreateBoard(int x , int y) 
            => new BattleBoard(x,y,12,12);

        public bool RemeberArrowHit() 
            => true;

        public Dictionary<string, int> AssignResources() 
            => new Dictionary<string, int>();
        
        public List<IShip> ShipmentDelivery() => new List<IShip>()
        {
            ShipFactory.CreateShip(ShipType.Carrier),
            ShipFactory.CreateShip(ShipType.Battleship),
            ShipFactory.CreateShip(ShipType.Battleship),
            ShipFactory.CreateShip(ShipType.Destroyer),
            ShipFactory.CreateShip(ShipType.Destroyer),
            ShipFactory.CreateShip(ShipType.Destroyer),
            ShipFactory.CreateShip(ShipType.Submarine),
            ShipFactory.CreateShip(ShipType.Submarine)
        };
        public List<(int x, int y)> GetShipmentPlacementCoords()
        {
            return _coords;
        }

        public List<int> GetShipSizes()
        {
            return new List<int> { 5, 4, 3, 3, 1 };
        }
    }

    public class DuelGameMode : IGameMode
    {
        private ShipType _shipType;
        readonly (int x, int y) _coords;
        public List<(int x, int y)> GetShipmentPlacementCoords()
        {
            List<(int x,int y)> result = new List<(int x, int y)>() { 
                _coords
            };
            if(result.Count != 1)
            {
                throw new Exception("More shipment are not allowed for duels");
            }
            return result;
        }
        public string GameThemeAudio() => "Pixel War Overlord";
        public CoordsToDrawBoard BoardCoords() => new CoordsToDrawBoard(52, 7, 88, 7);
        public DuelGameMode(ShipType type)
        { 
            _shipType = type;
        }
        public BattleBoard CreateBoard(int x , int y)
        {
            return new BattleBoard(x,y,5,5);
        }
        public bool RemeberArrowHit()
        {
            return true;
        }
        public Dictionary<string, int> AssignResources()
        {
            return null;
        }
        public List<IShip> ShipmentDelivery() => new List<IShip>()
        {
            ShipFactory.CreateShip(ShipType.Battleship)
            
        };
        public List<int> GetShipSizes()
        {
            return new List<int> { 3};
        }
    }


    public class WarhammerGameMode : IGameMode
    {
        readonly List<(int x, int y)> _coords = new List<(int x, int y)>();
        readonly Fraction _playerFraction;

        public List<(int x, int y)> GetShipmentPlacementCoords()
        {
            return _coords;
        }
        public WarhammerGameMode(Fraction fr) 
        {
            _playerFraction = fr;
        }

        public string GameThemeAudio() => "2-11 - Blood of Man";
        public CoordsToDrawBoard BoardCoords() => new CoordsToDrawBoard(2, 20, 2, 1);
        public BattleBoard CreateBoard(int x , int y)
        {
            return new BattleBoard(x,y,90,16);
        }
        public bool RemeberArrowHit()
        {
            return false;
        }

        public Dictionary<string, int> AssignResources()
        {
            Dictionary<string,int> resources = new Dictionary<string,int>();
            resources.Add("Energy",210);
            resources.Add("Requisition", 1750);
            resources.Add("Action Points", 2);
            return resources;
        }

        //local private methods

        void InsertDrukhariShipNames(IWindowBuilder windowBuilder)
        {
            windowBuilder.AddComponent(new Button("Reaver JetBike"));
            windowBuilder.AddComponent(new Button("Raider"));
            windowBuilder.AddComponent(new Button("Ravanger"));
            windowBuilder.AddComponent(new Button("Dair of Destruction"));
        }
        void InsertDrukhariPrices(IWindowBuilder windowBuilder)
        {
            windowBuilder.AddComponent(new TextOutput("Req: 150  En: 10"));
            windowBuilder.AddComponent(new TextOutput("Req: 280  En: 20"));
            windowBuilder.AddComponent(new TextOutput("Req: 375  En: 30"));
            windowBuilder.AddComponent(new TextOutput("Req: 720  En: 50"));
        }
        void InsertEldarShipNames(IWindowBuilder windowBuilder)
        {
            windowBuilder.AddComponent(new Button("Falcon"));
            windowBuilder.AddComponent(new Button("Vyper"));
            windowBuilder.AddComponent(new Button("Fire Prism"));
        }
        void InsertEldarPrices(IWindowBuilder windowBuilder)
        {
            windowBuilder.AddComponent(new TextOutput("Req: 300  En: 40"));
            windowBuilder.AddComponent(new TextOutput("Req: 200  En: 30"));
            windowBuilder.AddComponent(new TextOutput("Req: 600  En: 70"));
        }
        void InsertSpaceShipNames(IWindowBuilder windowBuilder)
        {
            windowBuilder.AddComponent(new Button("Land Speeder"));
            windowBuilder.AddComponent(new Button("Dreadnought"));
            windowBuilder.AddComponent(new Button("Land Raider"));
        }
        void InsertSpacePrices(IWindowBuilder windowBuilder)
        {
            windowBuilder.AddComponent(new TextOutput("Req: 270  En: 40"));
            windowBuilder.AddComponent(new TextOutput("Req: 380  En: 50"));
            windowBuilder.AddComponent(new TextOutput("Req: 850  En: 80"));
        }
        void InsertSaxonyShipNames(IWindowBuilder windowBuilder)
        {
            windowBuilder.AddComponent(new Button("Eisenhans"));
            windowBuilder.AddComponent(new Button("SdKS Grimbart"));
            windowBuilder.AddComponent(new Button("SdKS Isegrim"));
            windowBuilder.AddComponent(new Button("Stormtrooper ship"));
        }
        void InsertSaxonyPrices(IWindowBuilder windowBuilder)
        {
            windowBuilder.AddComponent(new TextOutput("Req: 450  En: 60"));
            windowBuilder.AddComponent(new TextOutput("Req: 480  En: 40"));
            windowBuilder.AddComponent(new TextOutput("Req: 475  En: 50"));
            windowBuilder.AddComponent(new TextOutput("Req: 160  En: 20"));
        }

        public List<IShip> ShipmentDelivery()
        {
            return new List<IShip>() 
            { 
                ShipFactory.CreateShip(ShipType.Dr_JetBike),
                ShipFactory.CreateShip(ShipType.Dr_Ravanger),
                ShipFactory.CreateShip(ShipType.Dr_Raider),
                ShipFactory.CreateShip(ShipType.Dr_Dair)            
            };

            /// help ;_;
            /// 
            IWindowBuilder windowBuilder = new WindowBuilder();
            windowBuilder
               .SetPosition(96, 2)
               .ColorHighlights(ConsoleColor.Green, ConsoleColor.DarkGreen)
               .ColorBorders(ConsoleColor.Cyan, ConsoleColor.DarkYellow);
            switch (_playerFraction)
            {
                case Fraction.Drukhari:
                    InsertDrukhariShipNames(windowBuilder); break;
                case Fraction.SaxonyEmpire:
                    InsertSaxonyShipNames(windowBuilder); break;
                case Fraction.BloodRavens:
                    InsertSpaceShipNames(windowBuilder); break;
                case Fraction.BielTan:
                    InsertEldarShipNames(windowBuilder); break;
            }
            windowBuilder.AddComponent(new Button("=>"));
            Window shipsWindow = windowBuilder.Build();
            windowBuilder.ResetBuilder();
            //
            windowBuilder
               .SetPosition(118, 2)
               .ColorHighlights(ConsoleColor.Yellow, ConsoleColor.DarkMagenta)
               .ColorBorders(ConsoleColor.Black, ConsoleColor.White);
            switch (_playerFraction)
            {
                case Fraction.Drukhari:
                    InsertDrukhariPrices(windowBuilder); break;
                case Fraction.SaxonyEmpire:
                    InsertSaxonyPrices(windowBuilder); break;
                case Fraction.BielTan: 
                    InsertEldarPrices(windowBuilder);break;
                case Fraction.BloodRavens:
                    InsertSpacePrices(windowBuilder); break;
            }
            Window costsWindow = windowBuilder.Build();
            windowBuilder.ResetBuilder();

            List<IShip> result = new List<IShip>();
            UIController controller = new UIController();
            controller.AddWindow(shipsWindow);
            controller.AddWindow(costsWindow);

            int bought = 0;//buy mininum 1
            string option = "";
            while (option != "=>" || bought ==0)
            {
                option = controller.DrawAndStart().FirstOrDefault();
                //Console.WriteLine(option);
                //Console.WriteLine(costsWindow.GetComponent(0).GetOption());
            }

            return result;
        }
            
            
            
        /*    => _playerFraction switch
        {
            Fraction.Drukhari => new List<IShip>()
            {

            },
            Fraction.BloodRavens => new List<IShip>() 
            { 
            
            },
            Fraction.BielTan => new List<IShip>() 
            { 
            
            },
            Fraction.SaxonyEmpire => new List<IShip>() 
            { 
            
            }
        };*/
        
        public List<int> GetShipSizes()
        {
            return new List<int> { 3};
        }
    }


    public abstract class GameModeFactory
    {
        public abstract IGameMode GetGameMode();
    }

    public class ClassicModeFactory : GameModeFactory 
    {
        public override IGameMode GetGameMode()
        { 
            //set standart gamemode
            return new ClassicGameMode();
        }
    }
    public class DuelModeFactory : GameModeFactory
    {
        public override IGameMode GetGameMode()
        {
            //One ship 1v1
            return new DuelGameMode(ShipType.Battleship);
        }
    }
    public class WarhammerModeFactory : GameModeFactory
    {
        Fraction _fraction;
        public WarhammerModeFactory(Fraction fraction)
        {
            _fraction = fraction;
        }
        public override IGameMode GetGameMode()
        {
            //battleship40k
            return new WarhammerGameMode(_fraction);
        }
    }
}
