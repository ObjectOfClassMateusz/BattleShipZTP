using BattleshipZTP.Utilities;
using Microsoft.VisualBasic.FileIO;
using System.Linq;
using BattleshipZTP.GameAssets;
using BattleshipZTP.Settings;

namespace BattleshipZTP.UI
{
    public interface IComponentUI
    {
        void   SetMargin(int width);
        int    GetMargin();
        void   Print(int rigthMarginFullfilment = 0);
        string GetOption();
        string HandleKey(ConsoleKey key);
    }

    public class Button : IComponentUI
    {
        //Button component that return an string option result
        string _option { get; set; } = "";
        public Button(string option)
        {
            _option = option;
        }
        public string GetOption()
        {
            return _option;
        }
        public string HandleKey(ConsoleKey key)
        {
            //Skip other handlers for other keys
            return "";
        }
        int _margin = 0;
        public void SetMargin(int width)
        {
            _margin = width;
        }
        public int GetMargin() => _margin;
        public void Print(int rigthMarginFullfilment=0)
        {
            int k = 0;
            for (int i=0; i< _margin; i++)
            {
                Console.Write(' ');k++;
            }
            Console.Write(GetOption()); 
            k += GetOption().Length;
            for (int i = 0; i < _margin; i++)
            {
                Console.Write(' ');k++;
            }
            int diff = (rigthMarginFullfilment - k)-1;
            for(int i = 0; i < diff; i++)
            {
                Console.Write(' ');
            }
        }
    }

    public class MaskedButton : IComponentUI
    {
        //Button component that return option result other than displayed
        string _option { get; set; } = "";
        string _display { get; set; } = "";

        string _backupOption { get; set; } = "";
        public MaskedButton(string option,string display)
        {
            _option = option;
            _display = display;
            _backupOption = _option;
        }
        public string GetOption()
        {
            return _option;
        }
        public string HandleKey(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
            {
                _option = _display;
            }
            //Skip other handlers for other keys
            return "";
        }
        int _margin = 0;
        public void SetMargin(int width)
        {
            _margin = width;
        }
        public int GetMargin() => _margin;
        public void Print(int rigthMarginFullfilment = 0)
        {
            _option = _backupOption;
            int k = 0;
            for (int i = 0; i < _margin; i++)
            {
                Console.Write(' '); k++;
            }
            Console.Write(GetOption());
            k += GetOption().Length;
            for (int i = 0; i < _margin; i++)
            {
                Console.Write(' '); k++;
            }
            int diff = (rigthMarginFullfilment - k) - 1;
            for (int i = 0; i < diff; i++)
            {
                Console.Write(' ');
            }
        }
    }

    public class CheckBox : IComponentUI
    {
        //Checkbox component that return boolean result
        string _booleanBody = "[ ]";
        bool _value = false;
        int _margin = 0;
        string _booleanName;
        public CheckBox(string boolName)
        {
            _booleanName = boolName;
        }
        public void SetMargin(int width)
        {
            _margin = width;
        }
        public int GetMargin() => _margin;
        public string GetOption() => "";
        public void Print(int rigthMarginFullfilment = 0)
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
            int diff = (rigthMarginFullfilment - k) - 1;
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
                    _booleanBody = "[ ]";//Uncheck
                    _value = false;
                    return $"checkbox-{_booleanName}";
                }
                else 
                {
                    _booleanBody = "[✓]";//Check
                    _value = true;
                    return $"checkbox-{_booleanName}";
                }
            }
            return "";//Skip
        }
    }

    public class TextBox : IComponentUI
    {
        //Editable user input that returns string result
        string _value;
        string _enterTextMark = "➤ ";
        string _option;
        int _charLimit = 5;
        public TextBox(string option, int charLimit=5 , string initialValue="")
        {
            if(charLimit < 0)
            {
                throw new ArgumentOutOfRangeException("");
            }
            _charLimit = charLimit;
            _option = option;
            _value = initialValue;
        }
        int _margin = 0;
        public void SetMargin(int width)
        {
            _margin = width;
        }
        public int GetMargin() => _margin;
        public string GetOption()
        {
            return _value;
        }
        public void Print(int rigthMarginFullfilment = 0)
        {
            int k = 0;
            for (int i = 0; i < _margin; i++)
            {
                Console.Write(' '); k++;
            }
            Console.Write(_enterTextMark);
            Console.Write(GetOption());
            k += GetOption().Length+2;
            for (int i = 0; i < _margin; i++)
            {
                Console.Write(' '); k++;
            }
            int diff = (rigthMarginFullfilment - k) - 1;
            for (int i = 0; i < diff; i++)
            {
                Console.Write(' ');
            }
        }
        public string HandleKey(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
                return "none";//Skip
            if (key == ConsoleKey.Backspace)
            {
                //Erase last character
                if (!string.IsNullOrEmpty(_value))
                    _value = _value[..^1];
                return $"input-{_option}#:{_value}";
            }
            if (_value.Length >= _charLimit)
            {
                //Character limit reached
                return "";
            }
            if(key == ConsoleKey.Spacebar)
            {
                _value += " ";
                return $"input-{_option}#:{_value}";
            }
            // Letters
            if (key >= ConsoleKey.A && key <= ConsoleKey.Z)
            {
                _value += key.ToString();
                return $"input-{_option}#:{_value}";
            }
            // Digits
            else if (key >= ConsoleKey.D0 && key <= ConsoleKey.D9)
            {
                _value += (char)('0' + (key - ConsoleKey.D0));
                return $"input-{_option}#:{_value}";
            }
            // Numpad digits
            else if (key >= ConsoleKey.NumPad0 && key <= ConsoleKey.NumPad9)
            {
                _value += (char)('0' + (key - ConsoleKey.NumPad0));
                return $"input-{_option}#:{_value}";
            }
            return "";//Skip
        }
    }

    public class IntegerSideBar : IComponentUI 
    {
        char _mark = '█';
        string _option;
        int _value ;
        public IntegerSideBar(string name , int value=50)
        {
            _value = value;
            _option = name;
        }
        int _margin = 0;
        public void SetMargin(int width)
        {
            _margin = width;
        }
        public int GetMargin() => _margin;
        public string GetOption()
        {
            return _option;
        }
        public void Print(int rigthMarginFullfilment = 0)
        {
            int k = 0;
            for (int i = 0; i < _margin; i++)
            {
                Console.Write(' '); k++;
            }
            Console.Write(GetOption());
            for(int i=0; i<=10; i++)
            {
                if(i == _value / 10)
                {
                    Console.Write(_mark);
                }
                else { Console.Write('-'); }
            }
            k += GetOption().Length + 11;
            for (int i = 0; i < _margin; i++)
            {
                Console.Write(' '); k++;
            }
            int diff = (rigthMarginFullfilment - k) - 1;
            for (int i = 0; i < diff; i++)
            {
                Console.Write(' ');
            }
        }
        public string HandleKey(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
                return "none";//Skip
            if (key == ConsoleKey.LeftArrow && _value!=0)
            {
                _value -= 10;
                return $"slider-{_option}#:{_value}";
            }
            if (key == ConsoleKey.RightArrow && _value!=100) {
                _value += 10;
                return $"slider-{_option}#:{_value}";
            }
            //Skip other handlers for other keys
            return "";
        }
    }

    public class TextOutput : IComponentUI 
    {
        //Block of text
        private string _text;
        int _margin = 0;
        public TextOutput(string content)
        {
            _text = content;
        }
        public string GetOption()
        {
            return _text;
        }
        public string HandleKey(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
                return "none";//Do none
            return "";//Skip
        }
        public void SetMargin(int width)
        {
            _margin = width;
        }
        public int GetMargin() => _margin;
        public void Print(int rigthMarginFullfilment = 0)
        {
            int k = 0;
            for (int i = 0; i < _margin; i++)
            {
                Console.Write(' '); k++;
            }
            Console.Write(GetOption());
            k += GetOption().Length;
            for (int i = 0; i < _margin; i++)
            {
                Console.Write(' '); k++;
            }
            int diff = (rigthMarginFullfilment - k) - 1;
            for (int i = 0; i < diff; i++)
            {
                Console.Write(' ');
            }
        }
    }

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
        Window _window;
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
        readonly List<IComponentUI> _components = new List<IComponentUI>();

        void ReCalcSize()
        {
            int longest_string = 0;
            foreach (IComponentUI c in _components)
            {
                if (c.GetOption().Length + c.GetMargin() * 2 > longest_string)
                {
                    longest_string = c.GetOption().Length + c.GetMargin() * 2;
                }
            }
            this._width = (longest_string + 1 > _width)
                ? longest_string + 1
                : _width;
            this._height = this._components.Count + 1 > _height
                ? this._components.Count + 1
                : _height;
        }

        public void Remove(int index)
        {
            _components.RemoveAt(index);
            ReCalcSize();
        }

        public void Add(IComponentUI component)
        {
            _components.Add(component);
            ReCalcSize();
        }

        public IComponentUI GetComponent(int index) => _components[index];
        public int ComponentsLenght() => _components.Count;
        
        int cornerX;
        int cornerY;
        public void SetPosition(int startPlaceX , int startPlaceY)
        {
            this.cornerX = startPlaceX;
            this.cornerY = startPlaceY;
        }
        public (int X, int Y) GetCorner() => (cornerX, cornerY);

        int _width { get; set; }
        int _height { get; set; }
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public void SetSize(int width = 0, int height = 0)
        {
            this._width = width;
            this._height = height;
        }

        (ConsoleColor foreground, ConsoleColor background) _borderColor; 
        (ConsoleColor foreground, ConsoleColor background) _highlightColor;
        public (ConsoleColor foreground, ConsoleColor background) GetBorderColors()
        {
            return _borderColor;
        }
        public void SetBorderColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            this._borderColor.foreground = foregroundColor;
            this._borderColor.background = backgroundColor;
        }
        public (ConsoleColor foreground, ConsoleColor background) GetHighColors()
        {
            return _highlightColor;
        }
        public void SetHighColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            this._highlightColor.foreground = foregroundColor;
            this._highlightColor.background = backgroundColor;
        }

        public Window(){}
        string _selectedOption;
        public string SelectedOption() => this._selectedOption;

        int lastRemembered = 0;

        public string DrawAndStart()
        {
            ConsoleKeyInfo klawisz;//user input key
            int Selected = lastRemembered;
            while (true)
            {
                //print highlighted option
                Env.CursorPos(cornerX + 1, cornerY + 1 + Selected);
                Env.SetColor(_highlightColor.foreground, _highlightColor.background);
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
                    if (UserSettings.Instance.SfxEnabled == true)
                    {
                        AudioManager.Instance.Play("przyciski");
                    }
                    Env.CursorPos(cornerX + 1, cornerY + 1 + Selected);
                    Env.SetColor();
                    _components[Selected].Print(Width);
                    Selected--;//Up
                }
                if (klawisz.Key == ConsoleKey.DownArrow && Selected < _components.Count - 1)
                {
                    //downlight previous component
                    if (UserSettings.Instance.SfxEnabled == true)
                    {
                        AudioManager.Instance.Play("przyciski");
                    }
                    Env.CursorPos(cornerX + 1, cornerY + 1 + Selected);
                    Env.SetColor();
                    _components[Selected].Print(Width);
                    Selected++;//Down
                }
                if (klawisz.Key == ConsoleKey.Enter)
                {
                    _selectedOption = _components[Selected].GetOption();
                    if (UserSettings.Instance.SfxEnabled == true)
                    {
                        AudioManager.Instance.Play("przyciski");
                    }
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
                    if (UserSettings.Instance.SfxEnabled == true)
                    {
                        AudioManager.Instance.Play("przyciski");
                    }
                    _components[Selected].Print(Width);
                            //Empty Respone, take to the next Window
                    return "";
                }
                lastRemembered = Selected;
            }
        }
    }

    public class UIController
    {
        protected List<Window> windows;//list of windows
        public UIController(){
            windows = new List<Window>();
        }
        public void AddWindow(Window w)
        {
            windows.Add(w);
        }

        bool _valid = false;
        void OnceValidate()
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
            DrawAndEndSequence();
            // Main navigation
            List<string> OptionsReturns = new List<string>();
            for (int i = 0; true; i = (i + 1) % windows.Count)
            {
                string respone = windows[i].DrawAndStart();
                
                if (respone == "r")//if user pressed Enter for Button
                {
                    OptionsReturns.Add(windows[i].SelectedOption());
                    return OptionsReturns;//return component option as the string
                }
                
                if (respone == "r-handle")//if user pressed handled key
                {
                    string optionHandlerString = windows[i].SelectedOption();
                    /*
                     * if checkbox-{name}           -> bla bla ...
                     * if text-{input}              -> bla bla ...
                     * if intSlider-{number}-{name} -> bla bla ...
                     */
                    if(optionHandlerString.Contains("input-"))
                    {
                        //Handle TextBox user inputs
                        string inputId = optionHandlerString.Split(new[] { "#:" }, StringSplitOptions.None)[0];//separate
                        string value = optionHandlerString.Split(new[] { "#:" }, StringSplitOptions.None)[1];
                        if (!OptionsReturns.Any(s => s.Contains(inputId)))
                        {
                            OptionsReturns.Add($"{inputId}#:{value}");
                        }
                        else
                        {
                            int index = OptionsReturns.FindIndex(s => s.Contains(inputId));
                            OptionsReturns[index] = $"{inputId}#:{value}";
                        }
                        if(value == "")
                        {
                            OptionsReturns.Remove(optionHandlerString);
                        }
                    }
                    else if (optionHandlerString.Contains("slider-"))
                    {
                        //Handle slider user inputs
                        string inputId = optionHandlerString.Split(new[] { "#:" }, StringSplitOptions.None)[0];
                        string value = optionHandlerString.Split(new[] { "#:" }, StringSplitOptions.None)[1];
                        if (!OptionsReturns.Any(s => s.Contains(inputId)))
                        {
                            OptionsReturns.Add($"{inputId}#:{value}");
                        }
                        else
                        {
                            int index = OptionsReturns.FindIndex(s => s.Contains(inputId));
                            OptionsReturns[index] = $"{inputId}#:{value}";
                        }
                    }
                    else
                    {
                        //Handle CheckBox user inputs
                        if (!OptionsReturns.Contains(optionHandlerString) && optionHandlerString != "none")
                        {
                            OptionsReturns.Add(optionHandlerString);
                        }
                        else
                        {
                            OptionsReturns.Remove(optionHandlerString);
                        }
                    }
                    i--;
                }
            }
        }
        public void DrawAndEndSequence()
        {
            if (!_valid)
            {
                OnceValidate();
            }
            foreach (Window window in windows)
            {
                //Do the draw

                //color border and set cursor to start the drawing
                Env.SetColor(window.GetBorderColors().foreground, window.GetBorderColors().background);
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

                    Env.SetColor(window.GetBorderColors().foreground, window.GetBorderColors().background);
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
        }
    }
}