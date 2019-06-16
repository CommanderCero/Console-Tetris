using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTetris.Game;

namespace ConsoleTetris
{
    public class TetrisBlock
    {
        #region Default Shape Constants
        public static readonly TetrisBlock I = new TetrisBlock(TetrisBlockType.I, new List<bool[,]>
        {
            new [,]
            {
                {false, false, false, false},
                {true, true, true, true},
                {false, false, false, false},
                {false, false, false, false}
            },
            new [,]
            {
                {false, false, true, false},
                {false, false, true, false},
                {false, false, true, false},
                {false, false, true, false}
            },
            new [,]
            {
                {false, false, false, false},
                {false, false, false, false},
                {true, true, true, true},
                {false, false, false, false}
            },
            new [,]
            {
                {false, true, false, false},
                {false, true, false, false},
                {false, true, false, false},
                {false, true, false, false}
            },
        });

        public static readonly TetrisBlock J = new TetrisBlock(TetrisBlockType.J, new List<bool[,]>
        {
            new [,]
            {
                {true, false, false},
                {true, true, true},
                {false, false, false},
            },
            new [,]
            {
                {false, true, true,},
                {false, true, false},
                {false, true, false},
            },
            new [,]
            {
                {false, false, false},
                {true, true, true},
                {false, false, true},
            },
            new [,]
            {
                {false, true, false},
                {false, true, false},
                {true, true, false},
            }
        });

        public static readonly TetrisBlock L = new TetrisBlock(TetrisBlockType.L, new List<bool[,]>
        {
            new [,]
            {
                {false, false, true},
                {true, true, true},
                {false, false, false},
            },
            new [,]
            {
                {false, true, false},
                {false, true, false},
                {false, true, true},
            },
            new [,]
            {
                {false, false, false},
                {true, true, true},
                {true, false, false},
            },
            new [,]
            {
                {true, true, false},
                {false, true, false},
                {false, true, false},
            }
        });

        public static readonly TetrisBlock O = new TetrisBlock(TetrisBlockType.O, new List<bool[,]>
        {
            new [,]
            {
                {true, true},
                {true, true}
            },
            new [,]
            {
                {true, true},
                {true, true}
            },
            new [,]
            {
                {true, true},
                {true, true}
            },
            new [,]
            {
                {true, true},
                {true, true}
            }
        });

        public static readonly TetrisBlock S = new TetrisBlock(TetrisBlockType.S, new List<bool[,]>
        {
            new [,]
            {
                {false, true, true},
                {true, true, false},
                {false, false, false},
            },
            new [,]
            {
                {false, true, false},
                {false, true, true},
                {false, false, true},
            },
            new [,]
            {
                {false, false, false},
                {false, true, true},
                {true, true, false}
            },
            new [,]
            {
                {true, false, false},
                {true, true, false},
                {false, true, false},
            }
        });

        public static readonly TetrisBlock T = new TetrisBlock(TetrisBlockType.T, new List<bool[,]>
        {
            new [,]
            {
                {false, true, false},
                {true, true, true},
                {false, false, false},
            },
            new [,]
            {
                {false, true, false},
                {false, true, true},
                {false, true, false},
            },
            new [,]
            {
                {false, false, false},
                {true, true, true},
                {false, true, false}
            },
            new [,]
            {
                {false, true, false},
                {true, true, false},
                {false, true, false},
            }
        });

        public static readonly TetrisBlock Z = new TetrisBlock(TetrisBlockType.Z, new List<bool[,]>
        {
            new [,]
            {
                {true, true, false},
                {false, true, true},
                {false, false, false},
            },
            new [,]
            {
                {false, false, true},
                {false, true, true},
                {false, true, false},
            },
            new [,]
            {
                {false, false, false},
                {true, true, false},
                {false, true, true}
            },
            new [,]
            {
                {false, true, false},
                {true, true, false},
                {true, false, false},
            }
        });

        public static readonly TetrisBlock[] AllBlocks = { I, J, L, O, S, T, Z };
        #endregion

        public TetrisBlockType Type { get; }
        public int Height { get; }
        public int Width { get; }
        
        public int X { get; set; }
        public int Y { get; set; }
        public bool[,] Shape => rotations[currRotationIndex];

        private readonly List<bool[,]> rotations;
        private readonly int currRotationIndex;

        public TetrisBlock(TetrisBlockType type, List<bool[,]> rotations, int initialRotationIndex = 0)
        {
            this.rotations = rotations;
            currRotationIndex = initialRotationIndex;

            Type = type;
            Height = rotations[0].GetLength(0);
            Width = rotations[0].GetLength(1);
        }

        public TetrisBlock Rotate()
        {
            var newRotationIndex = (currRotationIndex + 1) % rotations.Count;
            var rotatedBlock = new TetrisBlock(Type, rotations, newRotationIndex)
            {
                X = X,
                Y = Y
            };

            return rotatedBlock;
        }

        public bool FitsInGrid(TetrisBlockType[,] grid, int leftTopX, int leftTopY)
        {
            var gridWidth = grid.GetLength(1);
            var gridHeight = grid.GetLength(0);

            return !IsOutOfBounds(leftTopX, leftTopY, gridWidth, gridHeight)
                && !OversectsGrid(grid, leftTopX, leftTopY);
        }

        public bool IsOutOfBounds(int leftTopX, int leftTopY, int width, int height)
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (Shape[y, x] && (leftTopX + x < 0 || leftTopX + x >= width || leftTopY + y >= height))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool OversectsGrid(TetrisBlockType[,] grid, int leftTopX, int leftTopY)
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (Shape[y, x] && grid[y + leftTopY, x + leftTopX] != TetrisBlockType.Empty)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void PlaceInGrid(TetrisBlockType[,] grid, int leftTopX, int leftTopY)
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (Shape[y, x])
                    {
                        grid[leftTopY + y, leftTopX + x] = Type;
                    }
                }
            }
        }

        public TetrisBlock Clone()
        {
            var clone = new TetrisBlock(Type, rotations, currRotationIndex)
            {
                X = X,
                Y = Y
            };

            return clone;
        }
    }
}
