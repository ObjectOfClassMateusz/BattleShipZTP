using BattleshipZTP.Utilities;

namespace BattleshipZTP.UI
{
    public interface IComponentUI
    {
        void   SetMargin(int width);
        int    GetMargin();
        void   Print(int fulfilment=0);
        string Option();
        string HandleKey(ConsoleKey key);
    }

    public class Button : IComponentUI
    {
        private string _option { get; set; } = "";
        public Button(string opt)
        {
            _option = opt;
        }
        public string Option()
        {
            return _option;
        }

        public string HandleKey(ConsoleKey key)
        {
            return "";
        }

        private int _margin = 0;
        public void SetMargin(int width)
        {
            _margin = width;
        }
        public int GetMargin() => _margin;
        public void Print(int fulfilment=0)
        {
            int k = 0;
            for (int i=0; i< _margin; i++)
            {
                Console.Write(' ');k++;
            }
            Console.Write(Option()); 
            k += Option().Length;
            for (int i = 0; i < _margin; i++)
            {
                Console.Write(' ');k++;
            }
            int diff = (fulfilment - k)-1;
            for(int i = 0; i < diff; i++)
            {
                Console.Write(' ');
            }
        }
    }

    public class CheckBox : IComponentUI
    {
        private int _margin = 0;
        public void SetMargin(int width)
        {
            _margin = width;
        }
        public int GetMargin() => _margin;
        public string Option() => "";

        private string _booleanName;
        public CheckBox(string boolName)
        {
            _booleanName = boolName;
        }
        private string _booleanBody = "[ ]";
        private bool _value = false;

        public void Print(int fulfilment = 0)
        {
            int k = 0;
            for (int i = 0; i < _margin; i++)
            {
                Console.Write(' '); k++;
            }
            Console.Write(_booleanName+" "+_booleanBody);
            k += _booleanName.Length+4;
            for (int i = 0; i < _margin; i++)
            {
                Console.Write(' '); k++;
            }
            int diff = (fulfilment - k) - 1;
            for (int i = 0; i < diff; i++)
            {
                Console.Write(' ');
            }
        }
        
        public string HandleKey(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter) 
            {
                if (_value)
                {
                    _booleanBody = "[ ]";
                    _value = false;
                    return $"checkbox-{_booleanName}";
                }
                else 
                {
                    _booleanBody = "[X]";
                    _value = true;
                    return $"checkbox-{_booleanName}";
                }
            }
            return "";
        }
    }

    //public class TextBox : IComponentUI { }
    //public class IntegerSideBar : IComponentUI { }
    //public class TextInput

    public interface IWindowBuilder
    {
        IWindowBuilder SetPosition(int startPlaceX, int startPlaceY);
        IWindowBuilder SetSize(int width=0, int height=0);
        IWindowBuilder ColorBorders(ConsoleColor foreground, ConsoleColor background);
        IWindowBuilder ColorHighlights(ConsoleColor foreground, ConsoleColor background);
        IWindowBuilder AddComponent(IComponentUI component);
        Window Build();
        void ResetBuilder();
    }
    public class WindowBuilder : IWindowBuilder
    {
        private Window _window;
        public WindowBuilder()
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
        public void ResetBuilder()
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
                if (c.Option().Length + c.GetMargin()*2 > longest_string)
                {
                    longest_string = c.Option().Length + c.GetMargin()*2;
                }
            }
            this._width = (longest_string + 1 > _width) 
                ? longest_string + 1 
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

        private int lastRemembered = 0;

        public string DrawAndStart()
        {
            ConsoleKeyInfo klawisz;//user input key
            int Selected = lastRemembered;
            while (true)
            {
                //print highlighted option
                Env.CursorPos(cornerX + 1, cornerY + 1 + Selected);
                Env.SetColor(_highlightColor.Item1, _highlightColor.Item2);
                _components[Selected].Print(Width);

                klawisz = Console.ReadKey(true);

                //custom handler for components
                string result = _components[Selected].HandleKey(klawisz.Key);
                if (result != "")
                {
                    _selectedOption = result;
                    Env.SetColor();
                    return "r-handle";
                }

                if (klawisz.Key == ConsoleKey.UpArrow && Selected != 0)
                {
                    //downlight previous component
                    Env.CursorPos(cornerX + 1, cornerY + 1 + Selected);
                    Env.SetColor();
                    _components[Selected].Print(Width);
                    Selected--;//Up
                }
                if (klawisz.Key == ConsoleKey.DownArrow && Selected < _components.Count - 1)
                {
                    //downlight previous component
                    Env.CursorPos(cornerX + 1, cornerY + 1 + Selected);
                    Env.SetColor();
                    _components[Selected].Print(Width);
                    Selected++;//Down
                }
                if (klawisz.Key == ConsoleKey.Enter)
                {
                    _selectedOption = _components[Selected].Option();
                    Env.SetColor();
                    //Selected Option Respone, return Component option
                    if(_selectedOption != "")
                    {
                        return "r";
                    }
                }
                if (klawisz.Key == ConsoleKey.Tab)
                {
                    Env.CursorPos(cornerX + 1, cornerY + 1 + Selected);
                    Env.SetColor();
                    _components[Selected].Print(Width);
                    //Empty Respone, take to the next Window
                    return "";
                }
                lastRemembered = Selected;
            }
        }
    };

    public class UIController
    {
        protected List<Window> windows;//list of windows
        public UIController(){//ctor
            windows = new List<Window>();
        }
        public void AddWindow(Window w)
        {
            windows.Add(w);
        }

        private bool _valid = false;
        private void OnceValidate()
        {
            if (windows.Count == 0)
            {
                throw new Exception("No windows found for this controller");
            }
            if (windows[0].ComponentsLenght() == 0)
            {
                throw new Exception("No components found for this window");
            }
            _valid = true;
        }
        public List<string> DrawAndStart()
        {
            if (!_valid)
            {
                OnceValidate();
            }
            foreach (Window window in windows)
            {
                //Do the draw

                //color border and set cursor to start the drawing
                Env.SetColor(window.GetBorderColors().Item1, window.GetBorderColors().Item2);
                Env.CursorPos(window.GetCorner().X, window.GetCorner().Y);

                // top border --------------------
                for (int j = 0; j < window.Width + 1; j++)
                { Console.Write("-"); }

                // list of window components
                //              ...
                //          |component1 |
                //          |component2 |
                //              ...
                for (int i = 1; i < window.Height; i++)
                {
                    Env.CursorPos(window.GetCorner().X, window.GetCorner().Y + i);
                    Console.Write("|");//left wall

                    if (i <= window.ComponentsLenght())
                    {
                        Env.SetColor();
                        window.GetComponent(i - 1).Print(window.Width);//dispay component
                    }

                    Env.SetColor(window.GetBorderColors().Item1, window.GetBorderColors().Item2);
                    Env.CursorPos(window.GetCorner().X + window.Width, window.GetCorner().Y + i);
                    Console.Write("|");//right wall
                }
                //bottom border --------------------
                Env.CursorPos(window.GetCorner().X, window.GetCorner().Y + window.Height);
                for (int j = 0; j < window.Width + 1; j++)
                {
                    Console.Write("-");
                }
            }

            // Main navigation
            List<string> OptionsReturns = new List<string>();

            for (int i = 0; true; i = (i + 1) % windows.Count)
            {
                string respone = windows[i].DrawAndStart();

                //if user pressed Enter for Button
                if (respone == "r")
                {
                    OptionsReturns.Add(windows[i].SelectedOption());
                    return OptionsReturns;//return component option as the string
                }
                //if user pressed handled key
                if (respone == "r-handle")
                {
                    string optionHandlerString = windows[i].SelectedOption();
                    /*
                     * if checkbox-{name}           -> bla bla ...
                     * if text-{input}              -> bla bla ...
                     * if intSlider-{number}-{name} -> bla bla ...
                     */
                    if (!OptionsReturns.Contains(optionHandlerString))
                    {
                        OptionsReturns.Add(optionHandlerString);
                    }
                    else
                    {
                        OptionsReturns.Remove(optionHandlerString);
                    }
                    i--;
                }
            }
        }
    }
}