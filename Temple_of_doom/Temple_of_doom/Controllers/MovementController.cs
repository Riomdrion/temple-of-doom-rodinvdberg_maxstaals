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
        public static bool MovePlayer(GameWorld gameWorld, string direction)
        {
            var currentRoom = gameWorld.CurrentRoom;
            var player = gameWorld.Player;
            var newPosition = player.Position;

            switch (direction)
            {
                case "up":
                    newPosition.Y--;
                    break;
                case "down":
                    newPosition.Y++;
                    break;
                case "left":
                    newPosition.X--;
                    break;
                case "right":
                    newPosition.X++;
                    break;
            }

            if (currentRoom.IsPositionWalkable(newPosition))
            {
                player.Position = newPosition;
                currentRoom.HandlePlayerInteraction(player);
                return true;
            }

            return false;
        }
    }
}
