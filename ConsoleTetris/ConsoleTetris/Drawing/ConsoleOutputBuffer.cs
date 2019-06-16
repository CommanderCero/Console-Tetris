using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTetris.Game;

namespace ConsoleTetris.Drawing
{
    /// <summary>
    /// Helperclass that caches the output for the console and provides some utility Methods.
    /// Can be used to draw Complex structures, without any flickering that would normally appear.
    /// </summary>
    public class ConsoleOutputBuffer
    {
        public ConsoleColor CurrentDrawColor { get; set; } = ConsoleColor.White;
        public int Left { get; set; }
        public int Top { get; set; }

        public char UpperLeftCornerSymbol { get; set; } = '╔';
        public char UpperRightCornerSymbol { get; set; } = '╗';
        public char LowerLeftCornerSymbol { get; set; } = '╚';
        public char LowerRightCornerSymbol { get; set; } = '╝';

        public char HorizontalBorderSymbol { get; set; } = '═';
        public char VerticalBorderSymbol { get; set; } = '║';

        public int Width { get; }
        public int Height { get; }
        public char[,] OutputBuffer { get; }
        public ConsoleColor[,] ColorBuffer { get; }

        public ConsoleOutputBuffer(int width, int height)
        {
            Width = width;
            Height = height;

            OutputBuffer = new char[height,width];
            ColorBuffer = new ConsoleColor[height, width];
        }

        public void PrintToConsole()
        {
            for (var y = 0; y < Height; y++)
            {
                Console.SetCursorPosition(Left, Top + y);
                for (var x = 0; x < Width; x++)
                {
                    Console.ForegroundColor = ColorBuffer[y, x];
                    Console.Write(OutputBuffer[y, x] == '\0' ? ' ' : OutputBuffer[y, x]);
                }
            }
        }

        public void DrawString(int x, int y, string str)
        {
            for (var i = 0; i < str.Length; i++)
            {
                DrawSymbol(x + i, y, str[i]);
            }
        }

        public void DrawBorder(int xStart, int yStart, int innerWidth, int innerHeight)
        {
            var xEnd = xStart + innerWidth + 1;
            var yEnd = yStart + innerHeight + 1;

            // Create Corners
            DrawSymbol(xStart, yStart, UpperLeftCornerSymbol);
            DrawSymbol(xEnd, yStart, UpperRightCornerSymbol);
            DrawSymbol(xStart, yEnd, LowerLeftCornerSymbol);
            DrawSymbol(xEnd, yEnd, LowerRightCornerSymbol);

            // Create Horizontal Lines
            DrawSymbolBlock(xStart + 1, yStart, xEnd - 1, yStart, HorizontalBorderSymbol);
            DrawSymbolBlock(xStart + 1, yEnd, xEnd - 1, yEnd, HorizontalBorderSymbol);

            // Create Vertical Lines
            DrawSymbolBlock(xStart, yStart + 1, xStart, yEnd - 1, VerticalBorderSymbol);
            DrawSymbolBlock(xEnd, yStart + 1, xEnd, yEnd - 1, VerticalBorderSymbol);
        }

        public void DrawSymbol(int x, int y, char Symbol)
        {
            OutputBuffer[y, x] = Symbol;
            ColorBuffer[y, x] = CurrentDrawColor;
        }

        public void DrawSymbol(int x, int y, char Symbol, ConsoleColor color)
        {
            OutputBuffer[y, x] = Symbol;
            ColorBuffer[y, x] = color;
        }

        public void DrawSymbolBlock(int xStart, int yStart, int xEnd, int yEnd, char Symbol)
        {
            OutputBuffer.FillBlock(xStart, yStart, xEnd, yEnd, Symbol);
            ColorBuffer.FillBlock(xStart, yStart, xEnd, yEnd, CurrentDrawColor);
        }
    }
}
