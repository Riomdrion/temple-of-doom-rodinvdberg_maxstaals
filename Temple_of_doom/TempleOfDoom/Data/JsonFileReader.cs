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

        try
        {
            var json = File.ReadAllText(filePath);

            // Custom JsonSerializerSettings
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

            // Parse JSON into DTOs
            var parsedJson = JsonConvert.DeserializeObject<dynamic>(json, settings);
            var roomsData = JsonConvert.DeserializeObject<List<RoomDto>>(parsedJson["rooms"].ToString(), settings);
            var connectionsData = JsonConvert.DeserializeObject<List<RoomDto>>(parsedJson["connections"].ToString(), settings);
            

            // Create Rooms using RoomFactory
            var rooms = RoomFactory.CreateRooms(roomsData);

            // Add connections and doors
            ConnectionFactory.CreateRoomDoors(rooms, connectionsData);

            // Create Player
            var playerJson = parsedJson["player"];
            var player = new Player(
                (int)playerJson["lives"],
                new Position((int)playerJson["startX"], (int)playerJson["startY"])
            );

            // Set starting room for Player
            var startRoomId = (int)playerJson["startRoomId"];
            var startRoom = rooms.FirstOrDefault(new Func<Room, bool>(r => r.Id == startRoomId));
            if (startRoom == null)
                throw new Exception($"Start room with ID {startRoomId} not found.");

            player.CurrentRoom = startRoom;

            // Create GameWorld
            var gameWorld = new GameWorld(player, rooms)
            {
                CurrentRoom = startRoom
            };

            Console.WriteLine("GameWorld loaded successfully from JSON.");
            return gameWorld;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading JSON file: {ex.Message}");
            throw;
        }
    }
}
