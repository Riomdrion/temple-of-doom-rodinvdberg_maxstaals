using System.Drawing;
using TempleOfDoom.data.Enums;

namespace TempleOfDoom.data.Models.Items;

public class Key : Item
{
    public string KeyColor { get; set; }

    public Key(int x, int y) : base(x, y, "key")
    {
        KeyColor = ColorEnum.Yellow.ToString();
    }
}