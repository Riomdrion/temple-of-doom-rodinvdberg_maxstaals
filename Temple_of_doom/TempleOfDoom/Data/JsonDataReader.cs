using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Data
{
    public class JsonDataReader : IDataReader
    {
        public GameWorld LoadGameWorld(string filePath)
        {
            return JsonFileReader.LoadGameWorld(filePath);
        }
    }
}
