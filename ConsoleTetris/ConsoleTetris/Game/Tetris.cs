using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTetris.Game;

namespace ConsoleTetris
{
    /// <summary>
    /// A Tetris simulation
    /// 
    /// Currently contains no Level-System and has no GameOver
    /// </summary>
    public class Tetris
    {
        public long Score { get; private set; }
        public int Width { get; }
        public int Height { get; }

        public TetrisBlockType[,] Grid { get; }
        public TetrisBlock CurrentBlock { get; private set; }
        public TetrisBlock UpcomingBlock { get; private set; }

        private readonly Random randomGenerator;

        public Tetris(int width = 10, int height = 15)
        {
            Width = width;
            Height = height;

            Grid = new TetrisBlockType[Height, Width];
            randomGenerator = new Random();

            CurrentBlock = ChooseRandomBlock();
            UpcomingBlock = ChooseRandomBlock();
        }

        public void Step()
        {
            if (CurrentBlock.FitsInGrid(Grid, CurrentBlock.X, CurrentBlock.Y + 1))
            {
                CurrentBlock.Y += 1;
            }
            else
            {
                CurrentBlock.PlaceInGrid(Grid, CurrentBlock.X, CurrentBlock.Y);
                CurrentBlock = UpcomingBlock;
                UpcomingBlock = ChooseRandomBlock();

                RemoveFullLines();
            }
        }

        public void MoveLeft()
        {
            if (CurrentBlock.FitsInGrid(Grid, CurrentBlock.X - 1, CurrentBlock.Y))
            {
                CurrentBlock.X -= 1;
            }
        }

        public void MoveRight()
        {
            if (CurrentBlock.FitsInGrid(Grid, CurrentBlock.X + 1, CurrentBlock.Y))
            {
                CurrentBlock.X += 1;
            }
        }

        public void RotateRight()
        {
            var rotatedBlock = CurrentBlock.Rotate();
            if (rotatedBlock.FitsInGrid(Grid, CurrentBlock.X, CurrentBlock.Y))
            {
                CurrentBlock = rotatedBlock;
            }
        }

        private void RemoveFullLines()
        {
            var lineCount = 0;
            for (var y = 0; y < Height; y++)
            {
                var isFull = true;
                for (var x = 0; x < Width; x++)
                {
                    if (Grid[y, x] == TetrisBlockType.Empty)
                    {
                        isFull = false;
                    }
                }

                if (isFull)
                {
                    lineCount++;

                    // Move the whole grid above the full line one line down
                    Grid.MoveBlock(0, y, 1);
                    Grid.FillBlock(0, 1, TetrisBlockType.Empty);
                    // Check the current line again
                    y--;
                }
            }

            // Award Score
            switch (lineCount)
            {
                case 1: Score += 40; break;
                case 2: Score += 100; break;
                case 3: Score += 300; break;
                case 4: Score += 1200; break;
            }
        }

        private TetrisBlock ChooseRandomBlock()
        {
            var randomIndex = randomGenerator.Next(TetrisBlock.AllBlocks.Length);
            var block = TetrisBlock.AllBlocks[randomIndex].Clone();
            block.X = Width / 2 - block.Width / 2 - 1;

            return block;
        }
    }
}
