using System;
using System.Collections.Generic;
using TempleOfDoom.data.Models.Items;

namespace TempleOfDoom.data.Factory
{
    public static class ItemFactory
    {
        public static List<Item> CreateItems(dynamic itemsData)
        {
            var items = new List<Item>();

            if (itemsData == null) return items;

            foreach (var itemData in itemsData)
            {
                var item = itemData.type switch
                {
                    "sankara stone" => new SankaraStone("Sankara Stone", itemData.x, itemData.y),
                    "key" => new Item($"Key ({itemData.color})", itemData.x, itemData.y),
                    "boobytrap" => new Item("Boobytrap", itemData.x, itemData.y),
                    "disappearing boobytrap" => new Item("Disappearing Boobytrap", itemData.x, itemData.y),
                    "pressure plate" => new Item("Pressure Plate", itemData.x, itemData.y),
                    _ => null
                };

                if (item != null) items.Add(item);
            }

            return items;
        }
    }
}