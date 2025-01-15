using TempleOfDoom.data.Models.Items;

namespace TempleOfDoom.data.Models.Map;

public class Room : UiObserver
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
                Layout[door.Position.Y, door.Position.X] = door.Symbol;
            }
        }
        foreach (var item in Items)
        {
            if (item.X >= 0 && item.X < Width && item.Y >= 0 && item.Y < Height)
            {
                var itemSymbol = item switch
                {
                    Item keyItem when keyItem.Type == "key" => 'K',
                    Item sankaraStoneItem when sankaraStoneItem.Type == "sankara stone" => 'S',
                    Item boobytrapItem when boobytrapItem.Type == "boobytrap" => 'B',
                    Item disappearingBoobytrapItem when disappearingBoobytrapItem.Type == "disappearing boobytrap" => 'D',
                    Item pressurePlateItem when pressurePlateItem.Type == "pressure plate" => 'T',
                    _ => ' '  // Default symbol for unknown item types
                };

                Layout[item.Y, item.X] = itemSymbol;
            }
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
                Layout[player.Position.Y, player.Position.X] = '.'; // Verwijder de key uit de kamer

                // Verwijder het item uit de Items-lijst van de kamer
                var keyItem = player.currentRoom.Items.FirstOrDefault(i => i.X == player.Position.X && i.Y == player.Position.Y);
                if (keyItem != null)
                {
                    player.currentRoom.Items.Remove(keyItem);
                    Update("Key removed from the room.");
                }
                else
                {
                    Update("Key item not found in the room.");
                }

                Update("You picked up a Key!");
                break;

            case 'S': // Sankara Stone
                Update("You found a Sankara Stone!");
                player.Inventory.AddItem("sankara stone"); // Voeg toe aan inventaris
                Layout[player.Position.Y, player.Position.X] = '.'; // Verwijder de steen uit de kamer

                // Verwijder het item uit de Items-lijst van de kamer
                var sankaraStoneItem = player.currentRoom.Items.FirstOrDefault(i => i.X == player.Position.X && i.Y == player.Position.Y);
                if (sankaraStoneItem != null)
                {
                    player.currentRoom.Items.Remove(sankaraStoneItem);
                    Update("Sankara Stone removed from the room.");
                }
                else
                {
                    Update("Sankara Stone item not found in the room.");
                }

                Update("You picked up a Sankara Stone!");

                // Toon de huidige hoeveelheid Sankara Stones in de inventaris
                int sankaraStoneCount = player.Inventory.GetItemCount("Sankara Stone");
                Update("You now have " + sankaraStoneCount + " Sankara Stones.");

                // Controleer of de speler heeft gewonnen (bijvoorbeeld: 5 Sankara Stones verzameld)
                if (sankaraStoneCount == 5)
                {
                    player.HasWon = true;
                    Update("You have collected all 5 Sankara Stones! You win!");
                }
                player.CheckWinCondition();
                break;

            case 'B': // Boobytrap
                      // Vind het item op de huidige positie van de speler
                var boobytrapItem = player.currentRoom.Items.FirstOrDefault(i => i.X == player.Position.X && i.Y == player.Position.Y);

                if (boobytrapItem != null && boobytrapItem.Type == "boobytrap")
                {
                    // Debugging: Controleer of het een normale boobytrap is
                    Update($"Normal boobytrap found at position ({player.Position.X}, {player.Position.Y}).");

                    // Verwerk een normale boobytrap
                    player.Lives--;
                    Update("Boobytrap triggered! Player loses 1 life.");
                }
                else
                {
                    // Debugging: Geen normale boobytrap gevonden
                    Update("No normal boobytrap found at the player's position.");
                }
                break;

            case 'D': // Boobytrap
                      // Vind het item op de huidige positie van de speler
                var dboobytrapItem = player.currentRoom.Items.FirstOrDefault(i => i.X == player.Position.X && i.Y == player.Position.Y);

                if (dboobytrapItem != null && dboobytrapItem.Type == "disappearing boobytrap")
                {
                    // Verwerk een normale boobytrap
                    player.Lives--;
                    player.currentRoom.Items.Remove(dboobytrapItem);

                    Update("Boobytrap triggered! Player loses 1 life.");
                }
                else
                {
                    // Debugging: Geen normale boobytrap gevonden
                    Update("No normal boobytrap found at the player's position.");
                }
                break;
        }

        // Debugging: Toon de inhoud van de kamer na de interactie
        Clear();
        InitializeRoomLayout();

        Update("Current items in the room after interaction:");
        foreach (var item in player.currentRoom.Items)
        {
            Update($"Item: {item.Type} at ({item.X}, {item.Y})");
        }
    }
}