using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Tetris
{
    public class Player
    {
        
        private double _x;
        private double _y;
        public bool RestrictMovementLeft { get; set; } = false;
        public bool RestrictMovementRight { get; set; } = false;
        public bool RestrictMovementDown { get; set; } = false;

        public bool RestrictRotation { get; set; } = false;
        public BlockTypeConstructor _block { get; set; }
        // private List<BlockTypeConstructor> blockList = new List<BlockTypeConstructor>();
        public int Score { get; set; }
        public int SPEED = 1;
        public bool Quit { get; set; } = false;

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }
        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public Player(Window GameWindow)
        {
            _block = BlockSelection(GameWindow);
            Score = 0;
        }
        
        public void HandleInput(Window GameWindow)
        {

            if (SplashKit.KeyTyped(KeyCode.DKey))
            {
                if (RestrictMovementRight == false)
                {
                    MoveRight();
                }
                RestrictMovementRight = false;
                
            }
            else if (SplashKit.KeyTyped(KeyCode.AKey))
            {
                if (RestrictMovementLeft == false)
                {
                    MoveLeft();
                }
                RestrictMovementLeft = false;
            }
            else if (SplashKit.KeyTyped(KeyCode.SKey))
            {
                if (RestrictMovementDown == false)
                {
                    MoveDown(); 
                }
                RestrictMovementDown = false;
                
            }
            else if (SplashKit.KeyTyped(KeyCode.SpaceKey))
            {
                if (RestrictRotation == false)
                {
                    Rotate(GameWindow);
                }
                RestrictRotation = false;
            }
            else if (SplashKit.KeyTyped(KeyCode.EscapeKey))
            {
                Quit = true;
            }
            
        }
        public void Update(Window GameWindow)
        {
            Y += SPEED;
            foreach (Block subBlock in _block.Blocks)
            {
                subBlock.Y += SPEED;
            }

        }
        public void Draw(Window GameWindow)
        {
            foreach (Block subBlock in _block.Blocks)
            {
                subBlock.Draw(subBlock.MainColor, GameWindow, subBlock.X, subBlock.Y);
            }
        }

        public void MoveLeft()
        {
            X -= 30;
            foreach (Block subBlock in _block.Blocks)
            {
                subBlock.X -= 30;
            }
        }

        public void MoveRight()
        {
            X += 30;
            foreach (Block subBlock in _block.Blocks)
            {
                subBlock.X += 30;
            }
        }

        public void MoveDown()
        {
            Y += 5;
            foreach (Block subBlock in _block.Blocks)
            {
                subBlock.Y += 5;
            }
        }

        public void Rotate(Window GameWindow)
        {
            _block.Rotate(GameWindow, X, Y);
        }

        public BlockTypeConstructor BlockSelection(Window GameWindow)
        {
            int randomInt = SplashKit.Rnd(0,6);
            if (randomInt == 0)
            {
                _block = new LBlock(GameWindow, X, Y);
            }
            else if (randomInt == 1)
            {
                _block = new IBlock(GameWindow, X, Y);
            }
            else if (randomInt == 2)
            {
                _block = new JBlock(GameWindow, X, Y);
            }
            else if (randomInt == 3)
            {
                _block = new OBlock(GameWindow, X, Y);
            }
            else if (randomInt == 4)
            {
                _block = new SBlock(GameWindow, X, Y);
            }
            else if (randomInt == 5)
            {
                _block = new TBlock(GameWindow, X, Y);
            }
            else
            {
                _block = new ZBlock(GameWindow, X, Y);
            }
            return _block;

        }

        public void NewBlock(Window GameWindow)
        {
            X = GameWindow.Width/2;
            Y = 0;
            _block = BlockSelection(GameWindow);

        }


        
    }
}