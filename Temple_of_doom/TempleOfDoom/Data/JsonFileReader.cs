using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using TempleOfDoom.data.Models.Map;
using TempleOfDoom.Factory;
using TempleOfDoom.data.DTO;

namespace TempleOfDoom.Data;

public static class JsonFileReader
{
    public static GameWorld LoadGameWorld(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }

        // Read JSON file
        var json = File.ReadAllText(filePath);
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None,
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };

        // Deserialize JSON
        var parsedJson = JsonConvert.DeserializeObject<dynamic>(json, settings);
        var roomsData = JsonConvert.DeserializeObject<List<RoomDto>>(parsedJson["rooms"].ToString(), settings);
        var connectionsData =
            JsonConvert.DeserializeObject<List<ConnectionDto>>(parsedJson["connections"].ToString(), settings);

        // Controleer en deserialiseer 'doors' indien aanwezig
        foreach (var connection in connectionsData)
        {
            if (connection.Doors == null || connection.Doors.Count == 0)
            {
                // Verifieer dat 'doors' juist geïnitialiseerd is in JSON
                var connections = parsedJson["connections"] as JArray;

                var matchingConnection = connections?
                    .FirstOrDefault(conn =>
                        (int?)conn["north"] == connection.north &&
                        (int?)conn["south"] == connection.south &&
                        (int?)conn["east"] == connection.east &&
                        (int?)conn["west"] == connection.west);

                connection.Doors =
                    matchingConnection?["doors"]?.ToObject<List<DoorDto>>(JsonSerializer.Create(settings)) ??
                    new List<DoorDto>();
            }
        }

        // Create rooms and connections
        var rooms = RoomFactory.CreateRooms(roomsData, connectionsData);

        // Add items to rooms after creation
        foreach (var room in rooms)
        {
            var roomData = ((IEnumerable<RoomDto>)roomsData).FirstOrDefault(r => r.Id == room.Id);
            if (roomData == null)
            {
                throw new KeyNotFoundException($"Room with Id {room.Id} not found in roomsData.");
            }

            if (roomData.Items != null)
            {
                foreach (var itemData in roomData.Items)
                {
                    // Use ItemFactory to convert item data into correct type (Key, SankaraStone, etc.)
                    var item = ItemFactory.CreateItem(itemData); // This method should handle the conversion logic
                    room.Items.Add(item);
                }
            }
        }

        // Create player
        var playerJson = parsedJson["player"];
        var player = new Player(
            (int)playerJson["lives"],
            new Position((int)playerJson["startX"], (int)playerJson["startY"])
        );

        // Set start room
        var startRoomId = (int)playerJson["startRoomId"];
        Room? startRoom = null;
        foreach (var room in rooms)
        {
            if (room.Id != startRoomId) continue;
            startRoom = room;
            break;
        }

        // Add the ItemConverter to handle item deserialization
        var converters = new List<JsonConverter>
        {
            new ItemConverter() // Custom converter for deserializing items
        };
        settings.Converters = converters;


        player.currentRoom = startRoom;

        // Create GameWorld
        var gameWorld = new GameWorld(player, rooms);

        return gameWorld;
    }
}