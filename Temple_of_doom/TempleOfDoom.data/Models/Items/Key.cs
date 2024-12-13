namespace TempleOfDoom.data.Models.Items;

public class Key : Item
{
    public Key(string itemDtoName)
    {
        KeyColor = itemDtoName;
    }

    public string KeyColor { get; set; }
}