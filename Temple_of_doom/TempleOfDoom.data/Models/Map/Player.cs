namespace TempleOfDoom.data.Models.Map;

public class Player
{
    public int Lives { get; set; }
    public bool HasWon { get; set; }
    public Position Position { get; set; }
    public Inventory Inventory { get; set; }
    public int StartingRoomId { get; set; }
    public Room CurrentRoom { get; set; }

    public Player(int lives, Position position)
    {
        Lives = lives;
        HasWon = false;
        Inventory = new Inventory();
        Position = position;
    }

    public bool HasKey(string keyColor)
    {
        return Inventory.HasItem(keyColor);
    }

    public Position GetPlayerStartPosition()
    {
        return Position;
    }

    public void Move(string command, Room currentRoom, List<Room> rooms)
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
        }
    
        // Controleer of de speler op een deur staat
        foreach (var door in currentRoom.Doors)
        {
            if (door.Position.X == Position.X && door.Position.Y == Position.Y)
            {
                Console.WriteLine($"You used the door to Room ID={door.TargetRoomId}");

                // Teleporteer naar de verbonden kamer
                var targetRoom = rooms.FirstOrDefault(r => r.Id == door.TargetRoomId);
                if (targetRoom != null)
                {
                    CurrentRoom = targetRoom;

                    // Stel de nieuwe positie in op basis van de richting van de deur
                    Position = door.Direction switch
                    {
                        Direction.NORTH => new Position(door.Position.X,
                            targetRoom.Height - 1), // Onder de deur in de nieuwe kamer
                        Direction.SOUTH => new Position(door.Position.X, 0), // Boven de deur in de nieuwe kamer
                        Direction.WEST => new Position(targetRoom.Width - 1,
                            door.Position.Y), // Rechts van de deur in de nieuwe kamer
                        Direction.EAST => new Position(0, door.Position.Y), // Links van de deur in de nieuwe kamer
                        _ => throw new Exception("Invalid door direction")
                    };

                    Console.WriteLine(
                        $"Teleported to Room ID={CurrentRoom.Id} at Position=({Position.X}, {Position.Y})");
                }

                break;
            }
        }
    }
}