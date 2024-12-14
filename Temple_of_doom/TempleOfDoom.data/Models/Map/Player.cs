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
    // Check if the player has collected all required Sankara Stones
    //public bool CheckWinCondition(int requiredStones = 5)
    //{
    //    if (Inventory.Items.Count(item => item == "Sankara Stone") >= requiredStones)
    //    {
    //        HasWon = true;
    //        Console.WriteLine("You have collected all 5 Sankara Stones! You win!");
    //        return true;
    //    }
    //    return false;
    //}
}