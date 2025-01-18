using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Data
{
    public class XmlDataReader : IDataReader
    {
        public GameWorld LoadGameWorld(string filePath)
        {
            return XmlFileReader.LoadGameWorld(filePath);
        }
    }
}