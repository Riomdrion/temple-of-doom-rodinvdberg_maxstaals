using System.Drawing;

namespace TempleOfDoom.data.Models.Items;

public class Key : Item
{
    public string KeyColor { get; set; }
    public Key(int x, int y)
            : base(x, y, "key")  // Call the base constructor with required parameters
        {
        //KeyColor = color;
        }
}