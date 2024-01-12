using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Tetris
{
    public class TetrisGame
    {
        private Window _GameWindow;
        private Player _Player;
        public BlockCollection _BlockCollection;
        public ScreenAttributes _ScreenAttributes;
        public bool CollisionDetected { get; set; } = false;
        public bool GameOver { get; set; } = false;
        public List<string> StartMenuList = new List<string>{"Play", "Help", "Quit"};
        public List<MenuOption> StartMenuOptions = new List<MenuOption>();

        
        
        public TetrisGame(Window GameWindow)
        {
            _GameWindow = GameWindow;
            _Player = new Player(_GameWindow);
            _BlockCollection = new BlockCollection(_GameWindow);
            _ScreenAttributes = new ScreenAttributes(_Player);

            
        }
        public void Run()
        {
            do
            {
                DisplayStartMenu();

            } while (_Player.Quit != true && _GameWindow.CloseRequested != true);
        }
        
        
        public void PlayGame()
        {
            do
            {
                SplashKit.ProcessEvents();
                _Player.HandleInput(_GameWindow);
                SplashKit.ClearScreen(Color.White);
                CollisionDetector();
                BoundaryDetector();
                _ScreenAttributes.Update(_Player);
                _ScreenAttributes.Draw(_Player, _GameWindow);
                _Player.Update(_GameWindow);
                _Player.Draw(_GameWindow);
                _BlockCollection.Update(_GameWindow, _Player);
                _BlockCollection.Draw(_GameWindow);
                _GameWindow.Refresh(150);
            } while (_Player.Quit != true && _GameWindow.CloseRequested != true && !GameOver);
            if (GameOver)
            {
                _GameWindow.DrawText($"Game Over. Final Score: {_Player.Score}", Color.Black, _GameWindow.Width/2, _GameWindow.Height/2);
                _GameWindow.Refresh(150);
                SplashKit.Delay(5000);
            }
            SplashKit.ClearScreen(Color.White);
        }

        public void CollisionDetector()
        {
            if (_Player._block.Blocks.Count > 0)
            {
                foreach (Block block in _Player._block.Blocks)
                {
                    if (block.BottomCollisionPoint.Y >= _GameWindow.Height)
                    {
                        CollisionDetected = true;
                    }
                    else if (_BlockCollection.blocks.Count > 0)
                    {
                        foreach (Block subBlock in _BlockCollection.blocks)
                        {
                            if (subBlock.TopCollisionPoint.X == block.BottomCollisionPoint.X && subBlock.TopCollisionPoint.Y == block.BottomCollisionPoint.Y)
                            {
                                CollisionDetected = true;
                            }
                        }
                    }
                }
                if (CollisionDetected == true)
                    {
                        foreach (Block n in _Player._block.Blocks)
                        {
                            n.RowNum = (int)Math.Floor(n.BottomCollisionPoint.Y) / (_GameWindow.Height / 30);
                            _BlockCollection.blocks.Add(n); 
                        }
                        _Player.NewBlock(_GameWindow);
                        CollisionDetected = false;
                    }
                    
            }
        }

        public void BoundaryDetector()
        {
            if (_Player._block.Blocks.Count > 0)
            {
                foreach (Block block in _Player._block.Blocks)
                {
                    if (block.RightCollisionPoint.X >= _GameWindow.Width)
                    {
                        _Player.RestrictMovementRight = true;
                        _Player.RestrictRotation = true;
                    }
                    else if (block.LeftCollisionPoint.X <= 0)
                    {
                        _Player.RestrictMovementLeft = true;
                        _Player.RestrictRotation = true;
                    }
                    if (_BlockCollection.blocks.Count > 0)
                    {
                        if (block.Y >= _BlockCollection.highestBlockY - 20 || block.Y >= _GameWindow.Height - 20)
                        {
                            _Player.RestrictMovementDown = true;
                            _Player.RestrictRotation = true;
                        }
                        foreach (Block subBlock in _BlockCollection.blocks)
                        {
                            if (subBlock.Y <= 60)
                            {
                                GameOver = true;
                                return;
                            }
                            if (subBlock.RightCollisionPoint.X == block.LeftCollisionPoint.X && Math.Abs(subBlock.RightCollisionPoint.Y - block.LeftCollisionPoint.Y) <= 15)
                            {
                                _Player.RestrictMovementLeft = true;
                                _Player.RestrictRotation = true;
                            }
                            else if (subBlock.LeftCollisionPoint.X == block.RightCollisionPoint.X && Math.Abs(subBlock.LeftCollisionPoint.Y - block.RightCollisionPoint.Y) <= 15)
                            {
                                _Player.RestrictMovementRight = true;
                                _Player.RestrictRotation = true;
                            }
                        }
                    }
                }
                
                    
            }


        }


        public void DisplayStartMenu()
        {
            _GameWindow.Clear(Color.White);
            int StartMenuLength = StartMenuList.Count;
            double X = (_GameWindow.Width - 300) / 2;
            double Y = (_GameWindow.Height - 400) / StartMenuLength;
            foreach (string item in StartMenuList)
            {
                Y += 125;
                MenuOption option = new MenuOption(_GameWindow, X, Y, item);
                if (item == "Play")
                {
                    option.OnClick += (MenuOption) => PlayGame();
                }
                else if (item == "Help")
                {
                    option.OnClick += (MenuOption) => DisplayHelpPage();
                }
                else if (item == "Quit")
                {
                    option.OnClick += (MenuOption) => _Player.Quit = true;
                }
                StartMenuOptions.Add(option);
            }
            _GameWindow.Refresh();
            SplashKit.ProcessEvents();
            foreach (MenuOption menuOption in StartMenuOptions)
            {
                menuOption.HandleInput();
            }

        }

        public void DisplayHelpPage()
        {
            do
            {
                SplashKit.ClearScreen(Color.White);
                _GameWindow.DrawText("Tetris Game Controls", Color.Black, _GameWindow.Width / 2 - 150, 200);
                _GameWindow.DrawText("'A' Key: Move Piece Left", Color.Black, _GameWindow.Width / 2 - 150, 250);
                _GameWindow.DrawText("'D' Key: Move Piece Right", Color.Black, _GameWindow.Width / 2 - 150, 300);
                _GameWindow.DrawText("'S' Key: Increase Speed Down", Color.Black, _GameWindow.Width / 2 - 150, 350);
                _GameWindow.DrawText("'Space' Key: Rotate Piece", Color.Black, _GameWindow.Width / 2 - 150, 400);
                _GameWindow.DrawText("Press 'Tab' Key to return to main menu", Color.Black, _GameWindow.Width / 2 - 150, 450);
                SplashKit.RefreshScreen();
                SplashKit.ProcessEvents();


            } while (!SplashKit.KeyTyped(KeyCode.TabKey));
        }
            

        public bool Quit
        {
            get
            {
                return _Player.Quit;
            }
        }

    }
}