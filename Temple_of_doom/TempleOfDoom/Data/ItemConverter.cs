using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Items;
using TempleOfDoom.data.Models.Map;
using TempleOfDoom.Factory;

namespace TempleOfDoom.Data;

public class ItemConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(Item).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jObject = JObject.Load(reader);

        Console.WriteLine($"Debug: Loaded JSON object: {jObject}");

        string type = jObject["type"]?.Value<string>();
        if (string.IsNullOrEmpty(type))
        {
            Console.WriteLine("Error: Item type is missing in the JSON object.");
            throw new JsonSerializationException("Item type is missing.");
        }

        // Ensure x and y are being read correctly from the JSON
        int x = jObject["x"]?.Value<int>() ?? 0;
        int y = jObject["y"]?.Value<int>() ?? 0;

        // Debug log for x and y
        Console.WriteLine($"Debug: Extracted Position - x: {x}, y: {y}");

        var itemDto = new ItemDto
        {
            Type = type.ToLower(),
            X = x,
            Y = y
        };

        // Debug log for Position
        Console.WriteLine($"Debug: ItemDto Position - x: {itemDto.X}, y: {itemDto.Y}");

        return ItemFactory.CreateItem(itemDto);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is Item item)
        {
            // Serialize the Item back into JSON
            var jObject = new JObject
            {
                ["type"] = item.Type,
                ["x"] = item.X,
                ["y"] = item.Y
            };
            jObject.WriteTo(writer);
        }
        else
        {
            throw new JsonSerializationException("Unexpected value when converting Item.");
        }
    }
}