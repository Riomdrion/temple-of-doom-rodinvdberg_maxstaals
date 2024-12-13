namespace TempleOfDoom.data.Models.Map;

public class Player
{
    public int Lives { get; set; }
    public bool HasWon { get; set; }
    public Position Position { get; set; }
    public Inventory Inventory { get; set; }
    public int StartingRoomId { get; set; }
    public int StartX { get; set; }
    public int StartY { get; set; }
    
    public Player(int startX, int startY, int lives)
    {
        StartX = startX;
        StartY = startY;
        Lives = lives;
        HasWon = false;
        Inventory = new Inventory();
        Position = new Position(startX, startY);
    }

    public bool HasKey(string keyColor)
    {
        return Inventory.HasItem(keyColor);
    }

    public Position GetPlayerStartPosition()
    {
        return new Position(StartX, StartY);
    }

    public void Move(string command, Room currentRoom)
    {
        var currentPosition = this.Position;
        var newPosition = command switch
        {
            "up" => new Position(currentPosition.X, currentPosition.Y - 1),
            "down" => new Position(currentPosition.X, currentPosition.Y + 1),
            "left" => new Position(currentPosition.X - 1, currentPosition.Y),
            "right" => new Position(currentPosition.X + 1, currentPosition.Y),
            _ => currentPosition
        };
    
        if (currentRoom.IsPositionWalkable(newPosition))
        {
            this.Position = newPosition;
        }
    }
}