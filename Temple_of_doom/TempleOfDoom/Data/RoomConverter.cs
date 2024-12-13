using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using TempleOfDoom.Models;

namespace TempleOfDoom.Data
{
    public class RoomConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Room);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            // Extract basic properties
            int id = jObject["id"].Value<int>();
            string name = jObject["name"].Value<string>();
            int width = jObject["width"].Value<int>();
            int height = jObject["height"].Value<int>();

            // Create the Room object
            Room room = new Room(id, name, width, height, null, null);

            // Deserialize items
            if (jObject["items"] != null)
            {
                room.Items = jObject["items"].ToObject<List<Item>>(serializer);
            }

            // Deserialize doors
            if (jObject["doors"] != null)
            {
                room.Doors = jObject["doors"].ToObject<List<Door>>(serializer);
            }

            // Generate layout based on width and height
            room.Layout = new char[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Default layout is empty space
                    room.Layout[y, x] = '.';
                }
            }

            // Add walls (optional for visual)
            for (int x = 0; x < width; x++)
            {
                room.Layout[0, x] = '#'; // Top wall
                room.Layout[height - 1, x] = '#'; // Bottom wall
            }

            for (int y = 0; y < height; y++)
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
}
