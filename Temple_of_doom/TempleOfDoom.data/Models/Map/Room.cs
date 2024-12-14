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
        Console.WriteLine("ffff");

        Console.WriteLine($"Room ID={Id} has the following doors:");
        for (int i = 0; i < Doors.Count; i++)
        {
            var door = Doors[i];
            Console.WriteLine(
                $"Door ID={door.Id}, Type={door.GetType().Name}, Position=({door.Position.X}, {door.Position.Y})");
        }

        // Stap 2: Voeg deuren toe aan de lay-out
        foreach (var door in Doors)
        {
            Console.WriteLine("qqqq");
            if (door.Position.X >= 0 && door.Position.X < Width &&
                door.Position.Y >= 0 && door.Position.Y < Height)
            {
                var doorSymbol = door switch
                {
                    SimpleDoor => (char)Symbols.VERTICALDOOR,
                    ColoredDoor => (char)Symbols.HORIZONTALDOOR,
                    ToggleDoor => (char)Symbols.TOGGLEDOOR,
                    ClosingGate => (char)Symbols.CLOSINGGATE,
                    _ => '?'
                };

                Layout[door.Position.Y, door.Position.X] = doorSymbol;
                Console.WriteLine(
                    $"Placed door in Room ID={Id} at Position=({door.Position.X}, {door.Position.Y}) with symbol={doorSymbol}");
            }
            else
            {
                Console.WriteLine($"Invalid door position for Room ID={Id}: ({door.Position.X}, {door.Position.Y})");
            }
        }

        // Stap 3: Vul randen met muren, maar laat deuren ongemoeid
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (Layout[y, x] == ' ' && (y == 0 || y == Height - 1 || x == 0 || x == Width - 1))
                {
                    Layout[y, x] = (char)Symbols.WALL;
                }
            }
        }
    }
}