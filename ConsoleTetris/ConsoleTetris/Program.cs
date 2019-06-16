using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleTetris.Drawing;
using ConsoleTetris.Game;

namespace ConsoleTetris
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var player = new SoundPlayer {SoundLocation = "./Music/tetris_typeA.wav"};
            player.PlayLooping();
            
            Console.CursorVisible = false;
            StartGame();
        }

        public static void StartGame()
        {
            var tetris = new Tetris();
            var tetrisPrinter = new TetrisConsolePrinter();
            var timer = new Stopwatch();
            timer.Start();
            
            while (true)
            {
                tetrisPrinter.PrintTetris(0, 0, tetris);

                if (Console.KeyAvailable)
                {
                    HandleInput(tetris, timer);
                }

                if (timer.ElapsedMilliseconds >= 500)
                {
                    tetris.Step();
                    timer.Restart();
                }
            }
        }

        public static void HandleInput(Tetris game, Stopwatch timer)
        {
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.DownArrow:
                    game.Step();
                    timer.Restart(); break;
                case ConsoleKey.LeftArrow: game.MoveLeft(); break;
                case ConsoleKey.RightArrow: game.MoveRight(); break;
                case ConsoleKey.UpArrow: game.RotateRight(); break;
                case ConsoleKey.Escape: return;
            }
        }
    }
}
