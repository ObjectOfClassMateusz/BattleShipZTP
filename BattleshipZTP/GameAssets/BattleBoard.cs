using BattleshipZTP.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleshipZTP.GameAssets.BattleBoard;

namespace BattleshipZTP.GameAssets
{
    public interface IBattleBoard
    {
        void Display();
        void DisplayField(int x, int y);

        void PlaceShip(IShip ship);
        //void PlaceCommand(ICommand command);
        //AttackCommand
        //PlaceShipCommand


        //void UpdateField(int x, int y, char character, ConsoleColor foreground, ConsoleColor background);
        //Field GetField(int x, int y)
<<<<<<< HEAD
=======

        void PlaceShip(IShip ship);
        //(int, int) PlaceCursor(CursorBody cursor);
        HitResult AttackPoint(Point target);
>>>>>>> 071a7e2a94364d0d7a18a8958e9bfa8159c9d056
    }

    public class Field
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Character { get; set; }
        public (ConsoleColor, ConsoleColor) colors = (ConsoleColor.White, ConsoleColor.Black);

        public bool ArrowHit {  get; set; }
        public IShip ShipReference {get;set;}

        public Field(char character, int x, int y)
        {
            Character = character;
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            Env.SetColor(colors.Item1, colors.Item2);
            return this.Character.ToString();
        }
    }

    public class BattleBoardMemento
    {
        public Field[,] State {  get;  }
        public BattleBoardMemento(Field[,] state)
        {
            State = state;
        }
    }

    public class BattleBoard : IBattleBoard
    {
        public int cornerX;
        public int cornerY;
        Field[,]? _field;//[y,x]
        public int width;
        public int height;

        public BattleBoard(int cornerLeft=0 , int cornerUp=0, int width = 90, int height = 16)
        {
            this.width = width;
            this.height = height;
            cornerX = cornerLeft;
            cornerY = cornerUp;
        }

        public void FieldsInitialization()
        {
            //Lazy initialization
            _field = new Field[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    char blankspace = ' ';
                    _field[i, j] = new Field(blankspace, j, i);
                }
            }
        }

        public bool IsFieldsSet()
        {
            return _field!=null;
        }
        
        public BattleBoardMemento GetSaveState()
        {
            Field[,] copy = new Field[height,width];
            for(int i = 0; i < height; i++)
            {
                for(int j=0; j<width; j++)
                {
                    copy[i,j] = _field[i,j];
                }
            }
            return new BattleBoardMemento(copy);
        }
        public void Display()
        {
            Env.CursorPos(cornerX+1, cornerY+1);
            for (int i = 0; i < height; i++)
            {
                StringBuilder str = new StringBuilder();
                for (int j = 0; j < width; j++) 
                {
                    str.Append(_field[i, j].Character);
                }
                Console.Write(str.ToString());//instant
                //Env.Wait(500);
                Env.CursorPos(cornerX+1, cornerY+i+2);
            }
        }
<<<<<<< HEAD

        public void DisplayField(int x, int y)
=======
        
        public void PlaceShip(IShip ship)
>>>>>>> 071a7e2a94364d0d7a18a8958e9bfa8159c9d056
        {
            Env.CursorPos(cornerX + 1 + x, cornerY + 1 + y);
            Console.WriteLine(_field[y,x]);
        }

        private List<(string text, int offset)> RotateShipment
            (List<(string text, int offset)> ship)
        {
            List<string> lines = ship
                .Select(x => new string(' ', x.offset) + x.text)
                .ToList();
            int maxWidth = lines.Max(l => l.Length);
            lines = lines
                .Select(l => l.PadRight(maxWidth, ' '))
                .ToList();
            int height = lines.Count;
            int width = maxWidth;
            List<(string text, int offset)> result = new();
            for (int x = 0; x < width; x++)
            {
                StringBuilder sb = new();
                for (int y = height - 1; y >= 0; y--)
                {
                    sb.Append(lines[y][x]);
                }
                result.Add((sb.ToString().TrimEnd(), 0));
            }
            return result;
        }




        

        public void PlaceShip(IShip ship)
        {
            //List<(string text, int offset)> Ship = ship.GetBody();
            List<(string text, int offset)> Ship = new()
            {
                ("٨", 2),
                     ("༺☫༻", 1),
                ("☽=☾", 1),
                ("◿═══◺", 0)
            };
            bool canPlace = true;
            int shipMostWidth = 1;
            int shipMostHeight = Ship.Count;

            foreach (var shipSlice in Ship) 
                if(shipSlice.text.Length > shipMostWidth)
                    shipMostWidth = shipSlice.text.Length;
            //
            Func<int,int,List<(int x, int y)>> fieldHistoryOn = (int x, int y) =>
            {
                List<(int x, int y)> history = new List<(int x, int y)>();
                Env.SetColor();
                ConsoleColor placementAvaible = ConsoleColor.Green;
                canPlace = true;
                foreach (var s in Ship)
                {
                    int cursorPosX = cornerX + 1 + s.offset + x;
                    int cursorPosY = cornerY + 1 + y;
                    for (int k = 0; k < s.text.Length; k++)
                    {
                        history.Add((cursorPosX + k, cursorPosY));
                        if (_field[y, x + k + s.offset].Character != ' ')
                        {
                            placementAvaible = ConsoleColor.Red;
                            canPlace = false;
                        }
                    }
                    Env.CursorPos(cursorPosX, cursorPosY);
                    Env.SetColor(placementAvaible);
                    Console.Write(s.text);
                    y++;
                }
                return history;
            };
            //
            int localX = 0;
            int localY = 0;
            List<(int x, int y)> history = new List<(int x, int y)>();
            while (true)
            {
                history = fieldHistoryOn(localX, localY);
                ConsoleKeyInfo klawisz  = Console.ReadKey(true);
                //
                if (klawisz.Key == ConsoleKey.Enter)//Placement condition
                {
                    if (canPlace)
                        break;
                    continue;
                }
                else if (klawisz.Key == ConsoleKey.Tab //Rotation
                    && localX - 1 < this.width - shipMostHeight
                    && localY - 1 < this.height - shipMostWidth)
                {
                    Ship = this.RotateShipment(Ship);
                    shipMostHeight = Ship.Count;
                    shipMostWidth = 1;
                    foreach (var shipSlice in Ship)
                        if (shipSlice.text.Length > shipMostWidth)
                            shipMostWidth = shipSlice.text.Length;
                }
                else if(klawisz.Key == ConsoleKey.UpArrow && localY > 0)
                {
                    localY--;
                }
                else if(klawisz.Key == ConsoleKey.DownArrow && localY < this.height - shipMostHeight)
                {
                    localY++;
                }
                else if(klawisz.Key == ConsoleKey.LeftArrow && localX > 0)
                {
                    localX--;
                }
                else if(klawisz.Key == ConsoleKey.RightArrow && localX < this.width - shipMostWidth)
                {
                    localX++;
                }
                else
                {
                    continue;
                }
                List<(int x, int y)> new_history = fieldHistoryOn(localX, localY);
                IEnumerable<(int x, int y)> pointsToRestore = history.Except(new_history);
                foreach (var point in pointsToRestore)
                {
                    Env.CursorPos(point.x, point.y);
                    Console.Write(_field[point.y - this.cornerY - 1, point.x - this.cornerX - 1]);
                }
            }
            //
            StringBuilder stringBuilder = new StringBuilder();
<<<<<<< HEAD
            foreach(var _ship in Ship)
            {
                foreach(char character in _ship.Item1)
=======
            foreach(var s in Ship)
            {
                foreach(char character in s.Item1)
>>>>>>> 071a7e2a94364d0d7a18a8958e9bfa8159c9d056
                {
                    stringBuilder.Append(character);
                }
            }
            string shipValue = stringBuilder.ToString();
            int shipIterator = 0;
            
            foreach((int,int) h in history) 
            {
<<<<<<< HEAD
                localY = h.Item2 - this.cornerY - 1;
                localX = h.Item1 - this.cornerX - 1;
                _field[localY, localX].Character = shipValue[shipIterator];
                DisplayField(localX, localY);
=======
                y = h.Item2 - this.cornerY - 1;
                x = h.Item1 - this.cornerX - 1;
                _field[y, x].Character = shipValue[shipIterator];
                
                _field [y, x].ShipReference = ship;
                
>>>>>>> 071a7e2a94364d0d7a18a8958e9bfa8159c9d056
                shipIterator++;
            }
        }
        public HitResult AttackPoint(Point target)
        {
            if (target.X >= 0 && target.X < width && target.Y >= 0 && target.Y < height)
            {
                Field field = _field[target.Y, target.X];
                
                if (field.ShipReference != null)
                {
                    HitResult result = field.ShipReference.TakeHit(target);
                    field.Character = 'X';
                    field.colors = (ConsoleColor.Red, ConsoleColor.Black);
                    return result;
                }
                else
                {
                    field.Character = '•';
                    field.colors = (ConsoleColor.Blue, ConsoleColor.Black);
                    return HitResult.Miss;
                }
            }
            return HitResult.Miss;
        }
    }

    public class BattleBoardProxy : IBattleBoard
    { 
        BattleBoard _board;
        BattleBoardMemento _memento;
        int _topLeftX;
        int _topLeftY;
        //private Dictionary<Guid, ICommand> _cacheOperations;
        public BattleBoardProxy(BattleBoard board)
        {
            _board = board;
            _topLeftX = _board.cornerX;
            _topLeftY = _board.cornerY;
        }
        public void FieldsInitialization()
        {
            if (!_board.IsFieldsSet())
            {
                _board.FieldsInitialization();
                _memento = _board.GetSaveState();
            }
        }

        //public IShip GetReference(Field field)
       // {

       // }

<<<<<<< HEAD
        public void PlaceShip(IShip ship)
=======
        public void PlaceShip(IShip ship) //(IShip ship)
>>>>>>> 071a7e2a94364d0d7a18a8958e9bfa8159c9d056
        //Pełnomocnik może przekazać żądanie obiektowi usługi tylko wtedy,
        //gdy klient przedstawi odpowiednie poświadczenia.
        {
            _board.PlaceShip(ship);
<<<<<<< HEAD
=======
        }

        public HitResult AttackPoint(Point target)
        {
            return _board.AttackPoint(target);
>>>>>>> 071a7e2a94364d0d7a18a8958e9bfa8159c9d056
        }

        public void Display()
        {
            Drawing.SetColors(ConsoleColor.Black, ConsoleColor.DarkGray);
            Drawing.DrawRight('#', _board.width + 2, 
                _topLeftX,
                _topLeftY);
            Drawing.DrawDown ('#', _board.height   , 
                _topLeftX,
                _topLeftY + 1);
            Drawing.DrawDown ('#', _board.height   , 
                _topLeftX + _board.width + 1,
                _topLeftY + 1);
            Drawing.DrawRight('#', _board.width + 2,
                _topLeftX,
                _topLeftY + 1 + _board.height);
            Env.CursorPos(_topLeftX + 1, _topLeftY + 1);
            for (int i = 0; i < _board.height; i++)
            {
                StringBuilder str = new StringBuilder();
                for (int j = 0; j < _board.width; j++)
                {
                    str.Append(' ');
                }
                Console.Write(str.ToString());
                Env.CursorPos(_topLeftX+1, _topLeftY+i+2);
            }
            if (_board.IsFieldsSet())
            {
                _board.Display();
            }
        }
        public void DisplayField(int x, int y)
        {
            _board.DisplayField (x, y);
        }
    }
}
