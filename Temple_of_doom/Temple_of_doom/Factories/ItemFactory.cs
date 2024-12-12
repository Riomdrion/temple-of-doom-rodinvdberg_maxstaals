using Temple_of_doom.DTO;
using Temple_of_doom.Models;

namespace Temple_of_doom.Factories;

public static class ItemFactory
{
    public static Item CreateItem(ItemDTO itemDTO)
    {
        return itemDTO.Type switch
        {
            "Key" => new Key(itemDTO.Name),
            "SankaraStone" => new SankaraStone(itemDTO.Name),
            _ => throw new ArgumentException($"Unknown item type: {itemDTO.Type}")
        };
    }
}
