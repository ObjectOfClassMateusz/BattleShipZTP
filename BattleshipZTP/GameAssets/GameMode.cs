using BattleshipZTP.Settings;
using BattleshipZTP.Ship;
using BattleshipZTP.Ship.DarkEldarShips;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using System.Resources;

namespace BattleshipZTP.GameAssets
{
    public interface IGameMode
    {
        BattleBoard CreateBoard(int x , int y);
        bool RemeberArrowHit();
        Dictionary<string, int> AssignResources();

        CoordsToDrawBoard BoardCoords();
        string GameThemeAudio();
        List<IShip> ShipmentDelivery(bool automatic=false);
        List<(int x , int y)> GetShipmentPlacementCoords();
        List<int> GetShipSizes();
        List<IShip> BuyShip(Dictionary<string, int> wallet);
    }

    public class ClassicGameMode : IGameMode
    {
        readonly List<(int x, int y)> _coords = new List<(int x, int y)>();
        public CoordsToDrawBoard BoardCoords() => new CoordsToDrawBoard(52, 7, 88, 7);
        public string GameThemeAudio() => "Pixel War Overlord";
        public BattleBoard CreateBoard(int x , int y) 
            => new BattleBoard(x,y,12,12);
        public bool RemeberArrowHit() 
            => true;
        public Dictionary<string, int> AssignResources() 
            => new Dictionary<string, int>();
        public List<IShip> ShipmentDelivery(bool automatic = false) => new List<IShip>()
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
        public List<IShip> BuyShip(Dictionary<string, int> wallet)
        {
            return null;
        }
    }

    public class SimulationMode : IGameMode
    {
        readonly List<(int x, int y)> _coords = new List<(int x, int y)>();
        public CoordsToDrawBoard BoardCoords() => new CoordsToDrawBoard(52, 7, 88, 7);
        public string GameThemeAudio() => "Pixel War Overlord";
        public BattleBoard CreateBoard(int x , int y) 
            => new BattleBoard(x,y,12,12);
        public bool RemeberArrowHit() 
            => true;
        public Dictionary<string, int> AssignResources() 
            => new Dictionary<string, int>();
        public List<IShip> ShipmentDelivery(bool automatic = false) => new List<IShip>()
        {
            ShipFactory.CreateShip(ShipType.Carrier),
            ShipFactory.CreateShip(ShipType.Carrier),
            ShipFactory.CreateShip(ShipType.Battleship),
            ShipFactory.CreateShip(ShipType.Battleship),
            ShipFactory.CreateShip(ShipType.Destroyer),
            ShipFactory.CreateShip(ShipType.Destroyer),
            ShipFactory.CreateShip(ShipType.Destroyer),
            ShipFactory.CreateShip(ShipType.Submarine),
        };
        public List<(int x, int y)> GetShipmentPlacementCoords()
        {
            return _coords;
        }
        public List<int> GetShipSizes()
        {
            return new List<int> { 5, 4, 3, 3, 1 };
        }
        public List<IShip> BuyShip(Dictionary<string, int> wallet)
        {
            return null;
        }
    }

    public class DuelGameMode : IGameMode
    {
        ShipType _type;
        public DuelGameMode(ShipType type)
        {
            _type = type;
        }
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
        public List<IShip> BuyShip(Dictionary<string, int> wallet)
        {
            return null;
        }
        public List<IShip> ShipmentDelivery(bool automatic = false) => new List<IShip>()
        {
            ShipFactory.CreateShip(ShipType.Battleship)
            
        };
        public List<int> GetShipSizes()
        {
            return new List<int> {3};
        }
    }

    public class WarhammerGameMode : IGameMode
    {
        readonly List<(int x, int y)> _coords = new List<(int x, int y)>();
        readonly Fraction _playerFraction;
        Dictionary<string, int> _resources;

        public List<(int x, int y)> GetShipmentPlacementCoords()
        {
            return _coords;
        }
        public WarhammerGameMode(Fraction fr) 
        {
            _resources = new Dictionary<string, int>();
            _resources.Add("Energy", 210);
            _resources.Add("Requisition", 1750);
            _resources.Add("Action Points", 2);
            _playerFraction = fr;
        }

        public string GameThemeAudio() => "2-11 - Blood of Man";
        public CoordsToDrawBoard BoardCoords() => new CoordsToDrawBoard(2, 20, 2, 1);
        public BattleBoard CreateBoard(int x , int y)
        {
            var result = new BattleBoard(x, y, 90, 16);
            result.EnableRotation(false);
            return result;
        }
        public bool RemeberArrowHit()
        {
            return false;
        }

        public Dictionary<string, int> AssignResources()
        {
            return _resources;
        }
        
        void InsertDrukhariShipNames(IWindowBuilder windowBuilder)
        {
            windowBuilder.AddComponent(new Button("Reaver JetBike"));
            windowBuilder.AddComponent(new Button("Raider"));
            windowBuilder.AddComponent(new Button("Ravanger"));
            windowBuilder.AddComponent(new Button("Dair of Destruction"));
        }
        void InsertDrukhariPrices(IWindowBuilder windowBuilder)
        {
            windowBuilder.AddComponent(new TextOutput
                ($"Req: {ReaverJetBikeShip.RequisitionCost}  En: {ReaverJetBikeShip.EnergyCost}"));
            windowBuilder.AddComponent(new TextOutput
                ($"Req: {RaiderShip.RequisitionCost}  En: {RaiderShip.EnergyCost}"));
            windowBuilder.AddComponent(new TextOutput
                ($"Req: {RavangerShip.RequisitionCost}  En: {RavangerShip.EnergyCost}"));
            windowBuilder.AddComponent(new TextOutput
                ($"Req: {DairOfDestructionShip.RequisitionCost}  En: {DairOfDestructionShip.EnergyCost}"));
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

        public List<IShip> ShipmentDelivery(bool automatic = false)
        {
            if (automatic)
            {
                return _playerFraction switch
                {
                    Fraction.Drukhari => new List<IShip>()
                    {
                        ShipFactory.CreateShip(ShipType.Dr_JetBike),
                        ShipFactory.CreateShip(ShipType.Dr_Raider),
                        ShipFactory.CreateShip(ShipType.Dr_Ravanger),
                        ShipFactory.CreateShip(ShipType.Dr_Dair),
                    },
                    Fraction.BielTan => new List<IShip>()
                    {
                        ShipFactory.CreateShip(ShipType.El_Falcon),
                        ShipFactory.CreateShip(ShipType.El_Vyper),
                        ShipFactory.CreateShip(ShipType.El_Prism),
                    },
                    Fraction.SaxonyEmpire => new List<IShip>()
                    {
                        ShipFactory.CreateShip(ShipType.Sax_Eisen),
                        ShipFactory.CreateShip(ShipType.Sax_sdksGrim),
                        ShipFactory.CreateShip(ShipType.Sax_sdksIse),
                        ShipFactory.CreateShip(ShipType.Sax_ship),
                    },
                    Fraction.BloodRavens => new List<IShip>()
                    {
                        ShipFactory.CreateShip(ShipType.SM_Raider),
                        ShipFactory.CreateShip(ShipType.SM_Dreadnought),
                        ShipFactory.CreateShip(ShipType.SM_Speeder),
                    },
                };
            }
            var resources = AssignResources();
            List<IShip> shipmentDelivery = new List<IShip>();
            int boughtShips = 0;//how much ships does user bought
            short paymentResult = 1;
            while (boughtShips == 0 || paymentResult == 1) 
            {
                var action = BuyShip(resources);
                paymentResult = (short)action.Count;
                if (paymentResult == 1)
                {
                    boughtShips++;
                    shipmentDelivery.Add(action.FirstOrDefault());
                }
                for (int i = 0; i < shipmentDelivery.Count; i++) 
                {
                    Env.CursorPos(96, 28+i);
                    Console.Write(shipmentDelivery[i].Name());
                }
            }
            Drawing.SetColors(ConsoleColor.Black, ConsoleColor.Black);
            Drawing.DrawRectangleArea(96, 19,43,20);
            return shipmentDelivery;
        }
        
        private (int req, int en, ShipType type) GetShipPrice(string name)
        {
            return name switch
            {
                // Drukhari
                "Reaver JetBike" => 
                (ReaverJetBikeShip.RequisitionCost, ReaverJetBikeShip.EnergyCost, ShipType.Dr_JetBike),
                "Raider" => 
                (RaiderShip.RequisitionCost, RaiderShip.EnergyCost, ShipType.Dr_Raider),
                "Ravanger" => 
                (RavangerShip.RequisitionCost, RavangerShip.EnergyCost, ShipType.Dr_Ravanger),
                "Dair of Destruction" => 
                (DairOfDestructionShip.RequisitionCost, DairOfDestructionShip.EnergyCost, ShipType.Dr_Dair),

                // Saxony
                "Eisenhans" => (450, 60, ShipType.Sax_Eisen),
                "SdKS Grimbart" => (480, 40, ShipType.Sax_sdksGrim), 
                "SdKS Isegrim" => (475, 50, ShipType.Sax_sdksIse),
                "Stormtrooper ship" => (160, 20, ShipType.Sax_ship),

                // Blood Ravens
                "Land Speeder" => (270, 40, ShipType.SM_Speeder),
                "Dreadnought" => (380, 50, ShipType.SM_Dreadnought),
                "Land Raider" => (850, 80, ShipType.SM_Raider),

                // BielTan
                "Falcon" => (300, 40, ShipType.El_Falcon),
                "Vyper" => (200, 30, ShipType.El_Vyper),
                "Fire Prism" => (600, 70, ShipType.El_Prism),

                _ => (0, 0, ShipType.Submarine) // jeśli nie znajdzie nazwy
            };
        }
        
        public List<int> GetShipSizes()
        {
            return new List<int> { 3};
        }
        
        public List<IShip> BuyShip(Dictionary<string, int> wallet)
        {
            IWindowBuilder windowBuilder = new WindowBuilder();
            windowBuilder.SetPosition(96, 20)
                .ColorHighlights(ConsoleColor.Green, ConsoleColor.DarkGreen)
                .ColorBorders(ConsoleColor.Cyan, ConsoleColor.DarkYellow);
            switch (_playerFraction) 
            {
                case Fraction.Drukhari: 
                    InsertDrukhariShipNames(windowBuilder); 
                    break;
                case Fraction.SaxonyEmpire: 
                    InsertSaxonyShipNames(windowBuilder); 
                    break;
                case Fraction.BielTan: 
                    InsertEldarShipNames(windowBuilder); 
                    break;
                case Fraction.BloodRavens: 
                    InsertSpaceShipNames(windowBuilder); 
                    break;
            }
            windowBuilder.AddComponent(new Button("POWROT"));
            Window shipsWindow = windowBuilder.Build();
            
            windowBuilder.ResetBuilder();
            windowBuilder.SetPosition(118, 20)
                .ColorHighlights(ConsoleColor.Yellow, ConsoleColor.DarkMagenta)
                .ColorBorders(ConsoleColor.Black, ConsoleColor.White);
    
            switch (_playerFraction) {
                case Fraction.Drukhari: InsertDrukhariPrices(windowBuilder); break;
                case Fraction.BielTan: InsertEldarPrices(windowBuilder); break;
                case Fraction.BloodRavens: InsertSpacePrices(windowBuilder); break;
                case Fraction.SaxonyEmpire: InsertSaxonyPrices(windowBuilder); break;
            }
            
            Window costsWindow = windowBuilder.Build();

            UIController controller = new UIController();
            controller.AddWindow(shipsWindow);
            controller.AddWindow(costsWindow);

            List<IShip> boughtShips = new List<IShip>();
            string option = "";
            Env.CursorPos(96, 19);
            Console.Write($"PORTFEL - Req: {wallet["Requisition"]} | En: {wallet["Energy"]}      ");

            //zakupy
            while (option != "POWROT")
            {
                option = controller.DrawAndStart().FirstOrDefault() ?? "";

                // PRZYPADEK 1: Gracz wybrał konkretny statek
                if (option != "POWROT" && option != "")
                {
                    var (req, en, type) = GetShipPrice(option);

                    if (wallet["Requisition"] >= req && wallet["Energy"] >= en)
                    {
                        wallet["Requisition"] -= req;
                        wallet["Energy"] -= en;

                        boughtShips.Add(ShipFactory.CreateShip(type));

                        if (UserSettings.Instance.SfxEnabled) 
                            AudioManager.Instance.Play("stawianie");

                        break;
                    }
                    else
                    {
                        if (UserSettings.Instance.SfxEnabled) AudioManager.Instance.Play("wrong");
                    }
                }

                if (option == "POWROT")
                {
                    break;
                }
            }
            Drawing.SetColors(ConsoleColor.Black, ConsoleColor.Black);
            Drawing.DrawRectangleArea(96, 19, 43, 20);
            return boughtShips;
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
            //battleship40k mode
            return new WarhammerGameMode(_fraction);
        }
    }

    public class SimulationModeFactory : GameModeFactory
    {
        public override IGameMode GetGameMode()
        {
            //ai simulation
            return new SimulationMode();
        }
    }
}
