using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Items;

namespace TempleOfDoom.Factory;

public static class ItemFactory
{
    public static Item CreateItem(ItemDto itemDto)
    {
        int x = itemDto.X;
        int y = itemDto.Y; 

        return itemDto.Type switch
        {
            "key" => new Key(x, y),
            "sankara stone" => new SankaraStone(x, y),
            "pressure plate" => new PressurePlate(x, y),
            "boobytrap" => new Boobytrap(x, y, 1),
            "disappearing boobytrap" => new DisappearingBoobytrap(x, y, 1),
            _ => throw new ArgumentException($"Unknown item type: {itemDto.Type}")
        };
    }
}