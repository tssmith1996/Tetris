using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Tetris
{
    public class SBlock: BlockTypeConstructor
    {
        public SBlock(Window GameWindow, double x, double y): base(GameWindow, x, y)
        {  
        }

        public override int[,] SetBlockMatrix()
        {
            BlockMatrix = new int[4,4]{{0, 0, 1, 0}, {0, 1, 1, 0}, {0, 1, 0, 0}, {0, 0, 0, 0}};
            return BlockMatrix;
        }
    }
}