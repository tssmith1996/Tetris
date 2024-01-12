using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Tetris
{
    public class BlockCollection
    {
        public List<Block> blocks = new List<Block>();
        private List<Block> removedBlocks = new List<Block>();
        private List<int> rows = new List<int>();
        public double highestBlockY = 0;
        public BlockCollection(Window GameWindow)
        {
            for (int i = 1; i <= GameWindow.Height / 30; i++)
            {
                rows.Add(0);
            }
        }

        
        public void Update(Window GameWindow, Player player)
        {
            ResetRowCount(GameWindow);
            foreach (Block block in blocks)
            {
                rows[block.RowNum - 1] += 1;
                if (block.TopCollisionPoint.Y > highestBlockY)
                {
                    highestBlockY = block.TopCollisionPoint.Y;
                }

            };
            
            for (int rowNum = 1; rowNum <= GameWindow.Height/30; rowNum++)
            {
                if (rows[rowNum - 1] == GameWindow.Width/30)
                {
                    DeleteRow(rowNum, GameWindow, player);
                }
            }

            
        }

        public void DeleteRow(int Row, Window GameWindow, Player player)
        {
            foreach (Block block in blocks)
            {
                if (block.RowNum == Row)
                {
                    removedBlocks.Add(block);
                    player.Score += 1;
                }
            }
            foreach (Block block in removedBlocks)
            {
                blocks.Remove(block);
            }
            foreach (Block block in blocks)
            {
                if (block.RowNum < Row)
                {
                    block.Y += GameWindow.Height / 30;
                }
            }
            
        }

        public void ResetRowCount(Window GameWindow)
        {
            rows = new List<int>();
            for (int i = 1; i <= GameWindow.Height / 30; i++)
            {
                rows.Add(0);
            }
        }

        public void Draw(Window GameWindow)
        {
            foreach (Block block in blocks)
            {
                block.Draw(block.MainColor, GameWindow, block.X, block.Y);

            }
            
        }

    }
}