using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TempleOfDoom.data.Models.Items;

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
        var type = jObject["type"]?.ToString();

        if (string.IsNullOrEmpty(type)) throw new JsonSerializationException("Item type is not specified.");

        // Get x and y coordinates from the JSON (or use default values)
        int x = jObject["x"]?.Value<int>() ?? 0;
        int y = jObject["y"]?.Value<int>() ?? 0;
        string name = jObject["Name"]?.ToString();

        Item item = type switch
        {
            "key" => new Key(x, y),  // Include color if available
            "sankara stone" => new SankaraStone(x, y),
            "disappearing boobytrap" => new DisappearingBoobytrap(x, y, 10),
            "boobytrap" => new Boobytrap(x, y, jObject["Damage"]?.Value<int>() ?? 0),  // Ensure Damage is passed
            "pressure plate" => new PressurePlate(x, y),  // PressurePlate might not require x, y
            _ => throw new JsonSerializationException($"Unknown item type: {type}")
        };

        serializer.Populate(jObject.CreateReader(), item);
        return item;
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}