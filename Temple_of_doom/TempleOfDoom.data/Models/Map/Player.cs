using TempleOfDoom.data.Models.Items;

namespace TempleOfDoom.data.Models.Map;

public class Player
{
    public int Lives { get; set; }
    public bool HasWon { get; set; }
    public Position Position { get; set; }
    public Inventory Inventory { get; set; }
    public int StartingRoomId { get; set; }
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
        }


        foreach (var door in currentRoom.Doors)
        {
            if (door.Position.X == Position.X && door.Position.Y == Position.Y)
            {
                // Teleporteer naar de verbonden kamer
                var targetRoom = rooms.FirstOrDefault(r => r.Id == door.TargetRoomId);
                if (targetRoom == null)
                {
                    Console.WriteLine($"Error: TargetRoomId={door.TargetRoomId} not found!");
                    return;
                }
                
                currentRoom = targetRoom;

                // Stel de nieuwe positie in
                Position = door.Direction switch
                {
                    Direction.NORTH => new Position(door.Position.X, currentRoom.Height/2),
                    Direction.SOUTH => new Position(door.Position.X, 0),
                    Direction.WEST => new Position(currentRoom.Width - 1, door.Position.Y/2),
                    Direction.EAST => new Position(0, door.Position.Y),
                    _ => throw new Exception("Invalid door direction")
                };

                Console.WriteLine($"Teleported to Room ID={currentRoom.Id} at Position=({Position.X}, {Position.Y})");
                
                break;
            }
        }
    }
    
    public void PickUpItem(Room room)
    {
        var itemAtPosition = room.Items.FirstOrDefault(i => i.X == Position.X && i.Y == Position.Y);
        if (itemAtPosition != null)
        {
            // Check if it's a Sankara Stone and add it to the inventory
            if (itemAtPosition.Type == "sankara stone")
            {
                Inventory.AddItem("Sankara Stone");
                room.Items.Remove(itemAtPosition); // Remove the Sankara Stone from the room
            }
            // Handle boobytrap activation
            else if (itemAtPosition.Type == "boobytrap")
            {
                if (itemAtPosition is Boobytrap boobytrap)
                {
                    boobytrap.Trigger(this); // Apply damage
                    //Console.WriteLine($"Boobytrap triggered! Player loses 1 life. Remaining lives: {Lives}");
                }
            }

            // Handle disappearing boobytrap activation
            else if (itemAtPosition.Type == "disappearing boobytrap")
            {
                if (itemAtPosition is DisappearingBoobytrap disappearingBoobytrap)
                {
                    disappearingBoobytrap.Activate(this); // Apply damage
                    Console.WriteLine($"Disappearing boobytrap triggered! Player loses 1 life. Remaining lives: {Lives}");
                    room.Items.Remove(itemAtPosition); // Remove the disappearing boobytrap
                }
            }
            // Additional item types can be handled here similarly
        }
        // After picking up an item, check if the player has won
        CheckWinCondition(); // Check win condition after picking up an item
    }

    public int GetItemCount()
    {
        return Inventory.GetItemCount("Sankara Stone");
    }


    //Check if the player has collected all required Sankara Stones
    public bool CheckWinCondition(int requiredStones = 5)
    {
        int collectedStones = Inventory.GetItemCount("Sankara Stone");

        if (collectedStones >= requiredStones)
        {
            HasWon = true;
            Console.WriteLine("");
            Console.WriteLine($"You have collected all {requiredStones} Sankara Stones! You win!");
            return true;
        }
        return false;
    }
}
