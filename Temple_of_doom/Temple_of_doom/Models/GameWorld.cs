

using Temple_of_doom.Controllers;

namespace Temple_of_doom.Models
{
    public class GameWorld
    {
        public Player Player { get; set; }
        public Room CurrentRoom { get; set; }
        public bool IsGameOver => Player?.Lives <= 0 || Player?.HasWon == true;

        public void MovePlayer(string direction)
        {
            if (Player == null || CurrentRoom == null)
                throw new NullReferenceException("GameWorld is not properly initialized.");

            MovementController.MovePlayer(this, direction);
        }
    }
}


