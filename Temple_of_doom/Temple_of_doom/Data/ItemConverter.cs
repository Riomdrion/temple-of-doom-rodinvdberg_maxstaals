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
        var type = jObject["type"]?.ToString();

        if (string.IsNullOrEmpty(type))
        {
            throw new JsonSerializationException("Item type is not specified.");
        }

        Item item = type switch
        {
            "key" => new Key(jObject["Name"]?.ToString()),
            "sankara stone" => new SankaraStone(jObject["Name"]?.ToString()),
            "disappearing boobytrap" => new DisappearingBoobytrap(jObject["Name"]?.ToString(),
                jObject["Damage"]?.Value<int>() ?? 0),
            "boobytrap" => new Boobytrap(jObject["Damage"]?.Value<int>() ?? 0),
            "pressure plate" => new PressurePlate(jObject["Name"]?.ToString()),
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


