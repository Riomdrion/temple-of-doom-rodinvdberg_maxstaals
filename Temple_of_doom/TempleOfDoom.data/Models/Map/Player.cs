using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.FloorTiles;
using TempleOfDoom.data.Models.Items;

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
        return Inventory.HasKey(keyColor);
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
        
        foreach (var enemy in currentRoom.Enemies)
        {
            if (Position.Equals(enemy.Position))
            {
                Lives--; // Speler neemt schade
                Console.WriteLine("Ouch! You were hit by an enemy.");
            }
        }
        currentRoom.MoveEnemies();

        foreach (var floorTile in currentRoom.FloorTiles)
        {
            if(floorTile is IceTile iceTile)
            {
                iceTile.HandleIceFloorTile(command, currentRoom, this);
            }
        }
        
        // Check if the player has reached a door
        foreach (var door in currentRoom.Doors)
        {
            if (door.Position.X == Position.X && door.Position.Y == Position.Y)
            {
                // Validatie: Controleer of de deur geopend kan worden
                if (!door.CanOpen(this))
                {
                    return;
                }
                
                // Zoek de nieuwe kamer
                var targetRoom = rooms.FirstOrDefault(r => r.Id == door.TargetRoomId);

                if (door is ClosingGate gate)
                {
                    gate.SetClosed();
                    foreach (var newDoor in targetRoom.Doors.OfType<ClosingGate>())
                    {
                        newDoor.SetClosed();
                    }
                }
                
                // Zoek de corresponderende deur in de doelkamer
                var correspondingDoor = targetRoom.Doors.FirstOrDefault(d =>
                    d.TargetRoomId == currentRoom.Id && d.Direction == GetOppositeDirection(door.Direction));
                
                // Teleporteer speler naar de nieuwe kamer
                Position = correspondingDoor.Position;
                currentRoom = targetRoom;
                
                break;
            }
        }
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