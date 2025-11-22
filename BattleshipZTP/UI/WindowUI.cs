using BattleshipZTP.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.UI
{
    public interface IComponentUI
    {
        void SetColors(ConsoleColor main, ConsoleColor background);
        void SetSize(int width, int height);
        void Display();
        void Highlight();
    }
    public class Button// : IComponentUI
    {
        public string option {  get; set; }
    }
    /*
    public class Button : IComponentUI 
    {
        private readonly string _content;
        private int _width=0;
        private int _height=0;
        public Button(string content)
        {
            _content = content;
        }
        void SetSize(int width, int height) { 
            _width = width; _height = height;
        }
        void SetCharacterStyle(params char[] chars)
        {
            if(chars.Count() != 2)
            {
                throw new ArgumentException("");
            }
        }
    }*/
    //public class CheckBox : IComponentUI { }
    //public class TextBox : IComponentUI { }
    //public class IntegerSideBar : IComponentUI { }


    //Casual builder = WindowBuilder
    //Main-menu = MainMenuBuilder



    public interface IWindowBuilder
    {
        IWindowBuilder SetPosition(int startPlaceX, int startPlaceY);
        IWindowBuilder SetSize(int width=0, int height=0);
        IWindowBuilder ColorBorders(ConsoleColor main, ConsoleColor background);
        IWindowBuilder AddComponent(Button component);
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
        public IWindowBuilder ColorBorders(ConsoleColor main, ConsoleColor background)
        {
            _window.SetColors(main, background);
            return this;
        }
        public IWindowBuilder AddComponent(Button component)
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
        private readonly List<Button> _components = new List<Button>();
        public void Add(Button component)
        {
            _components.Add(component);
            int longest_string = 0;
            foreach (Button c in _components)
            {
                if (c.option.Length > longest_string)
                {
                    longest_string = c.option.Length;
                }
            }
            this._width = (longest_string + 2 > _width) 
                ? longest_string + 2 
                : _width;
            this._height = this._components.Count + 1 > _height 
                ? this._components.Count + 1 
                : _height;
        }
        public Button GetButton(int index) => _components[index];
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

        private ConsoleColor _mainColor;
        private ConsoleColor _backgroundColor;
        public void SetColors(ConsoleColor mainColor, ConsoleColor backgroundColor)
        {
            this._mainColor = mainColor;
            this._backgroundColor = backgroundColor;
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
                Console.SetCursorPosition(cornerX + 1, cornerY + 1 + Selected);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write(_components[Selected].option);
                klawisz = Console.ReadKey(true);
                if (klawisz.Key == ConsoleKey.UpArrow && Selected != 0)
                {
                    Console.SetCursorPosition(cornerX + 1, cornerY + 1 + Selected);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(_components[Selected].option);
                    Selected--;
                    Console.SetCursorPosition(cornerX + 1, cornerY + 1 + Selected);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(_components[Selected].option);
                }
                if (klawisz.Key == ConsoleKey.DownArrow && Selected < _components.Count - 1)
                {
                    Console.SetCursorPosition(cornerX + 1, cornerY + 1 + Selected);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(_components[Selected].option);
                    Selected++;
                    Console.SetCursorPosition(cornerX + 1, cornerY + 1 + Selected);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(_components[Selected].option);
                }
                if (klawisz.Key == ConsoleKey.Enter)
                {
                    _selectedOption = _components[Selected].option;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    return "r";
                }
                if (klawisz.Key == ConsoleKey.Tab)
                {
                    Console.SetCursorPosition(cornerX + 1, cornerY + 1 + Selected);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(_components[Selected].option);
                    return "";
                }
            }
        }
    };

    public class UIController
    {
        private (ConsoleColor, ConsoleColor) _borderColor;
        protected List<Window> windows;
        public UIController(ConsoleColor mainColor, ConsoleColor backgroundColor)
        {
            _borderColor.Item1 = mainColor;
            _borderColor.Item2 = backgroundColor;
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
                Console.BackgroundColor = _borderColor.Item2;
                Console.ForegroundColor = _borderColor.Item1;
                Console.SetCursorPosition(window.GetCorner().X, window.GetCorner().Y);
                for (int j = 0; j < window.Width + 1; j++)
                { Console.Write("-"); }
                for (int i = 1; i < window.Height; i++)
                {
                    Console.SetCursorPosition(window.GetCorner().X, window.GetCorner().Y + i);
                    Console.Write("|");
                    if (i <= window.ComponentsLenght())
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(window.GetButton(i - 1).option);
                    }
                    Console.BackgroundColor = _borderColor.Item2;
                    Console.ForegroundColor = _borderColor.Item1;
                    Console.SetCursorPosition(window.GetCorner().X + window.Width, window.GetCorner().Y + i);
                    Console.Write("|");
                }
                Console.SetCursorPosition(window.GetCorner().X, window.GetCorner().Y + window.Height);
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

/*



public class WindowBuilder : IWindowBuilder
{
    private int _x, _y;
    private int _width, _height;
    private ConsoleColor _main, _bg;
    private List<string> _buttons = new();

    public IWindowBuilder SetPosition(int x, int y)
    {
        _x = x; _y = y;
        return this;
    }

    public IWindowBuilder SetSize(int width, int height)
    {
        _width = width; _height = height;
        return this;
    }

    public IWindowBuilder SetColors(ConsoleColor main, ConsoleColor background)
    {
        _main = main; _bg = background;
        return this;
    }

    public IWindowBuilder AddButton(string text)
    {
        _buttons.Add(text);
        return this;
    }

    public Window Build()
    {
        return new Window(_x, _y, _width, _height, _main, _bg, _buttons.ToArray());
    }
}



Window menu = new WindowBuilder()
    .SetPosition(10, 10)
    .SetSize(20, 10)
    .SetColors(ConsoleColor.White, ConsoleColor.Blue)
    .AddButton("Start")
    .AddButton("Exit")
    .Build();



*/