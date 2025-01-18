using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.FloorTiles;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Factory;

public static class FloorTileFactory
{
    public static FloorTile CreateTile(FloorTileDTO tileDto)
    {
        return tileDto.type.ToLower() switch
        {
            "ice" => new IceTile(new Position(tileDto.x,tileDto.y)),
            _ => new SimpleFloorTile(new Position(tileDto.x,tileDto.y))
        };
    }
}