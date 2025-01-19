using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.FloorTiles;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Factory
{
    public class FloorTileFactory
    {
        public static FloorTile CreateTile(FloorTileDTO floorTileDTO)
        {
            return floorTileDTO.type.ToLower() switch
            {
                "conveyor belt" => new Conveyorbelt(new Position(floorTileDTO.x, floorTileDTO.y)),
                _ => throw new ArgumentException($"Unknown floor tile type: {floorTileDTO.type}")
            };
        }
    }
}
