namespace TempleOfDoom.data.Models.Items;

public abstract class Item
{
    public int X { get; set; }  // X position
    public int Y { get; set; }  // Y position
    public string Type { get; set; }

    // You can keep the constructor for easier initialization
    public Item(int x, int y, string type)
    {
        X = x;
        Y = y;
        Type = type;
    }
}