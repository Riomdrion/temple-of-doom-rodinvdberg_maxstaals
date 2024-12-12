using System.IO;
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
                var gameWorld = JsonConvert.DeserializeObject<GameWorld>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                // Link player and starting room
                gameWorld.CurrentRoom = gameWorld.Rooms.FirstOrDefault(r => r.id == gameWorld.Player.StartingRoomId);
                if (gameWorld.CurrentRoom != null)
                {
                    gameWorld.Player.Position = new Position
                    {
                        X = gameWorld.Player.Position.X,
                        Y = gameWorld.Player.Position.Y
                    };
                }

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