namespace TempleOfDoom.data.Models.Map;

public class Player
{
    public int Lives { get; set; }
    public bool HasWon { get; set; }
    public Position Position { get; set; }
    public Inventory Inventory { get; set; }
    public int StartingRoomId { get; set; }
    public int StartX { get; set; }
    public int StartY { get; set; }

    public Player(int startX, int startY, int lives)
    {
        StartX = startX;
        StartY = startY;
        Lives = lives;
        HasWon = false;
        Inventory = new Inventory();
        Position = new Position(startX, startY);
    }

    public bool HasKey(string keyColor)
    {
        return Inventory.HasItem(keyColor);
    }

    public Position GetPlayerStartPosition(Room room)
    {
        if (room == null || room.Layout == null)
        {
            throw new ArgumentException("Room or room layout is not initialized.");
        }

        for (var y = 0; y < room.Layout.GetLength(0); y++)
        {
            for (var x = 0; x < room.Layout.GetLength(1); x++)
            {
                if (room.Layout[y, x] == 'X')
                {
                    return new Position(x, y);
                }
            }
        }

        // Default to center for small rooms, ensuring it's walkable
        int centerX = room.Width / 2;
        int centerY = room.Height / 2;

        if (room.IsPositionWalkable(new Position(centerX, centerY)))
        {
            return new Position(centerX, centerY);
        }

        throw new Exception("No valid player start position found.");
    }

    public void Move(string command, Room currentRoom)
    {
        if (currentRoom == null)
        {
            Console.WriteLine("Error: Current room is null.");
            return;
        }

        var currentPosition = this.Position;
        var newPosition = command.ToLower() switch
        {
            "up" => new Position(currentPosition.X, currentPosition.Y - 1),
            "down" => new Position(currentPosition.X, currentPosition.Y + 1),
            "left" => new Position(currentPosition.X - 1, currentPosition.Y),
            "right" => new Position(currentPosition.X + 1, currentPosition.Y),
            _ => currentPosition
        };

        // Check if the new position is walkable
        if (currentRoom.IsPositionWalkable(newPosition))
        {
            //Console.WriteLine($"Player moved from X:{currentPosition.X}, Y:{currentPosition.Y} to X:{newPosition.X}, Y:{newPosition.Y}.");
            this.Position = newPosition;

            // Handle interactions with items at the new position
            currentRoom.HandlePlayerInteraction(this);

            // Check win condition
            //if (CheckWinCondition())
            //{
            //    Console.WriteLine("Game Over: You won!");
            //    return;
            //}
        }
        else
        {
            Console.WriteLine($"Player cannot move to X:{newPosition.X}, Y:{newPosition.Y} - position is not walkable.");
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
