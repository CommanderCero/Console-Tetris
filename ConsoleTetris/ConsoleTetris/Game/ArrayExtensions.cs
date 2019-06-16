using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris.Game
{
    public static class ArrayExtensions
    {
        // Move a whole Block in an array
        public static void MoveBlock<T>(this T[,] sourceArr, int blockStartIndex, int blockLength, int blockNewStartIndex)
        {
            for (var yOff = blockLength - 1; yOff >= blockStartIndex; yOff--)
            {
                for (var x = 0; x < sourceArr.GetLength(1); x++)
                {
                    sourceArr[blockNewStartIndex + yOff, x] = sourceArr[blockStartIndex + yOff, x];
                }
            }
        }

        // Fill a whole Block with an specified element
        public static void FillBlock<T>(this T[,] sourceArr, int blockStartIndex, int blockLength, T fillElement)
        {
            for (var y = blockStartIndex; y < blockLength + blockStartIndex; y++)
            {
                for (var x = 0; x < sourceArr.GetLength(1); x++)
                {
                    sourceArr[y, x] = fillElement;
                }
            }
        }

        public static void FillBlock<T>(this T[,] sourceArr, int xStart, int yStart, int xEnd, int yEnd, T fillElement)
        {
            for (var y = yStart; y <= yEnd; y++)
            {
                for (var x = xStart; x <= xEnd; x++)
                {
                    sourceArr[y, x] = fillElement;
                }
            }
        }
    }
}
