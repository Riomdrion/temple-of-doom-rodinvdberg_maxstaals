using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Items;
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

        int x = jObject["x"]?.Value<int>() ?? 0;
        int y = jObject["y"]?.Value<int>() ?? 0;

        var itemDto = new ItemDto
        {
            Type = type.ToLower(),
            X = x,
            Y = y
        };

        return ItemFactory.CreateItem(itemDto);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is Item item)
        {
            // Serialize the Item back into JSON
            var jObject = new JObject
            {
                ["Type"] = item.Type,
                ["X"] = item.X,
                ["Y"] = item.Y
            };
            jObject.WriteTo(writer);
        }
        else
        {
            throw new JsonSerializationException("Unexpected value when converting Item.");
        }
    }
}