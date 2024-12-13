using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.Items;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Data;

public class RoomConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Room);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jObject = JObject.Load(reader);

        // Extract basic properties
        var id = jObject["id"].Value<int>();
        var name = jObject["name"].Value<string>();
        var width = jObject["width"].Value<int>();
        var height = jObject["height"].Value<int>();

        // Create the Room object
        Room room = new Room(id, name, width, height, null, null);

        // Deserialize items
        if (jObject["items"] != null) room.Items = jObject["items"].ToObject<List<Item>>(serializer);

        // Deserialize doors
        if (jObject["doors"] != null) room.Doors = jObject["doors"].ToObject<List<Door>>(serializer);

        // Generate layout based on width and height
        room.Layout = new char[height, width];
        for (var y = 0; y < height; y++)
        for (var x = 0; x < width; x++)
            // Default layout is empty space
            room.Layout[y, x] = '.';

        // Add walls (optional for visual)
        for (var x = 0; x < width; x++)
        {
            room.Layout[0, x] = '#'; // Top wall
            room.Layout[height - 1, x] = '#'; // Bottom wall
        }

        for (var y = 0; y < height; y++)
        {
            room.Layout[y, 0] = '#'; // Left wall
            room.Layout[y, width - 1] = '#'; // Right wall
        }

        return room;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException("Writing JSON is not required for this use case.");
    }
}