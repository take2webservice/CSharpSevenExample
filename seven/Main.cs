using System;
using com.example.seven.models;

namespace com.example.seven{
    class Seven
    {
        public Seven()
        {
            Game game = new Game(4);
            while(!game.IsCleared())
            {
                game.Play();
            }
        }
    }
}
