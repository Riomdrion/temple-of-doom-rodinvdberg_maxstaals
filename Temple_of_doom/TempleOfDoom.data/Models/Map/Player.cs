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

    public void Move(string command, Room currentRoom)
    {
        var newPosition = command switch
        {
            "up" => new Position(Position.X, Position.Y - 1),
            "down" => new Position(Position.X, Position.Y + 1),
            "left" => new Position(Position.X - 1, Position.Y),
            "right" => new Position(Position.X + 1, Position.Y),
            _ => Position
        };

        var door = currentRoom.Doors.FirstOrDefault(d => d.Position.Equals(newPosition));
        if (door != null)
        {
            if (door.CanOpen(this))
            {
                door.Open();
                Position = newPosition;
            }
        }
        else if (currentRoom.IsPositionWalkable(newPosition))
        {
            Position = newPosition;
        }
    }
}