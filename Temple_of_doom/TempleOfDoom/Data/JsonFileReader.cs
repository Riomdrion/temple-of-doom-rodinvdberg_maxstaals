using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TempleOfDoom.data.Models.Map;
using TempleOfDoom.Factory;

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
                    {
                        ProcessDictionaryKeys = true,
                        OverrideSpecifiedNames = true
                    }
                }
            };

            // Parse JSON into a dynamic object for custom processing
            dynamic parsedJson = JsonConvert.DeserializeObject(json, settings) ?? throw new InvalidOperationException();

            // Create Rooms using RoomFactory
            var rooms = RoomFactory.CreateRooms(parsedJson.rooms);

            // Create Connections using ConnectionFactory
            ConnectionFactory.CreateRoomDoors(rooms, parsedJson.connections);

            // Create Player
            var playerJson = parsedJson.player;
            var player = new Player((int)playerJson.lives,
                new Position((int)playerJson.startX, (int)playerJson.startY));

            // Set starting room
            var startRoomId = (int)playerJson.startRoomId;
            var startRoom = ((List<Room>)rooms).FirstOrDefault(r => r.Id == startRoomId);

            if (startRoom == null)
            {
                throw new Exception($"Start room with ID {startRoomId} not found.");
            }

            // Link player to start room
            player.CurrentRoom = startRoom;

            // Create GameWorld
            var gameWorld = new GameWorld(player, rooms) { CurrentRoom = startRoom };

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