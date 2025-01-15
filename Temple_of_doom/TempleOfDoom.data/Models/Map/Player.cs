namespace TempleOfDoom.data.Models.Map;

public class Player : UiObserver
{
    public int Lives { get; set; }
    public bool HasWon { get; set; }
    public Position Position { get; set; }
    public Inventory Inventory { get; set; }
    public Room currentRoom { get; set; }

    public Player(int lives, Position position)
    {
        Lives = lives;
        HasWon = false;
        Inventory = new Inventory();
        Position = position;
    }

    public bool HasKey(string keyColor)
    {
        return Inventory.HasItem(keyColor);
    }

    public Position GetPlayerStartPosition()
    {
        return Position;
    }

    public void Move(string command, List<Room> rooms)
    {
        // Bereken de nieuwe positie
        var newPosition = new Position
        (
            command switch
            {
                "up" => Position.X,
                "down" => Position.X,
                "left" => Position.X - 1,
                "right" => Position.X + 1,
                _ => Position.X
            },
            command switch
            {
                "up" => Position.Y - 1,
                "down" => Position.Y + 1,
                "left" => Position.Y,
                "right" => Position.Y,
                _ => Position.Y
            }
        );

        // Controleer of de nieuwe positie binnen de kamergrenzen ligt
        if (currentRoom.IsPositionWalkable(newPosition))
        {
            Position = newPosition;

            // Handle interactions with items at the new position
            currentRoom.HandlePlayerInteraction(this);
        }
        
        // Check if the player has reached a door
        foreach (var door in currentRoom.Doors)
        {
            if (door.Position.X == Position.X && door.Position.Y == Position.Y)
            {
                // find new room
                var targetRoom = rooms.FirstOrDefault(r => r.Id == door.TargetRoomId);
                if (targetRoom == null)
                {
                    Update($"Error: Target room with ID={door.TargetRoomId} not found!");
                    return;
                }

                // find corresponding door in target room
                var correspondingDoor = targetRoom.Doors.FirstOrDefault(d =>
                    d.TargetRoomId == currentRoom.Id && d.Direction == GetOppositeDirection(door.Direction));

                if (correspondingDoor == null)
                {
                    Update($"Error: Corresponding door not found in Room ID={targetRoom.Id} for Direction={GetOppositeDirection(door.Direction)}.");
                    return;
                }
                
                // teleport player to new room
                Position = correspondingDoor.Position;
                currentRoom = targetRoom;

                Update($"Teleported to Room ID={currentRoom.Id} at Position=({Position.X}, {Position.Y})");
                break;
            }
        }
    }

    public int GetItemCount()
    {
        return Inventory.GetItemCount("sankara stone");
    }

    //Check if the player has collected all required Sankara Stones
    public bool CheckWinCondition(int requiredStones = 5)
    {
        int collectedStones = Inventory.GetItemCount("sankara stone");

        if (collectedStones >= requiredStones)
        {
            HasWon = true;
            Update("");
            Update($"You have collected all {requiredStones} Sankara Stones! You win!");
            return true;
        }

        return false;
    }

    private Direction GetOppositeDirection(Direction direction)
    {
        return direction switch
        {
            Direction.NORTH => Direction.SOUTH,
            Direction.SOUTH => Direction.NORTH,
            Direction.EAST => Direction.WEST,
            Direction.WEST => Direction.EAST,
            _ => throw new Exception("Invalid direction")
        };
    }
}