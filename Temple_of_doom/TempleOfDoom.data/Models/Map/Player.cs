using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.FloorTiles;
using TempleOfDoom.data.Models.Items;
using TempleOfDoom.data.Observers;

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
            if (Position.X == enemy.Position.X && Position.Y == enemy.Position.Y)
            {
                Lives--;
            }
        }
        currentRoom.MoveEnemies(currentRoom);

        foreach (var floorTile in currentRoom.FloorTiles)
        {
            if(floorTile is IceTile iceTile)
            {
                iceTile.HandleIceFloorTile(command, currentRoom, this);
            }
        }

        foreach (var ladder in currentRoom.Ladders)
        {
            if (Position.X == ladder.UpperPosition.X && Position.Y == ladder.UpperPosition.Y)
            {
                // Ga naar de kamer onderaan de ladder
                var lowerRoom = rooms.FirstOrDefault(r => r.Id == ladder.LowerRoomId);
                if (lowerRoom != null)
                {
                    currentRoom = lowerRoom;
                    Position = ladder.LowerPosition;
                    break;
                }
            }
            else if (Position.X == ladder.LowerPosition.X && Position.Y == ladder.LowerPosition.Y)
            {
                // Ga naar de kamer bovenaan de ladder
                var upperRoom = rooms.FirstOrDefault(r => r.Id == ladder.UpperRoomId);
                if (upperRoom != null)
                {
                    currentRoom = upperRoom;
                    Position = ladder.UpperPosition;
                    break;
                }
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
    
    public void Shoot(Room currentRoom)
    {
        // Bepaal de posities rondom de speler
        var adjacentPositions = new List<Position>
        {
            new Position(Position.X, Position.Y - 1), // Noord
            new Position(Position.X, Position.Y + 1), // Zuid
            new Position(Position.X - 1, Position.Y), // West
            new Position(Position.X + 1, Position.Y)  // Oost
        };

        // Controleer op vijanden op de aangrenzende posities
        foreach (var position in adjacentPositions)
        {
            var enemy = currentRoom.Enemies.FirstOrDefault(e => e.Position.X == position.X && e.Position.Y == position.Y);
            if (enemy != null)
            {
                enemy.TakeDamage(); // Schade toebrengen aan de vijand
                Console.WriteLine($"Hit enemy at position ({position.X}, {position.Y})!");

                if (enemy.IsDead())
                {
                    currentRoom.Enemies.Remove(enemy); // Verwijder vijand als hij dood is
                    Console.WriteLine("Enemy defeated!");
                }
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