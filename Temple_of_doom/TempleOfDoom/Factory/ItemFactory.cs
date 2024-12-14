using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Items;

namespace TempleOfDoom.Factory;

public static class ItemFactory
{
    public static Item CreateItem(ItemDto itemDto)
        {
            // You need to ensure that x and y are available in the itemDto or set defaults if needed.
            // Assuming the ItemDto has X and Y properties.

            // Use default values for x and y (e.g., 0) if they're not available in itemDto
            int x = itemDto.X;  // Assuming ItemDto has X property
            int y = itemDto.Y;  // Assuming ItemDto has Y property

            return itemDto.Type switch
            {
                "key" => new Key(x, y),  // Pass x, y, and name to the Key constructor
                "SankaraStone" => new SankaraStone(x, y),
                _ => throw new ArgumentException($"Unknown item type: {itemDto.Type}")
            };
        }
}