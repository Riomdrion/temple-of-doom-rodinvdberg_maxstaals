using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.Items;

namespace TempleOfDoom.data.Models.Map;

public class Room
{
    public Room(int id, string name, int width, int height, List<Item> items, List<Door.Door> doors)
    {
        Id = id;
        Name = name;
        Width = width;
        Height = height;
        Layout = new char[height, width];
        Items = items ?? new List<Item>();
        Doors = doors ?? new List<Door.Door>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public List<Item> Items { get; set; }
    public List<Door.Door> Doors { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public char[,] Layout { get; set; }

    public bool IsPositionWalkable(Position position)
    {
        if (Layout == null)
            throw new NullReferenceException("Room layout is not initialized.");

        if (position.Y < 0 || position.Y >= Layout.GetLength(0) ||
            position.X < 0 || position.X >= Layout.GetLength(1))
            return false;

        return Layout[position.Y, position.X] != '#';
    }

    public void HandlePlayerInteraction(Player player)
    {
        if (Layout == null)
            throw new NullReferenceException("Room layout is not initialized.");

        var currentTile = Layout[player.Position.Y, player.Position.X];

        switch (currentTile)
        {
            case 'S':
                player.Inventory.AddItem("Sankara Stone");
                Layout[player.Position.Y, player.Position.X] = '.';
                Console.WriteLine("You picked up a Sankara Stone!");
                break;
            case 'K':
                player.Inventory.AddItem("Key");
                Layout[player.Position.Y, player.Position.X] = '.';
                Console.WriteLine("You picked up a Key!");
                break;
        }
    }

    public Position GetPlayerStartPosition()
    {
        if (Layout == null)
            throw new NullReferenceException("Room layout is not initialized.");

        for (var y = 0; y < Layout.GetLength(0); y++)
        for (var x = 0; x < Layout.GetLength(1); x++)
            if (Layout[y, x] == 'X')
                return new Position(x, y);

        throw new Exception("Player start position not defined in room layout.");
    }
    
    public void InitializeRoomLayout()
    {
        // Vul de layout met muren en lege ruimte
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (y == 0 || y == Height - 1 || x == 0 || x == Width - 1)
                {
                    Layout[y, x] = (char)Symbols.WALL; // Randen
                }
                else
                {
                    Layout[y, x] = ' '; // Lege ruimte
                }
            }
        }

        // Plaats de deuren in de lay-out
        foreach (var door in Doors)
        {
            if (door.Position.Y >= 0 && door.Position.Y < Height &&
                door.Position.X >= 0 && door.Position.X < Width)
            {
                var doorSymbol =
                    door is ToggleDoor ? (char)Symbols.TOGGLEDOOR :
                    door is ColoredDoor ? (char)Symbols.HORIZONTALDOOR :
                    door is ClosingGate ? (char)Symbols.CLOSINGGATE :
                    (char)Symbols.VERTICALDOOR; // Default voor andere deuren

                Layout[door.Position.Y, door.Position.X] = doorSymbol;
            }
        }
    }

}