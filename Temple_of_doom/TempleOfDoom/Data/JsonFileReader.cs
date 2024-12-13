using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TempleOfDoom.Models;

namespace TempleOfDoom.Data
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
                int startRoomId = 1;
                gameWorld.CurrentRoom = gameWorld.Rooms.FirstOrDefault(r => r.Id == startRoomId);
                gameWorld.Player.Position = new Position(gameWorld.Player.StartX, gameWorld.Player.StartY);
                
                Console.WriteLine("Debug: JSON file loaded successfully.");
                return gameWorld;
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine($"Error: A method is not implemented - {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                throw;
            }
        }
    }
}