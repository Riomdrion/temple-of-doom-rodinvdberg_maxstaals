using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.Items;

namespace TempleOfDoom.data.Models.Map;

public class Room(int id, int width, int height, List<Item> items, List<Door.Door> doors)
{
    public int Id { get; } = id;
    public List<Item> Items { get; set; } = items;
    public List<Door.Door> Doors { get; } = doors;
    public int Width { get; } = width;
    public int Height { get; } = height;
    public char[,] Layout { get; } = new char[height, width];

    public bool IsPositionWalkable(Position position)
    {
        if (Layout == null)
            throw new NullReferenceException("Room layout is not initialized.");

        if (position.Y < 0 || position.Y >= Layout.GetLength(0) ||
            position.X < 0 || position.X >= Layout.GetLength(1))
            return false;

        return Layout[position.Y, position.X] != '#';
    }

    public void InitializeRoomLayout()
    {
        // Stap 1: Vul alle posities met lege ruimte
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                Layout[y, x] = ' ';
            }
        }

        // Stap 2: Voeg muren toe langs de randen
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (y == 0 || y == Height - 1 || x == 0 || x == Width - 1)
                {
                    Layout[y, x] = (Layout[y, x] == ' ' || Layout[y, x] == '\0') ? '#' : Layout[y, x];
                }
            }
        }

        // Stap 3: Voeg deuren toe aan de lay-out
        foreach (var door in Doors)
        {
            if (door.Position.X >= 0 && door.Position.X < Width &&
                door.Position.Y >= 0 && door.Position.Y < Height)
            {
                var doorSymbol = door switch
                {
                    SimpleDoor => '|',
                    ColoredDoor => '=',
                    ToggleDoor => '_',
                    ClosingGate => '∩',
                    OpenOnOddDoor => '?',
                    OpenOnStonesDoor => '*',
                    _ => '?'
                };

                Layout[door.Position.Y, door.Position.X] = doorSymbol;
            }
        }
        Console.WriteLine($"Room ID={Id} Layout:");
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                Console.Write(Layout[y, x]);
            }
            Console.WriteLine();
        }
    }
    
    public void HandlePlayerInteraction(Player player)
    {
        if (Layout == null)
            throw new NullReferenceException("Room layout is not initialized.");

        var currentTile = Layout[player.Position.Y, player.Position.X];

        switch (currentTile)
        {
            case 'K': // Key
                player.Inventory.AddItem("Key");
                Layout[player.Position.Y, player.Position.X] = '.'; // Remove the key from the room
                Console.WriteLine("You picked up a Key!");
                break;

            case 'S': // Sankara Stone
                player.Inventory.AddItem("Sankara Stone"); // Add to inventory
                Layout[player.Position.Y, player.Position.X] = '.'; // Remove the stone from the room
                Console.WriteLine("You picked up a Sankara Stone!");

                // Display the current count of Sankara Stones in inventory
                int sankaraStoneCount = player.Inventory.GetItemCount("Sankara Stone");
                Console.WriteLine("You now have " + sankaraStoneCount + " Sankara Stones.");

                // Optional: Check if the player has won (e.g., collected 5 Sankara Stones)
                if (sankaraStoneCount >= 5)
                {
                    player.HasWon = true;
                    Console.WriteLine("You have collected all 5 Sankara Stones! You win!");
                }
                break;

            case 'B': // Boobytrap
                player.Lives--;
                Layout[player.Position.Y, player.Position.X] = '.'; // Remove the boobytrap if it's disappearing

                if (player.Lives <= 0)
                {
                    Console.WriteLine("Game Over: You have no lives left.");
                    player.HasWon = false;
                }
                break;

            default:
                //Console.WriteLine("Nothing to interact with here.");
                break;
        }
    }
}