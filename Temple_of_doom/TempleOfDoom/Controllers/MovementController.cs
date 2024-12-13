using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Controllers;

public class MovementController
{
    public GameWorld GameWorld { get; set; }

    public MovementController(GameWorld gameWorld)
    {
        GameWorld = gameWorld;
    }
    public void HandleInput(string command)
    {
        if (string.IsNullOrEmpty(command)) return;
        if (GameWorld?.Player == null)
        {
            Console.WriteLine("Player object is not initialized.");
            return;
        }

        var currentPosition = GameWorld.Player.Position;
        var newPosition = currentPosition;

        switch (command)
        {
            case "up":
                newPosition = new Position(currentPosition.X, currentPosition.Y - 1);
                break;
            case "down":
                newPosition = new Position(currentPosition.X, currentPosition.Y + 1);
                break;
            case "left":
                newPosition = new Position(currentPosition.X - 1, currentPosition.Y);
                break;
            case "right":
                newPosition = new Position(currentPosition.X + 1, currentPosition.Y);
                break;
            case "quit":
                Console.WriteLine("Exiting game.");
                Environment.Exit(0);
                break;
        }

        // Check if the new position is walkable
        if (GameWorld.CurrentRoom.IsPositionWalkable(newPosition))
        {
            GameWorld.Player.Position = newPosition;
            Console.WriteLine($"Player moved to: {newPosition.X}, {newPosition.Y}");
        }
        else
        {
            Console.WriteLine("You can't move there!");
        }
    }
}
