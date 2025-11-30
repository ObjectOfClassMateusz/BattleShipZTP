using BattleshipZTP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.GameAssets
{
    internal class BattleBoard
    {
        private class Field
        {
            public int X { get; set; }
            public int Y { get; set; }
            public char Character { get; set; }
            public (ConsoleColor, ConsoleColor) colors = (ConsoleColor.White, ConsoleColor.Black);
            public Field(char character, int x, int y)
            {
                this.Character = character;
                this.X = x;
                this.Y = y;
            }
            public override string ToString()
            {
                Env.SetColor(colors.Item1, colors.Item2);
                return this.Character.ToString();
            }
        }

        private int cornerX;
        private int cornerY;
        private char[,] _board;//[y,x]
        private Field[,] _field;
        private const int width = 90;
        private const int height = 16;
        public BattleBoard(int x=0 , int y=0)
        {
            cornerX = x;
            cornerY = y;
            _board = new char[height,width];
            _field = new Field[width,height];
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) { 
                    _board[i,j] = ' ';
                    _field[i, j] = new Field(' ', i, j);
                }
            }
        }
        
        public void Display()
        {
            Drawing.SetColors( ConsoleColor.Black, ConsoleColor.DarkGray);
            Drawing.DrawRight('#', width+2, cornerX        ,cornerY);
            Drawing.DrawDown( '#', height,  cornerX        ,cornerY+1);
            Drawing.DrawDown( '#', height,  cornerX+width+1,cornerY+1);
            Env.CursorPos(cornerX+1, cornerY+1);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++) 
                {
                    Console.Write(_board[i,j]);
                }
                Env.CursorPos(cornerX+1, cornerY+i+2);
            }
            Drawing.DrawRight('#', width+2, cornerX, cornerY+1+height);
        }
    }
}


/*
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.GameAssets
{
    public class Field
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Character { get; set; }
        public (ConsoleColor, ConsoleColor) colors;
        public Field(char character , int x , int y)
        {
            this.Character = character;
            this.X = x;
            this.Y = y;
            colors.Item1 = ConsoleColor.White;
            colors.Item2 = ConsoleColor.Black;
        }
        public override string ToString()
        {
            Env.SetColor(colors.Item1,colors.Item2);
            return this.Character.ToString();
        }
    }

    internal class BattleBoard
    {
        private int cornerX;
        private int cornerY;
        private Field[,] _board;//[y,x]
        public BattleBoard(int x=0 , int y=0)
        {
            cornerX = x;
            cornerY = y;
            _board = new Field[16,64];
            for (int i = 0; i < 16; i++) {
                for (int j = 0; j < 64; j++) {
                    _board[i, j] = new Field('-', j, i);
                }
            }
        }
        
        public void Display()
        {
            Env.CursorPos(cornerX, cornerY);
            for (int i = 0; i < 66; i++)
            {
                Console.Write("#");
            }
            Env.CursorPos(cornerX+1, cornerY+1);
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 64; j++) 
                {
                    Console.Write(_board[i,j]);
                }
                Env.CursorPos(cornerX+1, cornerY+i+2);
            }
            Env.CursorPos(cornerX, cornerY + 17);
            for (int i = 0; i < 66; i++)
            {
                Console.Write("#");
            }
        }
    }
}

 */