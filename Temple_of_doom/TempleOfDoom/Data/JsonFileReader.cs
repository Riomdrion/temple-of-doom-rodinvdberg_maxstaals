using Newtonsoft.Json;
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

        // Create rooms and connections
        var rooms = RoomFactory.CreateRooms(roomsData, connectionsData);

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


        player.CurrentRoom = startRoom;

        // Create GameWorld
        var gameWorld = new GameWorld(player, rooms);

        return gameWorld;
    }
}