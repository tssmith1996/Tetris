using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Tetris
{
    public class ScreenAttributes
    {
        
        private int _playerScore;
        private double _scoreX { get; set; } = 30;
        private double _scoreY { get; set; } = 30;

        
        
        public ScreenAttributes(Player player)
        {
            _playerScore = player.Score;
        }
        
        public void Update(Player player)
        {
            _playerScore = player.Score;
        }
        
        public void Draw(Player player, Window gameWindow)
        {
            gameWindow.DrawText($"Score: {_playerScore}", Color.Black, _scoreX, _scoreY);
        }

        
    }
}