using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Tetris
{
    public class Block
    {
        public double BlockWidth { get; set; }
        public double BlockHeight { get; set; }
        public double X, Y;
        public SplashKitSDK.Color MainColor;
        public Point2D TopCollisionPoint { get; set; }
        public Point2D BottomCollisionPoint { get; set; }
        public Point2D RightCollisionPoint { get; set; }
        public Point2D LeftCollisionPoint { get; set; }
        public int RowNum { get; set; }
        public Block(Window gameWindow)
        {
            BlockWidth = gameWindow.Width / 20;
            BlockHeight = gameWindow.Height / 30;
        }
        public void Draw(SplashKitSDK.Color MainColor, Window GameWindow, double x, double y)
        {
            GameWindow.FillRectangle(MainColor, x, y, BlockWidth, BlockHeight);
            X = x;
            Y = y;

            TopCollisionPoint = new Point2D()
            {
                X = X + (BlockWidth/2), Y = Y
            };
            BottomCollisionPoint = new Point2D()
            {
                X = X + (BlockWidth/2), Y = Y + BlockHeight
            };
            RightCollisionPoint = new Point2D()
            {
                X = X + BlockWidth, Y = Y + (BlockHeight/2)
            };
            LeftCollisionPoint = new Point2D()
            {
                X = X, Y = Y + (BlockHeight/2)
            };

        }
        
    }
}