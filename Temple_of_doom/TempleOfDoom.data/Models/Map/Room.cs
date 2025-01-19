using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.FloorTiles;
using TempleOfDoom.data.Models.Items;

namespace TempleOfDoom.data.Models.Map;

public class Room : UiObserver
{
    public int Id { get; }
    public int Width { get; }
    public int Height { get; }
    public List<Door.Door> Doors { get; set; }
    public List<Item> Items { get; set; }
    public List<FloorTile> FloorTiles { get; set; }
    public char[,] Layout { get; private set; }

    public Room(int id, int width, int height, List<Door.Door> doors, List<Item> items, List<FloorTile> floorTiles)
    {
        Id = id;
        Width = width;
        Height = height;
        Doors = doors;
        Items = items;
        FloorTiles = floorTiles;
        Layout = new char[height, width];
    }

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
                    Layout[y, x] = (Layout[y, x] == ' ' || Layout[y, x] == '\0') ? (char)Symbols.WALL : Layout[y, x];
                }
            }
        }

        // Stap 3: Voeg deuren toe aan de lay-out
        foreach (var door in Doors)
        {
            if (door.Position.X >= 0 && door.Position.X < Width && door.Position.Y >= 0 && door.Position.Y < Height)
            {
                Layout[door.Position.Y, door.Position.X] = door.Symbol;
            }
        }
        foreach (var item in Items)
        {
            if (item.X >= 0 && item.X < Width && item.Y >= 0 && item.Y < Height)
            {
                var itemSymbol = item switch
                {
                    Item keyItem when keyItem.Type == "key" => (char)Symbols.KEY,
                    Item sankaraStoneItem when sankaraStoneItem.Type == "sankara stone" => (char)Symbols.SANKARASTONE,
                    Item boobytrapItem when boobytrapItem.Type == "boobytrap" => (char)Symbols.BOOBYTRAP,
                    Item disappearingBoobytrapItem when disappearingBoobytrapItem.Type == "disappearing boobytrap" => (char)Symbols.DISSAPINGBOOBYTRAP,
                    Item pressurePlateItem when pressurePlateItem.Type == "pressure plate" => (char)Symbols.PRESSUREPLATE,
                    _ => ' '  // Default symbol for unknown item types
                };

                Layout[item.Y, item.X] = itemSymbol;
            }
        }

        foreach (var floorTile in FloorTiles)
        {
            if (floorTile.position.X >= 0 && floorTile.position.X < Width && floorTile.position.Y >= 0 &&
                floorTile.position.Y < Height)
            {
                Layout[floorTile.position.Y, floorTile.position.X] = floorTile.Symbol;
            }
        }
    }

    public void HandlePlayerInteraction(Player player)
    {
        if (Layout == null)
            throw new NullReferenceException("Room layout is not initialized.");

        var currentTile = Layout[player.Position.Y, player.Position.X];
        var item = Items.FirstOrDefault(i => i.X == player.Position.X && i.Y == player.Position.Y);

        if (item == null)
        {
            return; // No item to interact with
        }

        switch (item)
        {
            case Key:
                player.Inventory.AddItem(item);
                Items.Remove(item);
                Layout[player.Position.Y, player.Position.X] = ' ';
                break;

            case SankaraStone:
                player.Inventory.AddItem(item);
                Items.Remove(item);
                Layout[player.Position.Y, player.Position.X] = ' ';
                if (player.Inventory.GetItemCount<SankaraStone>() == 5)
                {
                    player.HasWon = true;
                }
                break;

            case Boobytrap boobytrap:
                boobytrap.Trigger(player);
                break;

            case DisappearingBoobytrap disappearingBoobytrap:
                disappearingBoobytrap.Activate(player);
                Items.Remove(disappearingBoobytrap);
                Layout[player.Position.Y, player.Position.X] = ' ';
                break;

            case PressurePlate pressurePlate:
                pressurePlate.StepOn();
                foreach (var door in Doors.OfType<ToggleDoor>())
                {
                    door.IsOpen = !door.IsOpen;
                }
                break;
        }
    }
}

