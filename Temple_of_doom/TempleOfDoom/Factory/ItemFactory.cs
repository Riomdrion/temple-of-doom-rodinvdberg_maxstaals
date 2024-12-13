using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Items;

namespace TempleOfDoom.Factory;

public static class ItemFactory
{
    public static Item CreateItem(ItemDto itemDto)
    {
        return itemDto.Type switch
        {
            "key" => new Key(itemDto.Name),
            "SankaraStone" => new SankaraStone(itemDto.Name),
            _ => throw new ArgumentException($"Unknown item type: {itemDto.Type}")
        };
    }
}