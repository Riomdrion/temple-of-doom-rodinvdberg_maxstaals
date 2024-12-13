namespace TempleOfDoom.data.Models.Map;

public class Player
{
    public Player(int startX, int startY, int lives)
    {
        StartX = startX;
        StartY = startY;
        Lives = lives;
        HasWon = false;
        Inventory = new Inventory();
        Position = new Position(startX, startY);
    }

    public int Lives { get; set; }
    public bool HasWon { get; set; }
    public Position Position { get; set; }
    public Inventory Inventory { get; set; }
    public int StartingRoomId { get; set; }
    public int StartX { get; set; }
    public int StartY { get; set; }

    public bool HasKey(string keyColor)
    {
        return Inventory.HasItem(keyColor);
    }

    public Position GetPlayerStartPosition()
    {
        return new Position(StartX, StartY);
    }
}