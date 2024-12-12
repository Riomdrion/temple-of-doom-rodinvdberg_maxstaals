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
        var type = jObject["type"]?.ToString();  // Changed from "Name" to "type"

        Item item = type switch
        {
            "sankara stone" => new SankaraStone(),  // Lowercase type
            "key" => new Key(),
            "disappearing boobytrap" => new Boobytrap() { Disappearing = true },
            "boobytrap" => new Boobytrap(),
            "pressure plate" => new PressurePlate(),
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
