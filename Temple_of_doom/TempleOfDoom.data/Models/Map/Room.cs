using TempleOfDoom.data.Models.Items;

namespace TempleOfDoom.data.Models.Map;

public class Room
{
    public int Id { get; }
    public int Width { get; }
    public int Height { get; }
    public List<Door.Door> Doors { get; set; }
    public List<Item> Items { get; set; }
    public char[,] Layout { get; private set; }

    public Room(int id, int width, int height, List<Door.Door> doors, List<Item> items)
    {
        Id = id;
        Width = width;
        Height = height;
        Doors = doors;
        Items = items;
        Layout = new char[height, width];
    }

    public bool IsPositionWalkable(Position position)
    {
        return position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height &&
               Layout[position.Y, position.X] != '#';
    }

    public void InitializeRoomLayout()
    {
        // Initialize layout with empty spaces
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                Layout[y, x] = ' ';
            }
        }

        // Add walls
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (y == 0 || y == Height - 1 || x == 0 || x == Width - 1)
                {
                    Layout[y, x] = '#';
                }
            }
        }

        // Add doors
        foreach (var door in Doors)
        {
            if (door.Position.X >= 0 && door.Position.X < Width &&
                door.Position.Y >= 0 && door.Position.Y < Height)
            {
                Layout[door.Position.Y, door.Position.X] = door.Symbol;
            }
        }

        // Add items
        // foreach (var item in Items)
        // {
        //     if (item.Position.X >= 0 && item.Position.X < Width &&
        //         item.Position.Y >= 0 && item.Position.Y < Height)
        //     {
        //         Layout[item.Position.Y, item.Position.X] = 'I'; // Represent items with 'I'
        //     }
        // }
    }
}

// public void HandlePlayerInteraction(Player player)
// {
//     if (Layout == null)
//         throw new NullReferenceException("Room layout is not initialized.");
//
//     var currentTile = Layout[player.Position.Y, player.Position.X];
//
//     switch (currentTile)
//     {
//         case 'K': // Key
//             player.Inventory.AddItem("Key");
//             Layout[player.Position.Y, player.Position.X] = '.'; // Remove the key from the room
//             Console.WriteLine("You picked up a Key!");
//             break;
//
//         case 'S': // Sankara Stone
//             player.Inventory.AddItem("Sankara Stone"); // Add to inventory
//             Layout[player.Position.Y, player.Position.X] = '.'; // Remove the stone from the room
//             Console.WriteLine("You picked up a Sankara Stone!");
//
//             // Display the current count of Sankara Stones in inventory
//             int sankaraStoneCount = player.Inventory.GetItemCount("Sankara Stone");
//             Console.WriteLine("You now have " + sankaraStoneCount + " Sankara Stones.");
//
//             // Optional: Check if the player has won (e.g., collected 5 Sankara Stones)
//             if (sankaraStoneCount >= 5)
//             {
//                 player.HasWon = true;
//                 Console.WriteLine("You have collected all 5 Sankara Stones! You win!");
//             }
//             break;
//
//         case 'B': // Boobytrap
//             player.Lives--;
//             Layout[player.Position.Y, player.Position.X] = '.'; // Remove the boobytrap if it's disappearing
//
//             if (player.Lives <= 0)
//             {
//                 Console.WriteLine("Game Over: You have no lives left.");
//                 player.HasWon = false;
//             }
//             break;
//
//         default:
//             //Console.WriteLine("Nothing to interact with here.");
//             break;
//     }

