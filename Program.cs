using System;
using SplashKitSDK;

namespace Tetris
{
    public class Program
    {
        public static void Main()
        {
            Window gameWindow = new Window("Tetris Game", 600, 900);
            TetrisGame tetris = new TetrisGame(gameWindow);
            tetris.Run();

        }
    }
}
