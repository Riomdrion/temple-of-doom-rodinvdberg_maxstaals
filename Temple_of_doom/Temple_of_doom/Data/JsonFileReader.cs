using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Temple_of_doom.Models;

namespace Temple_of_doom.Data
{
    public static class JsonFileReader
    {
        public static GameWorld LoadGameWorld(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return null;
            }

            try
            {
                string json = File.ReadAllText(filePath);

                // Custom JsonSerializerSettings
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.None,
                    Converters = new List<JsonConverter> { new ItemConverter() }
                };

                var gameWorld = JsonConvert.DeserializeObject<GameWorld>(json, settings);

                // Link player to starting room
                int startRoomId = gameWorld.Player.StartingRoomId;
                gameWorld.CurrentRoom = gameWorld.Rooms.Find(r => r.Id == startRoomId);

                    gameWorld.Player.Position = new Position(gameWorld.Player.StartX, gameWorld.Player.StartY);
               

                return gameWorld;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                return null;
            }
        }
    }
}