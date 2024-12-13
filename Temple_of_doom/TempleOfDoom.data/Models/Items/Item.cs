namespace TempleOfDoom.data.Models.Items;

public abstract class Item
{
    public int[] Coordinates { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
}