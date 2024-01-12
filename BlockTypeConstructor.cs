using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Tetris
{
    public abstract class BlockTypeConstructor
    {
        public int[,] BlockMatrix { get; set; }
        public List<Block> Blocks;
        public Color MainColor = SplashKit.RandomRGBColor(255);
        public BlockTypeConstructor(Window GameWindow, double x, double y)
        {
            BlockMatrix = SetBlockMatrix();
            Blocks = new List<Block>();
            GenerateBlocks(GameWindow, x, y);      
        }
        public abstract int[,] SetBlockMatrix();

        public void Update(Window GameWindow, double x, double y)
        {
            Blocks = new List<Block>();
            GenerateBlocks(GameWindow, x, y);      

        }
        
        public void GenerateBlocks(Window GameWindow, double x, double y)
        {
            for (int i=0; i<4; i++)
            {
                for (int j=0; j<4; j++)
                {
                    if (BlockMatrix[i,j] == 1)
                    {
                        Block block = new Block(GameWindow)
                        {
                            X = x + i * 30,
                            Y = y + j * 30,
                            MainColor = MainColor
                        };
                        Blocks.Add(block);
                    }
                }
            };

        }
        
        public void Draw(Window GameWindow)
        {
            
            foreach (Block subblock in Blocks)
            {
                subblock.Draw(MainColor, GameWindow, subblock.X, subblock.Y);

            }
            
        }

        public void Rotate(Window GameWindow, double x, double y)
        {
            Blocks = new List<Block>();
            int N = 4;
            for (int i = 0; i < N / 2; i++) 
            {
                for (int j = i; j < N - i - 1; j++) 
                {
    
                    int temp = BlockMatrix[i, j];
                    BlockMatrix[i, j] = BlockMatrix[N - 1 - j, i];
                    BlockMatrix[N - 1 - j, i] = BlockMatrix[N - 1 - i, N - 1 - j];
                    BlockMatrix[N - 1 - i, N - 1 - j] = BlockMatrix[j, N - 1 - i];
                    BlockMatrix[j, N - 1 - i] = temp;
                }
            }
            
            GenerateBlocks(GameWindow, x, y);      
        
            Draw(GameWindow);
            

        }


    }
}