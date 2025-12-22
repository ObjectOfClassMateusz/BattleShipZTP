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
        //void DisplayField(int x , int y);

        //void UpdateField(int x, int y, char character, ConsoleColor foreground, ConsoleColor background);
        //Field GetField(int x, int y)

        void PlaceShip();
        //(int, int) PlaceCursor(CursorBody cursor);
        //void AttackPoint();
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
        Field[,] _field;//[y,x]
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
                    copy[i,j]= _field[i,j];
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
                    //Env.SetColor(_field[i,j].colors.Item1 , _field[i, j].colors.Item2);
                    //Console.Write(_field[i,j].Character);
                    str.Append(_field[i, j].Character);
                }
                Console.Write(str.ToString());//instant
                //Env.Wait(500);
                Env.CursorPos(cornerX+1, cornerY+i+2);
            }
        }

        public void PlaceShip()
        {
            List<(string, int)> Ship = new List<(string, int)>()
            {
                ("٨",2),
                ("༺☫༻",1),
                ("☽=☾",1),
                ("◿═══◺",0)
            };
            int shipMostWidth = 5;
            bool canPlace = true;
            //

            Func<int,int,List<(int, int)>> action = (int x, int y) =>
            {
                List<(int, int)> history = new List<(int, int)>();
                Env.SetColor();
                ConsoleColor placementAvaible = ConsoleColor.Green;
                canPlace = true;
                foreach (var s in Ship)
                {
                    int cursorPosX = cornerX + 1 + s.Item2 + x;
                    int cursorPosY = cornerY + 1 + y;
                    for (int k = 0; k < s.Item1.Length; k++)
                    {
                        history.Add((cursorPosX + k, cursorPosY));
                        if (_field[y, x + k + s.Item2].Character != ' ')
                        {
                            placementAvaible = ConsoleColor.Red;
                            canPlace = false;
                        }
                    }
                    Env.CursorPos(cursorPosX, cursorPosY);
                    Env.SetColor(placementAvaible);
                    Console.Write(s.Item1);
                    y++;
                }
                return history;
            };
            int x = 0;
            int y = 0;
            List<(int,int)> history = new List<(int, int)>();
            while (true)
            {
                history = action(x, y);
                ConsoleKeyInfo klawisz  = Console.ReadKey();
                if (klawisz.Key == ConsoleKey.UpArrow)
                {
                    if (y > 0)
                    {
                        y--;
                        var new_history = action(x, y);
                        var pointsToRestore = history.Except(new_history);
                        foreach (var point in pointsToRestore)
                        {
                            Env.CursorPos(point.Item1, point.Item2);
                            Console.Write(_field[point.Item2 - this.cornerY - 1, point.Item1 - this.cornerX - 1]);
                        }
                    }
                }
                if (klawisz.Key == ConsoleKey.DownArrow)
                {
                    if (y < this.height-Ship.Count())
                    {
                        y++;
                        var new_history = action(x, y);
                        var pointsToRestore = history.Except(new_history);
                        foreach (var point in pointsToRestore)
                        {
                            Env.CursorPos(point.Item1, point.Item2);
                            Console.Write(_field[point.Item2 - this.cornerY - 1, point.Item1 - this.cornerX - 1]);
                        }
                    }
                }
                if (klawisz.Key == ConsoleKey.LeftArrow)
                {
                    if (x > 0)
                    {
                        x--;
                        var new_history = action(x, y);
                        var pointsToRestore = history.Except(new_history);
                        foreach (var point in pointsToRestore)
                        {
                            Env.CursorPos(point.Item1, point.Item2);
                            Console.Write(_field[point.Item2 - this.cornerY-1, point.Item1 - this.cornerX-1]);
                        }
                    }
                }
                if (klawisz.Key == ConsoleKey.RightArrow)
                {
                    if(x < width - shipMostWidth)
                    {
                        x++;
                        var new_history = action(x, y);
                        var pointsToRestore = history.Except(new_history);
                        foreach (var point in pointsToRestore)
                        {
                            Env.CursorPos(point.Item1, point.Item2);
                            Console.Write(_field[point.Item2 - this.cornerY - 1, point.Item1 - this.cornerX - 1]);
                        }
                    }
                }
                //
                if(klawisz.Key == ConsoleKey.Enter)
                {
                    //warunek umieszczenia

                    //tu nie można budowac
                    if(canPlace)
                        break;
                }
            }

            StringBuilder stringBuilder = new StringBuilder();
            foreach(var ship in Ship)
            {
                foreach(char character in ship.Item1)
                {
                    stringBuilder.Append(character);
                }
            }
            string shipValue = stringBuilder.ToString();
            int shipIterator = 0;
            
            foreach((int,int) h in history) 
            {
                y = h.Item2 - this.cornerY - 1;
                x = h.Item1 - this.cornerX - 1;
                _field[y, x].Character = shipValue[shipIterator];
                shipIterator++;
            }
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

        public void PlaceShip()//(IShip ship)
        //Pełnomocnik może przekazać żądanie obiektowi usługi tylko wtedy,
        //gdy klient przedstawi odpowiednie poświadczenia.
        {
            _board.PlaceShip();
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
    }
}
