using TempleOfDoom.data.Models.FloorTiles;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Factory;

public static class FloorTileFactory
{
    public static FloorTile CreateTile(string type, int x, int y)
    {
        return type.ToLower() switch
        {
            "ice" => new IceTile(new Position(x,y)),
            _ => throw new ArgumentException($"Unknown floor tile type: {type}")
        };
    }
}