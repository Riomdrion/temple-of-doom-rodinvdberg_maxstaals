using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temple_of_doom.Models;
using Newtonsoft.Json;
using System.IO;



namespace Temple_of_doom.Data
{
    public static class JsonFileReader
    {
        public static GameWorld LoadGameWorld(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' was not found.");
            }

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<GameWorld>(json);
        }
    }
}
