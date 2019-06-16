using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTetris.Game;

namespace ConsoleTetris.Drawing
{
    /// <summary>
    /// Helperclass to print Tetris to the Console
    /// </summary>
    public class TetrisConsolePrinter
    {
        public Dictionary<TetrisBlockType, ConsoleColor> TetrisBlockTypeColors = new Dictionary<TetrisBlockType, ConsoleColor>()
        {
            {TetrisBlockType.I, ConsoleColor.Red},
            {TetrisBlockType.J, ConsoleColor.Yellow},
            {TetrisBlockType.L, ConsoleColor.Magenta},
            {TetrisBlockType.O, ConsoleColor.Blue},
            {TetrisBlockType.S, ConsoleColor.Cyan},
            {TetrisBlockType.T, ConsoleColor.Green},
            {TetrisBlockType.Z, ConsoleColor.Cyan}
        };

        public string ScoreHeader { get; set; } = "Score";
        public char BlockSymbol { get; set; } = '█';

        public void PrintTetris(int left, int top, Tetris state)
        {
            var buffer = new ConsoleOutputBuffer(state.Width + 2 + 7, state.Height + 2);

            // Draw the tetris field
            buffer.DrawBorder(left, top, state.Width, state.Height);
            PrintGrid(buffer, left + 1, top + 1, state.Grid);
            PrintTetrisBlock(buffer, state.CurrentBlock.X + 1, state.CurrentBlock.Y + 1, state.CurrentBlock);

            // Draw the upcoming block
            buffer.DrawBorder(left + state.Width + 3, top, 4, 2);
            PrintTetrisBlock(buffer, left + state.Width + 4, top + 1, state.UpcomingBlock);

            // Draw score
            buffer.DrawString(left + state.Width + 3, top + 5, ScoreHeader);
            buffer.DrawString(left + state.Width + 3, top + 6, state.Score.ToString());

            buffer.PrintToConsole();
        }

        private void PrintGrid(ConsoleOutputBuffer buffer, int left, int top, TetrisBlockType[,] grid)
        {
            var height = grid.GetLength(0);
            var width = grid.GetLength(1);
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (grid[y, x] != TetrisBlockType.Empty)
                    {
                        var drawColor = TetrisBlockTypeColors[grid[y, x]];
                        buffer.DrawSymbol(left + x, top + y, BlockSymbol, drawColor);
                    }
                }
            }
        }

        private void PrintTetrisBlock(ConsoleOutputBuffer buffer, int left, int top, TetrisBlock block)
        {
            var height = block.Height;
            var width = block.Width;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (block.Shape[y, x])
                    {
                        var drawColor = TetrisBlockTypeColors[block.Type];
                        buffer.DrawSymbol(left + x, top + y, BlockSymbol, drawColor);
                    }
                }
            }
        }
    }
}
