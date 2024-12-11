

namespace Temple_of_doom.Models
{
    public class GameWorld
    {
        public GameWorld()
        {
            Player = new Player();
            CurrentRoom = new Room();
        }

        public Player Player { get; set; }
        public Room CurrentRoom { get; set; }

        public void MovePlayer(string direction)
        {
            // Logic for moving player in the current room
        }
    }
}

