using BattleshipZTP.Commands;
using BattleshipZTP.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipZTP.Settings;
using static BattleshipZTP.GameAssets.BattleBoard;
using static System.Net.Mime.MediaTypeNames;

namespace BattleshipZTP.GameAssets
{
    public interface IBattleBoard
    {
        void Display();
        void DisplayField(int x, int y);
        Field GetField(int x, int y);
        bool IsNeighborHaveShipRef(Field field);
        

        List<(int x, int y)> PlaceCommand(ICommand command);
        List<(int x, int y)> PlaceShip(IShip ship, int x, int y);

        Point ChooseAttackPoint();
        HitResult AttackPoint(Point target);
        void PlaceMarker(Point actionCoords, HitResult actionResult);
    }

    public class Field
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Character { get; set; }
        public (ConsoleColor foreground, ConsoleColor background) colors = (ConsoleColor.White, ConsoleColor.Black);

        public bool ArrowHit { get; set; }
        public IShip? ShipReference { get; set; }

        public Field(char character, int x, int y)
        {
            Character = character;
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            Env.SetColor(colors.foreground, colors.background);
            return this.Character.ToString();
        }
    }

    public class BattleBoardMemento
    {
        public Field[,] State { get; }

        public BattleBoardMemento(Field[,] state)
        {
            State = state;
        }
    }

    public class BattleBoard : IBattleBoard
    {
        Field[,]? _field; //[y,x]
        bool _canRotate = true;
        public int cornerX;
        public int cornerY;
        public int width;
        public int height;

        public BattleBoard(int cornerLeft = 0, int cornerUp = 0, int width = 90, int height = 16)
        {
            this.width = width;
            this.height = height;
            cornerX = cornerLeft;
            cornerY = cornerUp;
        }

        public void EnableRotation(bool v) { _canRotate = v; }

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
            return _field != null;
        }

        public BattleBoardMemento GetSaveState()
        {
            Field[,] copy = new Field[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Field original = _field[i, j];
                    copy[i, j] = new Field(original.Character, j, i)
                    {
                        colors = original.colors,
                        ShipReference = original.ShipReference
                    };
                }
            }
            return new BattleBoardMemento(copy);
        }

        public void Display()
        {
            (ConsoleColor foreground, ConsoleColor background) lastColor = _field[0, 0].colors;
            Env.CursorPos(cornerX + 1, cornerY + 1);
            for (int i = 0; i < height; i++)
            {
                StringBuilder str = new StringBuilder();
                for (int j = 0; j < width; j++)
                {
                    if (_field[i, j].colors == lastColor)
                    {
                        str.Append(_field[i, j].Character);
                    }
                    else
                    {
                        //Separete stream writes for direfent colors
                        Env.SetColor(lastColor.foreground, lastColor.background);
                        Console.Write(str.ToString());
                        str.Clear();
                        lastColor = _field[i, j].colors;
                        j--;
                    }
                }

                Env.SetColor(lastColor.foreground, lastColor.background);
                Console.Write(str.ToString());
                Env.CursorPos(cornerX + 1, cornerY + i + 2);
            }
        }

        public static List<(string text, int offset)> RotateBody(List<(string text, int offset)> ship)
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

                string k = sb.ToString().TrimEnd();
                int spaceCount = k.Count(c => c == ' ');
                result.Add((k.TrimStart(), spaceCount));
            }

            return result;
        }

        public void DisplayField(int x, int y)
        {
            Env.CursorPos(cornerX + 1 + x, cornerY + 1 + y);
            Console.Write(_field[y, x]);
        }

        public Field GetField(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                return null; 
            }
            return _field[y, x];
        }

        public bool IsNeighborHaveShipRef(Field field)
        {
            //Checks if field have an neighbor with ship reference
            for (int y = field.Y - 1; y <= field.Y + 1; y++)
            for (int x = field.X - 1; x <= field.X + 1; x++)
                if (x >= 0 && x < this.width && y >= 0 && y < this.height)
                    if (this._field[y, x].ShipReference != null)
                        return true;
            return false;
        }

        public void PlaceMarker(Point p, HitResult result)
        {
            if (p.X < 0 || p.X >= width || p.Y < 0 || p.Y >= height) return;
    
            Field field = _field[p.Y, p.X];
    
            if (result == HitResult.Hit || result == HitResult.HitAndSunk)
            {
                field.Character = 'X';
                field.colors = (ConsoleColor.Red, ConsoleColor.Black);
        
                if (result == HitResult.HitAndSunk && field.ShipReference != null)
                {
                    for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                        if (_field[i, j].ShipReference == field.ShipReference)
                            _field[i, j].colors = (ConsoleColor.DarkGray, ConsoleColor.Black);
                }
            }
            else
            {
                field.Character = '•';
                field.colors = (ConsoleColor.Blue, ConsoleColor.Black);
            }
        }

        public List<(int x, int y)> PlaceCommand(ICommand command)
        {
            List<(string text, int offset)> Body = command.GetBody();

            bool canPlace = true;
            int bodyMostWidth = Body.Any() ? Body.Max(b => b.text.Length) : 1;
            int bodyMostHeight = Body.Count;
            //
            Func<int, int, List<(int x, int y)>> fieldHistoryOn = (int x, int y) =>
            {
                List<(int x, int y)> history = new List<(int x, int y)>();
                Env.SetColor();
                ConsoleColor placementAvaible = ConsoleColor.Green;
                canPlace = true;
                int currentY = y;
                foreach (var s in Body)
                {
                    int cursorPosX = cornerX + 1 + s.offset + x;
                    int cursorPosY = cornerY + 1 + currentY;
                    for (int k = 0; k < s.text.Length; k++)
                    {
                        history.Add((cursorPosX + k, cursorPosY));
                        if (command.PlaceCondition(x + k + s.offset, currentY))
                        {
                            placementAvaible = ConsoleColor.Red;
                            canPlace = false;
                        }
                    }
                    Env.CursorPos(cursorPosX, cursorPosY);
                    Env.SetColor(placementAvaible);
                    Console.Write(s.text);
                    currentY++;
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
                ConsoleKeyInfo klawisz = Console.ReadKey(true);
                //
                if (klawisz.Key == ConsoleKey.Enter) //Placement
                {
                    if (canPlace)
                    {
                        if (UserSettings.Instance.SfxEnabled == true)
                        {
                            AudioManager.Instance.Play("stawianie");
                        }
                        break;
                    }
                    else
                    {
                        if (UserSettings.Instance.SfxEnabled == true)
                        {
                            AudioManager.Instance.Play("wrong");
                        }
                        continue;
                    }
                }
                else if (klawisz.Key == ConsoleKey.Tab
                         && localX - 1 < this.width - bodyMostHeight
                         && localY - 1 < this.height - bodyMostWidth
                         && _canRotate)//Rotation
                {
                    Body = RotateBody(Body);
                    command.SetBody(Body);
                    bodyMostHeight = Body.Count;
                    bodyMostWidth = 1;
                    foreach (var bodySlice in Body)
                        if (bodySlice.text.Length > bodyMostWidth)
                            bodyMostWidth = bodySlice.text.Length;
                }
                else if (klawisz.Key == ConsoleKey.UpArrow && localY > 0)
                    localY--;
                else if (klawisz.Key == ConsoleKey.DownArrow && localY < this.height - bodyMostHeight)
                    localY++;
                else if (klawisz.Key == ConsoleKey.LeftArrow && localX > 0)
                    localX--;
                else if (klawisz.Key == ConsoleKey.RightArrow && localX < this.width - bodyMostWidth)
                    localX++;
                else
                    continue;

                List<(int x, int y)> new_history = fieldHistoryOn(localX, localY);
                IEnumerable<(int x, int y)> pointsToRestore = history.Except(new_history);
                foreach (var point in pointsToRestore)
                {
                    Env.CursorPos(point.x, point.y);
                    Console.Write(_field[point.y - this.cornerY - 1, point.x - this.cornerX - 1]);
                }
            }

            return history;
        }

        public Point ChooseAttackPoint()
        {
            int localX = 0;
            int localY = 0;
            char cursorChar = '+';
            while (true)
            {
                Env.CursorPos(cornerX + 1 + localX, cornerY + 1 + localY);
                Env.SetColor(ConsoleColor.Yellow);
                Console.Write(cursorChar);

                ConsoleKeyInfo key = Console.ReadKey(true);

                Env.CursorPos(cornerX + 1 + localX, cornerY + 1 + localY);
                var field = _field[localY, localX];
                if(field.ArrowHit)
                {
                    Console.Write(field);
                }
                else
                {
                    Console.Write(' ');
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    var selectedField = field;
                    if (selectedField.Character != 'X' && selectedField.Character != '•' && !selectedField.ArrowHit)
                    {
                        return new Point(localX, localY);
                    }
                    AudioManager.Instance.Play("wrong");
                    continue;
                }

                if (key.Key == ConsoleKey.UpArrow && localY > 0) localY--;
                else if (key.Key == ConsoleKey.DownArrow && localY < height - 1) localY++;
                else if (key.Key == ConsoleKey.LeftArrow && localX > 0) localX--;
                else if (key.Key == ConsoleKey.RightArrow && localX < width - 1) localX++;
            }
        }

        public List<(int x, int y)> PlaceShip(IShip ship, int x, int y)
        {
            int localX = cornerX + 1 + x;
            int localY = cornerY + 1 + y;
            List<(string text, int offset)> shipBody = ship.GetBody();
            List<(int x, int y)> history = new List<(int x, int y)>();

            int j = 0;
            foreach (var sB in shipBody)
            {
                int i = 0;
                foreach (char @char in sB.text)
                {
                    history.Add((localX + sB.offset + i, localY + j));
                    i++;
                }
                j++;
            }
            return history;
        }

        // csharp
        public HitResult AttackPoint(Point target)
        {
            if (target.X >= 0 && target.X < width && target.Y >= 0 && target.Y < height)
            {
                Field field = _field[target.Y, target.X];

                if (field.ShipReference != null)
                {
                    if (field.Character == 'X') {
                        DisplayField(target.X, target.Y);
                        return HitResult.Hit;
                    } 

                    HitResult result = field.ShipReference.TakeHit(target);
                    field.Character = 'X';
                    if (result == HitResult.HitAndSunk)
                    {
                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                if (_field[i, j].ShipReference == field.ShipReference)
                                {
                                    _field[i, j].colors = (ConsoleColor.DarkGray, ConsoleColor.Black);
                                    _field[i, j].Character = 'X';
                                    DisplayField(j, i);
                                }
                            }
                        }
                    }
                    else
                    {
                        field.colors = (ConsoleColor.Red, ConsoleColor.Black);
                    }
                    DisplayField(target.X, target.Y);
                    return result;
                }
                else
                {
                    field.Character = '•';
                    field.colors = (ConsoleColor.Blue, ConsoleColor.Black);
                    DisplayField(target.X, target.Y);
                    //Console.Write('c');
                    return HitResult.Miss;
                }
            }
            return HitResult.Miss;
        }
        
        public void Restore(BattleBoardMemento memento)
        {
            if (memento == null) return;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    _field[i, j].Character = memento.State[i, j].Character;
                    _field[i, j].colors = memento.State[i, j].colors;
                    _field[i, j].ShipReference = memento.State[i, j].ShipReference;
                }
            }
        }

        public class BattleBoardProxy : IBattleBoard
        {
            public int Width => _board.width;
            public int Height => _board.height;
            BattleBoard _board;
            BattleBoardMemento _memento;
            int _topLeftX;
            int _topLeftY;
            private Dictionary<(int x, int y), ICommand> _cacheOperations;

            public BattleBoardProxy(BattleBoard board)
            {
                _board = board;
                _topLeftX = _board.cornerX;
                _topLeftY = _board.cornerY;
            }

            public List<(int x, int y)> PlaceCommand(ICommand command)
            {
                return _board.PlaceCommand(command);
            }

            public void FieldsInitialization()
            {
                if (!_board.IsFieldsSet())
                {
                    _board.FieldsInitialization();
                    _memento = _board.GetSaveState();
                }
            }

            public Field GetField(int x, int y)
            {
                return _board.GetField(x, y);
            }

            public bool IsNeighborHaveShipRef(Field field)
            {
                return _board.IsNeighborHaveShipRef(field);
            }

            public Point ChooseAttackPoint()
            {
                return _board.ChooseAttackPoint();

            }
            
            public HitResult AttackPoint(Point target)
            {
                return _board.AttackPoint(target);
            }

            public BattleBoard GetBoard() => _board;
            
            public void Display()
            {
                Drawing.SetColors(ConsoleColor.Black, ConsoleColor.DarkGray);
                Drawing.DrawRight('═', _board.width + 2,
                    _topLeftX,
                    _topLeftY);
                Drawing.DrawDown('║', _board.height,
                    _topLeftX,
                    _topLeftY + 1);
                Drawing.DrawDown('║', _board.height,
                    _topLeftX + _board.width + 1,
                    _topLeftY + 1);
                Drawing.DrawRight('═', _board.width + 2,
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
                    Env.CursorPos(_topLeftX + 1, _topLeftY + i + 2);
                }

                if (_board.IsFieldsSet())
                {
                    _board.Display();
                }
            }

            public void DisplayField(int x, int y)
            {
                if (x >= 0 && x < _board.width && y >= 0 && y < _board.height)
                {
                    _board.DisplayField(x, y);
                }

                throw new Exception("Field cordinates out of range");
            }

            public List<(int x, int y)> PlaceShip(IShip ship, int x, int y)
            {
                return _board.PlaceShip(ship, x, y);
            }

            public void PlaceMarker(Point actionCoords, HitResult actionResult)
            {
                _board.PlaceMarker(actionCoords, actionResult);
            }
        }
        
    }
    
}
