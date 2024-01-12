using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Tetris
{
    public delegate void ClickAction(MenuOption menuOption);
    public class MenuOption
    {
        private double _menuBoxWidth { get; set; } = 300;
        private double _menuBoxHeight { get; set; } = 100;
        private double _menuBoxX { get; set; }
        private double _menuBoxY  { get; set; }
        public Rectangle RectangleCollision { get; set; }
        public MenuOption(Window GameWindow, double x, double y, string option)
        {
            RectangleCollision = new Rectangle()
            {
                X = x, 
                Y = y, 
                Width = _menuBoxWidth, 
                Height = _menuBoxHeight
            };
            _menuBoxX = RectangleCollision.X;
            _menuBoxY = RectangleCollision.Y;
            GameWindow.DrawRectangle(Color.Black, RectangleCollision);
            GameWindow.DrawText(option, Color.Black, _menuBoxX + (_menuBoxWidth / 2) - 10, _menuBoxY + (_menuBoxHeight / 2));

        }
        public bool IsMouseHover
        {
            get
            {
                return SplashKit.PointInRectangle(SplashKit.MousePosition(), this.RectangleCollision);
            }
        }
        public event ClickAction OnClick;
        public void HandleInput()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton) && IsMouseHover)
            {
                if (OnClick != null)
                    OnClick(this);
            }
        }

        
    }
}