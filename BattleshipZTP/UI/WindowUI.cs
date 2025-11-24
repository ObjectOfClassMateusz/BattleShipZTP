using BattleshipZTP.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BattleshipZTP.UI
{
    public interface IComponentUI
    {
        void SetColors(ConsoleColor foreground, ConsoleColor background);
        void SetSize(int width, int height);
        void Print();
        void Highlight(ConsoleColor foreground, ConsoleColor background);
        string Option();
    }
    public class Button : IComponentUI
    {
        private string _option {  get; set; }
        public Button(string opt)
        {
            _option = opt;
        }
        public string Option() => _option;
        private string _button = "";
        
        private (ConsoleColor, ConsoleColor) _colors;
        public void SetColors(ConsoleColor foreground, ConsoleColor background)
        {
            _colors.Item1 = foreground;
            _colors.Item2 = background;
        }
        private int _width = 0;
        private int _height = 0;
        public void SetSize(int width, int height)
        {
            _width = width;
            _height = height;
        }

        //public void Print((int,int) cursorOrigin = default)
        public void Print()
        {
            Console.Write(Option());
            return;
            /*
             * TESTING
             * 
             * 
             * if(_button != "")
            {
                Console.WriteLine(_button);
            }
            StringBuilder sb = new StringBuilder();
            int totalWidth = _option.Length + 2 * _width;
            for (int i = 0; i < _height; i++)
            {
                sb.Append(new string(' ', totalWidth));
                sb.Append('\n');
            }
            Console.Write(sb.ToString());
            sb = new StringBuilder();
            Env.CursorPos(cursorOrigin.Item1, cursorOrigin.Item2+1);
            sb.Append(new string(' ', _width));
            sb.Append(_option);
            sb.Append(new string(' ', _width));
            sb.Append('\n');
            Console.Write(sb.ToString());
            sb = new StringBuilder();
            Env.CursorPos(cursorOrigin.Item1, cursorOrigin.Item2+2);

            for (int i = 0; i < _height; i++)
            {
                 sb.Append(new string(' ', totalWidth));
                 sb.Append('\n');
            }
            Console.Write(sb.ToString());*/

        }
        public void Highlight(ConsoleColor foreground, ConsoleColor background)
        {
            Env.SetColor(foreground, background);
        }
    }

    //public class CheckBox : IComponentUI { }
    //public class TextBox : IComponentUI { }
    //public class IntegerSideBar : IComponentUI { }

    public interface IWindowBuilder
    {
        IWindowBuilder SetPosition(int startPlaceX, int startPlaceY);
        IWindowBuilder SetSize(int width=0, int height=0);
        IWindowBuilder ColorBorders(ConsoleColor foreground, ConsoleColor background);
        IWindowBuilder ColorHighlights(ConsoleColor foreground, ConsoleColor background);
        IWindowBuilder AddComponent(IComponentUI component);
        Window Build();
        void ClearDataRef();
    }
    public class WindowsBuilder : IWindowBuilder
    {
        private Window _window;
        public WindowsBuilder()
        {
            _window = new Window();
        }
        public IWindowBuilder SetPosition(int startPlaceX, int startPlaceY)
        {
            _window.SetPosition(startPlaceX, startPlaceY);
            return this;
        }
        public IWindowBuilder SetSize(int width=0, int height=0)
        {
            _window.SetSize(width, height);
            return this;
        }
        public IWindowBuilder ColorBorders(ConsoleColor foreground, ConsoleColor background)
        {
            _window.SetBorderColors(foreground, background);
            return this;
        }
        public IWindowBuilder ColorHighlights(ConsoleColor foreground, ConsoleColor background)
        {
            _window.SetHighColors(foreground, background);
            return this;
        }
        public IWindowBuilder AddComponent(IComponentUI component)
        {
            _window.Add(component);
            return this;
        }
        public Window Build() => _window;
        public void ClearDataRef()
        {
            _window = new Window();
        }
    }

    public class Window
    {
        private readonly List<IComponentUI> _components = new List<IComponentUI>();
        public void Add(IComponentUI component)
        {
            _components.Add(component);
            int longest_string = 0;
            foreach (IComponentUI c in _components)
            {
                if (c.Option().Length > longest_string)
                {
                    longest_string = c.Option().Length;
                }
            }
            this._width = (longest_string + 2 > _width) 
                ? longest_string + 2 
                : _width;
            this._height = this._components.Count + 1 > _height 
                ? this._components.Count + 1 
                : _height;
        }
        public IComponentUI GetComponent(int index) => _components[index];
        public int ComponentsLenght() => _components.Count;
        
        private int cornerX;
        private int cornerY;
        public void SetPosition(int startPlaceX , int startPlaceY)
        {
            this.cornerX = startPlaceX;
            this.cornerY = startPlaceY;
        }
        public (int X, int Y) GetCorner() => (cornerX, cornerY);

        private int _width { get; set; }
        private int _height { get; set; }
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public void SetSize(int width = 0, int height = 0)
        {
            this._width = width;
            this._height = height;
        }

        private (ConsoleColor, ConsoleColor) _borderColor;
        public (ConsoleColor, ConsoleColor) GetBorderColors()
        {
            return _borderColor;
        }
        public void SetBorderColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            this._borderColor.Item1 = foregroundColor;
            this._borderColor.Item2 = backgroundColor;
        }
        private (ConsoleColor, ConsoleColor) _highlightColor;
        public (ConsoleColor, ConsoleColor) GetHighColors()
        {
            return _highlightColor;
        }
        public void SetHighColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            this._highlightColor.Item1 = foregroundColor;
            this._highlightColor.Item2 = backgroundColor;
        }

        public Window(){}

        private string _selectedOption;
        public string SelectedOption() => this._selectedOption;

        public string DrawAndStart()
        {
            ConsoleKeyInfo klawisz;
            int Selected = 0;
            while (true)
            {
                Env.CursorPos(cornerX + 1, cornerY + 1 + Selected);
                Env.SetColor(_highlightColor.Item1, _highlightColor.Item2);
                _components[Selected].Print();

                klawisz = Console.ReadKey(true);
                if (klawisz.Key == ConsoleKey.UpArrow && Selected != 0)
                {
                    Env.CursorPos(cornerX + 1, cornerY + 1 + Selected);
                    Env.SetColor();
                    _components[Selected].Print();
                    Selected--;
                }
                if (klawisz.Key == ConsoleKey.DownArrow && Selected < _components.Count - 1)
                {
                    Env.CursorPos(cornerX + 1, cornerY + 1 + Selected);
                    Env.SetColor();
                    _components[Selected].Print();
                    Selected++;
                }
                if (klawisz.Key == ConsoleKey.Enter)
                {
                    _selectedOption = _components[Selected].Option();
                    Env.SetColor();
                    return "r";
                }
                if (klawisz.Key == ConsoleKey.Tab)
                {
                    Env.CursorPos(cornerX + 1, cornerY + 1 + Selected);
                    Env.SetColor();
                    _components[Selected].Print();
                    return "";
                }
            }
        }
    };

    public class UIController
    {
        protected List<Window> windows;
        public UIController(){
            windows = new List<Window>();
        }
        public void AddWindow(Window w)
        {
            windows.Add(w);
        }
        public string SelectedOption()
        {
            return "";
        }
        public string DrawAndStart()
        {
            foreach (Window window in windows)
            {
                Env.SetColor(window.GetBorderColors().Item1, window.GetBorderColors().Item2);
                Env.CursorPos(window.GetCorner().X, window.GetCorner().Y);
                for (int j = 0; j < window.Width + 1; j++)
                { Console.Write("-"); }
                for (int i = 1; i < window.Height; i++)
                {
                    Env.CursorPos(window.GetCorner().X, window.GetCorner().Y + i);
                    Console.Write("|");
                    if (i <= window.ComponentsLenght())
                    {
                        Env.SetColor();
                        window.GetComponent(i - 1).Print();
                    }
                    Env.SetColor(window.GetBorderColors().Item1, window.GetBorderColors().Item2);
                    Env.CursorPos(window.GetCorner().X + window.Width, window.GetCorner().Y + i);
                    Console.Write("|");
                }
                Env.CursorPos(window.GetCorner().X, window.GetCorner().Y + window.Height);
                for (int j = 0; j < window.Width + 1; j++)
                {
                    Console.Write("-");
                }
            }
            for (int i = 0; true; i = (i + 1) % windows.Count)
            {
                string respone = windows[i].DrawAndStart();
                if (respone == "r")
                {
                    Console.Clear();
                    return windows[i].SelectedOption();
                }
            }
        }
    }
}