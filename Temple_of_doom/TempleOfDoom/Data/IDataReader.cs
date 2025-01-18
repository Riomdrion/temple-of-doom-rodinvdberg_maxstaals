using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Data;

public interface IDataReader
{
    GameWorld LoadGameWorld(string filePath);
}