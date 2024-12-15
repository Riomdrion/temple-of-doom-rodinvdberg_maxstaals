using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Items;

namespace TempleOfDoom.Factory;

public static class ItemFactory
{
    public static Item CreateItem(ItemDto itemDto)
        {
        Console.WriteLine($"Creating item of type: {itemDto.Type} at ({itemDto.X}, {itemDto.Y})");
        // Use default values for x and y (e.g., 0) if they're not available in itemDto
        int x = itemDto.X;  // Assuming ItemDto has X property
            int y = itemDto.Y;  // Assuming ItemDto has Y property

            return itemDto.Type switch
            {
                "key" => new Key(x, y),  // Pass x, y, and name to the Key constructor
                "sankara stone" => new SankaraStone(x, y),
                "pressure plate" => new PressurePlate(x, y),
                "boobytrap" => new Boobytrap(x, y, 1), // Assuming Damage is optional in ItemDto
                "disappearing boobytrap" => new DisappearingBoobytrap(x, y, 1),
                _ => throw new ArgumentException($"Unknown item type: {itemDto.Type}")
            };
        }
}