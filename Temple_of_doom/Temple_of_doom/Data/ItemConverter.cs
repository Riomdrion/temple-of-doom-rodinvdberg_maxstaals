using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Temple_of_doom.Models;

namespace Temple_of_doom.Data;

public class ItemConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(Item).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jObject = JObject.Load(reader);
        var type = jObject["Name"]?.ToString();

        Item item = type switch
        {
            "Sankara Stone" => new SankaraStone(),
            "Key" => new Key(),
            _ => throw new JsonSerializationException($"Unknown item type: {type}")
        };

        serializer.Populate(jObject.CreateReader(), item);
        return item;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}
