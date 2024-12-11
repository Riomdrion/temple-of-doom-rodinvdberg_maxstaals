using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temple_of_doom.Models;


namespace Temple_of_doom.Controllers
{
    public class MovementController
    {
        private GameWorld _gameWorld;

        public MovementController(GameWorld gameWorld)
        {
            _gameWorld = gameWorld;
        }

        public void ProcessMovement(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    _gameWorld.MovePlayer("up");
                    break;
                case ConsoleKey.DownArrow:
                    _gameWorld.MovePlayer("down");
                    break;
                case ConsoleKey.LeftArrow:
                    _gameWorld.MovePlayer("left");
                    break;
                case ConsoleKey.RightArrow:
                    _gameWorld.MovePlayer("right");
                    break;
                default:
                    Console.WriteLine("Invalid key for movement.");
                    break;
            }
        }
    }
}