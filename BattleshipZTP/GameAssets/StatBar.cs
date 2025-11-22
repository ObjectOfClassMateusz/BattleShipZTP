using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.GameAssets
{
    public class StatBar
    {
        protected int v { get; set; }
        protected int curr_v { get; set; }

        ConsoleColor c { get; set; }
        protected int l;

        public StatBar(int value, ConsoleColor colour, int length)
        {
            this.v = value;
            this.c = colour;
            this.l = length;
            this.curr_v = value;
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
            double procent = (this.curr_v * 100) / this.v;
            procent =
                Math.Ceiling(
                    (procent * (3 + (2 * this.l)
                    +
                    (2 * num(this.v))))
                / 100);
            StringBuilder x = new StringBuilder();
            if (procent == 0 && this.curr_v != 0) { procent++; }
            x.Append("[");
            x.Append(' ', this.l);
            for (int i = 0; i < num(this.v) - num(this.curr_v); i++)
            { if (this.curr_v == 0) { i++; } x.Append(" "); }
            x.Append(this.curr_v.ToString());
            x.Append("/");
            x.Append(this.v.ToString());
            x.Append(' ', this.l);
            x.Append("]");
            for (int i = 0; i < x.Length; i++)
            {
                if (procent != 0)
                {
                    Console.BackgroundColor = (ConsoleColor)this.c;
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
            this.curr_v -= decrease;
            if (this.curr_v < 0)
            {
                this.curr_v = 0;
            }
        }
        public void Increase(int increase)
        {
            this.curr_v += increase;
            if (this.curr_v > this.v)
            {
                this.curr_v = this.v;
            }
        }
    }
}
