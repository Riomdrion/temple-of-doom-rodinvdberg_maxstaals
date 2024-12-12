

using Temple_of_doom.Controllers;

namespace Temple_of_doom.Models
{
    public class GameWorld
    {
        public Player Player { get; set; }
        public Room CurrentRoom { get; set; }
        public bool IsGameOver => Player.Lives <= 0 || Player.HasWon;

        public void MovePlayer(string direction)
        {
            MovementController.MovePlayer(this, direction);
        }
    }
}


