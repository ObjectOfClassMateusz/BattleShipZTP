using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.GameAssets
{
    //Statictic Bar
    public class StatBar
    {
        protected int value { get; set; }
        protected int currentValue { get; set; }

        protected ConsoleColor color { get; set; }
        protected int length;

        public StatBar(int value, ConsoleColor colour, int length)
        {
            this.value = value;
            this.color = colour;
            this.length = length;
            this.currentValue = value;
        }
        public void Show()
        {
            Func<int, short> num = (a) =>
            {
                short wynik = 0;
                while (a != 0)
                { a = a / 10; wynik++; }
                return wynik;
            };
            //
            double procent = (this.currentValue * 100) / this.value;
            procent =
                Math.Ceiling(
                    (procent * (3 + (2 * this.length)
                    +
                    (2 * num(this.value))))
                / 100);
            StringBuilder x = new StringBuilder();
            if (procent == 0 && this.currentValue != 0) { procent++; }
            x.Append("[");
            x.Append(' ', this.length);
            for (int i = 0; i < num(this.value) - num(this.currentValue); i++)
            { if (this.currentValue == 0) { i++; } x.Append(" "); }
            x.Append(this.currentValue.ToString());
            x.Append("/");
            x.Append(this.value.ToString());
            x.Append(' ', this.length);
            x.Append("]");
            for (int i = 0; i < x.Length; i++)
            {
                if (procent != 0)
                {
                    Console.BackgroundColor = (ConsoleColor)this.color;
                    procent--;
                }
                else
                {
                    Console.ResetColor();
                }
                Console.Write(x[i]);
            }
            Console.ResetColor();
        }
        public void Decrease(int decrease)
        {
            this.currentValue -= decrease;
            if (this.currentValue < 0)
            {
                this.currentValue = 0;
            }
        }
        public void Increase(int increase)
        {
            this.currentValue += increase;
            if (this.currentValue > this.value)
            {
                this.currentValue = this.value;
            }
        }
    }
}
