namespace TempleOfDoom.data.Models.Items;

public abstract class Item
{
    public int X { get; set; }  // X position
    public int Y { get; set; }  // Y position
    public string Type { get; set; }
}